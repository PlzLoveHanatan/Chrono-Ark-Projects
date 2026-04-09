using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using GameDataEditor;
using UseItem;
using static MiyukiSone.Affection;
using PItem;
using System.Reflection;
using DG.Tweening.Plugins.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static MiyukiSone.MiyukiPatchesHelpers;
using static Spine.Unity.Examples.SpineboyFootplanter;
using static MiyukiSone.Utils;
using DarkTonic.MasterAudio;
using System.Collections;
using I2.Loc;
using System.Collections.Specialized;
using System;
using Random = UnityEngine.Random;
using Spine;
using static ItemCollection;
using static Gamepad_HoldKey;

namespace MiyukiSone
{
	[HarmonyPatch]
	public static class MiyukiPatches
	{
		#region Various Patches
		[HarmonyPatch(typeof(PrintText))]
		[HarmonyPatch(nameof(PrintText.TextInput))]
		public class VoiceOn
		{
			[HarmonyPrefix]
			public static void Prefix(PrintText __instance, string inText)
			{
				string audioFile = EventsData.GetAudioFileByText(inText);

				if (!string.IsNullOrEmpty(audioFile))
				{
					PlaySoundFromAsset($"Assets/Audio/Events/BattleChar/{audioFile}.ogg", true);
					Debug.Log($"[VoiceOn] Playing: {audioFile} from text: {inText}");
				}
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
				MiyukiSaveManager.Instance?.ResetSave();
			}
		}

		[HarmonyPatch(typeof(SkillToolTip), nameof(SkillToolTip.Input))]
		class SkillToolTipPatch
		{
			[HarmonyPostfix]
			public static void Postfix(SkillToolTip __instance, Skill Skill)
			{
				if (Skill == null || Skill.MySkill == null) return;

				foreach (var ex in Skill.AllExtendeds)
				{
					if (ex is IP_MiyukiSkillPreviewChange previewChange)
					{
						var preview = previewChange.SkillPreviewChange();

						if (preview != null && preview.MySkill != null)
						{
							__instance.PlusSkillView = previewChange.SkillPreviewChange();

							//if (!string.IsNullOrEmpty(Skill.MySkill.PlusSkillView))
							//{
							//	preview.MySkill.PlusSkillView = Skill.MySkill.PlusSkillView;
							//}

							//__instance.PlusSkillView = preview;

							if (!string.IsNullOrEmpty(preview.MySkill.KeyID))
							{
								string previewName = new GDESkillData(preview.MySkill.KeyID).Name;
								__instance.PlusTooltipsView("<b>" + previewName + "</b>", ControlTooltip.InitText(6, ScriptLocalization.Battle_Keyword.PlusSkillView, "XBOX_LSTICK_KEY"));
							}

							return;
						}
					}
				}
			}
		}

		//Redirect skills in Yandere mood
		[HarmonyPatch(typeof(BattleAlly))]
		[HarmonyPatch(nameof(BattleAlly.UseSkill), new Type[] { typeof(Skill), typeof(List<BattleChar>) })]
		public class Skill_Redirect_Patch
		{
			[HarmonyPrefix]
			[HarmonyPriority(Priority.First)]
			public static void Prefix(BattleAlly __instance, Skill skill, ref List<BattleChar> Target)
			{
				if (!IsYandere || skill.Master.Info.KeyData != ModItemKeys.Character_Miyuki) return;

				if (RandomManager.RandomPer("MiyukiYandereRedirect", 100, 20))
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

				if (!IsCamp()) MiyukiData.FinalViewCharge++;

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

			public static bool IsCamp()
			{
				string[] campKeys =
					{
					GDEItemKeys.Stage_Stage_Camp,
					GDEItemKeys.Stage_Stage2_Camp,
					GDEItemKeys.Stage_Stage3_Camp,
					GDEItemKeys.Stage_Stage4_Camp
				};
				return campKeys.Contains(StageSystem.instance?.StageData?.Key);
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

				if (MiyukiDecides) GetRandomAffection();
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
					GetRandomAffection();

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
						FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(FinalViewSelection), ModLocalization.FinalView, false, false, true, false, false));
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
				PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki).RandomElement().Let(a => { a.Hp = 0; a.Incapacitated = true; MiyukiData.FinalViewDamage++; });
				MiyukiData.FinalViewCharge = 0;
				CurrentAffection = MiyukiAffection.Yandere;
				EventsData.MiyukiTextEvent();
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

