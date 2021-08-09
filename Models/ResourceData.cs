using HistoryGenerator.Utility;

namespace HistoryGenerator.Models
{
	public class ResourceType
	{
		public enum OriginType { Plant, Animal, Mineral }
		public enum FulfillmentType { Food, Shelter, Clothing, Fuel, Tools }

		public string Name;
		public OriginType Origin;
		public FulfillmentType[] Fulfillments;
		public ClimateData.Biome[] Biomes;
	}

	public class ResourceData
	{
		public ResourceType Type;
		public Position Location;
		public double Amount;
		public int Range;

		public double GrowthRate;
	}
}