using System;

namespace HistoryGenerator.Core.Settings
{
	public abstract class SettingAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class NumberSettingAttribute : SettingAttribute
	{
		public double MinValue;
		public double MaxValue;
		public double Increment;
		public int Decimals;
		public NumberSettingAttribute(double minValue = 0, double maxValue = 1000, double increment = 1, int decimals = 0)
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
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class ColorSettingAttribute : SettingAttribute
	{
	}
}
