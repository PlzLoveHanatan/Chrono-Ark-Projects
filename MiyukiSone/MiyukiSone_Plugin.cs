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
using Steamworks;
using System.Reflection;
using UnityEngine.EventSystems;
using static CharacterDocument;
using System.IO.Ports;
using Newtonsoft.Json.Linq;
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
			}
		}

		// Reset custom save file
		[HarmonyPatch(typeof(Credits))]
		[HarmonyPatch("Start")]
		public class Patch_Credits_Save_reset
		{
			[HarmonyPostfix]
			public static void Postfix()
			{
				MiyukiSaveManager.Instance.ResetSave();
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
				if (!IsYandere || skill.Master.Info.KeyData != ModItemKeys.Character_Miyuki /*|| skill.MySkill.KeyID == ModItemKeys.Skill_S_Miyuki_Rare_FinalView*/) return;

				if (RandomManager.RandomPer("MiyukiYandereRedirect", 100, 15))
				{
					BattleChar target;

					if (skill.IsDamage)
					{
						target = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).RandomElement();
					}
					else if (skill.IsHeal)
					{
						target = BattleSystem.instance.EnemyTeam.AliveChars.RandomElement();
					}
					else
					{
						EventsData.MiyukiTextEvent();
						return;
					}

					if (target == null) return;
					Target.Clear();
					Target.Add(target);
					EventsData.MiyukiTextEvent(CurrentAffection);
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

					if (MiyukiDecides)
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

				if (!IsCamp) MiyukiData.FinalViewCharge++;

				if (!MiyukiSaveManager.Instance.CurrentData.GameRestarted && MiyukiSaveManager.Instance.CurrentData.GameUpdated)
				{
					MiyukiSaveManager.Instance.CurrentData.GameRestarted = true;
					MiyukiSaveManager.Instance.Save();
					FieldSystem.instance.StartCoroutine(WaitForSeconds());
				}
			}

			private static IEnumerator WaitForSeconds()
			{
				yield return new WaitForSeconds(2.5f);
				yield return Events.ExitGame();
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

			public static bool IsCamp
			{
				get
				{
					string key = StageSystem.instance?.StageData?.Key;
					return key == GDEItemKeys.Stage_Stage_Camp ||
						   key == GDEItemKeys.Stage_Stage2_Camp ||
						   key == GDEItemKeys.Stage_Stage3_Camp ||
						   key == GDEItemKeys.Stage_Stage4_Camp;
				}
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

				//if (MiyukiDecides) PlayData.TSavedata.Party.FindAll(c => c.Incapacitated).ForEach(c => { c.Incapacitated = false; c.Hp = c.get_stat.maxhp / 4; });

				GetRandomAffection();
				SaveManager.savemanager.ProgressOneSave();
			}
		}

		[HarmonyPatch(typeof(SkillButton), "Click")]
		public static class SkillButton_Click_Patch
		{
			public static bool Prefix(SkillButton __instance)
			{
				if (__instance == null || !__instance.CharStatView || !MiyukiInParty || MiyukiData.GameUpdated) return true;

				string skillKey = __instance?.Myskill?.MySkill?.KeyID;

				if (skillKey == ModItemKeys.Skill_S_Miyuki_Rare_GameUpdate)
				{
					if (FieldSystem.instance != null && PlayData.AP >= __instance.Myskill.AP)
					{
						var skillList = GameUpdateSelectionList();
						if (skillList.Count > 0)
						{
							FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(skillList, new SkillButton.SkillClickDel(GameUpdateSelection), ModLocalization.GameUpdate, false, false, true, false, false));
							return false;
						}
					}
				}
				else if (skillKey == ModItemKeys.Skill_S_Miyuki_Rare_FinalView && MiyukiData.FinalViewCharge >= 2)
				{
					//List<Skill> list = PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki).Select(a => a.GetBattleChar).Where(b => b != null)
					//	.Select(b => Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Rare_FinalView_0, b, PlayData.TempBattleTeam)).ToList();

					var ally = PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki && !a.Incapacitated).Select(a => a.GetBattleChar).RandomElement();
					Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Rare_FinalView_0, ally, PlayData.TempBattleTeam);

					if (skill != null)
					{
						FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(FinalViewSelection), ModLocalization.FinalView, false, false, true, false, true));
						return false;
					}
					
				}

				return true;
			}

			private static List<Skill> GameUpdateSelectionList()
			{
				return MiyukiChar == null ? new List<Skill>() : new List<Skill>()
				{
					Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Rare_GameUpdate_0, MiyukiChar, PlayData.TempBattleTeam),
					Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Rare_GameUpdate_1, MiyukiChar, PlayData.TempBattleTeam),
				};
			}

			private static void GameUpdateSelection(SkillButton button)
			{
				if (button.Myskill.MySkill.KeyID != ModItemKeys.Skill_S_Miyuki_Rare_GameUpdate_0) return;

				var skillData = MiyukiChar.Info.SkillDatas.FirstOrDefault(sd => sd.SkillInfo?.KeyID == ModItemKeys.Skill_S_Miyuki_Rare_GameUpdate);

				if (skillData != null)
				{
					skillData.SKillExtended = null;
					skillData.SKillExtended = Skill_Extended.DataToExtended(GDEItemKeys.SkillExtended_SkillWe_NoExchange);
				}

				MiyukiData.GameUpdated = true;
				CurrentAffection = MiyukiAffection.Kuudere;
				EventsData.MiyukiTextEvent();
				MiyukiSaveManager.Instance.CurrentData.GameUpdated = true;
				MiyukiSaveManager.Instance.Save();
				Events.RestartStage();
			}

			private static void FinalViewSelection(SkillButton button)
			{
				//if (button?.Myskill?.Master == null) return;

				int rarity = 0;
				PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki).RandomElement().Let(a => { a.Hp = 0; a.Incapacitated = true; rarity++; });
				
				//button.Myskill.Master.Info.Hp = 0;
				//button.Myskill.Master.Info.Incapacitated = true;
				//if (target is BattleAlly battleAlly) battleAlly.IsDead = true;
				MiyukiData.FinalViewCharge = 0;
				CurrentAffection = MiyukiAffection.Yandere;
				EventsData.MiyukiTextEvent();
				GainEquip(3);
			}
		}

		[HarmonyPatch(typeof(StageChest))]
		[HarmonyPatch("init")]
		public static class StageChest_init_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(StageChest __instance)
			{
				if (!MiyukiDecides || IsKuudere) return;

				__instance.ClassNum += MiyukiResult() ? 1 : -1;
				__instance.ClassNum = Math.Max(0, Math.Min(4, __instance.ClassNum));
			}
		}


		[HarmonyPatch(typeof(P_Mement_0), nameof(P_Mement_0.Draw))]
		public static class Mement_Draw_Patch
		{
			[HarmonyPrefix]
			public static bool Prefix(Skill Drawskill, ref IEnumerator __result)
			{
				if (!MiyukiInParty) return true;

				if (Drawskill.IsCreatedInBattle && Drawskill.AP >= 1 && Drawskill.ExtendedFind("Mement_Ex_0", true) == null &&
					Drawskill.Master.Info.KeyData != GDEItemKeys.Character_Mement && Drawskill.Master.Info.KeyData != ModItemKeys.Character_Miyuki && !Drawskill.Master.IsLucy)
				{
					Drawskill.ExtendedAdd(Skill_Extended.DataToExtended(GDEItemKeys.SkillExtended_Mement_Ex_0));
				}

				__result = Dummy();
				return false;
			}

			private static IEnumerator Dummy()
			{
				yield return null;
			}
		}

		[HarmonyPatch(typeof(PartyInventory), "Update")]
		public static class PartyInventory_Update_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(PartyInventory __instance)
			{
				if (PlayData.TSavedata.SpRule is MiyukiSpecialRules)
				{
					__instance?.SpruleIcon?.SetActive(false);
				}
			}
		}

		[HarmonyPatch(typeof(CampUI), "Update")]
		public static class CampUI_Update_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(CampUI __instance)
			{
				if (PlayData.TSavedata.SpRule is MiyukiSpecialRules)
				{
					__instance?.Button_BloodyMist?.gameObject?.SetActive(true);
				}
			}
		}

		[HarmonyPatch(typeof(Camp), "Start")]
		public static class Camp_Update_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(Camp __instance)
			{
				if (PlayData.TSavedata.SpRule is MiyukiSpecialRules)
				{
					__instance?.UseNecklaceObj?.SetActive(true);
				}
			}
		}

		[HarmonyPatch(typeof(BloodyMist))]
		[HarmonyPatch("IncreaseLevel")]
		public static class BloodyMist_IncreaseLevel_Patch
		{
			[HarmonyPrefix]
			public static bool Prefix(BloodyMist __instance)
			{
				if (PlayData.TSavedata.SpRule is MiyukiSpecialRules)
				{
					if (SaveManager.Difficalty == 2)
					{
						if (SaveManager.NowData.statistics.BestBloodmistinExpert <= __instance.Level)
						{
							SaveManager.NowData.statistics.BestBloodmistinExpert = __instance.Level;
						}
					}
					else
					{
						if (SaveManager.NowData.statistics.BestBloodmistinNomal <= __instance.Level)
						{
							SaveManager.NowData.statistics.BestBloodmistinNomal = __instance.Level;
						}
					}

					if (SaveManager.NowData.unlockList.UnlockSpecialRuleKey.Contains("BloodyMistLV4"))
					{
						if (__instance.Level > 4) __instance.Level = 4;
					}
					else if (__instance.Level > 3)
					{
						__instance.Level = 3;
					}

					__instance.Level++;
					__instance.BeforeClearCampUI = true;

					switch (__instance.Level)
					{
						case 1:
							__instance.RuleChange.EnemyPerStat.MaxHP = 25;
							__instance.RuleChange.StorePricePer = 25;
							break;
						case 2:
							InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter, 2));
							PlayData.TSavedata.LucySkills.Add(GDEItemKeys.Skill_S_Transcendence_Main);
							break;
						case 3: PlayData.TSavedata.LucySkills.Add(GDEItemKeys.Skill_S_Transcendence_Main); break;
						default: break;
					}

					__instance.Dropartifactpouch = true;
					PlayData.TSavedata.ArkPassivePlus++;
					PlayData.TSavedata.Passive_Itembase.Add(null);

					return false;
				}
				return true;
			}
		}
	}
}