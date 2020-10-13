using HistoryGenerator.Collections;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Processes.Generators;
using HistoryGenerator.Utility;
using System;
using System.Drawing;

namespace HistoryGenerator.Processes.Renderers
{
	[Process(ProcessChain = "Render", Dependencies = new Type[] { typeof(HeightMapGenerator) })]
	public class HeightMapRenderer : RenderProcess
	{
		[ColorSetting]
		public Color Color { get; set; } = Color.White;

		public override void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics)
		{
			Map<double> heightMap = processUnit.Get<Map<double>>("HeightMap");

			ForEachPixel(bitmap, (x,y) =>
			{
				double scale = MathExtensions.Lerp(0.2f, 1, heightMap[x, y]);
				return Color.Multiply(scale);
			});
		}
	}
}
