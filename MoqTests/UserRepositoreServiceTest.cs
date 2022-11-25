using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Moq;
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

			var repositoryMockUser = new Mock<IRepository<Client>>();
			var repositoryMockEmployee = new Mock<IRepository<Employee>>();

			repositoryMockUser.Setup(x => x.Create(newClient)).Returns(true);



			//var service = new UserService(repositoryMockProduct.Object, repositoryMockUser.Object, repositpryMockOrder.Object);

			//Act


		}
	}
}
