using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace HistoryGenerator
{
	public class Program
	{
		public static HistoryGeneratorWindow Window;

		public static event EventHandler<ProcessUnit> Regenerated;
		public static event EventHandler<ProcessUnit> Rerendered;

		private static ProcessUnit _processUnit;

		[STAThread]
		static void Main()
		{
			BuildWindow();

			Application.EnableVisualStyles();
			Application.Run(Window);
		}

		private static void BuildWindow()
		{
			Window = new HistoryGeneratorWindow();

			foreach (KeyValuePair<string, ProcessChain> item in ProcessLoader.LoadedProcessChains)
			{
				foreach (Process process in item.Value)
				{
					AddSettings(process, item.Key, process.GetType().Name);
				}
			}
		}

		private static void AddSettings(Process process, string tabName, string groupName)
		{
			IntPtr tabHandle = Window.GetTab(tabName);
			if (tabHandle == IntPtr.Zero)
			{
				tabHandle = Window.AddTab(tabName);
			}

			IntPtr groupHandle = Window.AddGroup(tabHandle, groupName);

			IEnumerable<KeyValuePair<PropertyInfo, SettingAttribute>> settings = SettingsManager.GetSettings(process);

			foreach (KeyValuePair<PropertyInfo, SettingAttribute> item in settings)
			{
				PropertyInfo pInfo = item.Key;
				SettingAttribute attribute = item.Value;

				if (attribute is NumberSettingAttribute a)
				{
					Window.AddNumberSetting(groupHandle, pInfo.Name, process, pInfo.Name, a.MinValue, a.MaxValue, a.Increment, a.Decimals);
				}
				else if (attribute is BooleanSettingAttribute)
				{
					Window.AddBooleanSetting(groupHandle, pInfo.Name, process, pInfo.Name);
				}
				else if (attribute is ColorSettingAttribute)
				{
					Window.AddColorSetting(groupHandle, pInfo.Name, process, pInfo.Name);
				}
				else if (attribute is EnumSettingAttribute)
				{
					Window.AddEnumSetting(groupHandle, pInfo.Name, process, pInfo.Name);
				}
			}
		}

		public static void Regenerate()
		{
			_processUnit = new ProcessUnit();
			ProcessLoader.LoadedProcessChains["Generate"].Execute(_processUnit);
			Regenerated?.Invoke(null, _processUnit);
		}

		public static void Render()
		{
			World world = _processUnit.Get<World>("World");

			if (!_processUnit.HasKey("RenderImage"))
			{
				Bitmap bitmap = new Bitmap(world.Width, world.Height);
				Graphics graphics = Graphics.FromImage(bitmap);
				_processUnit.Add("RenderImage", bitmap);
				_processUnit.Add("RenderGraphics", graphics);
			}

			_processUnit.Get<Graphics>("RenderGraphics").Clear(Color.White);

			ProcessLoader.LoadedProcessChains["Render"].Execute(_processUnit);
			Rerendered?.Invoke(null, _processUnit);
		}

		public static void SaveSettings(string filePath)
		{
			try
			{
				SettingsManager.SaveSettings(ProcessLoader.LoadedProcesses, filePath);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}\n\nDetails:\n\n{ex.StackTrace}");
			}
		}

		public static void LoadSettings(string filePath)
		{
			try
			{
				SettingsManager.LoadSettings(ProcessLoader.LoadedProcesses, filePath);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}\n\nDetails:\n\n{ex.StackTrace}");
				return;
			}

			Window.RefreshSettings();

			Regenerate();
			Render();
		}
	}
}
