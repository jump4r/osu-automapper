using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osu_automapper
{
    /// <summary>
    /// Code to represent a HitSpinner object.
    /// </summary>
	class HitSpinner : HitObject
	{
		public int EndTime { get; set; }

		public HitSpinner() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">X-position</param>
        /// <param name="y">Y-position</param>
        /// <param name="time">Time (Millisecond)</param>
        /// <param name="hitType">HitObjectType</param>
        /// <param name="hitsound">HitObjectSoundType</param>
        /// <param name="endTime">End Time of the Spinner</param>
		public HitSpinner(int x, int y, int time, HitObjectType hitType, HitObjectSoundType hitsound, int endTime)
		{
			Position = new Vector2(x, y);
			this.Time = time;
			this.HitType = (int)hitType;
			this.HitSound = (int)hitsound;
			this.EndTime = endTime;
		}

        /// <summary>
        /// Overrite string to an .osu file readable format.
        /// </summary>
        /// <returns></returns>
		public override string SerializeForOsu()
		{
			return string.Format("{0},{1},{2},{3},{4},{5},0:0:0:0:",
				MathHelper.RoundToInt(Position.X), MathHelper.RoundToInt(Position.Y), Time,
				(int)HitType, (int)HitSound, EndTime);
		}

	}
}
