using System;
using System.Drawing;

namespace HistoryGenerator.Utility
{
	public static class ColorUtil
	{
		public static Color Multiply(this Color color, double value)
		{
			return Color.FromArgb(color.A, (byte)(color.R * value), (byte)(color.G * value), (byte)(color.B * value));
		}
	}
}
