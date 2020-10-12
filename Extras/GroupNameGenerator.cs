using HistoryGenerator.Utility;

namespace HistoryGenerator
{
	public class GroupNameGenerator
	{
		private static readonly string[] PrimarySubjects = new string[]
		{
			"Kingdom", "Club", "Pact", "Resistance", "Faction", "Force", "League", "Empire",
			"Union", "Guild", "Gang", "Order", "Alliance", "Band", "Tribe"
		};

		private static readonly string[] SecondarySubjects = new string[]
		{
			"Sun", "Moon", "Hearts", "Souls", "Power", "Harmony", "Men", "Women", "People", "Creatures",
			"Lands", "Seas", "Voices", "Secrets"
		};

		private static readonly string[] Adjectives = new string[]
		{
			"Sacred", "Banished", "Broken", "Forgotten", "Forsaken", "Legendary", "Strange", "Extraordinary",
			"Triumphant", "Stolen", "Promised", "Ancient", "Undying", "Terrible", "Devious", "Spiritual"
		};

		private static readonly string[] NameTemplates = new string[]
		{
			"The {a} {p}",
			"The {p} of {a} {s}",
			"{p} of the {a} {s}",
			"The {a} {s} {p}",
			"{a} {p} of {s}"
		};

		public string Generate(int seed=0)
		{
			RNG rng = new RNG(seed);

			string template = rng.ItemInList(NameTemplates);
			if (template.Contains("{p}"))
			{
				string subject = rng.ItemInList(PrimarySubjects);
				template = template.Replace("{p}", subject);
			}

			if (template.Contains("{s}"))
			{
				string subject = rng.ItemInList(SecondarySubjects);
				template = template.Replace("{s}", subject);
			}

			if (template.Contains("{a}"))
			{
				string adjective = rng.ItemInList(Adjectives);
				template = template.Replace("{a}", adjective);
			}

			return template;
		}
	}
}
