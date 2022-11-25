using Infrastructure.Data.SQLServer;
using Xunit;


namespace ConncectionTests
{
	public class ConnectionTestMSSQL
	{
		[Fact]
		public void TestMSServer_WithTheIncorrecrPath_ThrowWxception()
		{
			string connectionPath = "path";
			int id = -1;
			var repository = new UsersRepository(connectionPath);
			var result = repository.GetT(id);

			
			
		}

	}
}
