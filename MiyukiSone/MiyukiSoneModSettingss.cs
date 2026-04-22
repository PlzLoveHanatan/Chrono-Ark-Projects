using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;

namespace MiyukiSone
{
	public static class ModSettingss
	{
		public static bool SwimsuitSkin => GetOption<ToggleSetting>("Swimsuit Skin").Value;

		public static T GetOption<T>(string key) where T : SettingEntry
		{
			return Utils.ThisMod.GetSetting<T>(key);
		}
	}
}
