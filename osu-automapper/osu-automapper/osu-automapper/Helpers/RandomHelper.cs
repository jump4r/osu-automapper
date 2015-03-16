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
	}
}
