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
    class AwaysAtCanvasCenterBehavoir:Behavior<UIElement>
    {
        Canvas Parent;
        protected override void OnAttached()
        {
            base.OnAttached();
            if (Parent == null)
            {
                DependencyObject _t = AssociatedObject;
                while (!(_t is Canvas))
                {
                    Console.WriteLine(_t.GetType().FullName);
                    _t = VisualTreeHelper.GetParent(_t);
                }
                Parent = _t as Canvas;
            }
            if (Parent != null)
            {
                Parent.SizeChanged += Parent_SizeChanged;
            }
        }



        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (Parent != null)
            {
                Parent.SizeChanged -= Parent_SizeChanged;
            }
        }

        private void Parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var w = (e.NewSize.Width - AssociatedObject.DesiredSize.Width)/2;
            var h = (e.NewSize.Height - AssociatedObject.DesiredSize.Height)/2;
            Canvas.SetLeft(AssociatedObject, w);
            Canvas.SetTop(AssociatedObject, h);

        }
    }
}
