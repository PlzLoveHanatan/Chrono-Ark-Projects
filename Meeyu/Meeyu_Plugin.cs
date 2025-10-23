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
namespace Meeyu
{
	public class Meeyu_Plugin : ChronoArkPlugin
	{
		Harmony harmony = new Harmony("Meeyu");

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
				Debug.Log("Meeyu: Patch Catch: " + e.ToString());
			}
		}

		public static bool MeeyuInParty()
		{
			return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Meeyu);
		}

		[HarmonyPatch(typeof(FieldSystem), "StageStart")]
		public static class StagePatch
		{
			[HarmonyPostfix]
			public static void StageStartPostfix()
			{
				if (!MeeyuInParty()) return;

				var data = GetOrCreateMeeyuData();

				if (PlayData.TSavedata.StageNum >= 0)
				{
					if (!data.GainStartingReward)
					{
						MeeyuReward();
						data.GainStartingReward = true;
					}
				}
			}

			private static Value GetOrCreateMeeyuData()
			{
				var data = PlayData.TSavedata.GetCustomValue<Value>();
				if (data == null)
				{
					data = new Value();
					PlayData.TSavedata.AddCustomValue(data);
				}
				return data;
			}

			private static void MeeyuReward()
			{
				var randomEquip = PlayData.GetEquipRandom(4, false, new List<string>());
				PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 2000));
				PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(randomEquip, 1));
				PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy, 1));
			}
		}
	}
}