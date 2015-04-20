using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
	public abstract class HitObject
	{
		protected float[] validAngles = { 0f, 30f, 60f, 90f, 120f, 150f, 180f, 210f, 240f, 270f, 300f, 330f };

		public Vector2 Position { get; set; }
		public int Time { get; set; }
		public int HitType { get; set; }
		public int HitSound { get; set; }

        /// <summary>
        /// Overrite string to an .osu file readable format.
        /// </summary>
        /// <returns></returns>
		public abstract string SerializeForOsu();
	}
}
