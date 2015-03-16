using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
	[Flags]
	public enum HitObjectType
	{
		Normal = 1 << 0,
		Slider = 1 << 1,
		NewCombo = 1 << 2,
		NormalNewCombo = Normal | NewCombo,
		SliderNewCombo = Slider | NewCombo,
		Spinner = 1 << 3,
		ColourHax = 112,
		Hold = 1 << 7,
		ManiaLong = Hold,
	} ;
}
