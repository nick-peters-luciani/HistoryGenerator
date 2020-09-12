using System;
using System.Collections.Generic;

namespace HistoryGenerator.Core
{
	[Settings("World", "General", Priority=0)]
	public class WorldSettings
	{
		[NumberSetting(DefaultValue=800, MinValue=1, MaxValue=5000, Increment=100)]
		public int Width { get; set; }
		
		[NumberSetting(DefaultValue=800, MinValue=1, MaxValue=5000, Increment=100)]
		public int Height { get; set; }
	}

	public class World
	{
		public readonly int Width;
		public readonly int Height;

		private readonly Dictionary<string, object> _data = new Dictionary<string, object>();

		public World(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public bool HasData(string key) => _data.ContainsKey(key);

		public void AddData(string key, object data)
		{
			if (HasData(key)) throw new Exception($"Data key already exists in world: '{key}'");
			_data[key] = data;
		}
		
		public T GetData<T>(string key)
		{
			if (!HasData(key)) throw new Exception($"Data key does not exist in world: '{key}'");
			return (T)_data[key];
		}
	}
}
