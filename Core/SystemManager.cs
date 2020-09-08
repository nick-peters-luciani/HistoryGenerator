using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace HistoryGenerator.Core
{
	public class SystemManager
	{
		public readonly List<SystemBase> Systems = new List<SystemBase>();
		public readonly List<object> Settings = new List<object>();

		public void Initialize()
		{
			Systems.Clear();
			Settings.Clear();

			Type[] assemblyTypes = GetType().Assembly.GetTypes();
			foreach(Type type in assemblyTypes)
			{
				if (type.GetCustomAttribute<SystemAttribute>() != null)
				{
					InitiliazeSystem(type);
				}
				else if (type.GetCustomAttribute<SettingsAttribute>() != null)
				{
					InitializeSettings(type);
				}
			}
		}
		
		public static void AssertIsSystemType(Type systemType)
		{
			if (!systemType.IsSubclassOf(typeof(SystemBase)))
			{
				throw new Exception($"Failed to load system '{systemType.FullName}'. Class does not extend from {nameof(SystemBase)}.");
			}

			if (systemType.GetCustomAttribute<SystemAttribute>() == null)
			{
				throw new Exception($"Failed to load system '{systemType.FullName}'. Class does not have {nameof(SystemAttribute)}).");
			}
		}

		private void InitiliazeSystem(Type systemType, List<Type> seenTypes=null)
		{
			AssertIsSystemType(systemType);

			var loadedSystemTypes = Systems.Select(s => s.GetType());
			if (loadedSystemTypes.Contains(systemType)) return;

			if (seenTypes == null)
			{
				seenTypes = new List<Type>();
			}
			else if (seenTypes.Contains(systemType))
			{
				throw new Exception($"Failed to load system '{systemType.FullName}'. Circular dependency detected.");
			}

			seenTypes.Add(systemType);

			SystemAttribute systemAttribute = systemType.GetCustomAttribute<SystemAttribute>();
			var unloadedDependencies = systemAttribute.Dependencies.Except(loadedSystemTypes);
			foreach (Type t in unloadedDependencies)
			{
				InitiliazeSystem(t, seenTypes);
			}
			
			SystemBase system = (SystemBase)Activator.CreateInstance(systemType);
			Systems.Add(system);
		}

		private void InitializeSettings(Type settingsType)
		{
			object settings = Activator.CreateInstance(settingsType);
			PropertyInfo[] settingProperties = settingsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pInfo in settingProperties)
			{
				foreach (Attribute attribute in pInfo.GetCustomAttributes())
				{
					if (attribute is NumberSettingAttribute a)
					{
						pInfo.SetValue(settings, Convert.ChangeType(a.DefaultValue, pInfo.PropertyType));
					}
				}
			}
			
			Settings.Add(settings);
		}

		public T GetSystem<T>() where T : SystemBase
		{
			return (T)Systems.FirstOrDefault(s => s is T);
		}

		public T GetSettings<T>()
		{
			return (T)Settings.FirstOrDefault(s => s is T);
		}
	}
}
