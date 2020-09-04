using System.Windows.Forms;

namespace HistoryGenerator.Rendering
{
	public class ProgramWindow : Form
	{
		public WorldRenderer WorldRenderer { get; private set; } = new WorldRenderer();

		private bool _isDirty;

		public ProgramWindow()
		{
			Text = "World Generator";
			Size = new System.Drawing.Size(500, 500);

			Paint += new PaintEventHandler(OnPaint);
		}

		private void OnPaint(object sender, PaintEventArgs e)
		{
			if (!_isDirty) return;
			
			WorldRenderer.Paint(e.Graphics);

			_isDirty = false;
		}

		public void Repaint()
		{
			_isDirty = true;
		}
	}
}
