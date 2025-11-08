using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;

namespace ZenaBaral
{
	public class ZenaUtils
	{
		public static bool HeadSFX => ModManager.getModInfo("ZenaBaral").GetSetting<ToggleSetting>("Head SFX").Value;
		public static bool HeadEquip => ModManager.getModInfo("ZenaBaral").GetSetting<ToggleSetting>("Head Equip").Value;
	}
}
