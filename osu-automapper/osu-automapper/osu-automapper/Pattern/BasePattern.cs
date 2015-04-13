using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    public abstract class BasePattern : HitObject
    {
        public float totalLength { get; set; } // Length of time it takes to complete the Pattern
        public float subsetLength { get; set; } // Length of time between each object in the Pattern 

        public float mpb { get; set; }

        public float numObjects { get; set; }
        public HitObjectType objectType { get; set; }

        public override abstract string SerializeForOsu();
    }
}
