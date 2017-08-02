using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using MongoDB.Driver;
using static CosmosDbTutorial.Configuration.Config;

namespace CosmosDbTutorial.DataAccess.Repository
{
	public class MongoDbRepository : IRepository
	{
		private readonly IMongoClient _client;
		private readonly IMongoDatabase _db;

		public MongoDbRepository()
		{
			var settings = MongoClientSettings.FromUrl(
				new MongoUrl(MongoDbConnectionString)
			);
			settings.SslSettings =
				new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
			_client =  new MongoClient(settings);
			_db = _client.GetDatabase(DatabaseName);
		}

		public async Task<bool> AnyAsync<T>(string id) where T : BaseEntity
		{
			return await _db.GetCollection<T>(typeof(T).Name).Find(x => x.id == id).AnyAsync();
		}

		public async Task<T> GetAsync<T>(string id) where T : BaseEntity
		{
			return await _db.GetCollection<T>(typeof(T).Name).Find(x => x.id == id).FirstAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseEntity
		{
			return  _db.GetCollection<T>(typeof(T).Name).Find(_ => true).ToEnumerable();
		}

		public async Task<IEnumerable<T>> GetAsync<T>(IEnumerable<string> ids) where T : BaseEntity
		{
			return _db.GetCollection<T>(typeof(T).Name).Find(x => ids.Contains(x.id)).ToEnumerable();
		}

		public async Task InsertAsync<T>(T entity) where T : BaseEntity
		{
			await _db.GetCollection<T>(typeof(T).Name)
				.InsertOneAsync(entity, new InsertOneOptions { BypassDocumentValidation = true });
		}

		public async Task InsertAsync<T>(IEnumerable<T> entities) where T : BaseEntity
		{
			await _db.GetCollection<T>(typeof(T).Name)
				.InsertManyAsync(entities, new InsertManyOptions { BypassDocumentValidation = true, IsOrdered = false });
		}

		public async Task UpsertAsync<T>(T entity) where T : BaseEntity
		{
			await _db.GetCollection<T>(typeof(T).Name).UpdateOneAsync(x => x.id == entity.id,
				Builders<T>.Update.Set(p => p, entity),
				new UpdateOptions { IsUpsert = true });
		}

		public async Task DeleteAsync<T>(string id) where T : BaseEntity
		{
			await _db.GetCollection<T>(typeof(T).Name).DeleteOneAsync(x => x.id == id);
		}

		public void Dispose()
		{
			throw new System.NotImplementedException();
		}
	}
}
