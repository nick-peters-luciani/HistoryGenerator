using System;

namespace HistoryGenerator.Models
{
	public struct ClimateData
	{
		public enum Biome { Jungle, Savanna, Desert, TemperateForest, Grassland, BorealForest, Taiga, Ice }

		private double _warmth;
		public double Warmth
		{ 
			get => _warmth;
			set => _warmth = Math.Clamp(value, 0, 1);
		}

		private double _wetness;
		public double Wetness
		{ 
			get => _wetness;
			set => _wetness = Math.Clamp(value, 0, 1);
		}

		public Biome GetBiome()
		{
			if (Warmth > 0.8)
			{
				if		(Wetness > 0.75) return Biome.Jungle;
				else if (Wetness > 0.10) return Biome.Savanna;
				else					 return Biome.Desert;
			}
			else if (Warmth > 0.3)
			{
				if		(Wetness > 0.50) return Biome.TemperateForest;
				else if (Wetness > 0.05) return Biome.Grassland;
				else					 return Biome.Desert;
			}
			else if (Warmth > 0.1)
			{
				if		(Wetness > 0.75) return Biome.BorealForest;
				//else if (Wetness > 0.33) return "Taiga";
				else					 return Biome.Taiga;
			}
			else
			{
				return Biome.Ice;
			}
		}
	}
}
