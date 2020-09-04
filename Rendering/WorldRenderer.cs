using HistoryGenerator.Collections;
using HistoryGenerator.Model;
using HistoryGenerator.Utility;
using System.Drawing;

namespace HistoryGenerator.Rendering
{
	public class WorldRenderer
	{
		private Bitmap _image;

		public void Paint(Graphics g)
		{
			if (_image == null) return;

			g.DrawImage(_image, 0, 0);
		}

		public void SetWorld(World world)
		{
			Map<double> heightMap = world.GetMap<Map<double>>("HeightMap");
			Map<WaterType> waterMap = world.GetMap<Map<WaterType>>("WaterMap");
			Map<ClimateData> climateMap = world.GetMap<Map<ClimateData>>("ClimateMap");

			double seaLevel = Program.WaterMapGenerator.Settings.SeaLevel;

			Bitmap bitmap = new Bitmap(world.Width, world.Height);

			for (int x=0; x<world.Width; x++)
			{
				for (int y=0; y<world.Height; y++)
				{
					/*int c = (int)Math.Round(heightMap[x, y] * 255);
					c = Math.Clamp(c, 0, 255);
					Color color = Color.FromArgb(c, c, c);
					bitmap.SetPixel(x, y, color);*/

					Color color;
					if (heightMap[x, y] > seaLevel+0.01)
					{
						string biome = climateMap[x,y].GetBiome();
						color = GetBiomeColor(biome).Scale(MathExtensions.Lerp(0.2f, 1, heightMap[x, y]));
					}
					else if (heightMap[x, y] > seaLevel)
					{
						color = Color.FromArgb(255, 211, 91);
					}
					else if (heightMap[x, y] > seaLevel-0.075)
					{
						color = Color.FromArgb(30, 140, 130);
					}
					else
					{
						color = Color.FromArgb(20, 120, 120);
					}

					WaterType waterType = waterMap[x,y];
					if (waterType != WaterType.None && waterType != WaterType.Sea)
					{
						color = Color.FromArgb(70, 175, 160);
					}
					bitmap.SetPixel(x, y, color);
				}
			}

			_image = bitmap;
		}

		/*public void SetClimateMap(ClimateMap climateMap)
		{
			Bitmap bitmap = new Bitmap(climateMap.Width, climateMap.Height);

			for (int x=0; x<climateMap.Width; x++)
			{
				for (int y=0; y<climateMap.Height; y++)
				{
					int r = (int)Math.Round(climateMap[x,y].Warmth * 255);
					int g = (int)Math.Round(climateMap[x,y].Wetness * 255);
					r = Math.Clamp(r, 0, 255);
					g = Math.Clamp(g, 0, 255);
					Color color = Color.FromArgb(r,g,0);
					bitmap.SetPixel(x, y, color);
				}
			}

			_heightMapImage = bitmap;
		}*/

		private Color GetBiomeColor(string biome)
		{
			return biome switch
			{
				"Jungle" => Color.FromArgb(60, 100, 0),
				"Savanna" => Color.FromArgb(170, 150, 0),
				"Desert" => Color.FromArgb(234, 163, 39),
				"TemperateForest" => Color.FromArgb(0, 124, 24),
				"Grassland" => Color.FromArgb(135, 173, 0),
				"BorealForest" => Color.FromArgb(31, 158, 101),
				"Taiga" => Color.FromArgb(135, 219, 181),
				"Ice" => Color.White,
				_ => Color.Black,
			};
		}

		private Color GetWaterColor(WaterType waterType)
		{
			return waterType switch
			{
				WaterType.Sea => Color.DarkBlue,
				WaterType.Spring => Color.Cyan,
				WaterType.Flow => Color.Blue,
				WaterType.Pool => Color.DarkBlue,
				WaterType.None => Color.Black,
				_ => Color.Black
			};
		}
	}

	public static class Temp
	{
		public static Color Scale(this Color color, double v)
		{
			return Color.FromArgb(color.A, (int)(color.R*v), (int)(color.G*v), (int)(color.B*v));
		}
	}
}
