using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    /// <summary>
    /// Triple pattern class, inherited from the BasePatern
    /// 3 16th-notes stacked in a slightly offset pattern. 
    /// </summary>
    class Triple : BasePattern
    {
        int xOffset = RandomHelper.Range(-3, 3);
        int yOffset = RandomHelper.Range(-3, 3);

        List<HitCircle> hitCircles = new List<HitCircle>();

        public Triple() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startPos">Start Position</param>
        /// <param name="time">Time (Millisecond)</param>
        /// <param name="hitSound">HitObjectSound</param>
        /// <param name="prevPoint">Previous Point</param>
        /// <param name="mpb">Milliseconds per beat</param>
        public Triple(Vector2 startPos, int time, HitObjectSoundType hitSound, Vector2 prevPoint, float mpb, Difficulty difficulty) // Simplest pattern, let's implement this first.
        {
            totalLength = difficulty.baseCircleTimestamp * 2.0f; // NoteDuration.Quarter;
            subsetLength = difficulty.baseCircleTimestamp / 2.0f; // NoteDuration.Sixteenth;

            this.mpb = mpb;

            numObjects = 3;

            this.HitType = (int)HitObjectType.Normal;
            this.HitSound = (int)hitSound;

            // Start at position determined by the first HitCircle. Then determine the rest of the points based off of that position.
            for (int i = 0; i < numObjects; i++)
            {
                if (i == 0)
                {
                    var hc = new HitCircle(startPos, (int)(time + (subsetLength * i * mpb)), (HitObjectType)HitType, (HitObjectSoundType)HitSound, prevPoint, 10f, false);
                    startPos = hc.Position;
                    hitCircles.Add(hc);
                    continue;
                }

                else {
                    Vector2 newPos = new Vector2(startPos.X + xOffset, startPos.Y + yOffset);
                    startPos =  newPos;
                    hitCircles.Add(new HitCircle(startPos, (int)(time + (subsetLength * i * mpb)), (HitObjectType)HitType, (HitObjectSoundType)HitSound, prevPoint, 10f, true));
                }
               
            }
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
