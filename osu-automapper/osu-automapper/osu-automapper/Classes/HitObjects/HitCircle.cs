using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osu_automapper
{
    class HitCircle : HitObject
    {
        public Point prevPoint { get; set; }
        private int[] direction = { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 };
        public int maxDistance { get; set; }
        private Random rnd;

        //properties
       
        private int oldX;
        private int oldY;

        //constructor
        public HitCircle() { }
        public HitCircle(int a, int b, int c, int d, int e, Point prevPoint, int maxDistance, Random rnd)
        {
            x = a;
            y = b;
            time = c;
            type = d;
            hitsound = e;

            this.prevPoint = prevPoint;
            this.maxDistance = maxDistance;
            this.rnd = rnd;

            CalculateHitPoint();
        }

        private void CalculateHitPoint()
        {
            if (prevPoint.X == -1)
                return;

            oldX = x;
            oldY = y;
            
            int angle = rnd.Next(0, direction.Length);
            x = prevPoint.X + (int)(maxDistance * Math.Cos(direction[angle]));
            y = prevPoint.Y + (int)(maxDistance * Math.Sin(direction[angle]));

            
            // Forgive my laziness, I just want this to work right now.
            while (x < 10 || x > 500 || y < 10 || y > 370 /*|| GetDistanceBetweenHitObjects() > maxDistance + 100 */)
            {
                angle = rnd.Next(0, direction.Length);
                x = prevPoint.X + (int)(maxDistance * Math.Cos(direction[angle]));
                y = prevPoint.Y + (int)(maxDistance * Math.Sin(direction[angle]));
              
            }
            // Console.WriteLine("Distance Between Notes is " + GetDistanceBetweenHitObjects());

        }

        private int GetDistanceBetweenHitObjects() {
            return (int)Math.Sqrt(Math.Pow((prevPoint.X - x), 2) + Math.Pow((prevPoint.Y - y), 2));
        }
        //funciton 
        public override string ToString()
        {
            return x + "," + y + "," + time + "," + type + "," + hitsound + ",0:0:0:0:";
        }

    }
}
