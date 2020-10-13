using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using System;
using System.Drawing;
using static HistoryGenerator.Utility.ColorUtil;

namespace HistoryGenerator.Processes.Renderers
{
	public abstract class RenderProcess : Process
	{
		[BooleanSetting]
		public bool IsEnabled { get; set; } = true;

		[EnumSetting]
		public BlendMode BlendMode { get; set; } = BlendMode.Normal;

		public override void Execute(ProcessUnit processUnit)
		{
			if (!IsEnabled) return;

			Bitmap bitmap = processUnit.Get<Bitmap>("RenderImage");
			Graphics graphics = processUnit.Get<Graphics>("RenderGraphics");
			Render(processUnit, bitmap, graphics);
		}

		public abstract void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics);

		protected void ForEachPixel(Bitmap bitmap, Func<int, int, Color> colorFunc)
		{
			for (int x=0; x<bitmap.Width; x++)
			{
				for (int y=0; y<bitmap.Height; y++)
				{
					Color color = bitmap.GetPixel(x,y).Blend(colorFunc(x,y), BlendMode);
					bitmap.SetPixel(x, y, color);
				}
			}
		}
	}
}
