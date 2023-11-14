using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.WebRequestMethods;

namespace Camera_Watcher.Models
{
    public class CameraDetector
    {
        private List<CameraData> data = new List<CameraData>();

        /// <summary>
        /// Принимает новый список экземпляров класса камеры для проверки. 
        /// </summary>
        /// <param name="data">Новый список CameraData</param>
        public void SetData(List<CameraData> data)
        {
            this.data = data;
        }

        /// <summary>
        /// Начинает проверку и отправляет строковый список ip адрессов . 
        /// </summary>
        public void DetectData()
        {
            List<string> ipAddresses = new List<string>();
            foreach(CameraData cam in data)
            {
                ipAddresses.Add(cam.IPAdress);
            }
            IPScanner.Search(ipAddresses);
        }

        /// <summary>
        /// Принимает адрес с которым удалось установить соединение, помечает его и отправляет проверочный HTTP запрос с пометкой. 
        /// </summary>
        /// /// <param name="ipAddress">ip адрес с которым установлено соединение для отправки HTTP запроса</param>
        public async void IPAdressSuccesDetect(string ipAddress)
        {
            foreach(CameraData cam in data)
            {
                if(ipAddress == cam.IPAdress)
                {
                    cam.PingStatus = true;
                    string request = $"http://{cam.IPAdress}/cgi-bin/admin/privacy.cgi";
                    switch (await HttpRequester.GetRequestAsyncBool(request))
                    {
                        case (true):
                            {
                                cam.RequestStatus = "Проверка прошла успешно";
                            }
                            break;
                        case (false):
                            {
                                cam.RequestStatus = "Не удалось выполнить проверку";
                            }
                            break;
                    }
                }
            }
        }
    }
}
