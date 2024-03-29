﻿using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HistoryGenerator.Processes.Generators
{
	[Process(ProcessChain = "Generate", Dependencies = new[] { typeof(HeightMapGenerator), typeof(WaterMapGenerator) })]
	public class ClimateMapGenerator : Process
	{
		[NumberSetting(MinValue = int.MinValue, MaxValue = int.MaxValue)]
		public int Seed { get; set; }

		[NumberSetting(MinValue = 0.01, MaxValue = 5000, Increment = 10, Decimals = 2)]
		public double Scale { get; set; } = 150;

		private Map<double> HeightMap;
		private Map<WaterType> WaterMap;

		private OpenSimplexNoise simplexNoise;

		public override void Execute(ProcessUnit processUnit)
		{
			simplexNoise = new OpenSimplexNoise(Seed);

			HeightMap = processUnit.Get<Map<double>>("HeightMap");
			WaterMap = processUnit.Get<Map<WaterType>>("WaterMap");

			Map<ClimateData> climateMap = new Map<ClimateData>(HeightMap.Width, HeightMap.Height);

			//GenerateWetnessMap();

			for (int x = 0; x < climateMap.Width; x++)
			{
				for (int y = 0; y < climateMap.Height; y++)
				{
					double warmth = GetWarmth(x, y);
					double wetness = GetWetness(x, y);

					climateMap[x, y] = new ClimateData
					{
						Warmth = warmth,
						Wetness = wetness
					};
				}
			}

			processUnit.Add("ClimateMap", climateMap);
		}

		private Map<double> _wetnessMap;
		private void GenerateWetnessMap()
		{
			_wetnessMap = new Map<double>(HeightMap.Width, HeightMap.Height);

			List<Position> waterPositions = new List<Position>();
			for (int x = 0; x < WaterMap.Width; x++)
			{
				for (int y = 0; y < WaterMap.Height; y++)
				{
					if (WaterMap[x, y] != WaterType.None && WaterMap[x, y] != WaterType.Sea)
					{
						waterPositions.Add(new Position(x, y));
					}
				}
			}

			for (int x = 0; x < WaterMap.Width; x++)
			{
				for (int y = 0; y < WaterMap.Height; y++)
				{
					if (WaterMap[x, y] != WaterType.None)
					{
						_wetnessMap[x, y] = 1;
					}
					else
					{
						List<int> waterDistances = waterPositions
							.Select(p => Grid.GetManhattanDistance(p, new Position(x, y)))
							.Where(d => d < 50)
							.ToList();

						double d = 50;
						if (waterDistances.Count > 0)
						{
							d = waterDistances
								.OrderBy(d => d)
								.First();
						}

						_wetnessMap[x, y] = Math.Clamp(1 - d / 50, 0, 1) * 0.3 + (1 - HeightMap[x, y]) * 0.7;
					}
				}
			}
		}

		private double GetWarmth(int x, int y)
		{
			return
				Math.Sin(y * Math.PI / HeightMap.Height) * 0.8 +        // Contribution of latitude
				(1 - HeightMap[x, y]) * 0.2;                                // Contribution of altitude
		}

		private double GetWetness(int x, int y)
		{
			/*if (WaterMap[x,y] == WaterType.Sea) return 1;

			int r;
			for (r=1; r<=5; r++)
			{
				bool found = false;
				foreach (Position n in Grid.GetMooreNeighborEnumerator(new Position(x,y), HeightMap.Width, HeightMap.Height, distance: r))
				{
					if (WaterMap[n.X, n.Y] != WaterType.None && WaterMap[n.X, n.Y] != WaterType.Sea)
					{
						found = true;
						break;
					}
				}
				if (found) break;
			}

			double waterRating = 1-((r-1)/15.0);

			return waterRating * 0.3 + (1-HeightMap[x,y]) * 0.7;*/

			//return _wetnessMap[x,y];


			double sampleX = (x - HeightMap.Width / 2f) / Scale;
			double sampleY = (y - HeightMap.Height / 2f) / Scale;
			return simplexNoise.EvaluateNormalized(sampleX, sampleY);
		}
	}
}
