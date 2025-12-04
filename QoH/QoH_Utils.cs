using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using ChronoArkMod.ModEditor;
using ChronoArkMod.ModData.Settings;

namespace QoH
{
	public static class QoH_Utils
	{
		public static bool JusticeVoice => ModManager.getModInfo("QoH").GetSetting<ToggleSetting>("Justice Voice").Value;
		public static bool JusticeEquip => ModManager.getModInfo("QoH").GetSetting<ToggleSetting>("Justice Equip").Value;

		public static QoH_Value Data => GetOrCreateData();

		public static QoH_Value GetOrCreateData()
		{
			var data = PlayData.TSavedata.GetCustomValue<QoH_Value>();
			if (data == null)
			{
				data = new QoH_Value();
				PlayData.TSavedata.AddCustomValue(data);
			}
			return data;
		}
	}
}
