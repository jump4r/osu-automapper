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

        public List<Point> curvePoints { get; set; }

        public int type { get; set; }
        public int hitsound { get; set; }
        public char sliderType { get; set; }
        public int repeat { get; set; }

        public int length { get; set; }

        // Slider Construtor
        public HitSlider() { }
        public HitSlider(int x, int y, int time, int numCurves, int type, int hitsound, char sliderType, int repeat)
        {
            this.x = x;
            this.y = y;
            this.time = time;
         
            this.hitsound = hitsound;
            this.sliderType = sliderType;
            this.repeat = repeat;
            
        }

        public override string ToString()
        {
            string rtn = x + "," + y + "," + time + "," + type + "," + hitsound + "|";
            for (int i = 0; i < curvePoints.Count(); i++)
            {
                string addition = curvePoints[i].X + ":" + curvePoints[i].Y + "|";
                rtn += addition;
            }
            rtn += repeat + "," + length + "," + hitsound + "|0,0:0|0:0,0:0:0:0:";
            return rtn;
        }
    }

}
