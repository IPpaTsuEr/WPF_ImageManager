using ImageManager.Helper;
using ImageManager.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Manager
{
    class ImageObjectManager
    {
        
        public static Dictionary<string, ImageInfos> _ImageObjectLib { get; } = new Dictionary<string, ImageInfos>();

        public static ImageInfos Get(string FilePath)
        {
            ImageInfos infos;
            if (_ImageObjectLib.TryGetValue(FilePath, out infos)) return infos;
            return null;
        }
        public static bool Add(string FilePath,ImageInfos infos)
        {
            if (!_ImageObjectLib.ContainsKey(FilePath))
            {
                _ImageObjectLib.Add(FilePath,infos);
                return true;
            }
            return false;
        }
        public static bool Add(string FilePath)
        {
            if (!_ImageObjectLib.ContainsKey(FilePath))
            {
                ImageInfos infos = LibraryFileHelper.AnalysisImage(FilePath);
                _ImageObjectLib.Add(FilePath,infos);
                return true;
            }
            return false;
        }
        public static bool Remove(string FilPath)
        {
            return _ImageObjectLib.Remove(FilPath);
        }
    }
}
