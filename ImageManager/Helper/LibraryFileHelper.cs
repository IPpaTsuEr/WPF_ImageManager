using ImageManager.Manager;
using ImageManager.Modle;
using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Helper
{
    class LibraryFileHelper
    {
        public static string ImageFilter = ".png.bmp.jpg";

        public static bool IsSubFolder(string StandPath,string OtherPath)
        {
            if (!OtherPath.StartsWith(StandPath)) return false;

            if (!System.IO.Directory.Exists(StandPath)) StandPath = System.IO.Path.GetDirectoryName(StandPath);
            if (!System.IO.Directory.Exists(OtherPath)) OtherPath = System.IO.Path.GetDirectoryName(OtherPath);
            
            var PS = StandPath.Split(System.IO.Path.DirectorySeparatorChar);
            var OS = OtherPath.Split(System.IO.Path.DirectorySeparatorChar);
            
            if (OS.Length < PS.Length) return false;

            for (int i = 0; i < PS.Length; i++)
            {
                if (PS[i] != OS[i]) return false;
            }
            return true;
        }

        public static string GetFolderThumbnial(string FolderPath,ICollection<ImageInfos> images)
        {
            var t = (from p in images where IsSubFolder(FolderPath,p.SourcePath) select p.SourcePath).ToList();
            if (t == null || t.Count == 0) return @"Resources\NotFound.png";
            return t[0];
        }

        public static bool GetFiles(string Path, List<string> ResultCollection,int CountLimit = -1,bool IncludeSubFolder = true)
        {
            if (!System.IO.Directory.Exists(Path)) return false;
            foreach (var item in System.IO.Directory.GetFiles(Path))
            {
                var ex = System.IO.Path.GetExtension(item);
                //不限制数量，或限制数量但当前数量未达到
                if(CountLimit == -1 || 
                    (CountLimit != -1 &&
                     (ResultCollection.Count ==0 || CountLimit - ResultCollection.Count ==0)))
                {
                    if (ImageFilter.IndexOf(ex) >= 0) 
                        ResultCollection.Add(item);
                } 

            }
            //递归子文件夹
            if (IncludeSubFolder)
            {
                foreach (var subitem in System.IO.Directory.GetDirectories(Path))
                {
                    Console.WriteLine(subitem);
                    GetFiles(subitem, ResultCollection, CountLimit, IncludeSubFolder);
                }
            }

            return true;
        }

        public static ImageInfos AnalysisImage(string Filepath)
        {
            if (File.Exists(Filepath))
            {
                ImageInfos _Infos = new ImageInfos();
                _Infos.SourcePath = Filepath;
                var mm = ImageMetadataReader.ReadMetadata(Filepath);
                foreach (var item in mm)
                {
                    foreach (var sub in item.Tags)
                    { 
                        ConvertToImageInfo(sub.Name,sub.Description, _Infos); 
                    }
                }
                if (_Infos.Date.Ticks == 0)
                {
                    _Infos.Date = new FileInfo(_Infos.SourcePath).CreationTime;
                }
                if (_Infos.ModifiedDate.Ticks == 0)
                {
                    _Infos.Date = new FileInfo(_Infos.SourcePath).LastWriteTime;
                }
                return _Infos;
            }
            return null;
            
        }

        public static void AddAllFile(string Path,ICollection<ImageInfos> images)
        {
            List<string> _list = new List<string>();
            if(GetFiles(Path, _list))
            {
                foreach (var item in _list)
                {
                    var imf = AnalysisImage(item); 
                    images.Add(imf);
                }
            }
        }

        private static void ConvertToImageInfo(string str,string value,ImageInfos imf)
        {
            var rt = str;
            switch (str)
            {
                case "Exif Version":
                    rt = "Exif版本";
                    break;
                case "Model":
                    rt = "相机型号";
                    imf.CameraModel = value;
                        
                    break;
                case "Lens Model":
                    rt = "镜头类型";
                    imf.LensModel = value;
                    break;
                case "File Name":
                    rt = "文件名";
                    imf.Name = value;
                    break;
                case "File Size":
                    rt = "文件大小";
                    break;
                case "Date/Time":
                    rt = "拍摄时间";
                    DateTime ct;
                    if (DateTime.TryParseExact(value, "yyyy:MM:dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out ct))
                        imf.Date = ct;
                    break;
                case "File Modified Date":
                    rt = "修改时间";
                    DateTime mct;
                    if(DateTime.TryParseExact(value, "ddd MMM dd HH:mm:ss zzz yyyy", System.Globalization.CultureInfo.CurrentUICulture, System.Globalization.DateTimeStyles.None, out mct))
                        imf.ModifiedDate = mct; 
                    break;
                case "Image Height":
                    rt = "照片高度";
                    int h;
                    if (int.TryParse(value.Split(' ')[0], out h)) imf.Height = h;
                    else imf.Height = -1;
                    break;
                case "Image Width":
                    rt = "照片宽度";
                    int w;
                    if (int.TryParse(value.Split(' ')[0], out w)) imf.Width = w;
                    else imf.Width = -1;
                    break;
                case "X Resolution":
                    rt = "水平分辨率";
                    break;
                case "Y Resolution":
                    rt = "垂直分辨率";
                    break;
                case "Color Space":
                    rt = "色彩空间";
                    break; 
                case "Shutter Speed Value":
                    rt = "快门速度";
                    break;
                case "F-Number":
                    rt = "光圈";//Aperture Value也表示光圈
                    imf.FNumber = value;
                    break;
                case "ISO Speed Ratings":
                    rt = "ISO";
                    imf.ISO = value;
                    break;
                case "Exposure Bias Value":
                    rt = "曝光补偿";
                    int eb;
                    int.TryParse(value,out eb);
                    imf.ExBias = eb;
                    break;
                case "Focal Length":
                    rt = "焦距";
                    
                    break; 
                case "Exposure Program":
                    rt = "曝光程序";
                    break;
                case "Metering Mode":
                    rt = "测光模式";
                    break;
                case "Flash Mode":
                    rt = "闪光灯";
                    bool fl;
                    bool.TryParse(value, out fl);
                    imf.FlashON = fl;
                    break;
                case "White Balance Mode":
                    rt = "白平衡";
                    break;
                case "Exposure Mode":
                    rt = "曝光模式";
                    break;
                case "Exposure Time":
                    //曝光时长
                    break;
                case "Continuous Drive Mode":
                    rt = "驱动模式";
                    break;
                case "Focus Mode":
                    rt = "对焦模式";
                    break;
                case "Detected File Type Name":
                    //JPEG
                    rt = "图像格式";
                    break;
                case "GPS Latitude Ref":
                    //N
                   
                    break;
                case "GPS Latitude":
                    //40° 0' 27.18"
                    rt = "GPS纬度";
                    break;
                case "GPS Longitude Ref":
                    //E
                    break;
                case "GPS Longitude":
                    //116° 23' 8.97"
                    rt = "GPS经度";
                    break;
                default:
                    rt = str;
                    break;
            }
            Console.WriteLine($"{rt},{value}");
        }
    }
}
