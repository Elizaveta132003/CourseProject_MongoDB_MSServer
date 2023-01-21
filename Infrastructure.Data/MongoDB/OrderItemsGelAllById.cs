using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB
{
	public class OrderItemsGelAllById
	{
		IMongoCollection<BsonDocument> _mongoCollection;

		public OrderItemsGelAllById(IMongoCollection<BsonDocument> mongoCollection)
		{
			_mongoCollection = mongoCollection;
		}

		public async Task<List<OrderItem>> GetAllByIdOneToMany(int id)
		{
			var pipeline = new BsonDocument
			{
				{"$unwind", "$products"}
			};

			var pipeline2 = new BsonDocument
			{
				{"$match", new BsonDocument{
					{"_id", id }
				}}
			};

			var pipeline3 = new BsonDocument
			{
				{
					"$project", new BsonDocument
					{
						{ "_id", "$_id"},
						{"productName", "$products.productName"},
						{"productPrice", "$products.productPrice"},
						{"productType", "$products.productType"},
						{"count",  "$products.count"}
					}
				}
			};

			BsonDocument[] pipelines = new BsonDocument[] { pipeline, pipeline2, pipeline3 };
			List<BsonDocument> results = await _mongoCollection.Aggregate<BsonDocument>(pipelines).ToListAsync();

			List<OrderItem> orderItems = new();

			foreach (BsonDocument item in results)
			{
				orderItems.Add(new OrderItem()
				{
					Product = new Product(item.GetValue("_id").ToInt32(), item.GetValue("productName").ToString(), item.GetValue("productType").ToInt32(), item.GetValue("productPrice").ToDecimal()),
					Count = item.GetValue("count").ToInt32()
				}); ;
			}

			return orderItems;
		}
	}
}
