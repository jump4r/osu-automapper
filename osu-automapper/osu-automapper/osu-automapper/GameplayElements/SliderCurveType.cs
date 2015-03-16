using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
	public enum SliderCurveType
	{
		Linear = 0,
		Bezier = 1,
		PSpline = 2,
	}

	public static class SliderCurveTypeExtensions
	{
		public static char GetSliderChar(this SliderCurveType curveType)
		{
			switch (curveType)
			{
				case SliderCurveType.Linear:
					return 'L';
				case SliderCurveType.Bezier:
					return 'B';
				case SliderCurveType.PSpline:
					return 'P';
				default:
					return 'X';
			}
		}
	}
}
