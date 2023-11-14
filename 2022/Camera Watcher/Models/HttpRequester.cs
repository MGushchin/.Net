using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Camera_Watcher.Models
{
    public class HttpRequester: IDisposable
    {
        private static HttpClient client = new HttpClient();

        /// <summary>
        /// Принимает путь запроса, отправляет запрос и возвращает bool результат успешен ли запрос. 
        /// </summary>
        /// <param name="path">Полная строка HTTP запроса</param>
        public static async Task<bool> GetRequestAsyncBool(string path)
        {
            string result = null;
            bool status = false;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
                status = true;
            }
            return status;
        }

        /// <summary>
        /// Принимает путь запроса, отправляет запрос и возвращает результат запроса. 
        /// </summary>
        /// <param name="path">Полная строка HTTP запроса</param>
        public static async Task<string> GetRequestAsync(string path)
        {
            string result = null;
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)

            {
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }

        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
