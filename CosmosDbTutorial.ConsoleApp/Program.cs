using System;
using System.Threading.Tasks;
using CosmosDbTutorial.DataAccess.Entities;
using CosmosDbTutorial.DataAccess.Repository;
using MongoDB.Bson;

namespace CosmosDbTutorial.ConsoleApp
{
	public class Program
	{
		private static async Task Main()
		{
			var documentDbRepository = new DocumentDbRepository();
			var mongoDbRepository = new MongoDbRepository();

			await mongoDbRepository.InsertAsync(new Person
			{
				Name = "Dmytro"
			});
			await documentDbRepository.InsertAsync(new Person
			{
				Name = "Dmytro",
			});
			Console.WriteLine(
				(await documentDbRepository.GetAsync<Person>("5980646860ed5943c0f1845b")).Name);
			Console.WriteLine(
				(await mongoDbRepository.GetAsync<Person>("598064fcc5886c8d0408a07d")).Name);
		}
	}
}