using System;
using System.Collections.Generic;

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
	}
}
