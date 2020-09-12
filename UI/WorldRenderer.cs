using HistoryGenerator.Collections;
using HistoryGenerator.Core;
using System.Drawing;

namespace HistoryGenerator.Rendering
{
	public class WorldRenderer
	{
		private Bitmap _image;

		public void Render(World world)
		{
			Map<Color> renderView = world.GetData<Map<Color>>("RenderView");
			_image = new Bitmap(renderView.Width, renderView.Height);
			for (int x=0; x<_image.Width; x++)
			{
				for (int y=0; y<_image.Height; y++)
				{
					_image.SetPixel(x, y, renderView[x,y]);
				}
			}
		}

		public void Paint(Graphics g)
		{
			if (_image == null) return;

			g.DrawImage(_image, 0, 0);
		}
	}
}
