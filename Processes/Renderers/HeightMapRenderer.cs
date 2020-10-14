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
		[BooleanSetting]
		public bool OnlyRenderEdgeMask { get; set; }  = false;

		[ColorSetting]
		public Color Color { get; set; } = Color.White;

		public override void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics)
		{
			Map<double> heightMap = processUnit.Get<Map<double>>("HeightMap");

			HeightMapGenerator heightMapGenerator = ProcessLoader.GetLoadedProcess<HeightMapGenerator>();

			ForEachPixel(bitmap, (x,y) =>
			{
				if (OnlyRenderEdgeMask)
				{
					double clampValue = heightMapGenerator.GetClampValue(x, y, bitmap.Width, bitmap.Height);
					int c = (int)(clampValue * 255);
					return Color.FromArgb(255, c, c, c);
				}
				else
				{
					double scale = heightMap[x,y];
					return Color.Multiply(scale);
				}
			});
		}
	}
}
