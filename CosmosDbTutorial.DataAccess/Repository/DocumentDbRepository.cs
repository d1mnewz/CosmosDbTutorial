using System;
using System.Collections.Generic;
using System.Linq;
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
		private readonly Database _db;

		public DocumentDbRepository()
		{
			_client = new DocumentClient(new Uri(DocumentDbEndpointUrl), DocumentDbPrimaryKey);
			_db = _client.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseName }).Result;
		}
		public void Dispose()
		{
			throw new NotImplementedException();
		}
		public async Task<bool> AnyAsync<T>(string id) where T : BaseEntity
		{
			return _client.CreateDocumentQuery<T>
				(UriFactory.CreateDocumentCollectionUri(DatabaseName, typeof(T).Name))
				.Any(x => x.id == id);
		}

		public async Task<T> GetAsync<T>(string id) where T : BaseEntity
		{
			return _client.CreateDocumentQuery<T>
				(UriFactory.CreateDocumentCollectionUri(DatabaseName, typeof(T).Name))
				.First(x => x.id == id);
		}

		public async Task<IList<T>> GetAllAsync<T>() where T : BaseEntity
		{
			return _client.CreateDocumentQuery<T>
				(UriFactory.CreateDocumentCollectionUri(DatabaseName, typeof(T).Name))
				.ToList();
		}

		public async Task<List<T>> GetAsync<T>(IList<string> ids) where T : BaseEntity
		{
			return _client.CreateDocumentQuery<T>
				(UriFactory.CreateDocumentCollectionUri(DatabaseName, typeof(T).Name))
				.Where(x => ids.Contains(x.id)).ToList();
		}

		/// <summary>
		/// Insert entity to database even though collection for this entity wasn't created yet.
		/// </summary>
		/// <typeparam name="T">Type of entity to insert</typeparam>
		/// <param name="entity">Entity to insert</param>
		public async Task InsertAsync<T>(T entity) where T : BaseEntity
		{
			await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseName),
							new DocumentCollection { Id = typeof(T).Name });
			await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, typeof(T).Name), entity);
		}

		/// <summary>
		/// Insert entities to database even though collection for these entities was not created yet.
		/// </summary>
		/// <typeparam name="T">Type of entity to insert</typeparam>
		/// <param name="entities">Entities to insert</param>
		public async Task InsertAsync<T>(IEnumerable<T> entities) where T : BaseEntity
		{
			await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseName),
				new DocumentCollection { Id = typeof(T).Name });

			foreach (var entity in entities)
				await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, typeof(T).Name), entity);
		}
		public async Task UpsertAsync<T>(T entity) where T : BaseEntity
		{
			await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, typeof(T).Name), entity);
		}

		public async Task DeleteAsync<T>(string id) where T : BaseEntity
		{
			await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, typeof(T).Name, id.ToString()));

		}
	}
}