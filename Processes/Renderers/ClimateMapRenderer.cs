using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.Processes.Generators;
using HistoryGenerator.Utility;
using System.Drawing;

namespace HistoryGenerator.Processes.Renderers
{
	[Process(ProcessChain = "Render", Dependencies = new[] { typeof(ClimateMapGenerator), typeof(WaterMapRenderer) })]
	public class ClimateMapRenderer : RenderProcess
	{
		#region Settings
		[BooleanSetting]
		public bool RenderHeight { get; set; } = true;

		[ColorSetting]
		public Color JungleColor { get; set; } = Color.FromArgb(60, 100, 0);

		[ColorSetting]
		public Color SavannaColor { get; set; } = Color.FromArgb(170, 150, 0);

		[ColorSetting]
		public Color DesertColor { get; set; } = Color.FromArgb(234, 163, 39);

		[ColorSetting]
		public Color TemperateForestColor { get; set; } = Color.FromArgb(0, 124, 24);

		[ColorSetting]
		public Color GrasslandColor { get; set; } = Color.FromArgb(135, 173, 0);

		[ColorSetting]
		public Color BorealForestColor { get; set; } = Color.FromArgb(31, 158, 101);

		[ColorSetting]
		public Color TaigaColor { get; set; } = Color.FromArgb(135, 219, 181);

		[ColorSetting]
		public Color IceColor { get; set; } = Color.FromArgb(255, 255, 255);
		#endregion

		public override void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics)
		{
			Map<ClimateData> climateMap = processUnit.Get<Map<ClimateData>>("ClimateMap");
			Map<double> heightMap = processUnit.Get<Map<double>>("HeightMap");
			Map<WaterType> waterMap = processUnit.Get<Map<WaterType>>("WaterMap");

			for (int x = 0; x < heightMap.Width; x++)
			{
				for (int y = 0; y < heightMap.Height; y++)
				{
					Color? color = GetBiomeColor(climateMap[x, y].GetBiome());
					if (color != null && waterMap[x, y] == WaterType.None)
					{
						if (RenderHeight)
						{
							color = color.Value.Multiply(MathExtensions.Lerp(0.2f, 1, heightMap[x, y]));
						}
						bitmap.SetPixel(x, y, color.Value);
					}
				}
			}
		}

		private Color? GetBiomeColor(string biome)
		{
			return biome switch
			{
				"Jungle" => JungleColor,
				"Savanna" => SavannaColor,
				"Desert" => DesertColor,
				"TemperateForest" => TemperateForestColor,
				"Grassland" => GrasslandColor,
				"BorealForest" => BorealForestColor,
				"Taiga" => TaigaColor,
				"Ice" => IceColor,
				_ => null,
			};
		}
	}
}
