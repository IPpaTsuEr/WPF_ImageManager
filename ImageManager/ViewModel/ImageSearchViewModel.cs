using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ImageManager.Helper;
using ImageManager.Manager;
using ImageManager.Modle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ImageManager.ViewModel
{
    public class ImageSearchViewModel:ViewModelBase
    {
        private double _ItemHeight=100;

        public double ItemHeight
        {
            get { return _ItemHeight; }
            set { _ItemHeight = value; RaisePropertyChanged("ItemHeight"); }
        }

        public RelayCommand<string> SearchCommand { get; set; }
        public RelayCommand<string> ViewItemHeightCommad { get; set; }

        public RelayCommand<DragEventArgs> DropCommand { get; set; }
        public RelayCommand<DragEventArgs> DragOverCommand { get; set; }

        private ObservableCollection<ImageInfos> _SearchResult;

        public ObservableCollection<ImageInfos> SearchResult
        {
            get { return _SearchResult; }
            set { _SearchResult = value;RaisePropertyChanged("SearchResult"); }
        }

        private Object Locker;

        public ImageSearchViewModel()
        {
            SearchResult = new ObservableCollection<ImageInfos>();
            SearchCommand = new RelayCommand<string>(OnSearch);
            ViewItemHeightCommad = new RelayCommand<string>(OnViewItemHeight);
            DropCommand = new RelayCommand<DragEventArgs>(OnDrop);
            DragOverCommand = new RelayCommand<DragEventArgs>(OnDrag);
            Locker = new Object();
            BindingOperations.EnableCollectionSynchronization(SearchResult, Locker);
        }

        private void OnDrag(DragEventArgs obj)
        {
            obj.Effects = DragDropEffects.Copy;
            obj.Handled = true;
        }

        private void OnViewItemHeight(string obj)
        {
            if (double.TryParse(obj, out double newHeight))
            {
                ItemHeight = newHeight;
            }
        }

        public void OnDrop(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var list = (string[])e.Data.GetData(DataFormats.FileDrop);
            if(list!=null && list.Length > 0)
            {
                OnSearch(list[0]);
            }
        }

        private void OnSearch(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            Console.WriteLine(key);
            SearchResult.Clear();
            var resource = ServiceLocator.Current.GetInstance<MainViewModel>().IamgeList;
            Task.Run(() => { Search(key,resource); });
        }

        private void Search(string key,ICollection<ImageInfos> resource)
        {
            // FilePath 搜索
            if(File.Exists(key) && (key.EndsWith(".jpg") || key.EndsWith(".png")|| key.EndsWith(".bmp")))
            {
                var hash = ImageHashManager.CalculateImageHash(key);
                float rate = 0;
                foreach (var item in resource)
                {
                    if (string.IsNullOrWhiteSpace(item.HashString))
                        item.HashString = ImageHashManager.CalculateImageHash(item.SourcePath);
                    rate = 0;
                    if (ImageHashManager.CompareHash(hash, item.HashString,out rate) && rate>=0.81)
                    {
                        SearchResult.Add(item);
                    }
                }
            }
            //# Tag搜索
           else if (key.StartsWith("#"))
            {
                key = key.Substring(1);
                var result =  resource.Where((r) => {
                    if (r.Tags == null || r.Tags.Count == 0) return false;
                    if(r.Tags.IndexOf(key)>=0)return true;
                    return false;
                });
                foreach (var item in result)
                {
                    SearchResult.Add(item);
                }
            }
            //@ Name搜索
            else if (key.StartsWith("@"))
            {
                key = key.Substring(1);
                var liked = resource.Where(
                    (i) => {
                        if (i.Name?.IndexOf(key)>=0) return true;
                        return false;
                    });
                foreach (var item in liked)
                {
                    SearchResult.Add(item);
                }
            }

        }
    }
}
