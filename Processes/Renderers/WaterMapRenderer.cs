using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.Processes.Generators;
using System.Drawing;

namespace HistoryGenerator.Processes.Renderers
{
	[Process(ProcessChain = "Render", Dependencies = new[] { typeof(HeightMapRenderer), typeof(WaterMapGenerator), typeof(HeightMapRenderer) })]
	public class WaterMapRenderer : RenderProcess
	{
		[ColorSetting]
		public Color DeepSeaColor { get; set; } = ColorTranslator.FromHtml("#147878");

		[ColorSetting]
		public Color ShallowSeaColor { get; set; } = ColorTranslator.FromHtml("#1e8c82");

		[ColorSetting]
		public Color FreshWaterColor { get; set; } = ColorTranslator.FromHtml("#46afa0");

		private double _seaLevel;

		public override void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics)
		{
			Map<double> heightMap = processUnit.Get<Map<double>>("HeightMap");
			Map<WaterType> waterMap = processUnit.Get<Map<WaterType>>("WaterMap");

			_seaLevel = processUnit.Get<double>("SeaLevel");

			ForEachPixel(bitmap, (x,y) => GetWaterColor(waterMap[x, y], heightMap[x, y]));
		}

		public Color GetWaterColor(WaterType waterType, double height)
		{
			if (waterType == WaterType.Sea)
			{
				if (height > _seaLevel - 0.075)
				{
					return ShallowSeaColor;
				}
				else
				{
					return DeepSeaColor;
				}
			}
			else if (waterType != WaterType.None)
			{
				return FreshWaterColor;
			}

			return Color.Transparent;
		}
	}
}
