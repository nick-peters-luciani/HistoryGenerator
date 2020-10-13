using System;
using System.Collections.Generic;

namespace HistoryGenerator.Core.Processing
{
	public class ProcessUnit
	{
		private readonly Dictionary<string, object> _data = new Dictionary<string, object>();

		public bool HasKey(string key) => _data.ContainsKey(key);

		public void Add(string key, object data)
		{
			if (HasKey(key)) throw new Exception($"Data key already exists: '{key}'");
			_data[key] = data;
		}

		public T Get<T>(string key)
		{
			if (!HasKey(key)) throw new Exception($"Data key does not exist: '{key}'");
			return (T)_data[key];
		}
	}
}
