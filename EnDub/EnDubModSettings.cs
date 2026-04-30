using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;

namespace EnDub
{
	public static class EnDubModSettings
	{
		public static float AudioVolume => GetOption<SliderSetting>("Audio Volume").Value;
		public static bool Console => GetOption<ToggleSetting>("Console").Value;
		public static bool SimpleBackground => GetOption<ToggleSetting>("Simple Background").Value;

		public static T GetOption<T>(string key) where T : SettingEntry
		{
			return Utils.ThisMod.GetSetting<T>(key);
		}
	}
}
