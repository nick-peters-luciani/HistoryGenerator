using HistoryGenerator.Models.Resources;
using HistoryGenerator.Utility;

namespace HistoryGenerator.Models
{
	public class ResourceData
	{
		public ResourceType Type;
		public Position Location;
		public double Amount;
		public int Range;

		public double GrowthRate;
	}
}