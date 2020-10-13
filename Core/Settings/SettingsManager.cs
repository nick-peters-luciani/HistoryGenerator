using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HistoryGenerator.Core.Settings
{
	public static class SettingsManager
	{
		public static IEnumerable<KeyValuePair<PropertyInfo, SettingAttribute>> GetSettings(object settingsObject)
		{
			return settingsObject
				.GetType()
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.GroupBy(property => property.DeclaringType)
				.Reverse()
				.SelectMany(grouping => grouping)
				.SelectMany(property => property
					.GetCustomAttributes()
					.Where(attribute => attribute
						.GetType()
						.IsSubclassOf(typeof(SettingAttribute))
					)
					.Select(attribute => KeyValuePair.Create(property, (SettingAttribute)attribute))
				);
		}

		public const string SettingsFileExt = ".json";

		public static void SaveSettings(IEnumerable<object> settingsObjects, string filePath)
		{
			JObject settingsManifest = new JObject();

			foreach (object settingsObject in settingsObjects)
			{
				string groupName = settingsObject.GetType().Name;
				JObject settings = new JObject();

				foreach (KeyValuePair<PropertyInfo, SettingAttribute> item in GetSettings(settingsObject))
				{
					settings.Add(item.Key.Name, JToken.FromObject(item.Key.GetValue(settingsObject)));
				}

				settingsManifest.Add(groupName, settings);
			}

			FileStream fs = null;
			try
			{
				fs = File.Open(filePath, FileMode.OpenOrCreate);
				fs.Write(Encoding.UTF8.GetBytes(settingsManifest.ToString(Formatting.Indented)));
			}
			finally
			{
				fs?.Close();
			}
		}

		public static void LoadSettings(IEnumerable<object> settingsObjects, string filePath)
		{
			string content;
			using (StreamReader reader = new StreamReader(filePath))
			{
				content = reader.ReadToEnd();
			}

			JObject settingsManifest = JObject.Parse(content);

			foreach (object settingsObject in settingsObjects)
			{
				string groupName = settingsObject.GetType().Name;

				foreach (KeyValuePair<PropertyInfo, SettingAttribute> item in GetSettings(settingsObject))
				{
					object value = settingsManifest[groupName][item.Key.Name].ToObject(item.Key.PropertyType);
					item.Key.SetValue(settingsObject, value);
				}
			}
		}
	}
}
