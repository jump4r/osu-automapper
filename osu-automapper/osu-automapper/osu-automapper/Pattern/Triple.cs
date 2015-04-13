using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    class Triple : BasePattern
    {
        int xOffset = RandomHelper.Range(-3, 3);
        int yOffset = RandomHelper.Range(-3, 3);

        List<HitCircle> hitCircles = new List<HitCircle>();

        public Triple() { }
        public Triple(Vector2 startPos, int time, HitObjectSoundType hitSound, Vector2 prevPoint, float mpb) // Simplest pattern, let's implement this first.
        {
            totalLength = NoteDuration.Quarter;
            subsetLength = NoteDuration.Sixteenth;

            this.mpb = mpb;

            numObjects = 3;

            this.HitType = (int)HitObjectType.Normal;
            this.HitSound = (int)hitSound;

            for (int i = 0; i < numObjects; i++)
            {
                if (i != 0) {
                    Vector2 newPos = new Vector2(startPos.X + xOffset, startPos.Y + yOffset);
                    startPos =  newPos;
                }
                hitCircles.Add(new HitCircle(startPos, (int)(time + (subsetLength * i * mpb)), (HitObjectType)HitType, (HitObjectSoundType)HitSound, prevPoint, 10f, true));
            }
        }

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
