using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Helper
{

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct GPUImageInfo
    {
        public int Width, Height, Deepth;
    };

    class GPUProcessHelper :IProcessHelper , IDisposable
    {
        [DllImport("GPU.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadImageFromFile(string filePathName, ref GPUImageInfo imageInfo);

        [DllImport("GPU.dll", CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SaveImageToFile(string path, IntPtr DataPtr, uint width, uint height, uint Deepth = 4);

        [DllImport("GPU.dll", CharSet = CharSet.Ansi)]
        private static extern void UnloadImage(IntPtr ptr);

        [DllImport("GPU.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr InitGpuProcecer();

        [DllImport("GPU.dll", CharSet = CharSet.Ansi)]
        private static extern void UnInitGpuProcecer(IntPtr ptr);

        [DllImport("GPU.dll", CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SelectGpuByName(IntPtr ptr, string name);

        [DllImport("GPU.dll", CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetTargetGPUName(IntPtr ptr, int Index, IntPtr buffer);

        [DllImport("GPU.dll", CharSet = CharSet.Ansi)]
        private static extern int GetUsableGPUCount(IntPtr ptr);

        [DllImport("GPU.dll", CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RunImageHashProcess(IntPtr Gpuptr, IntPtr ImageDataPtr, uint ImageWidth, uint ImageHeight, uint HashWidth, uint HashHeight, IntPtr OutPut);

        IntPtr _Gpu = IntPtr.Zero;
        IntPtr Buffer = IntPtr.Zero;

        char[] HashBuffer;


        public GPUProcessHelper()
        {
            _Gpu = InitGpuProcecer();
            HashBuffer = new char[AnalysisLength* AnalysisLength];
        }


        public void Dispose()
        {
            if (_Gpu != IntPtr.Zero)
            {
                UnInitGpuProcecer(_Gpu);
                _Gpu = IntPtr.Zero;
            }
            if (Buffer != IntPtr.Zero)
                Marshal.FreeHGlobal(Buffer);

        }

        /// <summary>
        /// 取得当前计算机上可取得的所有GPU设备名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetUsableGPUNames()
        {
            var _C = GetUsableGPUCount(_Gpu);
            List<string> DeviceList = new List<string>();

            IntPtr Buffer = Marshal.AllocHGlobal(256 * sizeof(char));
            for (int i = 0; i < _C; i++)
            {
                if (GetTargetGPUName(_Gpu, i, Buffer))
                    DeviceList.Add(Marshal.PtrToStringAnsi(Buffer));
            }
            Marshal.FreeHGlobal(Buffer);
            return DeviceList;
        }

        /// <summary>
        /// 通过GPU设定默认计算环境
        /// </summary>
        /// <param name="GPUName"></param>
        /// <returns></returns>
        public bool SelectGPU(string GPUName)
        {
            return SelectGpuByName(_Gpu, GPUName);
        }

        /// <summary>
        /// 计算指定图像的ImageHash
        /// </summary>
        /// <param name="FilePathName"></param>
        /// <returns></returns>
        public override string CaculateImageHash(string FilePath)
        {
            
            if (_Gpu == IntPtr.Zero)
            {
                Console.WriteLine("获取GPU失败");
                return null;
            }
            GPUImageInfo im = new GPUImageInfo();
            var _ImgData = LoadImageFromFile(FilePath, ref im);

            if( Buffer==IntPtr.Zero) Buffer = Marshal.AllocHGlobal((int)(AnalysisLength * AnalysisLength * sizeof(char)));

            if (!RunImageHashProcess(_Gpu, _ImgData, (uint)im.Width, (uint)im.Height, (uint)AnalysisLength, (uint)AnalysisLength, Buffer))
            {
                Marshal.FreeHGlobal(Buffer);
                Console.WriteLine("计算出错");
                return null;
            }
            //var r = SaveImageToFile("D:/tttt.png",_ImgData, (uint)im.Width, (uint)im.Height, (uint)im.Deepth);
            UnloadImage(_ImgData);
             
            Marshal.Copy(Buffer, HashBuffer, 0, (int)(AnalysisLength * AnalysisLength * sizeof(char)));
            string Hstr = new string(HashBuffer);
            Console.Write($"GPU:{Hstr}");
            return Encode(Hstr);
        }

        ///// <summary>
        ///// 比较两个被编码后的ImageHash
        ///// </summary>
        ///// <param name="ImageHashCodeA"></param>
        ///// <param name="ImageHashCodeB"></param>
        ///// <returns>0到1之间的小数，越接近1表示越相似</returns>
        //public static float CompareHash(string ImageHashCodeA, string ImageHashCodeB)
        //{
        //    if (ImageHashCodeA.Length != ImageHashCodeB.Length)
        //    {
        //        return 0.0f;
        //    }
        //    return CompareHash(Encoding.ASCII.GetBytes(ImageHashCodeA), Encoding.ASCII.GetBytes(ImageHashCodeB));
        //}

        ///// <summary>
        ///// 比较两个ImageHash的汉明距，
        ///// </summary>
        ///// <param name="A"></param>
        ///// <param name="B"></param>
        ///// <returns>0到1之间的小数，越接近1表示越相似</returns>
        //public static float CompareHash(Byte[] A, Byte[] B)
        //{
        //    if (A.Length != B.Length) return 0.0f;
        //    uint Count = 0;
        //    for (int i = 0; i < A.Length; i++)
        //    {
        //        for (int j = 0; j < 8; j++)
        //        {
        //            var va = (A[i] >> j) & 1;
        //            var vb = (B[i] >> j) & 1;
        //            if ((va ^ vb) == 1) Count++;
        //        }
        //    }
        //    return (A.Length * 8.0f - Count) / (A.Length * 8.0f);
        //}

    }
}

