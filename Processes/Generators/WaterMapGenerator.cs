using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.Utility;
using System.Collections.Generic;
using System.Linq;

namespace HistoryGenerator.Processes.Generators
{
	[Process(ProcessChain = "Generate", Dependencies = new[] { typeof(HeightMapGenerator) })]
	public class WaterMapGenerator : Process
	{
		#region Settings
		[NumberSetting(MinValue = int.MinValue, MaxValue = int.MaxValue)]
		public int Seed { get; set; }

		[NumberSetting(MinValue = 0, MaxValue = 1, Increment = 0.05, Decimals = 2)]
		public double SeaLevel { get; set; } = 0.4;

		[NumberSetting(MinValue = 0, MaxValue = 1, Increment = 0.05, Decimals = 2)]
		public double MinSpringLevel { get; set; } = 0.5;

		[NumberSetting(MinValue = 0, MaxValue = 100, Increment = 1)]
		public int MaxSpringCount { get; set; } = 30;

		[NumberSetting(MinValue = 0, MaxValue = 30, Increment = 1)]
		public int MaxPoolSize { get; set; } = 10;
		#endregion

		private Map<double> HeightMap;
		private RNG _rng;

		public override void Execute(ProcessUnit processUnit)
		{
			World world = processUnit.Get<World>("World");
			HeightMap = processUnit.Get<Map<double>>("HeightMap");

			_rng = new RNG(Seed);

			Map<WaterType> waterMap = new Map<WaterType>(world.Width, world.Height);

			for (int x = 0; x < waterMap.Width; x++)
			{
				for (int y = 0; y < waterMap.Height; y++)
				{
					if (HeightMap[x, y] <= SeaLevel)
					{
						waterMap[x, y] = WaterType.Sea;
					}
				}
			}

			List<Position> springPoints = new List<Position>();
			for (int i = 0; i < 1000; i++)
			{
				if (springPoints.Count >= MaxSpringCount) break;

				int rx = _rng.Range(0, waterMap.Width);
				int ry = _rng.Range(0, waterMap.Height);
				if (HeightMap[rx, ry] > MinSpringLevel)
				{
					waterMap[rx, ry] = WaterType.Spring;
					Position position = new Position(rx, ry);
					springPoints.Add(position);
					Flow(waterMap, position);
				}
			}

			processUnit.Add("SeaLevel", SeaLevel);
			processUnit.Add("WaterMap", waterMap);
		}

		private void Flow(Map<WaterType> waterMap, Position position)
		{
			Position lastPosition = Position.Zero;
			while (true)
			{
				double curHeight = HeightMap[position.X, position.Y];
				if (curHeight <= SeaLevel) break;

				/*double lowest = GetLowestNeighborValue(waterMap, position, out Position neighbor);
				if (lowest <= curHeight)
				{
					position = neighbor;
					if (waterMap[position.X, position.Y] == WaterType.None)
					{
						waterMap[position.X, position.Y] = WaterType.Flow;
					}
					else
					{
						break;
					}
				}
				else
				{
					Pool(waterMap, position);
					break;
				}*/

				List<Position> neighbors = Grid.GetMooreNeighborEnumerator(position, HeightMap.Width, HeightMap.Height).ToList();
				//if (neighbors.Any(n => waterMap[n.X, n.Y] == WaterType.Flow && !n.Equals(lastPosition))) break;

				neighbors = neighbors
					.Where(n => HeightMap[n.X, n.Y] <= curHeight && !n.Equals(lastPosition))
					.OrderBy(n => HeightMap[n.X, n.Y])
					.Take(3)
					.ToList();

				if (neighbors.Count > 0)
				{
					lastPosition = position;
					position = _rng.ItemInList(neighbors);
					waterMap[position.X, position.Y] = WaterType.Flow;
				}
				else
				{
					Pool(waterMap, position);
					break;
				}
			}
		}

		/*private double GetLowestNeighborValue(WaterMap waterMap, Position position, out Position lowestNeighbor)
		{
			double lowest = 1;
			lowestNeighbor = Position.Zero;
			foreach (Position n in Grid.GetMooreNeighborEnumerator(position, HeightMap.Width, HeightMap.Height))
			{
				if (HeightMap[n.X, n.Y] < lowest)
				{
					lowest = HeightMap[n.X, n.Y];
					lowestNeighbor = n;
				}
			}
			return lowest;
		}*/

		private void Pool(Map<WaterType> waterMap, Position position)
		{
			waterMap[position.X, position.Y] = WaterType.Pool;

			int r;
			for (r = 1; r <= MaxPoolSize; r++)
			{
				double lowest = 1;
				Position lowestNeighbor = Position.Zero;
				foreach (Position n in Grid.GetMooreNeighborEnumerator(position, HeightMap.Width, HeightMap.Height, distance: r))
				{
					if (HeightMap[n.X, n.Y] < lowest && waterMap[n.X, n.Y] == WaterType.None)
					{
						lowest = HeightMap[n.X, n.Y];
						lowestNeighbor = n;
					}
				}

				if (lowest <= HeightMap[position.X, position.Y])
				{
					Flow(waterMap, lowestNeighbor);
					break;
				}
				else
				{
					/*foreach (Position n in Grid.GetVonNeumanNeighborEnumerator(position, HeightMap.Width, HeightMap.Height, distance: r))
					{
						if (waterMap[n.X, n.Y] == WaterType.None)
						{
							waterMap[n.X, n.Y] = WaterType.Pool;
						}
					}*/
				}
			}

			double avg = 0;
			double count = 0;
			foreach (Position n in Grid.GetMooreNeighborEnumerator(position, HeightMap.Width, HeightMap.Height, distance: r))
			{
				avg += HeightMap[n.X, n.Y];
				count++;
			}
			avg /= count;

			foreach (Position n in Grid.GetMooreNeighborEnumerator(position, HeightMap.Width, HeightMap.Height, distance: r))
			{
				if (/*waterMap[n.X, n.Y] == WaterType.None && */HeightMap[n.X, n.Y] <= avg)
				{
					waterMap[n.X, n.Y] = WaterType.Pool;
				}
			}
		}
	}
}
