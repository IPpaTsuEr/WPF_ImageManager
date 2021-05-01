using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Modle
{
    public class LibraryInfos:ViewModelBase
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; RaisePropertyChanged("Name"); }
        }

        private string _Path;

        public string Path
        {
            get { return _Path; }
            set { _Path = value; RaisePropertyChanged("Path"); }
        }

        private string _Thumbnail=@"Resources\NotFound.png";

        public string Thumbnial
        {
            get { return _Thumbnail; }
            set { _Thumbnail = value; RaisePropertyChanged("Thumbnial"); }
        }

        private bool _IsValid;

        public bool IsValid
        {
            get { return _IsValid; }
            set { _IsValid = value; RaisePropertyChanged("IsValid"); }
        }

        private string _ErrorInfo;

        public string ErrorInfo
        {
            get { return _ErrorInfo; }
            set { _ErrorInfo = value; RaisePropertyChanged("ErrorInfo"); }
        }


    }
}
