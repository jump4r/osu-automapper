using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    class HitCircle
    {
        //constructor
        public HitCircle() { }
        public HitCircle(int a, int b, int c, int d, int e)
        {
            x = a;
            y = b;
            time = c;
            type = d;
            hitsound = e;

        }

        //properties
        public int x { get; set; }
        public int y { get; set; }
        public int time { get; set; }
        public int type { get; set; }
        public int hitsound { get; set; }

        //funciton 
        public override string ToString()
        {
            return x + "," + y + "," + time + "," + type + "," + hitsound + ",0:0:0:0:";
        }

    }
}
