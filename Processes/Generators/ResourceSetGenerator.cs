using System.Collections.Generic;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.Models.Resources;
using HistoryGenerator.Utility;

namespace HistoryGenerator.Processes.Generators
{
	[Process(ProcessChain = "Generate")]
	public class ResourceSetGenerator : Process
	{
		[NumberSetting(MinValue = int.MinValue, MaxValue = int.MaxValue)]
		public int Seed { get; set; }
		
		[NumberSetting(MinValue = 1, MaxValue = 100)]
		public int ResourceTypeCount { get; set; } = 10;

		private RNG _rng;
		
		public override void Execute(ProcessUnit processUnit)
		{
			_rng = new RNG(Seed);

			ResourceSet resourceSet = new ResourceSet();
			resourceSet.Types = new List<ResourceType>();

			for (int i=0; i<ResourceTypeCount; i++)
			{
				resourceSet.Types.Add(GenerateNewResourceType());
			}

			processUnit.Add("ResourceSet", resourceSet);
		}

		private int id = 0;
		private ResourceType GenerateNewResourceType()
		{
			ResourceType resourceType = new ResourceType();
			resourceType.Name = (id++).ToString(); 
			resourceType.Origin = _rng.Enum<ResourceType.OriginType>();
			resourceType.Fulfillments = _rng.Enums<ResourceType.FulfillmentType>(_rng.Range(1,3));
			resourceType.Biomes = _rng.Enums<ClimateData.Biome>(_rng.Range(1,2));
			return resourceType;
		}
	}
}