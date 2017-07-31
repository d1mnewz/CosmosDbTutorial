namespace CosmosDbTutorial.DataAccess.Entities
{
	/// <summary>
	/// id field is automaticly generated on server-side.
	/// </summary>
	public abstract class BaseEntity
	{
		// ReSharper disable once InconsistentNaming
		public string id { get; set; }
	}
}