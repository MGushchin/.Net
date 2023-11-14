using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using Newtonsoft.Json;
using System.Windows;
using ToolsClientWPF.Models;

namespace ToolsClientWPF
{

    public class HttpClientConnector
    {
        static HttpClient client = new HttpClient();

        //POST
        public static async Task<Uri> CreateProductAsync(ToolItem item)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:7297/api/toolitems", item);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.Headers.Location;
        }

        //GET
        public static async Task<List<ToolItem>> GetProductAsync(string path)
        {
            List<ToolItem> toolList = null;
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)

            {
                string jsonString = await response.Content.ReadAsStringAsync();
                toolList = JsonConvert.DeserializeObject<List<ToolItem>>(jsonString);
            }

            return toolList;
        }


        //PUT
        public static async Task<ToolItem> UpdateProductAsync(ToolItem item)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"https://localhost:7297/api/toolitems/{item.Id}", item);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            item = await response.Content.ReadAsAsync<ToolItem>();
            return item;
        }

        //DELETE
        public static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"https://localhost:7297/api/toolitems/{id}");
            return response.StatusCode;
        }
    }
}
