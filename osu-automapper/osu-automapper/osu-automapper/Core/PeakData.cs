using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
	public struct PeakData
	{
		public int index;//The index that the beat 
		public int time;//Estimated time the peak occurs in ms
		public double value;//Amplitude in decibels

		public PeakData(int index, int time, double value)
		{
			this.index = index;
			this.time = time;
			this.value = value;
		}

		public override string ToString()
		{
			return string.Format("index:{0} time:{1} value:{2}", index.ToString(), time.ToString(), value.ToString());
		}
	}
}
