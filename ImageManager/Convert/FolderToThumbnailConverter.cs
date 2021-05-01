using ImageManager.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageManager.Convert
{
    class FolderToThumbnailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || value is string)
            {
                string Folder = value as string;
                var result = new List<string>();
                LibraryFileHelper.GetFiles(Folder, result, 1, true);
                return result.Count > 0 ? result[0] : "";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
