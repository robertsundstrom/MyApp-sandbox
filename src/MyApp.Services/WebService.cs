using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class WebService : IWebService
    {
        private HttpClient client;
        private JsonSerializerSettings jsonSettings;

        public WebService(Uri uri)
        {
            client = new HttpClient();

            ServiceUri = uri;

            jsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        public Uri ServiceUri { get; private set; }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var response = await client.GetAsync(new Uri(ServiceUri, new Uri("/api/Items", UriKind.Relative)));
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Item>>(jsonString, jsonSettings);
        }

        public async Task<Item> PostItemAsync(Item item)
        {
            var str = JsonConvert.SerializeObject(item, jsonSettings);
            var response = await client.PostAsync(new Uri(ServiceUri, new Uri("/api/Items", UriKind.Relative)), new StringContent(str, Encoding.UTF8, "application/json"));
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Item>(jsonString, jsonSettings);
        }
    }
}
