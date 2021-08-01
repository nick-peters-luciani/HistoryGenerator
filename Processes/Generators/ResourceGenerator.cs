using System.Collections.Generic;
using HistoryGenerator.Core.Processing;
using HistoryGenerator.Models;

namespace HistoryGenerator.Processes.Generators
{
	[Process(ProcessChain = "Generate", Dependencies = new[] { typeof(ClimateMapGenerator) })]
	public class ResourceGenerator : Process
	{
		public static Dictionary<string, ResourceData> Resources = new Dictionary<string, ResourceData>()
		{
			{ "Deer", new ResourceData("Deer", 100, 10, ResourceType.Animal, ExtractionType.Huntable) }
		};

		public override void Execute(ProcessUnit processUnit)
		{
			
		}
	}
}