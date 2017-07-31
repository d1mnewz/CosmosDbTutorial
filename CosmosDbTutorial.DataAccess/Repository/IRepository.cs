using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CosmosDbTutorial.DataAccess.Entities;

namespace CosmosDbTutorial.DataAccess.Repository
{
	public interface IRepository : IDisposable
	{
		Task<bool> AnyAsync<T>(string id) where T : BaseEntity;
		Task<T> GetAsync<T>(string id) where T : BaseEntity;
		Task<IList<T>> GetAllAsync<T>() where T : BaseEntity;
		Task<List<T>> GetAsync<T>(IList<string> ids) where T : BaseEntity;
		Task InsertAsync<T>(T entity) where T : BaseEntity;
		Task InsertAsync<T>(IEnumerable<T> entities) where T : BaseEntity;
		Task UpsertAsync<T>(T entity) where T : BaseEntity;
		Task DeleteAsync<T>(string id) where T : BaseEntity;
	}
}