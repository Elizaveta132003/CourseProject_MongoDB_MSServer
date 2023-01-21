using Infrastructure.Data.MongoDB;
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
	public class ConnectionTestMongoDB
	{

		[Fact]
		public void TestMongoDB_WithIncorrectPath_ThrowsException()
		{
			string connectionPath = "path";
			int id = -1;
			try
			{
				var employeeRepository = new EmployeesRepository(connectionPath);
				var employeeService = new EmployeeRepositoryService(employeeRepository);
				var result = employeeService.GetEmployeeById(id);
			}
			catch (Exception ex)
			{
				Assert.True(true);
				return;
			}

			Assert.True(false);
		}

		[Fact]
		public void TestMongoDB_WithCorrectPath_True()
		{
			//Arrange
			string connectionPath = "mongodb://localhost:27017";
			int id = -1;
			var employeeRepository = new EmployeesRepository(connectionPath);
			var employeeService = new EmployeeRepositoryService(employeeRepository);


			//Act
			DateTime startTime = DateTime.Now;
			var result = employeeService.GetEmployeeById(id);

			if (DateTime.Now.Subtract(startTime) >= new TimeSpan(0, 0, 0, 0, 10000))
			{
				Assert.True(false);
			}

			//Assert
			Assert.True(true);
		}
	}
}
