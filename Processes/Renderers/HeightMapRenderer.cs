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

			for (int x = 0; x < heightMap.Width; x++)
			{
				for (int y = 0; y < heightMap.Height; y++)
				{
					int c = (int)(MathExtensions.Lerp(0.2f, 1, heightMap[x, y]) * 255);
					Color color = Color.FromArgb(c, c, c).Multiply(Color);
					bitmap.SetPixel(x, y, color);
				}
			}
		}
	}
}
