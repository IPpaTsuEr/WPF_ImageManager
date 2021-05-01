using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Helper
{
    public class IProcessHelper
    {
        public int AnalysisLength  = 32;
        public Encoding StrCoder = Encoding.ASCII;



        public virtual string Encode(string RawHash) {
            var datal = AnalysisLength * AnalysisLength / 8;
            byte[] hashcode = new byte[datal];
            int index = 0;
            foreach (var item in RawHash)
            {
                var i = index / 8;
                var o = index % 8;
                if (item == '0')
                {

                    hashcode[i] = (byte)(hashcode[i] << o);
                }
                else
                {
                    hashcode[i] = (byte)((hashcode[i] << o) + 1);
                }
                index++;
            }
            return StrCoder.GetString(hashcode);
        }
        public virtual string Decode(string EncodedHash) {
            var d = StrCoder.GetBytes(EncodedHash);
            StringBuilder sb = new StringBuilder(AnalysisLength * AnalysisLength);
            foreach (var item in d)
            {
                for (int i = 0; i < 8; i++)
                {
                    var v = (item >> i) & 1;
                    sb.Append(v);
                }
            }
            return sb.ToString();
        }

        public virtual string CaculateImageHash(string FilePath) { return null; }

        public virtual bool Compare(string ImageHashA,string ImageHashB,out float LikeRate) {
            var a = Decode(ImageHashA);
            var b = Decode(ImageHashB);
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) count++;
            }
            LikeRate = 1 - (count * 1.0f / a.Length);
            Console.WriteLine($"Link:{LikeRate} Count:{count}");
            if (count <= 10 * (AnalysisLength / 8) * (AnalysisLength / 8)) return true;
            return false;
        }
    }
}
