using HistoryGenerator.Collections;
using HistoryGenerator.Core;
using HistoryGenerator.Utility;
using System;
using System.Drawing;

namespace HistoryGenerator.Systems
{
	[System(Dependencies = new Type[] {typeof(HeightMapSystem)})]
	public class HeightMapRenderer : SystemBase
	{
		public override void Execute(World world)
		{
			Map<Color> renderView = world.GetData<Map<Color>>("RenderView");
			Map<double> heightMap = world.GetData<Map<double>>("HeightMap");
			
			for (int x=0; x<world.Width; x++)
			{
				for (int y=0; y<world.Height; y++)
				{
					int c = (int)(MathExtensions.Lerp(0.2f, 1, heightMap[x,y]) * 255);
					Color color = Color.FromArgb(c, c, c);
					renderView[x,y] = color;
				}
			}
		}
	}
}
