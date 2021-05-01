using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Helper
{
    public class CPUProcessHelper:IProcessHelper
    {
        //static int AnalysisLength = 8;

        double[] HashBuffer;

        public CPUProcessHelper()
        {
            HashBuffer = new double[AnalysisLength * AnalysisLength];
        }

        //private static string Decode(string hashstr)
        //{
        //    var d = Encoding.ASCII.GetBytes(hashstr);
        //    StringBuilder sb = new StringBuilder(AnalysisLength * AnalysisLength);
        //    foreach (var item in d)
        //    {
        //        for (int i = 0; i < 8; i++)
        //        {
        //            var v = (item >> i) & 1;
        //            sb.Append(v);
        //        }
        //    }
        //    return sb.ToString();
        //}
        //private static string Encode(StringBuilder sb)
        //{
        //    var datal = AnalysisLength * AnalysisLength / 8;
        //    byte[] hashcode = new byte[datal];
        //    int index = 0;
        //    foreach (var item in sb.ToString())
        //    {
        //        var i = index / 8;
        //        var o = index % 8;
        //        if (item == '0')
        //        {

        //            hashcode[i] = (byte)(hashcode[i] << o);
        //        }
        //        else
        //        {
        //            hashcode[i] = (byte)((hashcode[i] << o) + 1);
        //        }
        //        index++;
        //    }
        //    return Encoding.ASCII.GetString(hashcode);
        //}

        //public static bool Compare(string a, string b)
        //{
        //    a = Decode(a);
        //    b = Decode(b);
        //    int c = 0;
        //    for (int i = 0; i < a.Length; i++)
        //    {
        //        if (a[i] != b[i]) c++;
        //    }
        //    Console.WriteLine($"Link:{1 - (c * 1.0f / a.Length)} Count:{c}");
        //    if (c <= 10 * (AnalysisLength / 8) * (AnalysisLength / 8)) return true;
        //    return false;
        //}

        private static double ConvertToGray(Color color)
        {
            return color.R * 0.3 + color.G * 0.59 + color.B * 0.11;
        }

        public override string CaculateImageHash(string FilePath)
        {
            Bitmap img = new Bitmap(FilePath);
            Bitmap s_img = new Bitmap(img, new Size(AnalysisLength, AnalysisLength));
            img.Dispose();

            for (int y = 0; y < s_img.Height; y++)
            {
                for (int x = 0; x < s_img.Width; x++)
                {
                    var c = s_img.GetPixel(x, y);
                    HashBuffer[x + y * AnalysisLength] = ConvertToGray(c);
                }
            }
            double avarage = 0.0;
            foreach (var item in HashBuffer)
            {
                avarage += item;
            }
            avarage /= AnalysisLength * AnalysisLength;
            StringBuilder sb = new StringBuilder(AnalysisLength * AnalysisLength);
            int index = 0;

            foreach (var item in HashBuffer)
            {
                if (item >= avarage) sb.Append("1");
                else sb.Append("0");
                index++;
            }
            var Hstr = sb.ToString();
            //Console.Write($"CPU:{Hstr}");
            return Encode(Hstr);
        }
    }
}
