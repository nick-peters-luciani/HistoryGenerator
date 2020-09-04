using HistoryGenerator.Collections;
using HistoryGenerator.Model;
using HistoryGenerator.Utility;
using System.Collections.Generic;
using System.Linq;

namespace HistoryGenerator.Generators
{
	public class WaterMapGeneratorSettings
	{
		public int Seed { get; set; }
		public double SeaLevel { get; set; }
		public double MinSpringLevel { get; set; }
		public int MaxSpringCount { get; set; }
		public int MaxPoolSize { get; set; }
	}

	public class WaterMapGenerator
	{	
		public WaterMapGeneratorSettings Settings { get; set; }

		private Map<double> HeightMap;
		private RNG _rng;

		public Map<WaterType> Generate()
		{
			HeightMap = Program.World.GetMap<Map<double>>("HeightMap");

			_rng = new RNG(Settings.Seed);

			Map<WaterType> waterMap = new Map<WaterType>(Program.World.Width, Program.World.Height);

			for (int x=0; x<waterMap.Width; x++)
			{
				for (int y=0; y<waterMap.Height; y++)
				{
					if (HeightMap[x,y] <= Settings.SeaLevel)
					{
						waterMap[x,y] = WaterType.Sea;
					}
				}
			}

			List<Position> springPoints = new List<Position>();
			for (int i=0; i<1000; i++)
			{
				if (springPoints.Count >= Settings.MaxSpringCount) break;

				int rx = _rng.Range(0, waterMap.Width);
				int ry = _rng.Range(0, waterMap.Height);
				if (HeightMap[rx,ry] > Settings.MinSpringLevel)
				{
					waterMap[rx,ry] = WaterType.Spring;
					Position position = new Position(rx,ry);
					springPoints.Add(position);
					Flow(waterMap, position);
				}
			}

			return waterMap;
		}

		private void Flow(Map<WaterType> waterMap, Position position)
		{
			Position lastPosition = Position.Zero;
			while (true)
			{
				double curHeight = HeightMap[position.X, position.Y];
				if (curHeight <= Settings.SeaLevel) break;
				
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
			for (r=1; r<=Settings.MaxPoolSize; r++)
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
