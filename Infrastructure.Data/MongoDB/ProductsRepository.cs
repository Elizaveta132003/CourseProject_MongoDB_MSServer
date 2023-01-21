using Domain.Core.Models.Products;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data.MongoDB
{
	public class ProductsRepository : MainMongoRepository<Product>
	{
		public ProductsRepository(string connectionString) : base(connectionString, "products")
		{
		}

		public override async Task<bool> CreateAsync(Product item)
		{
			var parser = new MongoParser();

			item.Id = parser.MaxIndex(_mongoCollection) + 1;
			var document = new BsonDocument
			{
				{"_id", item.Id},
				{"name", item.Name},
				{"type", item.Type},
				{"price", item.Price}
			};
			await _mongoCollection.InsertOneAsync(document);

			return true;
		}

		public override async Task<bool> UpdateAsync(Product item)
		{
			var filter = Builders<BsonDocument>.Filter.Eq("_id", item.Id);

			var update = Builders<BsonDocument>.Update.Set("name", item.Name);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("type", item.Type);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("price", item.Price);
			await _mongoCollection.UpdateOneAsync(filter, update);

			return true;
		}

		protected override Product Initialization(BsonDocument item)
			=> new Product()
			{
				Id = item.GetValue("_id").ToInt32(),
				Name = item.GetValue("name").ToString(),
				Type = item.GetValue("type").ToInt32(),
				Price = item.GetValue("price").ToInt32()
			};
	}
}
