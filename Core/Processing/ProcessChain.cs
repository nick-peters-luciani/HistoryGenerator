using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace HistoryGenerator.Core.Processing
{
	public class ProcessChain
	{
		public readonly string Tag;

		private readonly List<Process> _processes = new List<Process>();

		public ProcessChain(string tag="")
		{
			Tag = tag;
		}

		public void Add(Process process)
		{
			_processes.Add(process);
		}

		public bool Has<T>() where T : Process
		{
			return _processes.Any(p => p is T);
		}

		public T Get<T>() where T : Process
		{
			return (T)_processes.First(p => p is T);
		}

		public IEnumerator<Process> GetEnumerator()
		{
			return _processes.GetEnumerator();
		}

		public void Execute(ProcessUnit processUnit)
		{
			Console.WriteLine($"Begin execute process chain: {Tag}");
			Stopwatch chainStopwatch = Stopwatch.StartNew();

			foreach (Process process in _processes)
			{
				Console.Write($"Executing process: {process.GetType().FullName}");
				Stopwatch processStopwatch = Stopwatch.StartNew();

				process.Execute(processUnit);

				processStopwatch.Stop();
				Console.WriteLine($"\tElapsed ms: {processStopwatch.ElapsedMilliseconds}");
			}

			chainStopwatch.Stop();
			Console.WriteLine($"End execute process chain: {Tag}\t Elapsed ms: {chainStopwatch.ElapsedMilliseconds}\n");
		}
	}
}
