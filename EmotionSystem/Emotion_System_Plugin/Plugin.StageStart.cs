using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using HarmonyLib;

namespace EmotionSystem
{
	public partial class EmotionlSystem_Plugin
	{
		[HarmonyPatch(typeof(FieldSystem), "StageStart")]
		public static class StagePatch
		{
			[HarmonyPostfix]
			public static void StageStartPostfix()
			{
				if (IsCamp)
				{
					Scripts.ChargeLucyNeck();
				}
			}
		}

		public static bool IsCamp
		{
			get
			{
				StageSystem instance = StageSystem.instance;
				string key = instance?.StageData?.Key;
				return IsCampKey(key);
			}
		}

		public static bool IsCampKey(string key)
		{
			return key == GDEItemKeys.Stage_Stage_Camp ||
				   key == GDEItemKeys.Stage_Stage2_Camp ||
				   key == GDEItemKeys.Stage_Stage3_Camp ||
				   key == GDEItemKeys.Stage_Stage4_Camp;
		}
	}
}
