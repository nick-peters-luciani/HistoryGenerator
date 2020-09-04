using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HistoryGenerator.UI.Controls
{
	public class CollapsablePanel : UserControl, INotifyPropertyChanged
	{
		public Label Header { get; private set; }
		public Panel Content { get; private set; }

		private bool _isCollapsed;
		public bool IsCollapsed
		{
			get { return _isCollapsed; }
			set {
				_isCollapsed = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCollapsed)));
				Invalidate();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public CollapsablePanel()
		{
			InitializeComponent();
		}

		public void InitializeComponent()
		{
			AutoSize = true;

			Header = new Label
			{
				Dock = DockStyle.Top,
				TextAlign = ContentAlignment.MiddleLeft,
				Padding = new Padding(2,0,0,0),
				Font = new Font(Label.DefaultFont, FontStyle.Bold),
				BackColor = Color.FromArgb(240, 240, 240)
			};
			Binding textBinding = new Binding(nameof(Label.Text), this, nameof(this.Text), true);
			textBinding.Format += OnLabelFormat;
			Header.DataBindings.Add(textBinding);
			Header.Click += OnLabelClick;

			Content = new Panel
			{
				Dock = DockStyle.Fill,
				AutoSize = true,
				MinimumSize = new Size(0, 20)
			};
			Binding binding = new Binding(nameof(Panel.Visible), this, nameof(this.IsCollapsed));
			binding.Format += SwitchBool;
			binding.Parse += SwitchBool;
			Content.DataBindings.Add(binding);

			Controls.Add(Content);
			Controls.Add(Header);
		}

		private void OnLabelFormat(object sender, ConvertEventArgs e)
		{
			string icon = IsCollapsed ? "▲" : "▼";
			e.Value = $"{icon}  {e.Value}";
		}

		private void OnLabelClick(object sender, System.EventArgs e)
		{
			IsCollapsed = !IsCollapsed;
		}

		private void SwitchBool(object sender, ConvertEventArgs e)
		{ 
			e.Value = !(bool)e.Value;
		}
	}
}
