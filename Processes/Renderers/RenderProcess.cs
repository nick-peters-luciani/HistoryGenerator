using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
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
			// Using Bitmap.LockBits is more performant than GetPixel and SetPixel
			// See: https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bitmap.lockbits
			Rectangle bounds = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			BitmapData bitmapData = bitmap.LockBits(bounds, ImageLockMode.ReadWrite, bitmap.PixelFormat);
			IntPtr ptr = bitmapData.Scan0;
        	int[] rgbValues = new int[bitmap.Width * bitmap.Height];
			Marshal.Copy(ptr, rgbValues, 0, rgbValues.Length);

			for (int x=0; x<bitmap.Width; x++)
			{
				for (int y=0; y<bitmap.Height; y++)
				{
					int index = x + (y * bitmap.Width);
					Color color = Color.FromArgb(rgbValues[index]);
					color = color.Blend(colorFunc(x,y), BlendMode);
					rgbValues[index] = color.ToArgb();
				}
			}
			
			Marshal.Copy(rgbValues, 0, ptr, rgbValues.Length);
			bitmap.UnlockBits(bitmapData);
		}
	}
}
