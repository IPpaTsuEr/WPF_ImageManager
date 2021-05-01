using ImageManager.Modle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Helper
{
    class JsonHelper
    {

        public static string Convert(ImageInfos imif)
        {
            try
            {
                return JsonConvert.SerializeObject(imif);
            }catch(Exception e)
            {
                Trace.WriteLine($"转换为Json失败：{e.Message}");
                return "";
            }
        }

        public static ImageInfos Convert(string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<ImageInfos>(str);
            }catch(Exception e)
            {
                Trace.WriteLine($"由Json {str} 转换为指定对象失败：{e.Message}");
                return null;
            }
           
        }
    }
}
