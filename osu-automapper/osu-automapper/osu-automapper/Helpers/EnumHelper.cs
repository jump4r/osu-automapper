using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
	public static class EnumHelper
	{
		public static T GetRandom<T>() where T : struct,IConvertible
		{
			Type enumType = typeof(T);
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("T must be an enumerated type");
			}

			var a = Enum.GetValues(enumType);

			return (T)a.GetValue(RandomHelper.Range(0, a.Length));
		}
	}
}
