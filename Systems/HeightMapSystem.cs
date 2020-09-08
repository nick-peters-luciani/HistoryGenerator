using HistoryGenerator.Collections;
using HistoryGenerator.Core;
using HistoryGenerator.Model;
using HistoryGenerator.Utility;
using System;

namespace HistoryGenerator.Systems
{
	[Settings("World", "Heightmap")]
	public class HeightMapSystemSettings
	{
		[NumberSetting(MinValue=int.MinValue, MaxValue=int.MaxValue)]
		public int Seed { get; set; }

		[NumberSetting(DefaultValue=150, MinValue=0.01, MaxValue=5000, Increment=10, Decimals=2)]
		public double Scale { get; set; }

		[NumberSetting(DefaultValue=5, MinValue=1, MaxValue=10)]
		public int Octaves { get; set; }

		[NumberSetting(DefaultValue=0.6, MinValue=0, MaxValue=5, Increment=0.1, Decimals=2)]
		public double Persistance { get; set; }

		[NumberSetting(DefaultValue=1.75, MinValue=0, MaxValue=5, Increment=0.1, Decimals=2)]
		public double Lacunarity { get; set; }
	}

	[System]
	public class HeightMapSystem : SystemBase
	{
		public override void Execute(World world)
		{
			HeightMapSystemSettings settings = Program.SystemManager.GetSettings<HeightMapSystemSettings>();

			OpenSimplexNoise noiseSampler = new OpenSimplexNoise(settings.Seed);

			Map<double> heightMap = new Map<double>(world.Width, world.Height);

			double min=0, max=0;

			for (int x=0; x<heightMap.Width; x++)
			{
				for (int y=0; y<heightMap.Height; y++)
				{
					double frequency = 1;
					double amplitude = 1;
					double noiseHeight = 0;

					for (int i=0; i<settings.Octaves; i++)
					{
						double sampleX = (x - heightMap.Width/2f  + i*1000) / settings.Scale * frequency;
						double sampleY = (y - heightMap.Height/2f + i*1000) / settings.Scale * frequency;
						double n = noiseSampler.EvaluateNormalized(sampleX, sampleY);
						n = Math.Clamp(n*2 - 1, -1, 1);
						noiseHeight += n * amplitude;

						amplitude *= settings.Persistance;
						frequency *= settings.Lacunarity;
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
				}
			}

			world.AddMap("HeightMap", heightMap);
		}
	}
}
