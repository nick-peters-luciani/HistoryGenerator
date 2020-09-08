using HistoryGenerator.Collections;
using HistoryGenerator.Core;
using HistoryGenerator.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using HistoryGenerator.Utility;

namespace HistoryGenerator
{
	public class Program
	{
		public static SystemManager SystemManager;
		public static HistoryGeneratorWindow Window;
		public static World World;

		public static event EventHandler Regenerated;

		[STAThread]
		static void Main()
		{
			SystemManager = new SystemManager();
			SystemManager.Initialize();

			BuildWindow();

			Application.EnableVisualStyles();
			Application.Run(Window);
		}

		private static void BuildWindow()
		{
			Window = new HistoryGeneratorWindow();

			var sortedSettings = SystemManager.Settings.OrderBy(s => s.GetType().GetCustomAttribute<SettingsAttribute>().Priority);
			foreach (object settings in sortedSettings)
			{
				AddSettings(settings);
			}
		}

		private static void AddSettings(object settings)
		{
			SettingsAttribute settingsAttribute = settings.GetType().GetCustomAttribute<SettingsAttribute>();

			IntPtr tabHandle = Window.GetTab(settingsAttribute.TabName);
			if (tabHandle == IntPtr.Zero)
			{
				tabHandle = Window.AddTab(settingsAttribute.TabName);
			}

			IntPtr groupHandle = Window.AddGroup(tabHandle, settingsAttribute.GroupName);

			PropertyInfo[] settingProperties = settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pInfo in settingProperties)
			{
				foreach (Attribute attribute in pInfo.GetCustomAttributes())
				{
					if (attribute is NumberSettingAttribute a)
					{
						Window.AddNumberSetting(groupHandle, pInfo.Name, settings, pInfo.Name, a.MinValue, a.MaxValue, a.Increment, a.Decimals);
					}
				}
			}
		}

		public static void Regenerate()
		{
			WorldSettings settings = SystemManager.GetSettings<WorldSettings>();
			World = new World(settings.Width, settings.Height);
			World.AddMap("RenderView", new Map<Color>(World.Width, World.Height));

			foreach (SystemBase system in SystemManager.Systems)
			{
				system.Execute(World);
			}

			Regenerated?.Invoke(null, EventArgs.Empty);
		}

		public const string SettingsFileExt = ".json";
		public static void SaveSettings(string filePath)
		{
			JObject settingsManifest = new JObject();

			foreach (object settings in SystemManager.Settings)
			{
				SettingsAttribute settingsAttribute = settings.GetType().GetCustomAttribute<SettingsAttribute>();
				settingsManifest.Add(settingsAttribute.Key, JObject.FromObject(settings));
			}

			FileStream fs = null;
			try
			{
				fs = File.Open(filePath, FileMode.OpenOrCreate);
				fs.Write(Encoding.UTF8.GetBytes(settingsManifest.ToString(Formatting.Indented)));
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}\n\nDetails:\n\n{ex.StackTrace}");
			}
			finally
			{
				fs?.Close();
			}
		}

		public static void LoadSettings(string filePath)
		{
			try
			{
				string content;
				using (StreamReader reader = new StreamReader(filePath))
				{
					content = reader.ReadToEnd();
				}

				JObject settingsManifest = JObject.Parse(content);

				foreach (object settings in SystemManager.Settings)
				{
					SettingsAttribute settingsAttribute = settings.GetType().GetCustomAttribute<SettingsAttribute>();
					if (settingsManifest.ContainsKey(settingsAttribute.Key))
					{
						object newSettings = settingsManifest[settingsAttribute.Key].ToObject(settings.GetType());
						MiscUtil.CopyProperties(newSettings, settings);
					}
				}

				Window.RefreshSettings();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}\n\nDetails:\n\n{ex.StackTrace}");
			}

			Regenerate();
		}
	}
}
