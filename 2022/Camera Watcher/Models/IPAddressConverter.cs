using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Camera_Watcher.Models
{
    public class IPAddressConverter
    {
        /// <summary>
        /// Принимает строку ip адреса или диапазона, проверяет корректность данных и возвращает результат со строкой ошибки и список строковых ip адресов. 
        /// Не обрабатывает более двух значений диапазона. 
        /// </summary>
        /// <param name="ipAddress">Строка с ip адресом или их диапазоном.</param>
        /// <param name="result">Строка ошибки, в случае удачной проверки остается пустой.</param>
        /// <param name="addresses">Список строковых ip адресов в случае удачной проверки.</param>
        public static bool TryConvert(string ipAddress, out string result, out List<string> addresses)
        {
            addresses = new List<string>();
            if (ipAddress == null || ipAddress == "")
            {
                result = "Некорректный ввод. Пустое поле диапазона.";
                return false;
            }
            string[] parts = ipAddress.Split(' ', '-');
            if (parts.Length == 1)
            {
                IPAddress address;
                IPAddress.TryParse(parts[0], out address);
                if (address != null)
                {
                    result = "";
                    addresses.Add(parts[0]);
                    return true;
                }
                else
                {
                    result = "Некорректный ввод. Не удалось сконвертировать значение IP адреса.";
                    return false;
                }
            }
            else if (parts.Length == 2)
            {
                IPAddress startIPAddress;
                IPAddress.TryParse(parts[0], out startIPAddress);
                IPAddress finalIPAddress;
                IPAddress.TryParse(parts[1], out finalIPAddress);
                if (startIPAddress != null && finalIPAddress != null)
                {
                    result = "";
                    addresses = iPAddressesRange(parts[0], parts[1]);
                    return true;
                }
                else
                {
                    result = "Некорректный ввод. Не удалось сконвертировать значение IP адреса.";
                    return false;
                }
            }
                result = "Некорректный ввод диапазона. Поддерживается ввод одного IP-адреса или двух значений диапазона с разделителями - или ' '.";
                return false;
        }

        /// <summary>
        /// Принимает строку ip адреса или диапазона. Проверяет корректность данных и возвращает строчный список ip адрессов. Не обрабатывает более двух значений диапазона. 
        /// </summary>
        /// <param name="ipAddress">Строка с ip адресом или их диапазоном.</param>
        public static List<string> Convert(string ipAddress)
        {
            List<string> addresses = new List<string>();
            string[] parts = ipAddress.Split(' ', '-');
            if (parts.Length == 1)
            {
                addresses.Add(parts[0]);
            }
            else if (parts.Length == 2)
            {
                addresses = iPAddressesRange(parts[0], parts[1]);
            }
           return addresses;
        }

        /// <summary>
        /// Принимает начальный и конечный адрес диапазона и возвращает список всех адресов в нем. 
        /// </summary>
        /// <param name="startIPAddress">Строка первого ip-адреса диапазона</param>
        /// <param name="finalIPAddress">Строка последнего ip-адреса диапазона</param>
        private static List<string> iPAddressesRange(string startIPAddress, string finalIPAddress)
        {
            IPAddress firstIPAddress = IPAddress.Parse(startIPAddress);
            IPAddress lastIPAddress = IPAddress.Parse(finalIPAddress);
            var firstIPAddressAsBytesArray = firstIPAddress.GetAddressBytes();

            var lastIPAddressAsBytesArray = lastIPAddress.GetAddressBytes();

            Array.Reverse(firstIPAddressAsBytesArray);

            Array.Reverse(lastIPAddressAsBytesArray);

            var firstIPAddressAsInt = BitConverter.ToInt32(firstIPAddressAsBytesArray, 0);

            var lastIPAddressAsInt = BitConverter.ToInt32(lastIPAddressAsBytesArray, 0);

            var ipAddressesInTheRange = new List<string>();

            for (var i = firstIPAddressAsInt; i <= lastIPAddressAsInt; i++)
            {
                var bytes = BitConverter.GetBytes(i);

                var newIp = new IPAddress(new[] { bytes[3], bytes[2], bytes[1], bytes[0] });

                ipAddressesInTheRange.Add(newIp.ToString());
            }

            return ipAddressesInTheRange;
        }
    }
}
