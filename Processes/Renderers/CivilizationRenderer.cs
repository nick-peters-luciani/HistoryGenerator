using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
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
		[NumberSetting]
		public float PinSize { get; set; } = 5;

		[ColorSetting]
		public Color PinColor { get; set; } = Color.Yellow;

		[ColorSetting]
		public Color TextColor { get; set; } = Color.White;

		[BooleanSetting]
		public bool TextShadow { get; set; } = true;

		private static Font Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);

		public override void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics)
		{
			List<Civilization> civilizations = processUnit.Get<List<Civilization>>("Civilizations");

			Brush pinBrush = new SolidBrush(PinColor);
			Brush textBrush = new SolidBrush(TextColor);

			foreach (Civilization civ in civilizations)
			{
				graphics.FillRectangle(pinBrush, civ.Location.X, civ.Location.Y, PinSize, PinSize);
				
				float l = civ.Name.Length * 5;
				if (TextShadow)
				{
					graphics.DrawString(civ.Name, Font, Brushes.Black, civ.Location.X - l / 2 - 1, civ.Location.Y - 14);
				}
				
				graphics.DrawString(civ.Name, Font, textBrush, civ.Location.X - l / 2, civ.Location.Y - 15);
			}
		}
	}
}
