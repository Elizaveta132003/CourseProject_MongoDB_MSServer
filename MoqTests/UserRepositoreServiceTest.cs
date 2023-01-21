using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Implementations.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MoqTests
{
	public class UserRepositoreServiceTest
	{
		[Fact]
		public void Insert_InsertNewUser_True()
		{
			//Arrange

			var newClient = new Client()
			{
				NameOrganization = "OOO Bread",
				PhoneNumber = "+375299999999",
				Password = "12345678Tf"
			};

			var repositoryMock = new Mock<IRepository<Client>>();
			repositoryMock.Setup(repo => repo.Create(newClient)).Returns(true);

			var service = new UserRepositoryService(repositoryMock.Object);

			//Act

			var result = service.CreateUser(newClient);

			//Assert

			Assert.True(result.Data);
		}

		[Fact]
		public void GetAll_GetAllUsers_Equal()
		{
			//Arrange

			var collectionUsers = GetTestUsers();
			int count = collectionUsers.Count;

			var repositoryMock = new Mock<IRepository<Client>>();
			repositoryMock.Setup(repo => repo.GetAll()).Returns(collectionUsers);

			var service = new UserRepositoryService(repositoryMock.Object);

			//Act

			var result = service.GetAllUsers();

			//Assert

			Assert.Equal(count, result.Data.Count);
		}

		[Fact]
		public void GetById_GetUserById_Equal()
		{
			//Arrange

			var collectionUsers = GetTestUsers();
			int correctId = collectionUsers[0].Id;
			int incorrectId = collectionUsers.Max(i => i.Id) + 1;


			var repositoryMock = new Mock<IRepository<Client>>();
			repositoryMock.Setup(repo => repo.GetT(correctId)).Returns(new Client() { Id = 1 });

			var service = new UserRepositoryService(repositoryMock.Object);

			//Act

			var result = service.GetUserById(incorrectId);

			//Assert

			Assert.NotEqual(new OkResult(), result.StatusCode);
		}

		[Fact]
		public void Delete_DeleteUser_True()
		{
			//Arrange
			var collectionUsers = GetTestUsers();
			var user = collectionUsers[0];

			var repositoryMock = new Mock<IRepository<Client>>();
			repositoryMock.Setup(repo => repo.Delete(user.Id)).Returns(true);

			var service = new UserRepositoryService(repositoryMock.Object);

			//Act

			var result = service.DeleteUser(user.Id);

			//Assert
			Assert.True(result.Data);
		}

		[Fact]
		public void Update_UpdateUser_True()
		{
			//Arrange
			var collectionUsers = GetTestUsers();
			var user = collectionUsers[0];

			var repositoryMock = new Mock<IRepository<Client>>();
			repositoryMock.Setup(repo => repo.Update(user)).Returns(true);

			var service = new UserRepositoryService(repositoryMock.Object);

			//Act

			var result = service.UpdateUser(user);

			//Assert
			Assert.False(result.Data);
		}

		private List<Client> GetTestUsers()
			=> new List<Client>()
			{
				new Client()
				{
					Id=1,
					NameOrganization="Client1",
					PhoneNumber="+375444444444",
					Password="1111111Adfv"
				},
				new Client()
				{
					Id=2,
					NameOrganization="Client2",
					PhoneNumber="+375299999999",
					Password="2222222Hdbvc"
				},
				new Client()
				{
					Id=3,
					NameOrganization="Client3",
					PhoneNumber="+375295555555",
					Password="3333333Pabcys"
				}
			};
	}
}
