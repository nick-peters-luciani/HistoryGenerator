using System.Collections.Generic;
using System.Linq;
using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.Utility;

namespace HistoryGenerator.Processes.Generators
{
	[Process(ProcessChain = "Generate", Dependencies = new[] { typeof(ClimateMapGenerator) })]
	public class ResourceGenerator : Process
	{
		[NumberSetting(MinValue = int.MinValue, MaxValue = int.MaxValue)]
		public int Seed { get; set; }
		
		public static readonly List<ResourceType> ResourceTypes = new List<ResourceType>()
		{
			new ResourceType {
				Name = "Deer",
				Origin = ResourceType.OriginType.Animal,
				Fulfillments = new[] { ResourceType.FulfillmentType.Food },
				Biomes = new[] { ClimateData.Biome.Grassland, ClimateData.Biome.TemperateForest, ClimateData.Biome.BorealForest }
			}
		};

		private RNG _rng;

		public override void Execute(ProcessUnit processUnit)
		{
			_rng = new RNG(Seed);
			
			List<ResourceData> resourceDatas = new List<ResourceData>();
			for (int i = 0; i < 10; i++)
			{
				Position position = FindLocation(processUnit, ResourceTypes[0].Biomes, resourceDatas);
				if (position.Equals(Position.Zero)) continue;

				ResourceData resourceData = new ResourceData
				{
					Type = ResourceTypes[0],
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