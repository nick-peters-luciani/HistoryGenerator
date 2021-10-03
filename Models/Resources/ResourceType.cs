namespace HistoryGenerator.Models.Resources
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
}