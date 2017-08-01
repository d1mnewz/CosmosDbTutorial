using System.IO;
using Microsoft.Extensions.Configuration;

namespace CosmosDbTutorial.Configuration
{
	public class Config
	{
		private static IConfigurationRoot Configuration { get; set; }
		public static string DatabaseName => Configuration[nameof(DatabaseName)];
		public static string DocumentDbPrimaryKey => Configuration[nameof(DocumentDbPrimaryKey)];
		public static string DocumentDbEndpointUrl => Configuration[nameof(DocumentDbEndpointUrl)];
		public static string MongoDbConnectionString => Configuration[nameof(MongoDbConnectionString)];

		static Config()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");

			Configuration = builder.Build();
		}
	}
}
