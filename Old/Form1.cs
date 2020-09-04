using HistoryGenerator.Rendering;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HistoryGenerator
{
	public partial class Form1 : Form
	{
		private HeightMapGenerator _heightMapGenerator;
		private WorldRenderer _worldRenderer = new WorldRenderer();

		private bool _isPanningCanvas; 
		private Point _panStartPosition;
		private Point _autoScrollPosition;
		private float _zoomLevel = 1;
		private Size _canvasSize;

		public Form1()
		{
			InitializeComponent();
		}

		public void SetHeightMapGenerator(HeightMapGenerator heightMapGenerator)
		{
			_heightMapGenerator = heightMapGenerator;

			HeightMapGeneratorSettings settings = _heightMapGenerator.Settings;
			HeightMapWidthUpDown.DataBindings.Add("Value", settings, nameof(settings.Width), false, DataSourceUpdateMode.OnPropertyChanged);
			HeightMapHeightUpDown.DataBindings.Add("Value", settings, nameof(settings.Height), false, DataSourceUpdateMode.OnPropertyChanged);
			HeightMapScaleUpDown.DataBindings.Add("Value", settings, nameof(settings.Scale), false, DataSourceUpdateMode.OnPropertyChanged);
			HeightMapOctavesUpDown.DataBindings.Add("Value", settings, nameof(settings.Octaves), false, DataSourceUpdateMode.OnPropertyChanged);
			HeightMapPersitanceUpDown.DataBindings.Add("Value", settings, nameof(settings.Persistance), false, DataSourceUpdateMode.OnPropertyChanged);
			HeightMapLacunarityUpDown.DataBindings.Add("Value", settings, nameof(settings.Lacunarity), false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void GenerateButton_Click(object sender, EventArgs e)
		{
			HeightMap heightMap = _heightMapGenerator.Generate();
			//_worldRenderer.SetHeightMap(heightMap);

			_canvasSize = new Size(heightMap.Width, heightMap.Height);
			Canvas.Size = _canvasSize;

			this.Refresh();
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.ScaleTransform(_zoomLevel, _zoomLevel);
			_worldRenderer.Paint(e.Graphics);
		}

		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Middle)
			{
				_isPanningCanvas = true;
				_panStartPosition = e.Location;
				_autoScrollPosition = new Point
				{
					X = Math.Abs(CanvasPanel.AutoScrollPosition.X),
					Y = Math.Abs(CanvasPanel.AutoScrollPosition.Y)
				};
			}
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (_isPanningCanvas)
			{
				Point deltaPoint = new Point
				{
					X = e.X - _panStartPosition.X,
					Y = e.Y - _panStartPosition.Y
				};
				CanvasPanel.AutoScrollPosition = new Point
				{
					X = Math.Clamp(_autoScrollPosition.X - deltaPoint.X, 0, Canvas.Width),
					Y = Math.Clamp(_autoScrollPosition.Y - deltaPoint.Y, 0, Canvas.Height)
				};
			}
		}

		private void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			_isPanningCanvas = false;
		}

		private void Canvas_MouseWheel(object sender, MouseEventArgs e)
		{
			_zoomLevel = Math.Clamp(_zoomLevel + e.Delta/1000f, 0.2f, 10);

			Canvas.Size = new Size((int)(_canvasSize.Width * _zoomLevel), (int)(_canvasSize.Height * _zoomLevel));
			CanvasPanel.AutoScrollPosition = new Point(0,0);

			this.Refresh();
		}
	}
}
