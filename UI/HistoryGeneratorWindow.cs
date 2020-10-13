using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using HistoryGenerator.UI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HistoryGenerator
{
	public partial class HistoryGeneratorWindow : Form
	{
		private readonly Dictionary<string, IntPtr> _tabHandles = new Dictionary<string, IntPtr>();
		private readonly List<IntPtr> _settingsHandles = new List<IntPtr>();
		private Image _graphicsImage;

		public HistoryGeneratorWindow()
		{
			InitializeComponent();

			RenderView.Paint += RenderView_Paint;
			MenuItemSave.Click += MenuItemSave_Click;
			MenuItemLoad.Click += MenuItemLoad_Click;

			Program.Rerendered += OnRendered;
		}

		public IntPtr GetTab(string name)
		{
			return _tabHandles.ContainsKey(name) ? _tabHandles[name] : IntPtr.Zero;
		}

		public IntPtr AddTab(string name)
		{
			tabControl1.TabPages.Add(name);

			TabPage tabPage = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
			tabPage.AutoScroll = true;

			_tabHandles[name] = tabPage.Handle;

			return tabPage.Handle;
		}

		public IntPtr AddGroup(IntPtr tabHandle, string name)
		{
			TabPage tabPage = FromHandle(tabHandle) as TabPage;

			CollapsablePanel panel = new CollapsablePanel
			{
				Text = name,
				BackColor = Color.White,
				Dock = DockStyle.Top
			};

			TableLayoutPanel table = new TableLayoutPanel
			{
				Dock = DockStyle.Fill,
				AutoSize = true
			};
			table.ColumnCount = 2;
			table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.5f));
			table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.5f));

			panel.Content.Controls.Add(table);

			// Shift panels
			Control[] controls = new Control[tabPage.Controls.Count];
			tabPage.Controls.CopyTo(controls, 0);
			for (int i = 0; i < controls.Length; i++)
			{
				tabPage.Controls.SetChildIndex(controls[i], i + 1);
			}

			tabPage.Controls.Add(panel);
			tabPage.Controls.SetChildIndex(panel, 0);

			return table.Handle;
		}

		public IntPtr AddNumberSetting(IntPtr groupHandle, string name, double value, double minValue = 0, double maxValue = 1000, double increment = 1, int decimals = 0)
		{
			TableLayoutPanel table = FromHandle(groupHandle) as TableLayoutPanel;

			Label label = new Label
			{
				Dock = DockStyle.Fill,
				Text = name,
				TextAlign = ContentAlignment.MiddleLeft
			};
			table.Controls.Add(label);
			table.SetRow(label, table.RowCount);
			table.SetColumn(label, 0);

			NumericUpDown upDown = new NumericUpDown
			{
				Dock = DockStyle.Fill,
				Minimum = (decimal)minValue,
				Maximum = (decimal)maxValue,
				Value = (decimal)value,
				Increment = (decimal)increment,
				DecimalPlaces = decimals
			};
			table.Controls.Add(upDown);
			table.SetRow(upDown, table.RowCount);
			table.SetColumn(upDown, 1);

			table.RowCount++;

			_settingsHandles.Add(upDown.Handle);

			return upDown.Handle;
		}

		public IntPtr AddNumberSetting(IntPtr groupHandle, string name, object dataSource, string dataMember, double minValue = 0, double maxValue = 1000, double increment = 1, int decimals = 0)
		{
			double defaultValue = (double)Convert.ChangeType(dataSource.GetType().GetProperty(dataMember).GetValue(dataSource), typeof(double));
			IntPtr settingHandle = AddNumberSetting(groupHandle, name, defaultValue, minValue, maxValue, increment, decimals);
			Control setting = FromHandle(settingHandle);
			setting.DataBindings.Add(nameof(NumericUpDown.Value), dataSource, dataMember, false, DataSourceUpdateMode.OnValidation);
			return settingHandle;
		}

		public IntPtr AddBooleanSetting(IntPtr groupHandle, string name, bool value)
		{
			TableLayoutPanel table = FromHandle(groupHandle) as TableLayoutPanel;

			Label label = new Label
			{
				Dock = DockStyle.Fill,
				Text = name,
				TextAlign = ContentAlignment.MiddleLeft
			};
			table.Controls.Add(label);
			table.SetRow(label, table.RowCount);
			table.SetColumn(label, 0);

			CheckBox checkBox = new CheckBox
			{
				Dock = DockStyle.Fill,
				Checked = value
			};
			table.Controls.Add(checkBox);
			table.SetRow(checkBox, table.RowCount);
			table.SetColumn(checkBox, 1);

			table.RowCount++;

			_settingsHandles.Add(checkBox.Handle);

			return checkBox.Handle;
		}

		public IntPtr AddBooleanSetting(IntPtr groupHandle, string name, object dataSource, string dataMember)
		{
			bool defaultValue = (bool)Convert.ChangeType(dataSource.GetType().GetProperty(dataMember).GetValue(dataSource), typeof(bool));
			IntPtr settingHandle = AddBooleanSetting(groupHandle, name, defaultValue);
			Control setting = FromHandle(settingHandle);
			setting.DataBindings.Add(nameof(CheckBox.Checked), dataSource, dataMember);
			return settingHandle;
		}

		public IntPtr AddColorSetting(IntPtr groupHandle, string name, Color value)
		{
			TableLayoutPanel table = FromHandle(groupHandle) as TableLayoutPanel;

			Label label = new Label
			{
				Dock = DockStyle.Fill,
				Text = name,
				TextAlign = ContentAlignment.MiddleLeft
			};
			table.Controls.Add(label);
			table.SetRow(label, table.RowCount);
			table.SetColumn(label, 0);

			Button button = new Button
			{
				Dock = DockStyle.Fill,
				BackColor = value,
				Text = ColorTranslator.ToHtml(value)
			};
			button.DataBindings.Add(nameof(Button.Text), button, nameof(Button.BackColor));
			button.Click += OnColorSettingButtonClicked;
			table.Controls.Add(button);
			table.SetRow(button, table.RowCount);
			table.SetColumn(button, 1);

			table.RowCount++;

			_settingsHandles.Add(button.Handle);

			return button.Handle;
		}

		private void OnColorSettingButtonClicked(object sender, EventArgs e)
		{
			Button button = sender as Button;
			ColorDialog dialog = new ColorDialog
			{
				Color = button.BackColor,
				FullOpen = true
			};

			DialogResult result = dialog.ShowDialog();
			if (result.HasFlag(DialogResult.OK))
			{
				button.BackColor = dialog.Color;
			}
		}

		public IntPtr AddColorSetting(IntPtr groupHandle, string name, object dataSource, string dataMember)
		{
			Color defaultValue = (Color)Convert.ChangeType(dataSource.GetType().GetProperty(dataMember).GetValue(dataSource), typeof(Color));
			IntPtr settingHandle = AddColorSetting(groupHandle, name, defaultValue);
			Control setting = FromHandle(settingHandle);
			setting.DataBindings.Add(nameof(Button.BackColor), dataSource, dataMember);
			return settingHandle;
		}

		public void RefreshSettings()
		{
			foreach (IntPtr handle in _settingsHandles)
			{
				foreach (Binding binding in FromHandle(handle).DataBindings)
				{
					binding.ReadValue();
				}
			}
		}

		private void GenerateButton_Click(object sender, EventArgs e)
		{
			Program.Regenerate();
			Program.Render();
		}

		private void RenderButton_Click(object sender, EventArgs e)
		{
			Program.Render();
		}

		private void OnRendered(object sender, ProcessUnit processUnit)
		{
			_graphicsImage = processUnit.Get<Image>("RenderImage");

			World world = processUnit.Get<World>("World");
			RenderView.Size = new Size(world.Width, world.Height);

			Refresh();
		}

		private void RenderView_Paint(object sender, PaintEventArgs e)
		{
			if (_graphicsImage == null) return;
			e.Graphics.DrawImage(_graphicsImage, 0, 0);
		}

		private void MenuItemSave_Click(object sender, EventArgs e)
		{
			OpenSaveDialog();
		}

		private void MenuItemLoad_Click(object sender, EventArgs e)
		{
			OpenLoadDialog();
		}

		public void OpenSaveDialog()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				Filter = $"HistoryGenerator Settings|*{SettingsManager.SettingsFileExt}",
				Title = "Save Settings"
			};
			saveFileDialog.ShowDialog();

			if (saveFileDialog.FileName != "")
			{
				Program.SaveSettings(saveFileDialog.FileName);
			}
		}

		public void OpenLoadDialog()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = $"HistoryGenerator Settings|*{SettingsManager.SettingsFileExt}",
				Title = "Load Settings"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Focus();    // To remove focus from any settings
				Program.LoadSettings(openFileDialog.FileName);
			}
		}
	}
}
