using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
	public class MongoRepository<T> : IRepository<T> where T:IEntity
	{
		private const string databaseName = "catalog";
		private readonly IMongoCollection<T> itemsCollection;
		private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
		public MongoRepository(IMongoDatabase database, string collectionName)
		{
			// IMongoDatabase database = mongoClient.GetDatabase(databaseName);
			itemsCollection = database.GetCollection<T>(collectionName);
		}










		public async Task CreateItemAsync(T item)
		{
			await itemsCollection.InsertOneAsync(item);
		}

		public async Task DeleteItemAsync(string id)
		{
			var filter = filterBuilder.Eq(item => item.Id, id);
			await itemsCollection.DeleteOneAsync(filter);
		}

		public async Task<T> GetItemAsync(string id)
		{
			var filter = filterBuilder.Eq(item => item.Id, id);
			return await itemsCollection.Find(filter).SingleOrDefaultAsync();
		}

		public async Task<List<T>> GetItemsAsync()
		{
			return await itemsCollection.Find(new BsonDocument()).ToListAsync();
		}

		public async Task updateItemAsync(T item)
		{
			var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
			await itemsCollection.ReplaceOneAsync(filter, item);
		}
	}
}