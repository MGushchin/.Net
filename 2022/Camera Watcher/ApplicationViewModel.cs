using Camera_Watcher.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Camera_Watcher
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private CameraDetector detector;
        private IPScanner scanner = new IPScanner();
        private HttpRequester httpRequester = new HttpRequester();

        public string IPAdressTextBox { get; set; }
        public ObservableCollection<CameraData> Cameras { get; set; } = new ObservableCollection<CameraData>();

        public ApplicationViewModel()
        {
            List<CameraData> cameras = new List<CameraData>
            {
                new CameraData { IPAdress="192.168.0.1", PingStatus=false, RequestStatus="Проверка не выполнена" },
                new CameraData { IPAdress="192.168.0.2", PingStatus=false, RequestStatus="Проверка не выполнена" },
                new CameraData { IPAdress="192.168.0.3", PingStatus=false, RequestStatus="Проверка не выполнена" },
                new CameraData { IPAdress="192.168.0.4", PingStatus=false, RequestStatus="Проверка не выполнена" }
            };
            foreach(CameraData cam in cameras)
            {
                Cameras.Add(cam);
            }
            detector = new CameraDetector();
            IPScanner.OnSuccess += detector.IPAdressSuccesDetect; //Подписываем на событие удачной проверки ip адреса метод для отправки HTTP запроса.
            detector.SetData(cameras); //Задает список экземпляров класса камер для проверки
            detector.DetectData(); //Запуск проверку заданного списка
        }

        private RelayCommand setCommand;
        public RelayCommand SetCommand
        {
            get
            {
                return setCommand ??
                  (setCommand = new RelayCommand(obj =>
                  {
                      Set();
                  }));
            }
        }

        /// <summary>
        /// Задает новый список IP адресов для отображения и проверки. 
        /// </summary>
        private void Set()
        {
            Cameras.Clear();
            string convertResult = ""; //Результат проверки введеной строки. Остается пустой при удачной проверке.
            List<string> addresses;
            if (!IPAddressConverter.TryConvert(IPAdressTextBox, out convertResult, out addresses)) // Проверка введенной строки. В случае ошибки выводится окно с текстом ошибки.
                MessageBox.Show(convertResult);
            else
            {
                List<CameraData> cameras = new List<CameraData>();
                foreach (string address in addresses)
                {
                    cameras.Add(new CameraData { IPAdress = address, PingStatus = false, RequestStatus = "Проверка не выполнена" });
                }
                foreach (CameraData cam in cameras) //Каждый экземпляр CameraData добавляем в ObservableCollection для отображения во View
                {
                    Cameras.Add(cam);
                }
                detector.SetData(cameras); //Задаем новый список IP адресов 
                detector.DetectData(); //Начало проверки
            }
        }

        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return refreshCommand ??
                  (refreshCommand = new RelayCommand(obj =>
                  {
                      Refresh();
                  }));
            }
        }

        /// <summary>
        /// Повторяет проверку уже заданных ip адресов. 
        /// </summary>
        private void Refresh()
        {
            detector.DetectData(); //Начало проверки
        }

        /// <summary>
        ///  Высвобождает неуправляемые ресурсы моделей.
        /// </summary>
        public void Close()
        {
            scanner.Dispose();
            httpRequester.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
