using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    /// <summary>
    /// Base class for code Patttern. Different patterns get differnent varaibles.
    /// </summary>
    public abstract class BasePattern : HitObject
    {
        public float totalLength { get; set; } // Length of time it takes to complete the Pattern
        public float subsetLength { get; set; } // Length of time between each object in the Pattern 

        public float mpb { get; set; }

        public float numObjects { get; set; }
        public HitObjectType objectType { get; set; }

        /// <summary>
        /// Overrite string to an .osu file readable format.
        /// </summary>
        /// <returns></returns>
        public override abstract string SerializeForOsu();
    }
}
