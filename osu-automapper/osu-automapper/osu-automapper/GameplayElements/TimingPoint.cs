using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    public class TimingPoint
    {
        public int Offset;
        public bool Inherited;
        public float SliderVelocityMultiplier;
        public int Index;

        public TimingPoint() { }
        public TimingPoint(int offset, bool inherited, float newSliderVelocity, int index)
        {
            this.Offset = offset;
            this.Inherited = inherited;
            this.SliderVelocityMultiplier = (float)(100 / Math.Abs(newSliderVelocity));
            this.Index = index;

            Console.WriteLine("Timing Point SliderVelMult: " + SliderVelocityMultiplier);
        }

        public static TimingPoint UpdateCurrentTimingPoint(int currentMillisecond, List<TimingPoint> timingPoints, TimingPoint currentTimingPoint) {
            if (currentTimingPoint.Index >= timingPoints.Count - 1)
            {
                return currentTimingPoint;
            }

            if (timingPoints[currentTimingPoint.Index + 1].Offset < currentMillisecond)
            {
                return timingPoints[currentTimingPoint.Index + 1];
            }

            else 
            {
                return currentTimingPoint;
            }
        }
    }

}
