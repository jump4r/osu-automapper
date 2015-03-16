using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
	public static class RandomHelper
	{
		private static readonly Random rand = new Random();

		public static float NextFloat
		{
			get { return (float)rand.NextDouble(); }
		}

		public static int NextInteger
		{
			get { return rand.Next(); }
		}

		public static float Range(float min, float max)
		{
			return min + ((float)rand.NextDouble() * (max - min));
		}

		public static int Range(int min, int max)
		{
			return rand.Next(min, max);
		}

		public static Vector2 OnCircle(Vector2 center, float radius = 1f)
		{
			return new Vector2(RandomHelper.Range(-1f, 1f), RandomHelper.Range(-1, 1f)).Normalized * radius;
		}
	}
}
