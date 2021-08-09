using System.Collections.Generic;
using System.Drawing;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Models;
using HistoryGenerator.Processes.Generators;
using HistoryGenerator.Processes.Renderers;
using HistoryGenerator.Utility;

[Process(ProcessChain = "Render", Dependencies = new[] { typeof(ResourceGenerator), typeof(ClimateMapRenderer) })]
	public class ResourceRenderer : RenderProcess
	{
		public override void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics)
		{
			List<ResourceData> resources = processUnit.Get<List<ResourceData>>("Resources");

			foreach (ResourceData res in resources)
			{
				float l = res.Type.Name.Length * 5;
                Position tl = res.Location - new Position(res.Range, res.Range)/4;
				graphics.DrawEllipse(Pens.Cyan, tl.X, tl.Y, res.Range/2, res.Range/2);
				graphics.DrawString(res.Type.Name, SystemFonts.DefaultFont, Brushes.Black, res.Location.X - l / 2 - 1, res.Location.Y - 14);
				graphics.DrawString(res.Type.Name, SystemFonts.DefaultFont, Brushes.White, res.Location.X - l / 2,     res.Location.Y - 15);
			}
		}
	}