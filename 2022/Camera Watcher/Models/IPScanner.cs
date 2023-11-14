using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Camera_Watcher.Models
{ 
    public class IPScanner: IDisposable
    {
        public delegate void IpCheckSuccessHandler(string message);
        public static event IpCheckSuccessHandler OnSuccess;
        private static Ping ping = new Ping();

        /// <summary>
        /// Принимает список IP адресов и врзвращает bool список удачных и неудачных попыток. 
        /// </summary>
        /// <param name="ipAdresses">Строковый список IP адресов</param>
        public static List<bool> Search(List<string> ipAdresses)
        {
            List<bool> pingStatus = new List<bool>();
            var timeout = 1000;
            var sync = new object();
            var counter = ipAdresses.Count;
            var isReady = new ManualResetEvent(false);
            foreach (string address in ipAdresses)
            {
                ping = new Ping();
                ping.PingCompleted += (s, e) =>
                {
                    lock (sync)
                    {
                        ping.Dispose();
                        if (e.Reply.Status == IPStatus.Success)
                        {
                            OnSuccess?.Invoke(address); //Для меньшей связности классов при удачных попытках вызывает срабатывание события.
                            pingStatus.Add(true);
                        }
                        else
                        { 
                            pingStatus.Add(false);
                        }
                        if (--counter == 0)
                        {
                            isReady.Set();
                        }
                    }
                };
                ping.SendAsync(address, timeout, null);
            }
            return pingStatus;
        }
        public void Dispose()
        {
            ping.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
