using Infrastructure.Data.SQLServer;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConnectionTests
{
	public class ConnectionTestMSSQL
	{
		[Fact]
		public void TestMSSqrver_WithIncorrectPath_ThrowsException()
		{
			//Arrange
			string connectionPath = "path";
			int id = -1;
			var employeeRepository = new EmployeesRepository(connectionPath);
			var userRepository = new UsersRepository(connectionPath);
			var administratorService = new AdministratorService(employeeRepository, userRepository);

			//Act
			var result = administratorService.GetAllEmployees();

			//Assert

			Assert.Equal((new NotFoundResult()).StatusCode, result.StatusCode.StatusCode);
		}

		[Fact]
		public void TestMSServer_WithCorrectPath_True()
		{
			//Arrange
			string connectionPath = @"Server=(localdb)\MSSQLLocalDB;Database=Kursovaya;Trusted_Connection=True;";
			int id = -1;
			var employeeRepository = new EmployeesRepository(connectionPath);
			var userRepository = new UsersRepository(connectionPath);
			var administratorService = new AdministratorService(employeeRepository, userRepository);

			//Act
			DateTime startTime = DateTime.Now;
			administratorService.GetAllEmployees();

			if (DateTime.Now.Subtract(startTime) >= new TimeSpan(0, 0, 0, 0, 10000))
			{
				Assert.True(false);
			}

			//Assert
			Assert.True(true);
		}
	}
}
