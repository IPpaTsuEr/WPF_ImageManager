using ImageManager.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ImageManager.Selector
{
    class LibStyleSelector:DataTemplateSelector
    {
        public DataTemplate AddType { get; set; }
        public DataTemplate NodeType { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var _i = item as LibraryInfos;
            if(_i == null || _i.Name == null || _i.Path == null)
                return base.SelectTemplate(item, container);
            if (_i.Name == "添加文件夹" && _i.Path == "向库中添加更多文件夹")
                return AddType;
            else
                return NodeType;
           
        }
    }
}
