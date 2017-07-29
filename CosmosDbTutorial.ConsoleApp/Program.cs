using System.Threading.Tasks;
using CosmosDbTutorial.DataAccess.Entities;
using CosmosDbTutorial.DataAccess.Repository;

namespace CosmosDbTutorial.ConsoleApp
{
	public class Program
	{
		private static async Task Main()
		{
			var documentDbRepository = new DocumentDbRepository();
			await documentDbRepository.InsertAsync(new Person());
		}
	}
}