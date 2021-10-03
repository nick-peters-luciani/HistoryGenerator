using System.Collections.Generic;
using System.Linq;
using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.Models.Resources;
using HistoryGenerator.Utility;

namespace HistoryGenerator.Processes.Generators
{
	[Process(ProcessChain = "Generate", Dependencies = new[] { typeof(ClimateMapGenerator), typeof(ResourceSetGenerator) })]
	public class ResourceGenerator : Process
	{
		[NumberSetting(MinValue = int.MinValue, MaxValue = int.MaxValue)]
		public int Seed { get; set; }

		[NumberSetting(MinValue = 0, MaxValue = 50)]
		public int ResourceCount { get; set; } = 15;

		private RNG _rng;

		public override void Execute(ProcessUnit processUnit)
		{
			_rng = new RNG(Seed);
			
			ResourceSet resourceSet = processUnit.Get<ResourceSet>("ResourceSet");
			List<ResourceData> resourceDatas = new List<ResourceData>();
			for (int i=0; i<ResourceCount; i++)
			{
				ResourceType resourceType = _rng.ItemInList(resourceSet.Types);
				Position position = FindLocation(processUnit, resourceType.Biomes, resourceDatas);
				if (position.Equals(Position.Zero)) continue;

				ResourceData resourceData = new ResourceData
				{
					Type = resourceType,
					Location = position,
					Amount = _rng.Range(100, 1000),
					GrowthRate = _rng.Range(-10, 10),
					Range = _rng.Range(10, 50)
				};

				resourceDatas.Add(resourceData);
			}

			processUnit.Add("Resources", resourceDatas);
		}

		private Position FindLocation(ProcessUnit processUnit, ClimateData.Biome[] biomes, List<ResourceData> resourceDatas)
		{
			World world = processUnit.Get<World>("World");
			Map<WaterType> waterMap = processUnit.Get<Map<WaterType>>("WaterMap");
			Map<ClimateData> climateMap = processUnit.Get<Map<ClimateData>>("ClimateMap");

			for (int i = 0; i < 1000; i++)
			{
				Position position = new Position
				{
					X = _rng.Range(50, world.Width - 50),
					Y = _rng.Range(50, world.Height - 50)
				};

				WaterType waterType = waterMap[position.X, position.Y];
				if (waterType != WaterType.None) continue;
				
				ClimateData.Biome biome = climateMap[position.X, position.Y].GetBiome();
				if (!biomes.Contains(biome)) continue;

				bool tooClose = false;
				foreach (ResourceData resource in resourceDatas)
				{
					int d = Grid.GetChebyshevDistance(position, resource.Location);
					if (d < 50)
					{
						tooClose = true;
						break;
					}
				}

				if (tooClose) continue;

				return position;
			}

			return Position.Zero;
		}
	}
}