using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
	public interface IItemRepository
	{
		Task<Item> GetItemAsync(string id);
		Task<List<Item>> GetItemsAsync();
		Task CreateItemAsync(Item item);
		Task updateItemAsync(Item item);
		Task DeleteItemAsync(string id);
	}

}