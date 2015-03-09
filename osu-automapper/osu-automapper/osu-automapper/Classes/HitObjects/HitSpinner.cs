using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osu_automapper
{
    class HitSpinner : HitObject
    {
      
        public int endTime { get; set; }

        public HitSpinner() { }
        public HitSpinner(int x, int y, int time, int type, int hitsound, int endTime)
        {
            this.x = x;
            this.y = y;
            this.time = time;
            this.type = type;
            this.hitsound = hitsound;
            this.endTime = endTime;
        }

        public override string ToString()
        {
            return x + "," + y + "," + time + "," + type + "," + hitsound + "," + endTime + ",0:0:0:0:";
        }

    }
}
