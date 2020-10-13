using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using System.Drawing;

namespace HistoryGenerator.Processes.Renderers
{
	public abstract class RenderProcess : Process
	{
		[BooleanSetting]
		public bool IsEnabled { get; set; } = true;

		public override void Execute(ProcessUnit processUnit)
		{
			if (!IsEnabled) return;

			Bitmap bitmap = processUnit.Get<Bitmap>("RenderImage");
			Graphics graphics = processUnit.Get<Graphics>("RenderGraphics");
			Render(processUnit, bitmap, graphics);
		}

		public abstract void Render(ProcessUnit processUnit, Bitmap bitmap, Graphics graphics);
	}
}
