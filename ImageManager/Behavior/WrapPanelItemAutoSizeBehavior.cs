using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ImageManager.Behavior
{
    class WrapPanelItemAutoSizeBehavior:Behavior<UIElement>
    {

        protected override void OnAttached()
        {
           if(AssociatedObject as WrapPanel != null)
            {
                (AssociatedObject as WrapPanel).SizeChanged += _Host_SizeChanged;
            }
        }


        protected override void OnDetaching()
        {
            (AssociatedObject as WrapPanel).SizeChanged -= _Host_SizeChanged;
        }


        private void _Host_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WrapPanel _p =  sender as WrapPanel;
            
            if (e.WidthChanged && _p.IsArrangeValid)
            {
                //var c = _p.Children[0] as ListViewItem;
                //var c1 = _p.Children[1] as ListViewItem;
                //var c2 = _p.Children[2] as ListViewItem;

                //var t = c.ActualWidth + c1.ActualWidth + c2.ActualWidth;

                var _c = _p.ActualWidth < 360 ? 2 : _p.ActualWidth < 720 ? 3 : _p.ActualWidth < 1366 ? 5 : _p.ActualWidth < 1920 ? 7 : _p.ActualWidth < 2056 ? 9 : _p.ActualWidth < 3096 ? 11 : 13;
                _p.ItemWidth = (_p.ActualWidth - 32) / _c;
                var _h = _p.ActualWidth < 360 ? 200 : _p.ActualWidth < 720 ? 240 : _p.ActualWidth < 1366 ? 360 : _p.ActualWidth < 2150 ? 480 : _p.ActualWidth < 2056 ? 500 : _p.ActualWidth < 3096 ? 520 : 540; 
                _p.ItemHeight = _h;
            }
        }
    }
}
