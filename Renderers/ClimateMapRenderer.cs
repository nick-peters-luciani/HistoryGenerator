using HistoryGenerator.Collections;
using HistoryGenerator.Core;
using HistoryGenerator.Model;
using HistoryGenerator.Utility;
using System;
using System.Drawing;

namespace HistoryGenerator.Systems
{
	[System(Dependencies = new Type[] {typeof(ClimateMapSystem), typeof(WaterMapRenderer)})]
	public class ClimateMapRenderer : SystemBase
	{
		public override void Execute(World world)
		{
			Map<Color> renderView = world.GetMap<Map<Color>>("RenderView");
			Map<ClimateData> climateMap = world.GetMap<Map<ClimateData>>("ClimateMap");
			Map<double> heightMap = world.GetMap<Map<double>>("HeightMap");
			Map<WaterType> waterMap = world.GetMap<Map<WaterType>>("WaterMap");

			for (int x=0; x<world.Width; x++)
			{
				for (int y=0; y<world.Height; y++)
				{
					Color? color = GetBiomeColor(climateMap[x,y].GetBiome());
					if (color != null && waterMap[x,y] == WaterType.None)
					{
						renderView[x,y] = color.Value.Scale(MathExtensions.Lerp(0.2f, 1, heightMap[x,y]));
					}
				}
			}
		}

		private Color? GetBiomeColor(string biome)
		{
			return biome switch
			{
				"Jungle" => Color.FromArgb(60, 100, 0),
				"Savanna" => Color.FromArgb(170, 150, 0),
				"Desert" => Color.FromArgb(234, 163, 39),
				"TemperateForest" => Color.FromArgb(0, 124, 24),
				"Grassland" => Color.FromArgb(135, 173, 0),
				"BorealForest" => Color.FromArgb(31, 158, 101),
				"Taiga" => Color.FromArgb(135, 219, 181),
				"Ice" => Color.White,
				_ => null,
			};
		}
	}

	public static class ColorUtil
	{
		public static Color Scale(this Color color, double v)
		{
			return Color.FromArgb(color.A, (int)(color.R*v), (int)(color.G*v), (int)(color.B*v));
		}
	}
}
