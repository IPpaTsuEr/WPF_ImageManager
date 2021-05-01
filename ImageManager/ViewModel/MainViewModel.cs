using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ImageManager.Helper;
using ImageManager.Manager;
using ImageManager.Modle;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageManager.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 

        private ObservableCollection<ImageInfos> _ImageList;

        public ObservableCollection<ImageInfos> IamgeList
        {
            get { return _ImageList; }
            set { _ImageList = value; }
        }

        private ObservableCollection<LibraryInfos> _LibList;

        public ObservableCollection<LibraryInfos> LibList
        {
            get { return _LibList; }
            set { _LibList = value; }
        }

        private double _ItemHeight = 200;

        public double ItemHeight
        {
            get { return _ItemHeight; }
            set { _ItemHeight = value; RaisePropertyChanged("ItemHeight"); }
        }



        public RelayCommand<LibraryInfos> CheckLibraryThumbnailCommand { get; set; }
        public RelayCommand<LibraryInfos> RemoveLibraryCommand { get; set; }
        public RelayCommand AddNewLibraryCommand { get; set; }
        public RelayCommand<ImageInfos> DetailViewCommand { get; set; }
        public RelayCommand<ImageInfos> DeleteImageCommand { get; set; }
        public RelayCommand<string> ViewItemHeightCommad { get; set; }

        Object ImageLocker = new Object();
        Object LibLocker = new Object();

        DataBaseHelper DBH;

        public void UpdateImageInfo(ImageInfos imfn)
        {
            DBH.UpdateImage(imfn);
        }
        public MainViewModel()
        {
            CheckLibraryThumbnailCommand = new RelayCommand<LibraryInfos>(OnCheckLibraryThumbnail);
            RemoveLibraryCommand = new RelayCommand<LibraryInfos>(OnRemoveLibrary);
            AddNewLibraryCommand = new RelayCommand(OnAddNewLibrary);
            DetailViewCommand = new RelayCommand<ImageInfos>(OnDetailView);
            DeleteImageCommand = new RelayCommand<ImageInfos>(OnDeleteImage);
            ViewItemHeightCommad = new RelayCommand<string>(OnViewItemHeight);

            if (IsInDesignMode)
            {
                LibList = new ObservableCollection<LibraryInfos>() {
                    new LibraryInfos() {  Name="添加文件夹",Path= @"向库中添加更多文件夹" },
                    new LibraryInfos() {  Name="图片",Path= @"C:\Users\IM\Pictures" },
                    new LibraryInfos() {  Name="家具",Path= @"D:\H\Image1" }
                };
                IamgeList = new ObservableCollection<ImageInfos>() { 
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_01.jpg",Date= new System.DateTime(2021,2,12)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_02.jpg",Date= new System.DateTime(2021,2,12)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_03.jpg",Date= new System.DateTime(2021,2,12)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_04.jpg",Date= new System.DateTime(2021,2,13)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_05.jpg",Date= new System.DateTime(2021,2,13)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_06.jpg",Date= new System.DateTime(2021,2,13)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_07.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_08.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_09.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_10.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_11.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_12.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_13.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_14.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_15.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_16.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_17.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_18.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_19.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_20.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_21.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_22.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_23.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_24.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_25.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_26.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_27.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_28.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_29.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_30.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_31.jpg",Date= new System.DateTime(2021,2,14)},
                    new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_32.jpg",Date= new System.DateTime(2021,2,14)}
                };

            }
            LibList = new ObservableCollection<LibraryInfos>() {
                    new LibraryInfos() {  Name="添加文件夹",Path= @"向库中添加更多文件夹" }
                    //,
                    //new LibraryInfos() {  Name="图片",Path= @"C:\Users\IM\Pictures" },
                    //new LibraryInfos() {  Name="家具",Path= @"D:\H\Image1" }
                };
            IamgeList = new ObservableCollection<ImageInfos>() {
                    //                   new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_01.jpg",Date= new System.DateTime(2021,2,12)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_02.jpg",Date= new System.DateTime(2021,2,12)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_03.jpg",Date= new System.DateTime(2021,2,12)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_04.jpg",Date= new System.DateTime(2021,2,13)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_05.jpg",Date= new System.DateTime(2021,2,13)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_06.jpg",Date= new System.DateTime(2021,2,13)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_07.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_08.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_09.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_10.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_11.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_12.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_13.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_14.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_15.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_16.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_17.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_18.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_19.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_20.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_21.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_22.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_23.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_24.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_25.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_26.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_27.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_28.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_29.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_30.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_31.jpg",Date= new System.DateTime(2021,2,14)},
                    //new ImageInfos(){ SourcePath =@"D:\H\Image\A\iamge_32.jpg",Date= new System.DateTime(2021,2,14)}
                };

            BindingOperations.EnableCollectionSynchronization(_ImageList, ImageLocker);
            BindingOperations.EnableCollectionSynchronization(_LibList, LibLocker);

            DBH = new DataBaseHelper(System.IO.Path.Combine(System.IO.Path.GetFullPath("."),"DB.db"));
            Task.Run(()=> {
                DBH.ReadAllLib(_LibList);
                DBH.ReadAllImage(_ImageList);
            }).ContinueWith((r)=> {
                    foreach (var item in _LibList)
                    {
                        if(string.IsNullOrWhiteSpace(item.Thumbnial)|| item.Thumbnial== @"Resources\NotFound.png" || !File.Exists(item.Thumbnial))
                        foreach (var sub in _ImageList)
                        {
                            if (LibraryFileHelper.IsSubFolder(item.Path, sub.SourcePath))
                            {
                                item.Thumbnial = sub.SourcePath;
                                break; 
                            }
                        }
                    }
                    foreach (var item in _ImageList)
                    {
                        if (string.IsNullOrEmpty(item.HashString))
                        {
                            try
                            {
                                item.HashString = ImageHashManager.CalculateImageHash(item.SourcePath);
                                DBH.UpdateImage(item);
                            }catch(Exception e)
                            {
                                System.Diagnostics.Trace.WriteLine(e.Message);
                            }
                            
                        }
                    }
            });
        }

        private void OnViewItemHeight(string obj)
        {
            if(double.TryParse(obj, out double newHeight))
            {
                ItemHeight = newHeight;
            }
        }

        private void OnCheckLibraryThumbnail(LibraryInfos infos)
        {
            if (!Directory.Exists(infos.Path))
            {
                infos.IsValid = false;
                infos.ErrorInfo = "指向的文件夹不存在";
                return;
            }
            //库缩略图未设置或不存在
            if( infos.Thumbnial== @"Resources\NotFound.png" || !File.Exists(infos.Thumbnial))
            {
                var p = LibraryFileHelper.GetFolderThumbnial(infos.Path,_ImageList);
                if(p!= @"Resources\NotFound.png")
                {
                    infos.Thumbnial = p;
                }
                else
                {
                    infos.IsValid = false;
                    infos.ErrorInfo = "指向的文件夹下没有支持的图像文件";
                    infos.Thumbnial = @"Resources\NotFound.png";
                }
            }
        }

        private void OnRemoveLibrary(LibraryInfos infos)
        {
            LibList.Remove(infos);
            Task.Run(() =>
            {
                List<ImageInfos> removelist = new List<ImageInfos>();
                DBH.RemoveLibrary(infos.Path);
                foreach (var item in _ImageList)
                {
                    if (LibraryFileHelper.IsSubFolder(infos.Path,item.SourcePath))
                    {
                        removelist.Add(item);
                    }
                }
                if (removelist.Count > 0)
                {
                    DBH.BatchRmoveImage(removelist);
                }
                App.Current.Dispatcher.Invoke(()=> {
                    foreach (var item in removelist)
                    {
                        _ImageList.Remove(item);
                    }
                });
            });
            
        }

        private void OnAddNewLibrary()
        {
            CommonOpenFileDialog FD = new CommonOpenFileDialog();
            FD.IsFolderPicker = true;
            FD.Multiselect = false;
            if (FD.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var NI = new LibraryInfos() { Path = FD.FileName, Name = System.IO.Path.GetFileName(FD.FileName) };
                LibList.Add(NI); 
                Task.Run(() => {
                    
                    DBH.AddLibrary(NI.Path);

                    List<ImageInfos> _lis = new List<ImageInfos>();
                    LibraryFileHelper.AddAllFile(NI.Path,_lis);
                    
                    foreach (var item in _lis)
                    {
                        _ImageList.Add(item);
                    }
                    OnCheckLibraryThumbnail(NI);
                    return _lis;
                }).ContinueWith((l)=> {
                    foreach (var item in l.Result)
                    {
                        DBH.AddImage(item);
                    }
                });
                //var iif = LibraryFileHelper.AnalysisImage(@"D:\IM\Downloads\BG\EkRbMZ5U0AAiE-A.jpg");
                //Console.WriteLine(iif.CameraModel);
            }
        }

        private void OnDetailView(ImageInfos info)
        {
            var iv = ServiceLocator.Current.GetInstance<ImageViewDetail>();
            iv.SetSource(info);
            iv.Viewable = System.Windows.Visibility.Visible;
        }

        private void OnDeleteImage(ImageInfos imf)
        {
            _ImageList.Remove(imf);
            DBH.RemoveImage(imf);
        }
    }
}