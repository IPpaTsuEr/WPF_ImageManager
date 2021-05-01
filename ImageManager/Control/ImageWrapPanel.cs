using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ImageManager.Control
{
    public class ImageWrapPanel:WrapPanel
    {


        public double LineHeight
        {
            get { return (double)GetValue(LineHeightProperty); }
            set { SetValue(LineHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineHeightProperty =
            DependencyProperty.Register("LineHeight", typeof(double), typeof(ImageWrapPanel), new PropertyMetadata(0.0,OnLineHeightChanged));

        private static void OnLineHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is ImageWrapPanel)
            {
                ImageWrapPanel imp = d as ImageWrapPanel;
                imp.InvalidateMeasure();
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size _t = new Size(0,0);
            foreach (UIElement item in  InternalChildren)
            {
                item.Measure(new Size(double.PositiveInfinity , LineHeight));
                if (_t.Width + item.DesiredSize.Width >= constraint.Width)
                {
                    _t.Height += LineHeight;
                    _t.Width = 0;
                }
                _t.Width += item.DesiredSize.Width;
                
            }
            if (_t.Width > 0) _t.Height += LineHeight;
             var f = new Size(constraint.Width,_t.Height);
            return f;
        }
        
        protected override Size ArrangeOverride(Size finalSize)
        {
            var bf = base.ArrangeOverride(finalSize);
            Size _o = new Size(0, 0);
            foreach (UIElement item in InternalChildren)
            {
                if (_o.Width + item.DesiredSize.Width >= finalSize.Width)
                {
                    _o.Height += LineHeight;
                    _o.Width = 0;
                }
                item.Arrange(new Rect(_o.Width ,_o.Height, item.DesiredSize.Width, LineHeight));
                _o.Width += item.DesiredSize.Width;
               
            }
            return bf;
        }
    }
}
