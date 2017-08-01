using System.Collections.Generic;
using CosmosDbTutorial.DataAccess;

namespace CosmosDbTutorial.Entities
{
	public class Person : BaseEntity
	{
		public string Name { get; set; }
		public List<Skill> Skills { get; set; }
	}
}