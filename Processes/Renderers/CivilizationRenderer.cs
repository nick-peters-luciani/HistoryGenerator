using HistoryGenerator.Core.Processing;
using HistoryGenerator.Models;
using HistoryGenerator.Processes.Generators;
using HistoryGenerator.Processes.Renderers;
using System.Collections.Generic;
using System.Drawing;

namespace HistoryGenerator.Systems
{
	[Process(ProcessChain = "Render", Dependencies = new[] { typeof(CivilizationGenerator), typeof(ClimateMapRenderer) })]
	public class CivilizationRenderer : RenderProcess
	{
		private static Font Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);

		public override void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics)
		{
			List<Civilization> civilizations = processUnit.Get<List<Civilization>>("Civilizations");

			foreach (Civilization civ in civilizations)
			{
				float l = civ.Name.Length * 5;
				graphics.FillRectangle(Brushes.Yellow, civ.Location.X, civ.Location.Y, 4, 4);
				graphics.DrawString(civ.Name, Font, Brushes.Black, civ.Location.X - l / 2 - 1, civ.Location.Y - 14);
				graphics.DrawString(civ.Name, Font, Brushes.White, civ.Location.X - l / 2, civ.Location.Y - 15);
			}
		}
	}
}
