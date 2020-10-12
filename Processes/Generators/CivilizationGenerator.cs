using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Extras;
using HistoryGenerator.Models;
using HistoryGenerator.Utility;
using System.Collections.Generic;

namespace HistoryGenerator.Processes.Generators
{
	[Process(ProcessChain = "Generate", Dependencies = new[] { typeof(ClimateMapGenerator) })]
	public class CivilizationGenerator : Process
	{
		[NumberSetting(MinValue = int.MinValue, MaxValue = int.MaxValue)]
		public int Seed { get; set; } = 0;

		[NumberSetting(MinValue = 0, MaxValue = 50)]
		public int Count { get; set; } = 10;

		[NumberSetting(MinValue = 10, MaxValue = 1000, Increment = 5)]
		public int MinDistance { get; set; } = 50;

		private RNG _rng;

		public override void Execute(ProcessUnit processUnit)
		{
			_rng = new RNG(Seed);

			NameGenerator nameGenerator = new NameGenerator(Seed);

			List<Civilization> civilizations = new List<Civilization>();
			for (int i = 0; i < Count; i++)
			{
				Position position = FindLocation(processUnit, civilizations);
				if (position.Equals(Position.Zero)) continue;

				Civilization civilization = new Civilization
				{
					Name = nameGenerator.Generate(),
					Population = _rng.Range(50, 1000),
					Location = position,
					CharacterTraits = GenerateCharacterTraits()
				};

				civilizations.Add(civilization);
			}

			processUnit.Add("Civilizations", civilizations);
		}

		private Position FindLocation(ProcessUnit processUnit, List<Civilization> civilizations)
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

				bool tooClose = false;
				foreach (Civilization civilization in civilizations)
				{
					int d = Grid.GetChebyshevDistance(position, civilization.Location);
					if (d < MinDistance)
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

		private CharacterTraits GenerateCharacterTraits()
		{
			return new CharacterTraits
			{
				Openness = _rng.Range(0.0, 1.0),
				Conscientiousness = _rng.Range(0.0, 1.0),
				Extroversion = _rng.Range(0.0, 1.0),
				Agreeableness = _rng.Range(0.0, 1.0),
				Neuroticism = _rng.Range(0.0, 1.0)
			};
		}
	}
}
