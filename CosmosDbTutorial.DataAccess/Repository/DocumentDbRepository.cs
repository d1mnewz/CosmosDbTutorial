using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CosmosDbTutorial.DataAccess.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using static CosmosDbTutorial.Configuration.Config;

namespace CosmosDbTutorial.DataAccess.Repository
{
	public class DocumentDbRepository : IRepository
	{
		private readonly DocumentClient _client;

		public DocumentDbRepository()
		{
			_client = new DocumentClient(new Uri(DocumentDbEndpointUrl), DocumentDbPrimaryKey);
			_client.CreateDatabaseIfNotExistsAsync(new Database {Id = DatabaseName}).Wait();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public async Task<bool> AnyAsync<T, TKey>(TKey id) where T : BaseEntity
		{
			throw new NotImplementedException();
		}

		public async Task<T> GetAsync<T, TKey>(TKey id) where T : BaseEntity
		{
			throw new NotImplementedException();
		}

		public async Task<IList<T>> GetAllAsync<T>() where T : BaseEntity
		{
			throw new NotImplementedException();
		}

		public async Task<List<T>> GetAsync<T, TKey>(IList<TKey> ids) where T : BaseEntity
		{
			throw new NotImplementedException();
		}

		public async Task InsertAsync<T>(T entity) where T : BaseEntity
		{
			await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseName),
				new DocumentCollection {Id = typeof(T).Name});
			await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, typeof(T).Name), entity);
		}

		public async Task InsertAsync<T>(IEnumerable<T> entities) where T : BaseEntity
		{
			await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseName),
				new DocumentCollection {Id = typeof(T).Name});

			foreach (var entity in entities)
				await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, typeof(T).Name), entity);
		}

		public async Task UpdateAsync<T>(T entity) where T : BaseEntity
		{
			throw new NotImplementedException();
		}

		public async Task DeleteAsync<T, TKey>(TKey id) where T : BaseEntity
		{
			throw new NotImplementedException();
		}
	}
}