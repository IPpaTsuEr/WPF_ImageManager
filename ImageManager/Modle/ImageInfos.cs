using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Modle
{
    public class ImageInfos:ViewModelBase
    {
        public long ID=-1;
        public List<string> Tags;
        public string HashString;

        private string _SourcePath;

        public string SourcePath
        {
            get { return _SourcePath; }
            set { _SourcePath = value; RaisePropertyChanged("SourcePath"); }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; RaisePropertyChanged("Name"); }
        }

        private DateTime _Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; RaisePropertyChanged("Date"); }
        }

        private int _Height;

        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        private int _Width;

        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        private string _ISO;

        public string ISO
        {
            get { return _ISO; }
            set { _ISO = value; }
        }

        private DateTime _ModifiedDate;

        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        private string _FNumber;

        public string FNumber
        {
            get { return _FNumber; }
            set { _FNumber = value; }
        }

        private bool _FlashON;

        public bool FlashON
        {
            get { return _FlashON; }
            set { _FlashON = value; }
        }

        private string _CameraModel;

        public string CameraModel
        {
            get { return _CameraModel; }
            set { _CameraModel = value; }
        }

        private string _LensModel;

        public string LensModel
        {
            get { return _LensModel; }
            set { _LensModel = value; }
        }

        private int _ExBias;

        public int ExBias
        {
            get { return _ExBias; }
            set { _ExBias = value; }
        }

    }
}
