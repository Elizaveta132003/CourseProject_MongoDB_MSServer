using Domain.Core.Models;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Data.SQLServer
{
	internal class StockRepository : ReadByDataBase, IRepository<Stock>
	{
		private string _insertQuery= @$"INSERT INTO Stock (DateDeliveryToStock, IdProduct, Count,IdEmployee) 
										VALUES(@dateDelivery,@idProduct,@count,@idEmployee)";
		private string _getAllQuery = @"select Stock.Id, Stock.DateDeliveryToStock, Products.Id, Products.ProductName,
									  Product.ProductTypeCode, Products.Price, Stock.Count, Employees.Id, Employees.PhoneNumber, 
									  Employees.Password, Employees.LastName, Employees.FirstName, Employees.MiddleName, Employees.PositionCode,
									  Stock.DateShipmentFromStock from Stock inner join Products on Stock.IdProduct=Products.Id
									  inner join Employees on Stock.IdEmployee=Employees.Id inner join TypeOfProducts 
									  on Products.ProductTypeCode=TypeOfProducts.Id";
		private string _updateQuery= @$"UPDATE Stock SET DateDeliveryToStock=@dateOfReceipt, IdProduct=@idProduct, Count=@count,
                                        IdEmployee=@idEmployee, DateShipmentFromStock=@dateOfDispatch where Id=@id";

		public StockRepository(string connectionString) : base(connectionString)
		{
		}

		public bool Create(Stock item)
		{
			try
			{
				var dateDeliveryToStock = DateTime.Now;
				var idProduct = item.Product.Id;
				var count = item.Count;
				var idEmployee = item.Employee.Id;

				var connect = Connect();
				connect.Open();
				string query = _insertQuery;

				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@dateDelivery", SqlDbType.DateTime).Value = dateDeliveryToStock;
				command.Parameters.Add("@idProduct", SqlDbType.Int).Value = idProduct;
				command.Parameters.Add("@count", SqlDbType.Int).Value = count;
				command.Parameters.Add("@idEmployee", SqlDbType.Int).Value = idEmployee;
				int number = command.ExecuteNonQuery();
				connect.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Delete(Stock item)
		{
			throw new NotImplementedException();
			//TODO: Когда продукты забирают со склада и количество продуктов сводится к нулю
		}

		public List<Stock> GetAll()
		{
			List<Stock> stocks = new List<Stock>();

			Read(_getAllQuery, stocks, GetStocks);
				
			return stocks;
		}
		private void GetStocks(SqlDataReader reader, List<Stock> stocks)
		{
			Stock stock = new Stock();
			stock.Id = reader.GetInt32(0);
			stock.DateOfReceipt = reader.GetDateTime(1);
			stock.Product = new Product( reader.GetString(3), reader.GetInt32(4), reader.GetDecimal(5));
			stock.Count = reader.GetInt32(6);
			stock.Employee = new Employee(reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetInt32(13));
			stock.DateOfDispatch = reader.GetDateTime(14);

			stocks.Add(stock);
		}

		public Stock GetT(int id)
		{
			Stock stock = new Stock();

			Read(@$"select Stock.Id, Stock.DateDeliveryToStock, Products.Id, Products.ProductName, 
								   Product.ProductTypeCode, Products.Price, Stock.Count, Employees.Id, Employees.PhoneNumber,
								   Employees.Password, Employees.LastName, Employees.FirstName, Employees.MiddleName,
								   Employees.PositionCode, Stock.DateShipmentFromStock from Stock inner join Products
								   on Stock.IdProduct=Products.Id inner join Employees on Stock.IdEmployee=Employees.Id
								   inner join TypeOfProducts on Products.ProductTypeCode=TypeOfProducts.Id where Id={id}", stock, GetStock);
			return stock;
		}

		private void GetStock(SqlDataReader reader, Stock stock)
		{
			stock.Id = reader.GetInt32(0);
			stock.DateOfReceipt = reader.GetDateTime(1);
			stock.Product = new Product( reader.GetString(3), reader.GetInt32(4), reader.GetDecimal(5));
			stock.Count = reader.GetInt32(6);
			stock.Employee = new Employee(reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetInt32(13));
			stock.DateOfDispatch = reader.GetDateTime(14);
		}

		public bool Update(Stock item)
		{
			try
			{
				var id=item.Id;
				var dateOfReceipt = item.DateOfReceipt;
				var idProduct = item.Product.Id;
				var count = item.Count;
				var idEmployee = item.Employee.Id;
				var dateIfDispatch = item.DateOfDispatch;

				var connect = Connect();
				connect.Open();
				string query = _updateQuery;

				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
				command.Parameters.Add("@dateDelivery", SqlDbType.DateTime).Value = dateIfDispatch;
				command.Parameters.Add("@idProduct", SqlDbType.Int).Value = idProduct;
				command.Parameters.Add("@count", SqlDbType.Int).Value = count;
				command.Parameters.Add("@idEmployee", SqlDbType.Int).Value = idEmployee;
				command.Parameters.Add("@dateOfReceipt", SqlDbType.DateTime).Value = dateOfReceipt;
				int number = command.ExecuteNonQuery();
				connect.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
