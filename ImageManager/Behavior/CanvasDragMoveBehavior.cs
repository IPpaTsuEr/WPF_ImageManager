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
    public class CanvasDragMoveBehavior : Behavior<UIElement>
    {
        Point Offset;
        Canvas Parent;
        bool IsDraging = false;

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
            
            AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseDown;
            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseUp;
            Parent.MouseWheel += Parent_MouseWheel;
            
        }

        private void Parent_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {

            var mt = e.GetPosition(Parent);
            var sc = Parent.RenderTransform as ScaleTransform;
            if (sc == null)
            {
                sc = new ScaleTransform();
                Parent.RenderTransform = sc;
            }
            if (sc.ScaleX <= 0.1) return;
            if (sc.ScaleY <= 0.1) return;

            sc.CenterX = mt.X;
            sc.CenterY = mt.Y;
            sc.ScaleX += e.Delta > 0 ? 0.1 : -0.05;
            sc.ScaleY += e.Delta > 0 ? 0.1 : -0.05;
            sc.ScaleX = sc.ScaleX < 0.1 ? 0.1 : sc.ScaleX;
            sc.ScaleY = sc.ScaleY < 0.1 ? 0.1 : sc.ScaleY;

        }

        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            Offset = e.GetPosition(AssociatedObject);
            IsDraging = true;
            AssociatedObject.CaptureMouse();
        }

        private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsDraging)
            {
                AssociatedObject.ReleaseMouseCapture();
                IsDraging = false;
            }
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (IsDraging)
            { 
                Point Location = e.GetPosition(Parent);
                var RX = Location.X - Offset.X;
                var RY = Location.Y - Offset.Y;
                //if(AssociatedObject.DesiredSize.Width <= Parent.ActualWidth)
                //{
                //    RX = RX < 0 ? 0 : RX > Parent.ActualWidth - AssociatedObject.DesiredSize.Width ? Parent.ActualWidth - AssociatedObject.DesiredSize.Width : RX;
                //}
                //else
                //{
                //    RX = RX < Parent.ActualWidth - AssociatedObject.DesiredSize.Width ? Parent.ActualWidth - AssociatedObject.DesiredSize.Width : RX > AssociatedObject.DesiredSize.Width - Parent.ActualWidth ? AssociatedObject.DesiredSize.Width - Parent.ActualWidth : RX;
                //} 
                
                //if(AssociatedObject.DesiredSize.Height <= Parent.ActualHeight)
                //{
                //    RY = RY < 0 ? 0 : RY > Parent.ActualHeight - AssociatedObject.DesiredSize.Height ? Parent.ActualHeight - AssociatedObject.DesiredSize.Height : RY;
                //}
                //else
                //{
                //    RY = RY < Parent.ActualHeight - AssociatedObject.DesiredSize.Height ? Parent.ActualHeight - AssociatedObject.DesiredSize.Height : RY > AssociatedObject.DesiredSize.Height - Parent.ActualHeight ? AssociatedObject.DesiredSize.Height - Parent.ActualHeight : RY; 
                //}
                Canvas.SetLeft(AssociatedObject, RX);
                Canvas.SetTop(AssociatedObject, RY);

                //AssociatedObject.SetValue(Canvas.LeftProperty, Location.X - Offset.X);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseDown;
            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseUp;
            Parent.MouseWheel -= Parent_MouseWheel;
        }
    }

}
