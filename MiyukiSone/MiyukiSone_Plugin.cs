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
using UseItem;
using System.Reflection.Emit;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
namespace MiyukiSone
{
	public class MiyukiSone_Plugin : ChronoArkPlugin
	{
		public const string modname = "MiyukiSone";

		public const string version = "0.9";

		public const string author = "MiyukiSone";

		private readonly Harmony harmony = new Harmony("MiyukiSone");

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
				Debug.Log("MiyukiSone: Patch Catch: " + e.ToString());
			}
		}

		// Reset custom save file
		[HarmonyPatch(typeof(PlayData))]
		[HarmonyPatch(nameof(PlayData.GameEndInit))]
		public class Patch_Reset_Save
		{
			[HarmonyPostfix]
			public static void Postfix()
			{
				//MiyukiSaveManager.Instance.ResetSave();
				//Debug.Log("Miyuki save file reset coimplete");
			}
		}

		// Redirect skills in Yandere mood
		[HarmonyPatch(typeof(BattleAlly))]
		[HarmonyPatch(nameof(BattleAlly.UseSkill), new Type[] { typeof(Skill), typeof(List<BattleChar>) })]
		public class Skill_Redirect_Patch
		{
			[HarmonyPrefix]
			public static void Prefix(BattleAlly __instance, Skill skill, ref List<BattleChar> Target)
			{
				if (!Affection.IsYandere || skill.Master.Info.KeyData != ModItemKeys.Character_Miyuki /*|| skill.MySkill.KeyID == ModItemKeys.Skill_S_Miyuki_Rare_FinalView*/ || !MiyukiInParty) return;

				if (RandomManager.RandomPer("MiyukiYandereRedirect", 100, 15))
				{
					List<BattleChar> possibleTargets = new List<BattleChar>();

					if (skill.IsDamage) possibleTargets = AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList();
					else if (skill.IsHeal) possibleTargets = Utils.EnemyTeam.AliveChars.Where(a => a != null).ToList();

					if (possibleTargets.Count > 0)
					{
						BattleChar target = possibleTargets.Random("MiyukiRandomTarget");
						Target.Clear();
						Target.Add(target);
						EventsData.MiyukiTextEvent(Affection.CurrentAffection);
					}
				}
			}
		}

		[HarmonyPatch(typeof(BattleSystem))]
		[HarmonyPatch(nameof(BattleSystem.TurnEnd))]
		class Patch_BattleSystem_TurnEnd
		{
			[HarmonyPrefix]
			public static bool Prefix()
			{
				if (Dialogue.DialogueWindows.Count > 0)
				{
					DialogueData.StartTurnEndDialogue();

					if (Affection.MiyukiDecides)
					{
						//Affection.CurrentAffection = Yandere;
						Events.YandereActionCut();
					}

					foreach (var windowObj in Dialogue.DialogueWindows)
					{
						if (windowObj != null)
						{

						}
					}
					return false;
				}
				return true;
			}
		}

		[HarmonyPatch(typeof(FieldSystem))]
		[HarmonyPatch(nameof(FieldSystem.StageStart))]
		public static class StagePatch
		{
			[HarmonyPostfix]
			public static void StageStartPostfix()
			{
				CheckSlots();

				if (!MiyukiSaveManager.Instance.CurrentData.GameRestarted && MiyukiSaveManager.Instance.CurrentData.GameUpdated)
				{
					MiyukiSaveManager.Instance.CurrentData.GameRestarted = true;
					MiyukiSaveManager.Instance.Save();
					FieldSystem.instance.StartCoroutine(WaitForSeconds());
				}
			}

			private static void CheckSlots()
			{
				if (!MiyukiData.SlotsCheck)
				{
					if (PlayData.TSavedata.Inventory.Count > 18)
					{
						int excess = PlayData.TSavedata.Inventory.Count - 18;

						for (int i = 0; i < excess; i++)
						{
							PlayData.TSavedata.Inventory.RemoveAt(PlayData.TSavedata.Inventory.Count - 1);
						}

						PlayData.MaxInventory = 18;
						PlayData.TSavedata.MaxinventoryNumPlus -= excess;
						PartyInventory.Ins?.UpdateInvenUI();
					}

					//if (PlayData.TSavedata.ArkPassivePlus > 4)
					//{
					//	int excess = PlayData.TSavedata.ArkPassivePlus - 4;
					//	for (int i = 0; i < excess; i++)
					//	{
					//		if (PlayData.TSavedata.Passive_Itembase.Count > 0)
					//		{
					//			PlayData.TSavedata.Passive_Itembase.RemoveAt(PlayData.TSavedata.Passive_Itembase.Count - 1);
					//		}
					//	}
					//	PlayData.TSavedata.ArkPassivePlus = 4;
					//}
				}
				MiyukiData.SlotsCheck = true;
			}

			private static IEnumerator WaitForSeconds()
			{
				yield return new WaitForSeconds(2.5f);
				yield return Events.ExitGame();
			}
		}

		[HarmonyPatch(typeof(FieldSystem))]
		[HarmonyPatch("BattleEnd")]
		public static class FieldSystem_BattleEnd_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(FieldSystem __instance, bool NoSaveAfterEnd, bool isDefeat)
			{
				if (!MiyukiInParty || isDefeat) return;

				if (MiyukiForces) PlayData.TSavedata.Party.FindAll(c => c.Incapacitated).ForEach(c => { c.Incapacitated = false; c.Hp = c.get_stat.maxhp / 2; });

				GetRandomAffection();
				SaveManager.savemanager.ProgressOneSave();
			}
		}

		[HarmonyPatch(typeof(StageChest))]
		[HarmonyPatch("Init")]
		public static class StageChest_Init_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(StageChest __instance)
			{
				if (!IsKuudere || !MiyukiDecides) return;

				__instance.ClassNum += (MiyukiInParty && MiyukiResult()) ? 1 : -1;
				if (__instance.ClassNum < 0) __instance.ClassNum = 0;
				if (__instance.ClassNum > 4) __instance.ClassNum = 4;
			}
		}
	}
}