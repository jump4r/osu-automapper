using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    // Sets the difficulty relative to the ApproachRate.
     public enum ApproachRate
        {
            EASY = 4,
            NORMAL = 6,
            HARD = 8,
            INSANE = 9,
            EXTRA = 10,
        }

    /// <summary>
    /// Difficulty 
    /// </summary>
    public class Difficulty
    {

        public float baseCircleTimestamp;
        public float sliderTimestamp1;
        public float sliderTimestamp2;

        public Difficulty(ApproachRate approachRate)
        {
            switch (approachRate)
            {
                case ApproachRate.EASY:
                    baseCircleTimestamp = NoteDuration.Half;
                    sliderTimestamp1 = NoteDuration.Half;
                    sliderTimestamp2 = NoteDuration.Quarter;
                    break;
                case ApproachRate.NORMAL:
                    baseCircleTimestamp = NoteDuration.Half;
                    sliderTimestamp1 = NoteDuration.Half;
                    sliderTimestamp2 = NoteDuration.Quarter;
                    break;
                case ApproachRate.HARD:
                    baseCircleTimestamp = NoteDuration.Quarter;
                    sliderTimestamp1 = NoteDuration.Quarter;
                    sliderTimestamp2 = NoteDuration.Quarter;
                    Console.WriteLine("Hard");
                    break;
                case ApproachRate.INSANE:
                    baseCircleTimestamp = NoteDuration.Eighth;
                    sliderTimestamp1 = NoteDuration.Quarter;
                    sliderTimestamp2 = NoteDuration.Eighth;
                    Console.WriteLine("Insane");
                    break;
                default:
                    baseCircleTimestamp = NoteDuration.Eighth;
                    sliderTimestamp1 = NoteDuration.Quarter;
                    sliderTimestamp2 = NoteDuration.Eighth;
                    Console.WriteLine("Default");
                    break;
            }

            Console.WriteLine("Base Circle Timestamp: " + baseCircleTimestamp);
        }

        public static ApproachRate Calculate(int approachRate)
        {
            if (approachRate <= (int)ApproachRate.EASY)
                return ApproachRate.EASY;

            if (approachRate <= (int)ApproachRate.NORMAL)
                return ApproachRate.NORMAL;

            if (approachRate <= (int)ApproachRate.HARD)
                return ApproachRate.HARD;

            else
                return ApproachRate.INSANE;

            // WE DONT TOUCH THE EXTRA AR 10 LIFESTYLE
        }
    }
}
