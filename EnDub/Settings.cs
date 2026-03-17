using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;

namespace EnDub
{
	public static class Settings
	{
		public static float AudioVolume => GetOption<SliderSetting>("Audio Volume").Value;

		public static T GetOption<T>(string key) where T : SettingEntry
		{
			return Utils.ThisMod.GetSetting<T>(key);
		}
	}
}
