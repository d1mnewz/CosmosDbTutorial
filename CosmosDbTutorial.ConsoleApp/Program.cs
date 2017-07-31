using System;
using System.Linq;
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
			Console.WriteLine((await documentDbRepository.GetAllAsync<Person>()).First().Name);
		}
	}
}