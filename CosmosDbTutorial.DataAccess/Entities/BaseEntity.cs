using System;

namespace CosmosDbTutorial.DataAccess.Entities
{
	public abstract class BaseEntity
	{
		public Guid Id { get; set; }
	}
}