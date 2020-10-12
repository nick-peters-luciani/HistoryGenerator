using HistoryGenerator.Utility;
using System.Collections.Generic;

namespace HistoryGenerator.Extras
{
	public class NameGenerator
	{
		private readonly RNG _rng;
		private readonly HashSet<string> _generatedNames = new HashSet<string>();

		public NameGenerator(int seed = 0)
		{
			_rng = new RNG(seed);
		}

		public string Generate()
		{
			string name = "";
			for (int i=0; i<1000; i++)
			{
				name = _rng.ItemInList(Prefixes) + _rng.ItemInList(Suffixes);
				if (!_generatedNames.Contains(name)) break;
			}

			_generatedNames.Add(name);

			int r = _rng.Range(0,3);
			if (r == 1)
			{
				name += " " + _rng.ItemInList(Groups);
			}
			else if (r == 2)
			{
				name = _rng.ItemInList(Groups) + " of " + name;
			}

			return name;
		}

		public void Reset()
		{
			_generatedNames.Clear();
		}

		private static readonly string[] Groups = new string[]
		{
			"Kingdom", "Empire", "Faction", "Order", "Tribe"
		};

		private static readonly string[] Prefixes = new string[]
		{
			"Nor", "Che", "Wod", "Lak", "Ston", "Fir", "Riv", "Mal", "Lat", "Lon",
			"Mor", "Sol", "Mon", "Falk", "Mar", "Jul", "Lor", "Fen", "Eth", "Balm"
		};

		private static readonly string[] Suffixes = new string[]
		{
			"shire", "ia", "land", "set", "ford", "folk", "on", "en", "helm", "karth",
			"stead", "gen", "reath", "hold", "ude", "hal", "wood", "heart", "fel", "ora"
		};
	}
}
