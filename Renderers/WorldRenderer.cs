using HistoryGenerator.Core;
using System.Drawing;

namespace HistoryGenerator.Systems
{
	[System]
	public class WorldRenderer : SystemBase
	{
		public override void Execute(World world)
		{
			Image image = new Bitmap(world.Width, world.Height);
			Graphics graphics = Graphics.FromImage(image);
			world.AddData("GraphicsImage", image);
			world.AddData("Graphics", graphics);
		}
	}
}
