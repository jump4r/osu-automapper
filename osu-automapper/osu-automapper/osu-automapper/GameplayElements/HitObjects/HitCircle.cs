using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osu_automapper
{
    /// <summary>
    /// Representation for HitCirlce Game Object.
    /// </summary>
	class HitCircle : HitObject
	{
		public Vector2 PrevPos { get; set; }
		public float MaxDistance { get; set; }

		public HitCircle() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">Hit object position</param>
        /// <param name="time">Time (Millisecond)</param>
        /// <param name="hitType">Hit Object Type</param>
        /// <param name="hitSound">Hit Object Sound Type</param>
        /// <param name="prevPoint">Previous Point (Reference)</param>
        /// <param name="maxDistance">Max distance between HitObjects</param>
        /// <param name="patternSkip">(Default = false) Determine whether to skip Point Calculation if Cirlce is a part of a pattern.</param>
		public HitCircle(Vector2 position, int time, HitObjectType hitType, HitObjectSoundType hitSound, Vector2 prevPoint, float maxDistance, bool patternSkip = false)
		{
			Position = position;
			this.Time = time;
			this.HitType = (int)hitType;
			this.HitSound = (int)hitSound;

			this.PrevPos = prevPoint;
			this.MaxDistance = maxDistance;

            if (!patternSkip) // Skips caclulating the new point if circle is part of a pattern .
            {
                CalculateHitPoint();
            }
		}

        /// <summary>
        /// Calculate New Hit Point based off of Max Distance. 
        /// If the HitObject is part of a pattern, then skip this step
        /// </summary>
		private void CalculateHitPoint()
		{
			if (PrevPos.X == -1)
			{
				return;
			}

			var oldPos = Position;

			int angleIndex = RandomHelper.Range(0, validAngles.Length);
			float x = MaxDistance * (float)Math.Cos(validAngles[angleIndex]);
			float y = MaxDistance * (float)Math.Sin(validAngles[angleIndex]);

			Position = PrevPos + new Vector2(x, y);
			var playField = Beatmap.PlayField;

            // Corrections for if HitObject is offscreen.
			if (!playField.Contains(Position))
			{
				Vector2 heading = Position - oldPos;
				Vector2 edgeDir = playField.FindOverlapEdge(Position);

				Position = Vector2.Project(heading, edgeDir) + oldPos;

				// Hack for corners. Proper collision solving required if you want consistency with distances.
				Position = playField.Clamp(Position);
			}

			//while (X < 10 || X > 500 || Y < 10 || Y > 370 /*|| GetDistanceBetweenHitObjects() > maxDistance + 100 */)

		}

		//private int GetDistanceBetweenHitObjects()
		//{
		//	return (int)Math.Sqrt(Math.Pow((PrevPos.X - X), 2) + Math.Pow((PrevPos.Y - Y), 2));
		//}
		//funciton 

        /// <summary>
        /// Overrite string to an .osu file readable format.
        /// </summary>
        /// <returns></returns>
		public override string SerializeForOsu()
		{
			return string.Format("{0},{1},{2},{3},{4},0:0:0:0:", MathHelper.RoundToInt(Position.X), MathHelper.RoundToInt(Position.Y), Time, (int)HitType, (int)HitSound);
		}

	}
}
