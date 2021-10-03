using System;
using System.Collections.Generic;
using System.Linq;

namespace HistoryGenerator.Utility
{
	public class RNG
	{
		private Random _random;

		private int _seed;
		public int Seed
		{
			get => _seed;
			set{
				_seed = value;
				_random = new Random(_seed);
			}
		}

		public RNG(int seed)
		{
			Seed = seed;
		}

		public RNG() : this(Environment.TickCount) { }

		public int Range(int minValue, int maxValue)
		{
			return _random.Next(minValue, maxValue);
		}

		public double Range(double minValue, double maxValue)
		{
			return _random.NextDouble() * (maxValue-minValue) + minValue;
		}

		public T ItemInList<T>(IList<T> list)
		{
			int i = Range(0, list.Count);
			return list[i];
		}

		public T Enum<T>()
		{
			if (!typeof(T).IsEnum) throw new ArgumentException("Type T must be an Enum!", "T");
			List<T> enumValues = typeof(T).GetEnumValues().OfType<T>().ToList();
			return ItemInList<T>(enumValues);
		}

		public T[] Enums<T>(int count)
		{
			if (!typeof(T).IsEnum) throw new ArgumentException("Type T must be an Enum!", "T");
			List<T> enumValues = typeof(T).GetEnumValues().OfType<T>().ToList();
			if (count > enumValues.Count) throw new ArgumentException("Count must be less than or equal to number of enum values.", "count");
			return enumValues.OrderBy(v => Guid.NewGuid()).Take(count).ToArray();
		}
	}
}
