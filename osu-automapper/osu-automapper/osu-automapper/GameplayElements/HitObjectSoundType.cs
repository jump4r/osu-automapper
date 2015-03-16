using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
	[Flags]
	public enum HitObjectSoundType
	{
		None = 0,
		Normal = 1 << 0,
		Whistle = 1 << 1,
		Finish = 1 << 2,
		Clap = 1 << 3
	} ;
}
