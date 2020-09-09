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

	public abstract class SettingAttribute : Attribute
	{
		public virtual object DefaultValue { get; set; }
		public SettingAttribute(object defaultValue)
		{
			DefaultValue = defaultValue;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class NumberSettingAttribute : SettingAttribute
	{
		public double MinValue;
		public double MaxValue;
		public double Increment;
		public int Decimals;
		public NumberSettingAttribute(double defaultValue=0, double minValue=0, double maxValue=1000, double increment=1, int decimals=0) : base(defaultValue)
		{
			MinValue = minValue;
			MaxValue = maxValue;
			Increment = increment;
			Decimals = decimals;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class BooleanSettingAttribute : SettingAttribute
	{
		public BooleanSettingAttribute(bool defaultValue=false) : base(defaultValue) { }
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class ColorSettingAttribute : SettingAttribute
	{
		private object _defaultValue;
		public override object DefaultValue
		{
			get => _defaultValue;
			set {
				if (value is string)
				{
					_defaultValue = System.Drawing.ColorTranslator.FromHtml((string)value);
				}
				else
				{
					_defaultValue = value;
				}
			}
		}

		public ColorSettingAttribute(string defaultValue="#FFFFFF") : base(System.Drawing.ColorTranslator.FromHtml(defaultValue)) { }
	}
}
