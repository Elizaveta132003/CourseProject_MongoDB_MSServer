using Domain.Core.Models;
using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data.MongoDB
{
	public abstract class MainMongoRepository<T> : IRepository<T> where T : EntityBase
	{
		protected readonly IMongoCollection<BsonDocument> _mongoCollection;
		private readonly string _nameDatabase = "bakery";

		public MainMongoRepository(string connectionString, string nameCollection)
		{
			if (connectionString == null)
				throw new ArgumentNullException(nameof(connectionString));

			var mongoClient = new MongoClient(connectionString);
			IMongoDatabase mongoDatabase = mongoClient.GetDatabase(_nameDatabase);
			_mongoCollection = mongoDatabase.GetCollection<BsonDocument>(nameCollection);
		}

		public bool Create(T item)
		{
			return CreateAsync(item).Result;
		}

		public abstract Task<bool> CreateAsync(T item);

		public bool Delete(int id)
		{
			return DeleteAsync(id).Result;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", id);

			await _mongoCollection.DeleteOneAsync(deleteFilter);

			return true;
		}

		public List<T> GetAll()
		{
			return GetAllAsync().Result;
		}

		public async Task<List<T>> GetAllAsync()
		{
			var filter = new BsonDocument();
			var collection = new List<T>();

			using (IAsyncCursor<BsonDocument> cursor = await _mongoCollection.FindAsync(filter))
			{
				while (await cursor.MoveNextAsync())
				{
					IEnumerable<BsonDocument> bakeryBson = cursor.Current;

					foreach (BsonDocument item in bakeryBson)
					{
						collection.Add(Initialization(item));
					}
				}
			}

			return collection;
		}
		protected abstract T Initialization(BsonDocument item);

		public T GetT(int id)
		{
			return GetTAsync(id).Result;
		}

		public async Task<T> GetTAsync(int id)
		{
			var filter = new BsonDocument("_id", id);

			using (IAsyncCursor<BsonDocument> cursor = await _mongoCollection.FindAsync(filter))
			{
				if (await cursor.MoveNextAsync())
				{
					if (cursor.Current.Count() == 0)
						return null;

					var elements = cursor.Current.ToList();
					BsonDocument item = elements[0];

					return Initialization(item);
				}
			}

			return null;
		}

		public bool Update(T item)
		{
			return UpdateAsync(item).Result;
		}

		public abstract Task<bool> UpdateAsync(T item);
	}
}
