using System;

namespace HistoryGenerator.Utility
{
	public struct Position
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static Position Zero => new Position(0,0);
		public static Position One => new Position(1,1);

		public override bool Equals(object obj)
		{
			return obj is Position position &&
				   X == position.X &&
				   Y == position.Y;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X,Y);
		}

		public static Position operator +(Position a, Position b) => new Position(a.X + b.X, a.Y + b.Y);
		public static Position operator -(Position a, Position b) => new Position(a.X - b.X, a.Y - b.Y);
		public static Position operator *(Position p, double d) => new Position((int)Math.Round(p.X * d), (int)Math.Round(p.Y * d));
		public static Position operator /(Position p, double d) => new Position((int)Math.Round(p.X / d), (int)Math.Round(p.Y / d));
	}
}
