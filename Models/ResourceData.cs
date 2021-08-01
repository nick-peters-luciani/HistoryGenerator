namespace HistoryGenerator.Models
{
	public enum ResourceType { Plant, Animal, Mineral }
	public enum ExtractionType { Huntable, Forageable, Farmable, Mineable }
	
	public struct ResourceData
	{
		public string Name;
		public double Amount;
		public double RecoveryRate;
		public ResourceType ResourceType;
		public ExtractionType ExtractionType;

		public ResourceData(string name, double amount, double recoveryRate, ResourceType resourceType, ExtractionType extractionType)
		{
			Name = name;
			Amount = amount;
			RecoveryRate = recoveryRate;
			ResourceType = resourceType;
			ExtractionType = extractionType;
		}
	}
}