using System.Collections.Generic;
using System.Linq;

namespace HistoryGenerator.Core.Processing
{
	public class ProcessChain
	{
		private readonly List<Process> _processes = new List<Process>();

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
			foreach (Process process in _processes)
			{
				process.Execute(processUnit);
			}
		}
	}
}
