using System;
using MongoDB.Bson;

namespace CosmosDbTutorial.DataAccess.Entities
{
	public abstract class BaseEntity
	{
		// ReSharper disable once InconsistentNaming
		public readonly string id = ObjectId.GenerateNewId(DateTime.Now).ToString();

	}
}