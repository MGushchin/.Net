using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Camera_Watcher
{
    public class CameraData : INotifyPropertyChanged
    {
        private string iPAddress;
        private bool pingStatus;
        private string requestStatus;

        public string IPAdress
        {
            get { return iPAddress; }
            set
            {
                iPAddress = value;
                OnPropertyChanged("IPAdress");
            }
        }
        public bool PingStatus
        {
            get { return pingStatus; }
            set
            {
                pingStatus = value;
                OnPropertyChanged("PingStatus");
            }
        }
        public string RequestStatus
        {
            get { return requestStatus; }
            set
            {
                requestStatus = value;
                OnPropertyChanged("RequestStatus");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
