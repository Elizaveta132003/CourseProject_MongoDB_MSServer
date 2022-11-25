using Domain.Core.Models.Products;
using Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Data.SQLServer
{
	public class ProductsRepository : ReadByDataBase, IRepository<Product>, IProductRepository
	{

		private string _queryInsert= $"INSERT INTO Products (ProductName, ProductTypeCode, Price, Hide) VALUES(@productName,@productTypeCode,@price, @hide)";
		private string _updateInsert= $"UPDATE Products SET Hide=@hide WHERE Id=@id";
		private string _getAll = "SELECT Products.Id, Products.ProductName, Products.ProductTypeCode, Products.Price, Products.Hide FROM Products INNER JOIN TypeOfProducts ON ProductTypeCode=TypeOfProducts.Id ";
		private string _getAllNotHide= @"SELECT Products.Id, Products.ProductName, Product.ProductTypeCode,
                                         Products.Price, Products.Hide FROM Products INNER JOIN TypeOfProducts 
                                         ON ProductTypeCode=TypeOfProducts.Id where Hide=0";

		public ProductsRepository(string connectionString) : base(connectionString)
		{
		}

		public bool Create(Product item)
		{
			try
			{
				var nameProduct = item.Name;
				var priceProduct = item.Price;
				var typeProduct = item.Type;

				var connect = Connect();
				connect.Open();
				string query = _queryInsert;

				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@productName", SqlDbType.VarChar).Value = nameProduct;
				command.Parameters.Add("@productTypeCode", SqlDbType.Int).Value = typeProduct;
				command.Parameters.Add("@price", SqlDbType.Decimal).Value = priceProduct;
				command.Parameters.Add("@hide", SqlDbType.Int).Value = 0;
				int number = command.ExecuteNonQuery();
				connect.Close();
				return true;
			}
			catch
			{
				return false;
			}	
		}

		public bool Delete(Product item)
		{
			try
			{

				string query = _updateInsert;
				var connect = Connect();
				connect.Open();
				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@id", SqlDbType.Int).Value=item.Id;
				SqlParameter parameter = new SqlParameter("@hide", 1);

				command.Parameters.Add(parameter);


				command.ExecuteNonQuery();
				connect.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public List<Product> GetAll()
		{
			var products = new List<Product>();
			Read(_getAll, products, GetProducts);
			return products;
		}
		public List<Product> GetAllNotHide()
		{
			var products=new List<Product>();
			Read(_getAllNotHide, products, GetProducts);
			return products;
		}

		private void GetProducts(SqlDataReader reader, List<Product> products)
		{
			

			var id = reader.GetInt32(0);
			var productName = reader.GetString(1);
			var productTypeCode = reader.GetInt32(2);
			var price=reader.GetDecimal(3);
			var hide = reader.GetInt32(4);

			var product = new Product(id, productName, productTypeCode, price);

			products.Add(product);
		}
		private void GetProduct(SqlDataReader reader, Product product)
		{
			
			product.Id = reader.GetInt32(0);
			product.Name = reader.GetString(1);
			product.Type = reader.GetInt32(2);
			product.Price = reader.GetDecimal(3);

			
		}


		public Product GetT(int id)
		{
			var product = new Product();
		    Read(@$"SELECT Products.Id, Products.ProductName, TypeOfProducts.Type, Products.Price, Products.Hide 
                                   FROM Products INNER JOIN TypeOfProducts ON ProductTypeCode=TypeOfProducts.Id where Id={id}", product,GetProduct);
			return product;
		}

		public bool Update(Product item)
		{
			throw new NotImplementedException();
		}
	}
}
