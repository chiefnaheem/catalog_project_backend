using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Entities;
namespace Catalog.Api.Repositories
{
	
	public class InMemItemRepository : IItemRepository

	{
		private readonly List<Item> items = new ()
		{
			new Item
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Potion",
				Price = 9,
				CreatedDate = DateTimeOffset.UtcNow
			},
			new Item
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Iron Sword",
				Price = 10,
				CreatedDate = DateTimeOffset.UtcNow
			},
			new Item
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Naheem",
				Price = 11,
				CreatedDate = DateTimeOffset.UtcNow
			},
			new Item
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Sword Ninja",
				Price = 12,
				CreatedDate = DateTimeOffset.UtcNow
			}
		};

		public async Task<List<Item>> GetItemsAsync()
		{
			return await Task.FromResult(items);
		}

		public async Task<Item> GetItemAsync(string id)
		{
			var item = items.Where(item => item.Id == id).FirstOrDefault();
			return await Task.FromResult(item);

		}

		public async Task CreateItemAsync(Item item)
		{
			items.Add(item);
			await Task.CompletedTask;
		}

		public async Task updateItemAsync(Item item)
		{
			var index =  items.FindIndex(existingItem => existingItem.Id == item.Id);
			items[index] = item;
			await Task.CompletedTask;
		}

		public async Task DeleteItemAsync(string id)
		{
			var index =  items.FindIndex(existingItem => existingItem.Id == id);
			items.RemoveAt(index);
			await Task.CompletedTask;
		}

	}
}