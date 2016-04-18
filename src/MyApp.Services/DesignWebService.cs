using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class DesignWebService : IWebService
    {
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return new[]
            {
                new Item
                {
                    Title = "Test 1"
                },
                new Item
                {
                    Title = "Test 2"
                },
                new Item
                {
                    Title = "Test 3"
                }
            };
        }

        public async Task<Item> PostItemAsync(Item item)
        {
            return new Item()
            {
                Title = item.Title
            };
        }
    }
}
