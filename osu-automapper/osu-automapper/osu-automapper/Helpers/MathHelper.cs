using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
	class MathHelper
	{
		public const double Rad2Deg = 180 / Math.PI;
		public const double Deg2Rad = Math.PI / 180;

		public static float Clamp(float value, float min, float max)
		{
			if (value < min) { return min; }

			if (value > max) { return max; }

			return value;
		}

		public static int Clamp(int value, int min, int max)
		{
			if (value < min) { return min; }

			if (value > max) { return max; }

			return value;
		}

		public static int RoundToInt(float value)
		{
			return (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);
		}
	}
}
