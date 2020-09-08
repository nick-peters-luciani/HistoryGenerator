using System;

namespace HistoryGenerator.Core
{
	[AttributeUsage(AttributeTargets.Class)]
	public class SystemAttribute : Attribute
	{
		public Type[] Dependencies;
		public SystemAttribute(Type[] dependencies=null)
		{
			Dependencies = dependencies ?? new Type[0];
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class SettingsAttribute : Attribute
	{
		public string TabName;
		public string GroupName;
		public int Priority;
		public SettingsAttribute(string tabName, string groupName, int priority=100)
		{
			TabName = tabName;
			GroupName = groupName;
			Priority = priority;
		}

		public string Key => $"{TabName}.{GroupName}";
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class NumberSettingAttribute : Attribute
	{
		public double DefaultValue;
		public double MinValue;
		public double MaxValue;
		public double Increment;
		public int Decimals;
		public NumberSettingAttribute(double defaultValue=0, double minValue=0, double maxValue=1000, double increment=1, int decimals=0)
		{
			DefaultValue = defaultValue;
			MinValue = minValue;
			MaxValue = maxValue;
			Increment = increment;
			Decimals = decimals;
		}
	}
}
