using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImageManager.Control
{
    class ThunbnailImage:Image
    {


        public double ResolveHeight
        {
            get { return (double)GetValue(ResolveHeightProperty); }
            set { SetValue(ResolveHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResolveHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResolveHeightProperty =
            DependencyProperty.Register("ResolveHeight", typeof(double), typeof(ThunbnailImage), new PropertyMetadata(0.0, OnResolveHeightChanged));



        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(ThunbnailImage), new PropertyMetadata(OnFilePathChanged));

        private static void OnFilePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is ThunbnailImage)
            {
                var img = d as ThunbnailImage;
                img.Source = CreateImage((string)e.NewValue,(int)img.ResolveHeight);
            }
            
        }

        private static void OnResolveHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ThunbnailImage)
            {
                var img = d as ThunbnailImage;
                img.Source = CreateImage((string)e.NewValue, (int)img.ResolveHeight);
            }
        }

        private static BitmapImage CreateImage(string FilePath,int ResolveHeight)
        {
            if (File.Exists(FilePath))
            {
                BitmapImage img = null;
                App.Current.Dispatcher.Invoke(() => { img = new BitmapImage(); });
                img.BeginInit();
                img.UriSource = new Uri(FilePath);
                img.DecodePixelHeight = ResolveHeight;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                return img;
            }
            return null;
        }
    }
}
