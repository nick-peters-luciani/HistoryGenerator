using HistoryGenerator.Collections;
using HistoryGenerator.Core;
using HistoryGenerator.Models;
using HistoryGenerator.Utility;
using System;
using System.Drawing;

namespace HistoryGenerator.Systems
{
	[Settings("Rendering", "Biomes")]
	public class ClimateMapRendererSettings
	{
		[BooleanSetting(DefaultValue=true)]
		public bool RenderHeight { get; set; }

		[ColorSetting(DefaultValue="60, 100, 0")]
		public Color JungleColor { get; set; }

		[ColorSetting(DefaultValue="170, 150, 0")]
		public Color SavannaColor { get; set; }

		[ColorSetting(DefaultValue="234, 163, 39")]
		public Color DesertColor { get; set; }

		[ColorSetting(DefaultValue="0, 124, 24")]
		public Color TemperateForestColor { get; set; }

		[ColorSetting(DefaultValue="135, 173, 0")]
		public Color GrasslandColor { get; set; }

		[ColorSetting(DefaultValue="31, 158, 101")]
		public Color BorealForestColor { get; set; }

		[ColorSetting(DefaultValue="135, 219, 181")]
		public Color TaigaColor { get; set; }

		[ColorSetting(DefaultValue="255, 255, 255")]
		public Color IceColor { get; set; }
	}

	[System(Dependencies = new Type[] {typeof(ClimateMapSystem), typeof(WaterMapRenderer)})]
	public class ClimateMapRenderer : SystemBase
	{
		private ClimateMapRendererSettings Settings;

		public override void Execute(World world)
		{
			Bitmap image = world.GetData<Bitmap>("GraphicsImage");
			Map<ClimateData> climateMap = world.GetData<Map<ClimateData>>("ClimateMap");
			Map<double> heightMap = world.GetData<Map<double>>("HeightMap");
			Map<WaterType> waterMap = world.GetData<Map<WaterType>>("WaterMap");

			Settings = Program.SystemManager.GetSettings<ClimateMapRendererSettings>();

			for (int x=0; x<world.Width; x++)
			{
				for (int y=0; y<world.Height; y++)
				{
					Color? color = GetBiomeColor(climateMap[x,y].GetBiome());
					if (color != null && waterMap[x,y] == WaterType.None)
					{
						if (Settings.RenderHeight)
						{
							color = color.Value.Scale(MathExtensions.Lerp(0.2f, 1, heightMap[x,y]));
						}
						image.SetPixel(x,y,color.Value);
					}
				}
			}
		}

		private Color? GetBiomeColor(string biome)
		{
			return biome switch
			{
				"Jungle" => Settings.JungleColor,
				"Savanna" => Settings.SavannaColor,
				"Desert" => Settings.DesertColor,
				"TemperateForest" => Settings.TemperateForestColor,
				"Grassland" => Settings.GrasslandColor,
				"BorealForest" => Settings.BorealForestColor,
				"Taiga" => Settings.TaigaColor,
				"Ice" => Settings.IceColor,
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
