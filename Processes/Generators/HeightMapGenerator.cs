using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.Utility;
using System;

namespace HistoryGenerator.Processes.Generators
{
	[Process(ProcessChain = "Generate", Dependencies = new[] { typeof(WorldGenerator) })]
	public class HeightMapGenerator : Process
	{
		#region Settings
		[NumberSetting(MinValue = int.MinValue, MaxValue = int.MaxValue)]
		public int Seed { get; set; } = 0;

		[NumberSetting(MinValue = 0.01, MaxValue = 5000, Increment = 10, Decimals = 2)]
		public double Scale { get; set; } = 150;

		[NumberSetting(MinValue = 1, MaxValue = 10)]
		public int Octaves { get; set; } = 5;

		[NumberSetting(MinValue = 0, MaxValue = 5, Increment = 0.1, Decimals = 2)]
		public double Persistance { get; set; } = 0.6;

		[NumberSetting(MinValue = 0, MaxValue = 5, Increment = 0.1, Decimals = 2)]
		public double Lacunarity { get; set; } = 1.75;

		[BooleanSetting]
		public bool ClampEdge { get; set; } = true;

		[NumberSetting(MinValue = 1, MaxValue = 3, Increment = 0.1, Decimals = 2)]
		public double ClampScale { get; set; } = 1.5;
		#endregion

		public override void Execute(ProcessUnit processUnit)
		{
			World world = processUnit.Get<World>("World");
			Map<double> heightmap = new Map<double>(world.Width, world.Height);

			OpenSimplexNoise noiseSampler = new OpenSimplexNoise(Seed);

			double min = 0, max = 0;

			for (int x = 0; x < heightmap.Width; x++)
			{
				for (int y = 0; y < heightmap.Height; y++)
				{
					double frequency = 1;
					double amplitude = 1;
					double noiseHeight = 0;

					for (int i = 0; i < Octaves; i++)
					{
						double sampleX = (x - heightmap.Width / 2f + i * 1000) / Scale * frequency;
						double sampleY = (y - heightmap.Height / 2f + i * 1000) / Scale * frequency;
						double n = noiseSampler.EvaluateNormalized(sampleX, sampleY);
						n = Math.Clamp(n * 2 - 1, -1, 1);
						noiseHeight += n * amplitude;

						amplitude *= Persistance;
						frequency *= Lacunarity;
					}

					if (noiseHeight < min) min = noiseHeight;
					if (noiseHeight > max) max = noiseHeight;

					heightmap[x, y] = noiseHeight;
				}
			}

			for (int x = 0; x < heightmap.Width; x++)
			{
				for (int y = 0; y < heightmap.Height; y++)
				{
					double normalizedHeight = (heightmap[x, y] - min) / (max - min);

					if (ClampEdge)
					{
						normalizedHeight *= GetClampValue(x, y, heightmap.Width, heightmap.Height);
					}

					heightmap[x, y] = normalizedHeight;
				}
			}

			processUnit.Add("HeightMap", heightmap);
		}

		public double GetClampValue(int x, int y, int width, int height)
		{
			double nx = (double)x/width;
			double ny = (double)y/height;
			return Math.Cos((nx-0.5)*Math.PI/ClampScale) * Math.Cos((ny-0.5)*Math.PI/ClampScale);
		}
	}
}
