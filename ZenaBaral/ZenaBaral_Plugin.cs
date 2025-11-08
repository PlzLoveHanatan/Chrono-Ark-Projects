using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using ChronoArkMod.ModData;
using HarmonyLib;
namespace ZenaBaral
{
	public class ZenaBaral_Plugin : ChronoArkPlugin
	{
		Harmony harmony = new Harmony("Zena");

		public override void Dispose()
		{
			if (harmony != null)
			{
				harmony.UnpatchSelf();
			}
		}

		public override void Initialize()
		{
			try
			{
				harmony.PatchAll();
			}
			catch (Exception e)
			{
				Debug.Log("Zena: Patch Catch: " + e.ToString());
			}
		}

		public static bool ZenaInParty()
		{
			return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Zena);
		}

		[HarmonyPatch(typeof(FieldSystem), "StageStart")]
		public static class StagePatch
		{
			[HarmonyPostfix]
			public static void StageStartPostfix()
			{
				if (!ZenaInParty()) return;

				var data = GetOrCreateZenaData();

				if (PlayData.TSavedata.StageNum >= 0)
				{
					if (!data.GainHeadEquip && ZenaUtils.HeadEquip)
					{
						ZenaReward();
						data.GainHeadEquip = true;
					}

					if (!data.FirstLevelStat)
					{
						var zena = PlayData.TSavedata.Party.Find(c => c.KeyData == ModItemKeys.Character_Zena);
						if (zena != null)
						{
							ZenaScripts.IncreaseStats(zena);
							data.FirstLevelStat = true;
						}
					}
				}
			}

			private static Value GetOrCreateZenaData()
			{
				var data = PlayData.TSavedata.GetCustomValue<Value>();
				if (data == null)
				{
					data = new Value();
					PlayData.TSavedata.AddCustomValue(data);
				}
				return data;
			}

			private static void ZenaReward()
			{
				PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_Zena_Glyph));
				PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_Baral_Vial));
			}
		}
	}
}