using System;
using System.Collections.Generic;

namespace HistoryGenerator.Utility
{
	public static class Grid
	{
		public static IEnumerable<Position> GetMooreNeighborEnumerator(Position p, int maxX=int.MaxValue, int maxY=int.MaxValue, int minX=0, int minY=0, int distance=1)
		{
			for (int x=p.X-distance; x<=p.X+distance; x++)
			{
				for (int y=p.Y-distance; y<=p.Y+distance; y++)
				{
					if ((x == p.X && y == p.Y) || (x < minX || x >= maxX || y < minY || y >= maxY)) continue;

					yield return new Position(x,y);
				}
			}
		}

		public static IEnumerable<Position> GetVonNeumanNeighborEnumerator(Position p, int maxX=int.MaxValue, int maxY=int.MaxValue, int minX=0, int minY=0, int distance=1)
		{
			for (int x=p.X-distance; x<=p.X+distance; x++)
			{
				for (int y=p.Y-distance; y<=p.Y+distance; y++)
				{
					if ((x == p.X && y == p.Y) || (x < minX || x >= maxX || y < minY || y >= maxY)) continue;

					int d = GetManhattanDistance(p, new Position(x,y));
					if (d > distance) continue;

					yield return new Position(x,y);
				}
			}
		}

		public static double GetEuclideanDistance(Position a, Position b)
		{
			return Math.Sqrt((a.X - b.X)*(a.X - b.X) + (a.Y - b.Y)*(a.Y - b.Y));
		}

		public static int GetManhattanDistance(Position a, Position b)
		{
			return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
		}

		public static int GetChebyshevDistance(Position a, Position b)
		{
			return Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
		}
	}
}
