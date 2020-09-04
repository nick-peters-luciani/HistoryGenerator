﻿using HistoryGenerator.Rendering;
using HistoryGenerator.UI.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HistoryGenerator
{
	public partial class HistoryGeneratorWindow : Form
	{
		private readonly WorldRenderer _worldRenderer;

		public HistoryGeneratorWindow()
		{
			_worldRenderer = new WorldRenderer();
			
			InitializeComponent();
			
			RenderView.Paint += RenderView_Paint;

			Program.Regenerated += OnRegenerated;
		}

		public IntPtr AddTab(string name)
		{
			tabControl1.TabPages.Add(name);

			TabPage tabPage = tabControl1.TabPages[tabControl1.TabPages.Count-1];
			tabPage.AutoScroll = true;

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
			for (int i=0; i<controls.Length; i++)
			{
				tabPage.Controls.SetChildIndex(controls[i], i+1);
			}

			tabPage.Controls.Add(panel);
			tabPage.Controls.SetChildIndex(panel, 0);

			return table.Handle;
		}

		public IntPtr AddNumberSetting(IntPtr groupHandle, string name, double value, double minValue=0, double maxValue=1000, double increment=1, int decimals=0)
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
				Value = (decimal)value,
				Minimum = (decimal)minValue,
				Maximum = (decimal)maxValue,
				Increment = (decimal)increment,
				DecimalPlaces = decimals
			};
			table.Controls.Add(upDown);
			table.SetRow(upDown, table.RowCount);
			table.SetColumn(upDown, 1);

			table.RowCount++;

			return upDown.Handle;
		}

		public IntPtr AddNumberSetting(IntPtr groupHandle, string name, object dataSource, string dataMember, double minValue=0, double maxValue=1000, double increment=1, int decimals=0)
		{
			IntPtr settingHandle = AddNumberSetting(groupHandle, name, 0, minValue, maxValue, increment, decimals);
			NumericUpDown setting = FromHandle(settingHandle) as NumericUpDown;
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

			return checkBox.Handle;
		}

		public T GetSettingValue<T>(IntPtr handle)
		{
			Control control = FromHandle(handle);

			object value = null;
			if (control is NumericUpDown upDown)
			{
				value = upDown.Value;
			}
			else if (control is CheckBox checkBox)
			{
				value = checkBox.Checked;
			}

			return value != null ? (T)Convert.ChangeType(value, typeof(T)) : default;
		}

		private void GenerateButton_Click(object sender, EventArgs e)
		{
			Program.Regenerate();
		}

		private void OnRegenerated(object sender, EventArgs eventArgs)
		{
			_worldRenderer.SetWorld(Program.World);
			RenderView.Size = new Size(Program.World.Width, Program.World.Height);
			Refresh();
		}

		private void RenderView_Paint(object sender, PaintEventArgs e)
		{
			_worldRenderer.Paint(e.Graphics);
		}
	}
}