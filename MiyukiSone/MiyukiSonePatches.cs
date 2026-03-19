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

namespace MiyukiSone
{
	[HarmonyPatch]
	public static class MiyukiPatches
	{
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(CharFace), "GetRandomSkill")]
		private static IEnumerable<CodeInstruction> CharacterUpgradeTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = instructions.ToList<CodeInstruction>();
			int num = list.FindLastIndex((CodeInstruction code) => code.opcode == OpCodes.Ldloc_0);
			list.InsertRange(num + 1, new List<CodeInstruction>
			{
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatches), nameof(MiyukiModifySkills)))
			});
			return list.AsEnumerable<CodeInstruction>();
		}

		[HarmonyTranspiler]
		[HarmonyPatch(typeof(SkillBookCharacter), nameof(SkillBookCharacter.Use))]
		[HarmonyPatch(typeof(SkillBookCharacter_Rare), nameof(SkillBookCharacter_Rare.Use))]
		[HarmonyPatch(typeof(SkillBookInfinity), nameof(SkillBookInfinity.Use))]
		[HarmonyPatch(typeof(SkillBookSuport), nameof(SkillBookSuport.Use))]
		private static IEnumerable<CodeInstruction> SkillBookUseTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			var codes = instructions.ToList();

			// Ищем последний ldloc.0 — это обычно наш список скиллов
			int insertIndex = codes.FindLastIndex(c => c.opcode == OpCodes.Ldloc_0);

			if (insertIndex >= 0)
			{
				// Вставляем вызов MiyukiModifySkills
				codes.InsertRange(insertIndex + 1, new List<CodeInstruction>
				{ new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatches), nameof(MiyukiModifySkills))) });
			}
			return codes.AsEnumerable();
		}

		public static List<Skill> MiyukiModifySkills(List<Skill> skills)
		{
			if (skills == null || skills.Count == 0 || MiyukiSone_Plugin.MiyukiInParty() && IsKuudere) return skills;

			int randomSkill = !MiyukiSone_Plugin.MiyukiInParty() || IsYandere ? 100 : 10;
			int randomEx = !MiyukiSone_Plugin.MiyukiInParty() || IsYandere ? 100 : 25;

			if (RandomManager.RandomPer("MiyukiReplaceSkill", 100, randomSkill))
			{
				int replaceIndex = RandomManager.RandomInt("MiyukiSkillIndex", 0, skills.Count);
				ReplaceSkill(skills, replaceIndex);
			}

			if (RandomManager.RandomPer("MiyukiAddExtended", 100, randomEx))
			{
				int extendIndex = RandomManager.RandomInt("MiyukiExIndex", 0, skills.Count);
				AddExtendedToSkill(skills, extendIndex);
			}
			return skills;
		}

		private static void ReplaceSkill(List<Skill> skills, int skillIndex)
		{
			//if (skills.Any(s => s?.Master?.Info?.HasRareSkill() == true) && MiyukiActing && MiyukiSone_Plugin.MiyukiInParty()) return;

			var negSkillKeys = new List<string>
			{
				GDEItemKeys.Skill_S_DefultSkill_0,
				GDEItemKeys.Skill_S_DefultSkill_1,
				GDEItemKeys.Skill_S_DefultSkill_2,
				ModItemKeys.Skill_S_Miyuki_Special_SacrificedKnowledge
			};

			var targetSkill = skills[skillIndex];
			if (targetSkill.Master.Info.KeyData == ModItemKeys.Character_Miyuki && IsYandere) return;
			// keep random rare skills ?
			var skillPoolKeys = !MiyukiSone_Plugin.MiyukiInParty() || IsYandere ? negSkillKeys : PlayData.ALLRARESKILLLIST.Select(s => s.KeyID).ToList();
			if (negSkillKeys.Count == 0) return;
			var newSkillKey = skillPoolKeys.Random("MiyukiRandomRareSkill");
			UnityEngine.Debug.Log($"[Miyuki] Replacing slot {skillIndex} | {targetSkill?.MySkill?.KeyID} -> {newSkillKey}");
			skills[skillIndex] = Skill.TempSkill(newSkillKey, targetSkill?.Master, targetSkill?.MyTeam);
		}

		private static void AddExtendedToSkill(List<Skill> skills, int skillIndex)
		{
			var targetSkill = skills[skillIndex];
			if (targetSkill.Master.Info.KeyData == ModItemKeys.Character_Miyuki && IsYandere) return;

			var skillData = targetSkill?.CharinfoSkilldata;

			if (skillData != null && skillData.SKillExtended == null)
			{
				if (MiyukiDecides && !IsYandere)
				{
					targetSkill.CelestialUpgrade();
				}
				else
				{
					List<Skill_Extended> upgrades = !MiyukiSone_Plugin.MiyukiInParty() || IsYandere ? PlayData.GetEnforce(true, targetSkill) : PlayData.GetEnforce(false, targetSkill);
					if (upgrades != null && upgrades.Count > 0) targetSkill.ExtendedAdd_Battle(upgrades.Random("RandomUpgrade"));
				}
			}
			else
			{
				Debug.Log($"Can't add Extended to skill {targetSkill.MySkill.KeyID}");
			}
		}

		private static bool HasRareSkill(this Character character)
		{
			if (character.BasicSkill?.SkillInfo?.Rare == true) return true;
			return character.SkillDatas?.Any(sd => sd.SkillInfo?.Rare == true) == true;
		}

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

				SetSprite(canvas.transform, "Can/Logo", "MiyukiVisual/Menu/logo.png");
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
		class Patch_PauseWindow_Miyuki
		{
			[HarmonyPostfix]
			public static void Postfix(PauseWindow __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				PlaySong();
				MiyukiVisual.Instance.StartParticlesOnTransform(__instance.transform, true, MiyukiVisual.PauseSettings);
				Transform root = __instance.transform;
				SetSprite(root, "Back", "MiyukiVisual/Menu/pause.png");
				SetSprite(root, "Main/Image", "MiyukiVisual/Menu/pause_window.png");
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
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				MasterAudio.StopBus("BGM");
				MasterAudio.FadeBusToVolume("BattleBGM", 1f, 0.5f);
				MasterAudio.FadeBusToVolume("BGM", 1f, 0.5f);
				MasterAudio.FadeBusToVolume("FieldBGM", 1f, 0.5f);
				MiyukiVisual.Instance.StopParticles();
				if (MiyukiData.BGMVolumeIncreased) ChangeSettingsVolume(0);
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
				MasterAudio.StopBus("BGM");
				MasterAudio.FadeBusToVolume("BattleBGM", 1f, 0.5f);
				MasterAudio.FadeBusToVolume("BGM", 1f, 0.5f);
				MasterAudio.FadeBusToVolume("FieldBGM", 1f, 0.5f);
				if (MiyukiData.BGMVolumeIncreased) ChangeSettingsVolume(0);

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
				ReplaceAllBackSprites("MiyukiVisual/Menu/option.png");
				SetSprite(root, "Image", "MiyukiVisual/Menu/option_window.png");
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
				ReplaceAllBackSprites("MiyukiVisual/Menu/pause.png");
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
				SetSprite(root, "Content", "MiyukiVisual/Menu/gameplay_window.png");
				MakeTransparent(root, "Content/Back");
				ProcessToggleOptions(root, "Content/Layout");
				SetSprite(root, "Image (1)", "MiyukiVisual/Menu/gameplay_window_apply.png");
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
				SetSprite(root, "Content", "MiyukiVisual/Menu/sound_window.png");
				MakeTransparent(root, "Content/Back");
				ProcessToggleOptions(root, "Content/Layout");
				SetSprite(root, "Image (1)", "MiyukiVisual/Menu/sound_window_apply.png");
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
				SetSprite(root, "Content", "MiyukiVisual/Menu/graphic_window.png");
				MakeTransparent(root, "Content/Back");
				ProcessToggleOptions(root, "Content/Layout");
				SetSprite(root, "Image (1)", "MiyukiVisual/Menu/graphic_window_apply.png");
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
				SetSprite(root, "KeyObj/Content", "MiyukiVisual/Menu/control_window.png");
				MakeTransparent(root, "KeyObj/Content/Back");

				// Настройки клавиш
				Transform layout = root.Find("KeyObj/Content/Scroll View/Viewport/Content/ToggleOptions");
				if (layout != null)
				{
					ProcessKeyGroup(layout.Find("MainKeyAlign"));
					ProcessKeyGroup(layout.Find("SubKeyAlign"));
				}

				SetSprite(root, "KeyObj/Image (1)", "MiyukiVisual/Menu/control_window_apply.png");
				MakeTransparent(root, "KeyObj/Image (1)/Image (1)");
				ProcessButtons(root, "KeyObj/Image (1)/Layout");

				// Попап
				SetSprite(root, "KeyObj/KeySetPopup", "MiyukiVisual/Menu/control_window_pop.png");
				MakeTransparent(root, "KeyObj/KeySetPopup/BG");
				SetText(root, "KeyObj/KeySetPopup/BG/Desc1", null, 0, true);
				SetText(root, "KeyObj/KeySetPopup/BG/Desc2", null, 0, true);

				// Геймпад окно
				MakeTransparent(root, "PadObj/Window");
			}
		}
	}
}