		[HarmonyPatch(typeof(Character), nameof(Character.GetStatUpdate))]
		public class Patch_GetStatUpdate
		{
			[HarmonyPrefix]
			public static void Prefix(Character __instance)
			{
				MiyukiStatContent.CharacterStack.Push(__instance);
			}

			[HarmonyPostfix]
			public static void Postfix()
			{
				if (MiyukiStatContent.CharacterStack.Count > 0) MiyukiStatContent.CharacterStack.Pop();
			}
		}

		[HarmonyPatch(typeof(Character), nameof(Character.StatC))]
		public class Patch_StatC
		{
			[HarmonyPrefix]
			public static bool Prefix(ref Stat inputstat, ref Stat __result)
			{
				Character character = null;

				if (MiyukiStatContent.CharacterStack.Count > 0) character = MiyukiStatContent.CharacterStack.Peek();

				if (inputstat != null && character != null && character.Equip != null && character.Equip.Any(e => e != null && e.itemkey == ModItemKeys.Item_Equip_E_Miyuki_WallBreaker))
				{
					Stat s = new Stat();
					s += inputstat;

					if (s.atk < 0f) s.atk = 0f;
					if (s.def < 0f) s.def = 0f;
					if (s.dod < 0f) s.dod = 0f;
					if (s.reg < 0f) s.reg = 0f;
					if (s.maxhp < 0) s.maxhp = 0;
					if (s.Penetration < 0f) s.Penetration = 0f;
					if (s.DeadImmune < 0) s.DeadImmune = 0;

					if (s.PerfectShield) s.def = 100f;
					if (s.PerfectDodge) s.dod = 500f;

					if (s.AggroPer < -100) s.AggroPer = -100;
					if (s.AggroPer > 100) s.AggroPer = 100;

					__result = s;
					return false;
				}

				return true;
			}
		}

		[HarmonyPatch(typeof(PartyInventory), "Update")]
		public static class PartyInventory_Update_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(PartyInventory __instance)
			{
				if (PlayData.TSavedata.SpRule is MiyukiSpecialRules) __instance?.SpruleIcon?.SetActive(false);
			}
		}

