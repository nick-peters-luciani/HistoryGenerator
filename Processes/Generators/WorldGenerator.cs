using HistoryGenerator.Core.Processing;
using HistoryGenerator.Core.Settings;
using HistoryGenerator.Models;

namespace HistoryGenerator.Processes
{
	[Process(ProcessChain = "Generate")]
	public class WorldGenerator : Process
	{
		[NumberSetting(MinValue = 1, MaxValue = 5000, Increment = 100)]
		public int Width { get; set; } = 800;

		[NumberSetting(MinValue = 1, MaxValue = 5000, Increment = 100)]
		public int Height { get; set; } = 800;

		public override void Execute(ProcessUnit processUnit)
		{
			World world = new World()
			{
				Width = Width,
				Height = Height,
				Name = "World"
			};

			processUnit.Add("World", world);
		}
	}
}
