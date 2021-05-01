using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ImageManager.Modle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ImageManager.ViewModel
{
    public class ImageViewDetail:ViewModelBase
    { 
        private Visibility _Viewable;

        public Visibility Viewable
        {
            get { return _Viewable; }
            set { if (value == Visibility.Collapsed && _Viewable == Visibility.Visible)OnClosePanel() ; _Viewable = value; RaisePropertyChanged("Viewable"); }
        }


        private ImageInfos _ImageTarget;

        public ImageInfos ImageTarget
        {
            get { return _ImageTarget; }
            set { _ImageTarget = value; RaisePropertyChanged("ImageTarget"); }
        }
        private double _Angle = 0;

        public double Angle
        {
            get { return _Angle; }
            set { _Angle = value; RaisePropertyChanged("Angle"); }
        }

        private double _Scale = 1;

        public double Scale
        {
            get { return _Scale; }
            set { _Scale = value; RaisePropertyChanged("Scale"); }
        }



        public ObservableCollection<string> ImageTags { get; set; } = new ObservableCollection<string>();

        public RelayCommand CloseImageViewPanelCommand { get; set; }
        public RelayCommand<Canvas> ViewResetCommand { get; set; }
        public RelayCommand<String> RemoveTagCommand { get; set; }
        public RelayCommand<String> AddTagCommand { get; set; }
        public RelayCommand ZoomoutImageViewPanelCommand { get; set; }
        public RelayCommand ZoominImageViewPanelCommand { get; set; }
        public RelayCommand RightRotateImageViewPanelCommand { get; set; }
        public RelayCommand LeftRotateImageViewPanelCommand { get; set; }
        public RelayCommand DeleteImageViewPanelCommand { get; set; }

        public ImageViewDetail()
        {
            Viewable = Visibility.Collapsed;

            CloseImageViewPanelCommand = new RelayCommand(OnCloseImageViewPanel);
            ViewResetCommand = new RelayCommand<Canvas>(OnViewReset);
            RemoveTagCommand = new RelayCommand<string>(OnRemoveTag);
            AddTagCommand = new RelayCommand<string>(OnAddTag);

            ZoomoutImageViewPanelCommand = new RelayCommand(OnZoomout);
            ZoominImageViewPanelCommand = new RelayCommand(OnZoomin);
            RightRotateImageViewPanelCommand = new RelayCommand(OnRightRotate);
            LeftRotateImageViewPanelCommand = new RelayCommand(OnLeftRotate);
            DeleteImageViewPanelCommand = new RelayCommand(OnDlete);

            if (IsInDesignMode)
            {
                Viewable = Visibility.Collapsed;
                ImageTags = new ObservableCollection<string>() { "风景","海滩","街道"};
            }
        }

        private void OnDlete()
        {
            var m = ServiceLocator.Current.GetInstance<MainViewModel>();
            m?.DeleteImageCommand.Execute(ImageTarget);
            Viewable = Visibility.Collapsed;
        }

        private void OnLeftRotate()
        {
            Angle -= 90;
        }

        private void OnZoomin()
        {
            Scale += 0.05;
            if (Scale > 2) Scale = 2;
            if (Scale < 0.2) Scale = 0.2;
        }

        private void OnRightRotate()
        {
            Angle += 90;
        }

        private void OnZoomout()
        {
            Scale -= 0.05;
            if (Scale > 2) Scale = 2;
            if (Scale < 0.2) Scale = 0.2;
        }

        public void SetSource(ImageInfos imf)
        {
            ImageTarget = imf;
            ImageTags.Clear();
            ImageTarget.Tags?.ForEach((s) => { ImageTags.Add(s); });
        }

        private void OnCloseImageViewPanel()
        {
            Viewable = Visibility.Collapsed;

        }
        private void OnViewReset(Canvas av)
        {
            av.RenderTransform = new ScaleTransform();
            var im = av.Children[0] as Image;
            Canvas.SetLeft(im, (av.ActualWidth-im.ActualWidth)/2);
            Canvas.SetTop(im, (av.ActualHeight-im.ActualHeight)/2);

        }

        private void OnRemoveTag(string Key)
        {
            ImageTags.Remove(Key);
            ImageTarget.Tags.Remove(Key);
        }

        private void OnAddTag(string Key)
        {
            if (!ImageTags.Contains(Key))
            {
                ImageTags.Insert(0, Key);
                if (ImageTarget.Tags == null) 
                    ImageTarget.Tags = new List<string>();
                
                Task.Run(() => {
                    ImageTarget.Tags.Add(Key);
                    var iv = ServiceLocator.Current.GetInstance<MainViewModel>();
                    iv.UpdateImageInfo(ImageTarget);
                });
            }
        }

        private void OnClosePanel()
        {
             

        }
    }
}
