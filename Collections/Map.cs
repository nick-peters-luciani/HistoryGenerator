namespace HistoryGenerator.Collections
{
	public interface IMap
	{
		public int Width { get; }
		public int Height { get; }
		public object this[int x, int y] { get; set; }
	}

	public class Map<T> : IMap
	{
		protected readonly T[,] _map;

		public int Width { get; private set; }
		public int Height { get; private set; }

		public Map(int width, int height, T defaultValue = default)
		{
			_map = new T[width, height];
			
			Width = width;
			Height = height;

			Fill(defaultValue);
		}

		public T this[int x, int y]
		{
			get => _map[x,y];
			set => _map[x,y] = value;
		}

		object IMap.this[int x, int y]
		{
			get => this[x,y];
			set => this[x,y] = (T)value;
		}

		public void Fill(T value)
		{
			for (int x=0; x<Width; x++)
			{
				for (int y=0; y<Height; y++)
				{
					_map[x,y] = value;
				}
			}
		}
	}
}
