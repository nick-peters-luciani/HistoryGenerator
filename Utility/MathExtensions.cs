namespace HistoryGenerator.Utility
{
	public class MathExtensions
	{
		public static float Lerp(float a, float b, float t)
		{
			return a + (b-a) * t;
		}

		public static double Lerp(double a, double b, double t)
		{
			return a + (b-a) * t;
		}

		public static float InverseLerp(float a, float b, float value)
		{
			return (value-a)/(b-a);
		}

		public static double InverseLerp(double a, double b, double value)
		{
			return (value-a)/(b-a);
		}
	}
}
