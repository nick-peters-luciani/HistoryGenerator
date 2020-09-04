using HistoryGenerator.Collections;
using System;
using System.Collections.Generic;

namespace HistoryGenerator.Model
{
	public class WorldSettings
	{
		public int Width { get; set; }
		public int Height { get; set; }
	}

	public class World
	{
		public readonly int Width;
		public readonly int Height;

		private readonly Dictionary<string, IMap> _mapData = new Dictionary<string, IMap>();

		public World(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public bool HasMap(string key) => _mapData.ContainsKey(key);

		public void AddMap(string key, IMap map)
		{
			if (HasMap(key)) throw new Exception($"Map key already exists in world: '{key}'");
			if (map.Width != Width || map.Height != Height) throw new Exception("Map must match dimensions of world.");
			_mapData[key] = map;
		}
		
		public T GetMap<T>(string key) where T : IMap
		{
			if (!HasMap(key)) throw new Exception($"Map key does not exist in world: '{key}'");
			return (T)_mapData[key];
		}
	}
}
