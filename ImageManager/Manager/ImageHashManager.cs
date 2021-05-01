using ImageManager.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Manager
{
    class ImageHashManager
    {
        static GPUProcessHelper GPU;
        static CPUProcessHelper CPU;
        static bool UseGPU = false;
        static ImageHashManager()
        {
            //GPU = new GPUProcessHelper();
            //var names = GPU.GetUsableGPUNames();
            //if (names != null && names.Count != 0) {
            //    if (!GPU.SelectGPU(names[0]))
            //    {
            //        UseGPU = false;
            //        CPU = new CPUProcessHelper();
            //    }
            //    else UseGPU = true;
            //}

            UseGPU = false;
            CPU = new CPUProcessHelper();
        }
        public static bool CompareHash(string A,string B,out float DifCount)
        {
            if (UseGPU) return GPU.Compare(A, B, out DifCount);
            return CPU.Compare(A, B, out DifCount);
        }
        public static string CalculateImageHash(string Path)
        {
            if (UseGPU) return GPU.CaculateImageHash(Path);
            return CPU.CaculateImageHash(Path);
        }

        ~ImageHashManager()
        {
            GPU.Dispose();
        }
    }
}
