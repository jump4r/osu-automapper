using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osu_automapper
{
	class HitSpinner : HitObject
	{
		public int EndTime { get; set; }

		public HitSpinner() { }
		public HitSpinner(int x, int y, int time, HitObjectType hitType, HitObjectSoundType hitsound, int endTime)
		{
			Position = new Vector2(x, y);
			this.Time = time;
			this.HitType = (int)hitType;
			this.HitSound = (int)hitsound;
			this.EndTime = endTime;
		}

		public override string SerializeForOsu()
		{
			return string.Format("{0},{1},{2},{3},{4},{5},0:0:0:0:",
				MathHelper.RoundToInt(Position.X), MathHelper.RoundToInt(Position.Y), Time,
				(int)HitType, (int)HitSound, EndTime);
		}

	}
}
