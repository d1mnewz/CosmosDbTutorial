using System;
using MongoDB.Bson;

namespace CosmosDbTutorial.DataAccess
{
	public abstract class BaseEntity
	{
		// ReSharper disable once InconsistentNaming
		public readonly string id = ObjectId.GenerateNewId(DateTime.Now).ToString();
	}
}