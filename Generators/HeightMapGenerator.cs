using HistoryGenerator.Collections;
using HistoryGenerator.Utility;
using System;

namespace HistoryGenerator.Generators
{
	public class HeightMapGeneratorSettings
	{
		public int Seed { get; set; }
		public double Scale { get; set; }
		public int Octaves { get; set; }
		public double Persistance { get; set; }
		public double Lacunarity { get; set; }
		public int Steps { get; set; }
	}

	public class HeightMapGenerator
	{
		public HeightMapGeneratorSettings Settings { get; set; }

		public Map<double> Generate()
		{
			OpenSimplexNoise noiseSampler = new OpenSimplexNoise(Settings.Seed);

			Map<double> heightMap = new Map<double>(Program.WorldSettings.Width, Program.WorldSettings.Height);

			double min=0, max=0;

			for (int x=0; x<heightMap.Width; x++)
			{
				for (int y=0; y<heightMap.Height; y++)
				{
					double frequency = 1;
					double amplitude = 1;
					double noiseHeight = 0;

					for (int i=0; i<Settings.Octaves; i++)
					{
						double sampleX = (x - heightMap.Width/2f  + i*1000) / Settings.Scale * frequency;
						double sampleY = (y - heightMap.Height/2f + i*1000) / Settings.Scale * frequency;
						double n = noiseSampler.EvaluateNormalized(sampleX, sampleY);
						n = Math.Clamp(n*2 - 1, -1, 1);
						noiseHeight += n * amplitude;

						amplitude *= Settings.Persistance;
						frequency *= Settings.Lacunarity;
					}

					if (noiseHeight < min) min = noiseHeight;
					if (noiseHeight > max) max = noiseHeight;

					heightMap[x,y] = noiseHeight;
				}
			}

			for (int x=0; x<heightMap.Width; x++)
			{
				for (int y=0; y<heightMap.Height; y++)
				{
					heightMap[x,y] = (heightMap[x,y] - min) / (max-min);
					
					if (Settings.Steps > 0)
					{
						heightMap[x,y] = Math.Round(heightMap[x,y]*Settings.Steps, 0)/Settings.Steps;
					}
				}
			}

			return heightMap;
		}
	}
}
