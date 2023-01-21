using Infrastructure.Data.SQLServer;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations.Repositories;
using System;
using Xunit;

namespace ConnectionTests
{
	public class ConnectionTestMSSQL
	{
		[Fact]
		public void TestMSServer_WithIncorrectPath_ThrowsException()
		{
			//Arrange
			string connectionPath = "path";
			int id = -1;
			var employeeRepository = new EmployeesRepository(connectionPath);
			var employeeService = new EmployeeRepositoryService(employeeRepository);

			//Act
			var result = employeeService.GetEmployeeById(id);

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
			var employeeService = new EmployeeRepositoryService(employeeRepository);

			//Act
			DateTime startTime = DateTime.Now;
			employeeService.GetAllEmployee();

			if (DateTime.Now.Subtract(startTime) >= new TimeSpan(0, 0, 0, 0, 10000))
			{
				Assert.True(false);
			}

			//Assert
			Assert.True(true);
		}
	}
}
