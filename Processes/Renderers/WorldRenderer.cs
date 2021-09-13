using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using System.Drawing;

namespace HistoryGenerator.Processes.Renderers
{
	[Process(ProcessChain = "Render")]
	public class WorldRenderer : RenderProcess
	{
		[ColorSetting]
		public Color Color { get; set; } = Color.White;

		public override void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics)
		{
			ForEachPixel(bitmap, (x,y) => Color);
		}
	}
}
