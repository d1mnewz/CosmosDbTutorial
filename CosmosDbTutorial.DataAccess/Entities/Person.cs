using System.Collections.Generic;

namespace CosmosDbTutorial.DataAccess.Entities
{
	public class Person : BaseEntity
	{
		public string Name { get; set; }
		public List<Skill> Skills { get; set; }
	}
}