using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    public class Square : BasePattern
    {
        private int noteDistance = (RandomHelper.NextFloat < 0.5) ? 50 : -50; // For testing;
        private List<HitCircle> hitCircles = new List<HitCircle>();

        public Square(Vector2 startPos, int time, HitObjectSoundType hitSound, Vector2 prevPoint, float mpb, Difficulty difficulty)
        {
            numObjects = 4;

            totalLength = difficulty.baseCircleTimestamp * numObjects + 1;
            subsetLength = difficulty.baseCircleTimestamp;

            hitCircles.Add(new HitCircle(startPos, (int)(time + (subsetLength * mpb)), (HitObjectType)HitType, (HitObjectSoundType)HitSound, prevPoint, 10f, false));
            hitCircles.Add(new HitCircle(new Vector2(startPos.X + noteDistance, startPos.Y),  (int)(time + (subsetLength * 2 * mpb)), (HitObjectType)HitType, (HitObjectSoundType)HitSound, prevPoint, 10f, true));
            hitCircles.Add(new HitCircle(new Vector2(startPos.X + noteDistance, startPos.Y + noteDistance), (int)(time + (subsetLength * 3 * mpb)), (HitObjectType)HitType, (HitObjectSoundType)HitSound, prevPoint, 10f, true));
            hitCircles.Add(new HitCircle(new Vector2(startPos.X - noteDistance, startPos.Y + noteDistance), (int)(time + (subsetLength * 4 * mpb)), (HitObjectType)HitType, (HitObjectSoundType)HitSound, prevPoint, 10f, true));
        }

        /// <summary>
        /// Overrite string to an .osu file readable format.
        /// </summary>
        /// <returns></returns>
        public override string SerializeForOsu()
        {
            string rtn = "";
            for (int i = 0; i < numObjects; i++)
            {
                rtn += string.Format("{0},{1},{2},{3},{4},0:0:0:0:", MathHelper.RoundToInt(hitCircles[i].Position.X), MathHelper.RoundToInt(hitCircles[i].Position.Y), hitCircles[i].Time, (int)HitType, (int)HitSound);
                if (i < numObjects - 1)
                {
                    rtn += "\n";
                }
            }
            return rtn;
        }

    }
}