		[HarmonyPatch(typeof(CampUI), "Update")]
		public static class CampUI_Update_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(CampUI __instance)
			{
				if (PlayData.TSavedata.SpRule is MiyukiSpecialRules) __instance?.Button_BloodyMist?.gameObject?.SetActive(true);
			}
		}

		[HarmonyPatch(typeof(Camp), "Start")]
		public static class Camp_Update_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(Camp __instance)
			{
				if (PlayData.TSavedata.SpRule is MiyukiSpecialRules) __instance?.UseNecklaceObj?.SetActive(true);
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
		#endregion

		#region Miyuki Game Update Visual
		// Change main BG art
		//[HarmonyPatch(typeof(MainSceneScript))]
		//[HarmonyPatch("Start")]
		//class Patch_MainMenu_Overlay
		//{
		//	[HarmonyPostfix]
		//	public static void Postfix(MainSceneScript __instance)
		//	{
		//		return;
		//		// Создаем канвас если нет
		//		GameObject canvas = GameObject.Find("MiyukiOverlayCanvas");
		//		if (canvas == null)
		//		{
		//			canvas = new GameObject("MiyukiOverlayCanvas");
		//			Canvas cv = canvas.AddComponent<Canvas>();
		//			cv.renderMode = RenderMode.ScreenSpaceOverlay;
		//			cv.sortingOrder = 999; // поверх всего

		//			CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();
		//			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

		//			canvas.AddComponent<GraphicRaycaster>();
		//		}

		//		// Создаем Image на весь экран
		//		GameObject overlay = new GameObject("MiyukiBackground");
		//		overlay.transform.SetParent(canvas.transform, false);

		//		Image img = overlay.AddComponent<Image>();
		//		img.raycastTarget = false; // чтобы не блокировать клики

		//		// Растягиваем на весь экран
		//		RectTransform rt = img.GetComponent<RectTransform>();
		//		rt.anchorMin = Vector2.zero;
		//		rt.anchorMax = Vector2.one;
		//		rt.offsetMin = Vector2.zero;
		//		rt.offsetMax = Vector2.zero;

		//		// Загружаем арт
		//		Sprite miyukiBg = UtilsUI.GetSprite("MiyukiVisual/Menu/.png");
		//		if (miyukiBg != null)
		//		{
		//			img.sprite = miyukiBg;
		//			img.color = Color.white;
		//			Debug.Log("[Miyuki] Fullscreen background overlay added");
		//		}
		//		else
		//		{
		//			// Если нет арта, делаем розовый фон
		//			//img.color = new Color(1f, 0.5f, 0.8f, 0.5f);
		//		}
		//	}
		//}

		[HarmonyPatch(typeof(CharStatV4))]
		[HarmonyPatch("OnEnable")]
		public static class CharStatV4_OnEnable_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(CharStatV4 __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				MiyukiVisual.Instance?.StartParticlesOnTransform(__instance.transform, true, MiyukiVisual.PauseSettings);
				MakeTransparent(__instance.transform, "BG");
				PlaySong();
			}
		}

		[HarmonyPatch(typeof(CharStatV4))]
		[HarmonyPatch("OnDisable")]
		public static class CharStatV4_OnDisable_Patch
		{
			[HarmonyPostfix]
			public static void Postfix()
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				MiyukiVisual.Instance?.StopParticles();
				StopSong();
			}
		}

		[HarmonyPatch(typeof(MainSceneScript))]
		[HarmonyPatch("Start")]
		public static class MainSceneScript_Start_Patch
		{
			[HarmonyPrefix]
			public static bool Prefix(MainSceneScript __instance)
			{
				if (MiyukiSaveManager.Instance.CurrentData.GameUpdated)
				{
					__instance.StartCoroutine(MiyukiStart(__instance));
					return false;
				}
				return true;
			}

			[HarmonyPostfix]
			public static void Postfix(MainSceneScript __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;

				GameObject canvas = GameObject.Find("Canvas");
				GameObject canvas2 = GameObject.Find("Canvas (2)");

				if (canvas == null) return;

				MiyukiVisual.Instance?.StartParticlesOnTransform(__instance.transform, true, MiyukiVisual.PauseSettings);
				SetSprite(canvas.transform, "Can/Logo", "Logo");
				SetText(canvas.transform, "Can/PressAnyKey/Text", null, 0, true);

				Transform mainMenu = canvas.transform.Find("MainMenu");
				if (mainMenu != null)
				{
					SetMenuButton(mainMenu, "TextMeshPro Text", "Start Game");
					SetMenuButton(mainMenu, "TextMeshPro Text (1)", "Options");
					SetMenuButton(mainMenu, "TextMeshPro Text (4)", "Workshop");
					SetMenuButton(mainMenu, "TextMeshPro Text (2)", "Credits");
					SetMenuButton(mainMenu, "Text (TMP)", "Language");
					SetMenuButton(mainMenu, "TextMeshPro Text (3)", "Exit");
				}

				if (canvas2 != null)
				{
					SetText(canvas2.transform, "TextMeshPro Text", null, 0, true);
					SetImageColor(canvas2.transform, "SNSAlign/Twitter", new Color(1f, 0.5f, 0.8f));
					SetImageColor(canvas2.transform, "SNSAlign/Discord", new Color(1f, 0.5f, 0.8f));
				}

				Debug.Log("[Miyuki] Main menu fully styled!");
			}

			private static IEnumerator MiyukiStart(MainSceneScript instance)
			{
				instance.PadDetectObj.SetActive(false);
				instance.PadOptionObj.SetActive(false);
				yield return new WaitForFixedUpdate();

				if (UIManager.inst != null)
				{
					UIManager.inst.ForceFadeIn();
				}
				else
				{
					Debug.Log("[Miyuki] UIManager.inst is null, skipping ForceFadeIn");
				}


				instance.SceneFirst.SetActive(false);
				instance.SceneError.SetActive(false);
				instance.SceneFirst_Error.SetActive(false);

				if (SaveManager.savemanager.TempSave != null && SaveManager.savemanager.TempSave.Party.Count != 0)
				{
					instance.IsKeepPlaying = true;
					instance.StartGameText.text = ScriptLocalization.UI_Main.Continue;
				}

				GamepadManager.GetList().Clear();
				GamepadManager.IsLayoutMode = false;
				GamepadManager.CursorSpeed = 50f;

				// skip the song

				instance.Version.text = Application.version;
				instance.StartCoroutine(StartMiyukiDelay(instance));
				instance.WorkshopObj.SetActive(!GameObject.Find("StovePCSDKManager"));

				yield break;
			}

			private static IEnumerator StartMiyukiDelay(MainSceneScript instance)
			{
				yield return new WaitForFixedUpdate();
				yield return new WaitForFixedUpdate();
				yield return new WaitForFixedUpdate();
				yield return new WaitForSeconds(0.5f);

				PlaySong();

				if (SaveManager.NowData.storydata.MainStoryProgress == 0)
				{
					//MasterAudio.PlaySound("YourCustomMusicKey", 1f, null, 0f, null, null, false, false);
					instance.SceneFirst.SetActive(true);
					instance.MainCanvas.worldCamera = instance.Scene1Cam_first;
				}

				if (SaveManager.NowData.storydata.MainStoryProgress == 1)
				{
					SaveManager.NowData.storydata.MainStoryProgress = 2;
				}

				if (SaveManager.NowData.storydata.MainStoryProgress == 2)
				{
					instance.SceneError.SetActive(true);
					instance.MainCanvas.worldCamera = instance.Scene2Cam_error;
					instance.Logo.material = instance.LogoChangedMaterial;
					//MasterAudio.PlaySound("Noise 2", 0.9f, new float?(0.2f), 0f, null, null, false, false);
				}

				if (SaveManager.NowData.storydata.MainStoryProgress >= 3)
				{
					instance.SceneFirst_Error.SetActive(true);
					instance.MainCanvas.worldCamera = instance.Scene3Cam_firsterror;
					//MasterAudio.PlaySound("MainMenu", 1f, null, 0f, null, null, false, false);
				}

				instance.Cam = instance.MainCanvas.worldCamera.transform.parent.GetComponent<Animator>();
				yield break;
			}
		}

		[HarmonyPatch(typeof(PauseWindow))]
		[HarmonyPatch(nameof(PauseWindow.Start))]
		public static class Patch_PauseWindowStart_Miyuki
		{
			[HarmonyPostfix]
			public static void Postfix(PauseWindow __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated && !MiyukiData.PauseOpen) return;
				string pauseSprite = MiyukiData.PauseOpen ? $"Assets/Images/Skills/JustForYou/{MiyukiData.MiyukiArtIndex}.png" : "PauseBG";
				PlaySong();
				MiyukiVisual.Instance.StartParticlesOnTransform(__instance.transform, true, MiyukiVisual.PauseSettings);
				Transform root = __instance.transform;
				if (MiyukiData.PauseOpen) SetSprite(root, "Back", null, pauseSprite);
				else SetSprite(root, "Back", pauseSprite);
				SetSprite(root, "Main/Image", "Pause_Window");
				if (MiyukiData.PauseOpen) MakeTransparent(root, "Main/Image");
				MakeTransparent(root, "Main/Image/Image (1)");
				SetText(root, "Main/TextMeshPro Text", "Pause", 38);
				ProcessButtons(root, "Main/Image/Layout");
				ProcessRemainArea(root);
				SetImageColor(root, "Help", new Color(1f, 0.5f, 0.8f, 0.2f));
			}
		}

		[HarmonyPatch(typeof(PauseWindow))]
		[HarmonyPatch("Delete")]
		public static class PauseWindow_Delete_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(PauseWindow __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated && !MiyukiData.PauseOpen) return;
				if (MiyukiData.PauseOpen) MiyukiData.PauseOpen = false;
				//MiyukiVisual.Instance?.StopParticles();
				StopSong(false);
			}
		}

		[HarmonyPatch(typeof(Collections))]
		[HarmonyPatch("Start")]
		public static class Collections_Start_Patch
		{
			[HarmonyPostfix]
			public static void Postfix()
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				PlaySong();
			}
		}

		[HarmonyPatch(typeof(Collections))]
		[HarmonyPatch(nameof(Collections.ESC))]
		public static class Collections_Close_Patch
		{
			[HarmonyPostfix]
			public static void Postfix()
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				StopSong();
			}
		}

		[HarmonyPatch(typeof(MainOptionMenu))]
		[HarmonyPatch("Start")]
		class Patch_MainOptionMenu
		{
			[HarmonyPostfix]
			public static void Postfix(MainOptionMenu __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				Transform root = __instance.transform;
				ReplaceAllBackSprites("OptionBG");
				SetSprite(root, "Image", "Option_Window");
				MakeTransparent(root, "Image/Image (1)");
				SetText(root, "TextMeshPro Text", "Options", 38);
				ProcessButtons(root, "Image/Layout");
			}
		}

		[HarmonyPatch(typeof(MainOptionMenu))]
		[HarmonyPatch("Esc")]
		class Patch_MainOptionMenu_Esc
		{
			[HarmonyPostfix]
			public static void Postfix()
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				ReplaceAllBackSprites("PauseBG");
			}
		}

		[HarmonyPatch(typeof(GamePlayOption))]
		[HarmonyPatch("Open")]
		class Patch_GamePlayOption
		{
			[HarmonyPostfix]
			public static void Postfix(GamePlayOption __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				Transform root = __instance.transform;
				SetText(root, "TextMeshPro Text", "Game Play", 38);
				SetSprite(root, "Content", "Gameplay_Window");
				MakeTransparent(root, "Content/Back");
				ProcessToggleOptions(root, "Content/Layout");
				SetSprite(root, "Image (1)", "Gameplay_Window_Apply");
				MakeTransparent(root, "Image (1)/Image (1)");
				ProcessButtons(root, "Image (1)/Layout");
			}
		}

		[HarmonyPatch(typeof(SoundOption))]
		[HarmonyPatch("Open")]
		class Patch_SoundOption
		{
			[HarmonyPostfix]
			public static void Postfix(SoundOption __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				Transform root = __instance.transform;
				SetText(root, "TextMeshPro Text", "Sound", 38);
				SetSprite(root, "Content", "Sound_Window");
				MakeTransparent(root, "Content/Back");
				ProcessToggleOptions(root, "Content/Layout");
				SetSprite(root, "Image (1)", "Sound_Window_Apply");
				MakeTransparent(root, "Image (1)/Image (1)");
				ProcessButtons(root, "Image (1)/Layout");
			}
		}

		[HarmonyPatch(typeof(GraphicOption))]
		[HarmonyPatch("Open")]
		class Patch_GraphicOption
		{
			[HarmonyPostfix]
			public static void Postfix(GraphicOption __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				Transform root = __instance.transform;
				SetText(root, "TextMeshPro Text", "Graphic", 38);
				SetSprite(root, "Content", "Graphic_Window");
				MakeTransparent(root, "Content/Back");
				ProcessToggleOptions(root, "Content/Layout");
				SetSprite(root, "Image (1)", "Graphic_Window_Apply");
				MakeTransparent(root, "Image (1)/Image (1)");
				ProcessButtons(root, "Image (1)/Layout");
			}
		}

		[HarmonyPatch(typeof(ControlOption))]
		[HarmonyPatch("Open")]
		class Patch_ControlOption
		{
			[HarmonyPostfix]
			public static void Postfix(ControlOption __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				Transform root = __instance.transform;
				SetText(root, "KeyObj/TextMeshPro Text", "Control", 38);
				SetSprite(root, "KeyObj/Content", "Control_Window");
				MakeTransparent(root, "KeyObj/Content/Back");

				// Настройки клавиш
				Transform layout = root.Find("KeyObj/Content/Scroll View/Viewport/Content/ToggleOptions");
				if (layout != null)
				{
					ProcessKeyGroup(layout.Find("MainKeyAlign"));
					ProcessKeyGroup(layout.Find("SubKeyAlign"));
				}

				SetSprite(root, "KeyObj/Image (1)", "Control_Window_Apply");
				MakeTransparent(root, "KeyObj/Image (1)/Image (1)");
				ProcessButtons(root, "KeyObj/Image (1)/Layout");

				// Попап
				SetSprite(root, "KeyObj/KeySetPopup", "Control_Window_Pop");
				MakeTransparent(root, "KeyObj/KeySetPopup/BG");
				SetText(root, "KeyObj/KeySetPopup/BG/Desc1", null, 0, true);
				SetText(root, "KeyObj/KeySetPopup/BG/Desc2", null, 0, true);

				// Геймпад окно
				MakeTransparent(root, "PadObj/Window");
			}
		}

		[HarmonyPatch(typeof(PauseCautionWindow))]
		[HarmonyPatch("Start")]
		public static class Patch_PauseCautionWindow_Miyuki
		{
			[HarmonyPostfix]
			public static void Postfix(PauseCautionWindow __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;

				Transform root = __instance.transform;

				//SetImageColor(root, "Back (1)", new Color(0f, 0f, 0f, 0.7f));
				SetSprite(root, "Back (1)", "PauseBG");
				SetSprite(root, "Image (1)", "Option_Window");
				MakeTransparent(root, "Image (1)/Image (1)");
				SetText(root, "Image (1)/Layout/TextMeshPro Text", null, 38, true);
				ProcessButtons(root, "Image (1)/Layout");
			}
		}
	}
	#endregion
}