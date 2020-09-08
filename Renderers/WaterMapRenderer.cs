using HistoryGenerator.Collections;
using HistoryGenerator.Core;
using HistoryGenerator.Models;
using System;
using System.Drawing;

namespace HistoryGenerator.Systems
{
	[System(Dependencies = new Type[] { typeof(HeightMapSystem), typeof(WaterMapSystem), typeof(HeightMapRenderer) })]
	public class WaterMapRenderer : SystemBase
	{
		private double _seaLevel;

		public override void Execute(World world)
		{
			Map<Color> renderView = world.GetMap<Map<Color>>("RenderView");
			Map<double> heightMap = world.GetMap<Map<double>>("HeightMap");
			Map<WaterType> waterMap = world.GetMap<Map<WaterType>>("WaterMap");
			
			_seaLevel = Program.SystemManager.GetSettings<WaterMapSystemSettings>().SeaLevel;

			for (int x=0; x<world.Width; x++)
			{
				for (int y=0; y<world.Height; y++)
				{
					Color? color = GetWaterColor(waterMap[x,y], heightMap[x,y]);
					if (color != null)
					{
						renderView[x,y] = color.Value;
					}
				}
			}
		}

		public Color? GetWaterColor(WaterType waterType, double height)
		{
			if (waterType == WaterType.Sea)
			{
				if (height > _seaLevel-0.075)
				{
					return Color.FromArgb(30, 140, 130);
				}
				else
				{
					return Color.FromArgb(20, 120, 120);
				}
			}
			else if (waterType != WaterType.None)
			{
				return Color.FromArgb(70, 175, 160);
			}

			return null;
		}
	}
}
