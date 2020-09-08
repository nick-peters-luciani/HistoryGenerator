using System;

namespace HistoryGenerator.Models
{
	public struct ClimateData
	{
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

		public string GetBiome()
		{
			if (Warmth > 0.8)
			{
				if		(Wetness > 0.75) return "Jungle";
				else if (Wetness > 0.10) return "Savanna";
				else					 return "Desert";
			}
			else if (Warmth > 0.3)
			{
				if		(Wetness > 0.50) return "TemperateForest";
				else if (Wetness > 0.05) return "Grassland";
				else					 return "Desert";
			}
			else if (Warmth > 0.1)
			{
				if		(Wetness > 0.75) return "BorealForest";
				//else if (Wetness > 0.33) return "Taiga";
				else					 return "Taiga";
			}
			else
			{
				return "Ice";
			}
		}
	}
}
