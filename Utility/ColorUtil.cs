﻿using System;
using System.Drawing;

namespace HistoryGenerator.Utility
{
	public static class ColorUtil
	{
		public enum BlendMode { Normal, Multiply, Screen, Overlay, Addition, Subtract, Difference }

		public static Color Blend(this Color color, Color otherColor, BlendMode blendMode)
		{
			if (otherColor.A == 0) return color;

			return blendMode switch
			{
				BlendMode.Normal => otherColor,
				BlendMode.Multiply => color.Multiply(otherColor),
				BlendMode.Screen => color.Screen(otherColor),
				BlendMode.Overlay => color.Overlay(otherColor),
				BlendMode.Addition => color.Add(otherColor),
				BlendMode.Subtract => color.Subtract(otherColor),
				BlendMode.Difference => color.Difference(otherColor),
				_ => otherColor,
			};
		}

		public static Color Multiply(this Color color, Color otherColor)
		{
			return Color.FromArgb(
				alpha: 255,
				red: (byte)(color.R * otherColor.R / 255.0),
				green: (byte)(color.G * otherColor.G / 255.0),
				blue: (byte)(color.B * otherColor.B / 255.0)
			);
		}

		public static Color Screen(this Color color, Color otherColor)
		{
			return Color.FromArgb(
				alpha: 255,
				red:   255 - (255-color.R)*(255-otherColor.R)/255,
				green: 255 - (255-color.G)*(255-otherColor.G)/255,
				blue:  255 - (255-color.B)*(255-otherColor.B)/255
			);
		}

		public static Color Overlay(this Color color, Color otherColor)
		{
			return Color.FromArgb(
				alpha: 255,
				red:   (color.R < 128) ? color.R*otherColor.R/128 : 255 - (255-color.R)*(255-otherColor.R)/128,
				green: (color.G < 128) ? color.G*otherColor.G/128 : 255 - (255-color.G)*(255-otherColor.G)/128,
				blue:  (color.B < 128) ? color.B*otherColor.B/128 : 255 - (255-color.B)*(255-otherColor.B)/128
			);
		}

		public static Color Add(this Color color, Color otherColor)
		{
			return Color.FromArgb(
				alpha: 255,
				red: Math.Min(color.R + otherColor.R, 255),
				green: Math.Min(color.G + otherColor.G, 255),
				blue: Math.Min(color.B + otherColor.B, 255)
			);
		}

		public static Color Subtract(this Color color, Color otherColor)
		{
			return Color.FromArgb(
				alpha: 255,
				red: Math.Max(color.R - otherColor.R, 0),
				green: Math.Max(color.G - otherColor.G, 0),
				blue: Math.Max(color.B - otherColor.B, 0)
			);
		}

		public static Color Difference(this Color color, Color otherColor)
		{
			return Color.FromArgb(
				alpha: 255,
				red: Math.Abs(color.R - otherColor.R),
				green: Math.Abs(color.G - otherColor.G),
				blue: Math.Abs(color.B - otherColor.B)
			);
		}

		public static Color Multiply(this Color color, double value)
		{
			return Color.FromArgb(color.A, (byte)(color.R * value), (byte)(color.G * value), (byte)(color.B * value));
		}
	}
}
