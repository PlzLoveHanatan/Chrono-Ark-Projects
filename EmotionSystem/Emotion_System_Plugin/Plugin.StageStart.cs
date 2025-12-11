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
				if (PlayData.TSavedata.StageNum >= 0 && Utils.BossInvitations && Utils.DistortedBosses)
				{
					var data =  Utils.GetOrCreateEmotionData();

					if (!data.StartingBonus)
					{
						GainEmotionReward();
						data.StartingBonus = true;
					}
				}

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

		

		private static void GainEmotionReward()
		{
			PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Consume_SmallChest));
			//PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Consume_SmallChest));
			PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy_Rare, 1));
			PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Consume_C_EmotionSystem_DreamingCurrent, 1));
		}
	}
}
