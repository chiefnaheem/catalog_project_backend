using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
	public class MongoItemRepository : IItemRepository
	{
		private const string databaseName = "catalog";
		private const string collectionName = "items";
		private readonly IMongoCollection<Item> itemsCollection;
		private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
		public MongoItemRepository(IMongoClient mongoClient)
		{
			IMongoDatabase database = mongoClient.GetDatabase(databaseName);
			itemsCollection = database.GetCollection<Item>(collectionName);
		}










		public async Task CreateItemAsync(Item item)
		{
			await itemsCollection.InsertOneAsync(item);
		}

		public async Task DeleteItemAsync(string id)
		{
			var filter = filterBuilder.Eq(item => item.Id, id);
			await itemsCollection.DeleteOneAsync(filter);
		}

		public async Task<Item> GetItemAsync(string id)
		{
			var filter = filterBuilder.Eq(item => item.Id, id);
			return await itemsCollection.Find(filter).SingleOrDefaultAsync();
		}

		public async Task<List<Item>> GetItemsAsync()
		{
			return await itemsCollection.Find(new BsonDocument()).ToListAsync();
		}

		public async Task updateItemAsync(Item item)
		{
			var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
			await itemsCollection.ReplaceOneAsync(filter, item);
		}
	}
}