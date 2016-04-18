using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface IWebService
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> PostItemAsync(Item item);
    }
}