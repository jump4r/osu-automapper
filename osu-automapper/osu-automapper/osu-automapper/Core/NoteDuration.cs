using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    /// <summary>
    /// Static class for NoteDuration, Given 4/4 time signature.
    /// Whole: Represents 1 measure.
    /// Sixteenth: Represents 16th note (1/16 measure)
    /// </summary>
	public static class NoteDuration
	{
		public static readonly float Whole = 4f;
		public static readonly float Half = 2f;
		public static readonly float Quarter = 1f;
		public static readonly float Eighth = 0.5f;
		public static readonly float Sixteenth = 0.25f;
	}
}
