using HistoryGenerator.Collections;
using HistoryGenerator.Core;
using HistoryGenerator.Models;
using System;
using System.Drawing;

namespace HistoryGenerator.Systems
{
	[Settings("Rendering", "Water")]
	public class WaterMapRendererSettings
	{
		[ColorSetting(DefaultValue="#147878")]
		public Color DeepSeaColor { get; set; }

		[ColorSetting(DefaultValue="#1e8c82")]
		public Color ShallowSeaColor { get; set; }

		[ColorSetting(DefaultValue="#46afa0")]
		public Color FreshWaterColor { get; set; }
	}

	[System(Dependencies = new Type[] { typeof(HeightMapSystem), typeof(WaterMapSystem), typeof(HeightMapRenderer) })]
	public class WaterMapRenderer : SystemBase
	{
		private double _seaLevel;
		private WaterMapRendererSettings Settings;

		public override void Execute(World world)
		{
			Map<Color> renderView = world.GetMap<Map<Color>>("RenderView");
			Map<double> heightMap = world.GetMap<Map<double>>("HeightMap");
			Map<WaterType> waterMap = world.GetMap<Map<WaterType>>("WaterMap");
			
			Settings = Program.SystemManager.GetSettings<WaterMapRendererSettings>();
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
					return Settings.ShallowSeaColor;
				}
				else
				{
					return Settings.DeepSeaColor;
				}
			}
			else if (waterType != WaterType.None)
			{
				return Settings.FreshWaterColor;
			}

			return null;
		}
	}
}
