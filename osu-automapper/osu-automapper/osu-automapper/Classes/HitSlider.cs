using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osu_automapper
{
    class HitSlider
    {
        // Properties
        public int x { get; set; }
        public int y { get; set; }
        public int time { get; set; }

        // public List<Point> curvePoints { get; set; }
        private List<Point> curvePoints = new List<Point>(); 

        public int type { get; set; }
        public int hitsound { get; set; }
        public char sliderType { get; set; }
        public int repeat { get; set; }

        public int length { get; set; }
        public float sliderVelocity { get; set; }

        // Private vars, used in construction of the slider for printing
        private int[] sliderAngles = { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 };

        Random rnd = new Random();

        // Slider Construtor
        public HitSlider() { }
        public HitSlider(int x, int y, int time, int type, int hitsound, char sliderType, int repeat, float sliderVelocity, int numCurves, int length)
        {
            this.x = x;
            this.y = y;
            this.time = time;
         
            this.hitsound = hitsound;
            this.sliderType = sliderType;
            this.repeat = repeat;
            this.type = type;

            this.sliderVelocity = 1.5f;
            this.length = length;

            // Construct the Slider
            ConstructSlider();
        }

        private void ConstructSlider()
        {
            switch (sliderType)
            {
                case 'L': // Linear Slider
                    AddLinearSlider();
                    break;
                case 'P': // P-spline
                    break;
                case 'B': // Bezier Spline
                    break;
            }
        }

        private void AddLinearSlider()
        {
            int angle = rnd.Next(0, sliderAngles.Length);
            Point endPoint = new Point();

            // Add the slider
            endPoint.X = x + (int)( length * Math.Cos( sliderAngles[angle] ) );
            endPoint.Y = y + (int)( length * Math.Sin( sliderAngles[angle] ) );
            curvePoints.Add(endPoint);
        }



        public override string ToString()
        {
            Console.WriteLine(type);
            string rtn = x + "," + y + "," + time + "," + type + "," + hitsound + "," + sliderType + "|";
            for (int i = 0; i < curvePoints.Count(); i++)
            {
                string addition = curvePoints[i].X + ":" + curvePoints[i].Y;
                rtn += addition;
                if (i < curvePoints.Count() - 1)
                    rtn += "|";
            }
            rtn += "," + repeat + "," + length;
            return rtn;
        }
    }

}
