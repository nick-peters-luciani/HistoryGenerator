using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.Processes.Generators;
using System.Drawing;
using static HistoryGenerator.Models.ClimateData;

namespace HistoryGenerator.Processes.Renderers
{
	[Process(ProcessChain = "Render", Dependencies = new[] { typeof(ClimateMapGenerator), typeof(WaterMapRenderer) })]
	public class ClimateMapRenderer : RenderProcess
	{
		#region Settings
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
			Map<WaterType> waterMap = processUnit.Get<Map<WaterType>>("WaterMap");

			ForEachPixel(bitmap, (x,y) =>
			{
				if (waterMap[x, y] != WaterType.None) return Color.Transparent;
				return GetBiomeColor(climateMap[x,y].GetBiome());
			});
		}

		private Color GetBiomeColor(Biome biome)
		{
			return biome switch
			{
				Biome.Jungle => JungleColor,
				Biome.Savanna => SavannaColor,
				Biome.Desert => DesertColor,
				Biome.TemperateForest => TemperateForestColor,
				Biome.Grassland => GrasslandColor,
				Biome.BorealForest => BorealForestColor,
				Biome.Taiga => TaigaColor,
				Biome.Ice => IceColor,
				_ => Color.Transparent
			};
		}
	}
}
