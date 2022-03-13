using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
	public interface IRepository<T> where T: IEntity
	{
		Task<T> GetItemAsync(string id);
		Task<List<T>> GetItemsAsync();
		Task CreateItemAsync(T item);
		Task updateItemAsync(T item);
		Task DeleteItemAsync(string id);
	}

}