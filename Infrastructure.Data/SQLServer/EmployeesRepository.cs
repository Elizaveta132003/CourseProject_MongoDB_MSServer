using Domain.Core.Models.Roles;
using Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Data.SQLServer
{
	public class EmployeesRepository : ReadByDataBase, IRepository<Employee>
	{
		private string _deleteQuery = $"UPDATE Employees SET Hide=@hide WHERE Id=@id";
		private string _insertQuery = @$"INSERT INTO Employees (PhoneNumber, Password, LastName, FirstName, MiddleName, PositionCode, Hide) 
                                        VALUES(@phoneNumber,@password,@lastName, @firstName, @middleName, @positionCode, @hide)";
		private string _getAllQuery = @"SELECT Employees.Id, Employees.PhoneNumber,Employees.Password, Employees.LastName, Employees.FirstName, Employees.MiddleName, 
                                       Employees.PositionCode, Employees.Hide, Employees.PositionCode FROM Employees INNER JOIN Positions ON PositionCode=Positions.Id";
		private string _updateQuery = @$"UPDATE Employees SET PhoneNumber=@phoneNumber, Password=@password, LastName=@lastName,
                                        FirstName=@firstName, MiddleName=@middleName, PositionCode=@positionCode, Hide=@hide where Id=@id";

		public EmployeesRepository(string connectionString) : base(connectionString)
		{
		}

		public bool Create(Employee item)
		{
			try
			{
				var phoneNumber = item.PhoneNumber;
				var password = item.Password;
				var name = item.FirstName;
				var lastName = item.LastName;
				var middleName = item.MiddleName;
				var indexPositions = item.PositionCode;

				var connect = Connect();
				connect.Open();
				string query = _insertQuery;

				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
				command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
				command.Parameters.Add("@lastName", SqlDbType.VarChar).Value = lastName;
				command.Parameters.Add("@firstName", SqlDbType.VarChar).Value = name;
				command.Parameters.Add("@middleName", SqlDbType.VarChar).Value = middleName;
				command.Parameters.Add("@positionCode", SqlDbType.Int).Value = indexPositions;
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

		public bool Delete(int id)
		{
			try
			{
				string query = _deleteQuery;
				var connect = Connect();
				connect.Open();
				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@id", SqlDbType.Int).Value = id;
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

		public List<Employee> GetAll()
		{
			List<Employee> _employees = new List<Employee>();
			Read(_getAllQuery, _employees, SelectAllEmployees);
			return _employees;

		}
		private void SelectAllEmployees(SqlDataReader reader, List<Employee> _employees)
		{
			Employee employee = null;

			if (reader.GetInt32(6) == 3)
			{
				employee = new Economist();
			}
			else if (reader.GetInt32(6) == 4)
			{
				employee = new CommodityExpert();
			}
			else if (reader.GetInt32(6) == 6)
			{
				employee = new Dispatcher();
			}
			else if (reader.GetInt32(6) == 7)
			{
				employee = new Administrator();
			}
			else if (reader.GetInt32(6) == 8)
			{
				employee = new Director();
			}

			employee.PhoneNumber = reader.GetString(1);
			employee.Password = reader.GetString(2);
			employee.LastName = reader.GetString(3);
			employee.FirstName = reader.GetString(4);
			employee.MiddleName = reader.GetString(5);
			employee.PositionCode = reader.GetInt32(6);
			employee.Id = reader.GetInt32(0);




			_employees.Add(employee);


		}

		public Employee GetT(int id)
		{
			Employee _employee = new Employee();
			Read($"SELECT * from Employees WHERE Id={id}", _employee, GetEmployee);
			return _employee;
		}
		private void GetEmployee(SqlDataReader reader, Employee _employee)
		{
			_employee.PhoneNumber = reader.GetString(1);
			_employee.Password = reader.GetString(2);
			_employee.LastName = reader.GetString(3);
			_employee.FirstName = reader.GetString(4);
			_employee.MiddleName = reader.GetString(5);
			_employee.PositionCode = reader.GetInt32(6);
			_employee.Id = reader.GetInt32(0);
		}

		public bool Update(Employee item)
		{
			try
			{
				var id = item.Id;
				var phoneNumber = item.PhoneNumber;
				var password = item.Password;
				var name = item.FirstName;
				var lastName = item.LastName;
				var middleName = item.MiddleName;
				var indexPositions = item.PositionCode;

				var connect = Connect();
				connect.Open();
				string query = _updateQuery;

				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
				command.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
				command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
				command.Parameters.Add("@lastName", SqlDbType.VarChar).Value = lastName;
				command.Parameters.Add("@firstName", SqlDbType.VarChar).Value = name;
				command.Parameters.Add("@middleName", SqlDbType.VarChar).Value = middleName;
				command.Parameters.Add("@positionCode", SqlDbType.Int).Value = indexPositions;
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
	}
}
