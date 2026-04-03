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
		#region Data & Constructor
		private static readonly string RelativePath = "Assets/Images/Ui/";

		private static List<Skill> _lucyCurseSkills;
		private static List<Skill> LucyCurseSkills
		{
			get
			{
				if (_lucyCurseSkills == null)
				{
					_lucyCurseSkills = new List<Skill>()
					{
						Skill.TempSkill(GDEItemKeys.Skill_S_LucyCurse_Banana, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam),
						Skill.TempSkill(GDEItemKeys.Skill_S_LucyCurse_CursedClock, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam),
						Skill.TempSkill(GDEItemKeys.Skill_S_LucyCurse_Heavy, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam),
						Skill.TempSkill(GDEItemKeys.Skill_S_LucyCurse_Late, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam),
					};
				}
				return _lucyCurseSkills;
			}
		}

		private static readonly List<string> CharacterBadSkillKeys = new List<string>
		{
			GDEItemKeys.Skill_S_DefultSkill_0,
			GDEItemKeys.Skill_S_DefultSkill_1,
			GDEItemKeys.Skill_S_DefultSkill_2,
			ModItemKeys.Skill_S_Miyuki_Special_SacrificedKnowledge,
			ModItemKeys.Skill_S_Miyuki_Special_Yabeley
		};
		#endregion

		[HarmonyTranspiler]
		[HarmonyPatch(typeof(CharFace), nameof(CharFace.GetRandomSkill))]
		[HarmonyPatch(typeof(CharStatV4), nameof(CharStatV4.ReturnLucyDrawCard))]
		private static IEnumerable<CodeInstruction> CharacterUpgradeTranspiler(IEnumerable<CodeInstruction> instructions, MethodBase original)
		{
			var codes = instructions.ToList();
			int index = codes.FindLastIndex(c => c.opcode == OpCodes.Ldloc_0);

			if (index != -1)
			{
				MethodInfo targetMethod;
				if (original.DeclaringType == typeof(CharFace)) targetMethod = AccessTools.Method(typeof(MiyukiPatches), nameof(ReplaceCharacterSkill));
				else targetMethod = AccessTools.Method(typeof(MiyukiPatches), nameof(ReplaceLucySkill));
				codes.Insert(index + 1, new CodeInstruction(OpCodes.Call, targetMethod));
			}

			return codes;
		}

		[HarmonyTranspiler]
		[HarmonyPatch(typeof(SkillBookCharacter), nameof(SkillBookCharacter.Use))]
		[HarmonyPatch(typeof(SkillBookCharacter_Rare), nameof(SkillBookCharacter_Rare.Use))]
		[HarmonyPatch(typeof(SkillBookInfinity), nameof(SkillBookInfinity.Use))]
		[HarmonyPatch(typeof(SkillBookSuport), nameof(SkillBookSuport.Use))]
		private static IEnumerable<CodeInstruction> SkillBookUseTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			var codes = instructions.ToList();
			int insertIndex = codes.FindLastIndex(c => c.opcode == OpCodes.Ldloc_0);
			if (insertIndex >= 0) codes.Insert(insertIndex + 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatches), nameof(ReplaceCharacterSkill))));
			return codes.AsEnumerable();
		}

		public static List<Skill> ReplaceCharacterSkill(List<Skill> skills)
		{
			return ReplaceSkills(skills, true);
		}

		public static List<Skill> ReplaceLucySkill(List<Skill> skills)
		{
			return ReplaceSkills(skills);
		}

		public static List<Skill> ReplaceSkills(List<Skill> skills, bool? isCharacterDraw = null)
		{
			if (IsKuudere) return skills;

			if (isCharacterDraw.HasValue)
			{
				if (MiyukiDecides) CharacterSkill(skills, RandomManager.RandomInt("SkillIndex", 0, skills.Count));
				if (MiyukiDecides) ApplyUpgrade(skills, RandomManager.RandomInt("ExIndex", 0, skills.Count));
			}
			else
			{
				if (MiyukiDecides) LucySkill(skills, RandomManager.RandomInt("LucySkillIndex", 0, skills.Count));
				//if (MiyukiDecides) ApplyUpgrade(skills, RandomManager.RandomInt("LucyExIndex", 0, skills.Count));
			}
			return skills;
		}

		private static void LucySkill(List<Skill> skills, int skillIndex)
		{
			var skill = (IsYandere ? LucyCurseSkills : PlayData.GetLucySkill(false)).RandomElement("RandomLucySkill");
			skills[skillIndex] = skill;
		}

		private static void CharacterSkill(List<Skill> skills, int skillIndex)
		{
			var targetSkill = skills[skillIndex];
			if (targetSkill?.Master?.Info?.KeyData == ModItemKeys.Character_Miyuki /*&& IsYandere*/) return;
			var normalSkills = PlayData.ALLSKILLLIST.Where(s => s.Category.Key != GDEItemKeys.SkillCategory_DefultSkill && s.User != "" && s.Category.Key != GDEItemKeys.SkillCategory_LucySkill).Select(s => s.KeyID);
			var rareSkills = PlayData.ALLRARESKILLLIST.Where(s => s.User != ModItemKeys.Character_Miyuki).Select(s => s.KeyID);
			var skillList = (IsYandere) ? CharacterBadSkillKeys : (MiyukiDecides ? normalSkills : rareSkills).ToList();
			if (skillList.Count > 0) skills[skillIndex] = Skill.TempSkill(skillList.RandomElement("RandomCharacterSkill"), targetSkill.Master, targetSkill.MyTeam);
		}

		private static void ApplyUpgrade(List<Skill> skills, int skillIndex)
		{
			var targetSkill = skills[skillIndex];
			if (targetSkill?.Master?.Info?.KeyData == ModItemKeys.Character_Miyuki && IsYandere) return;
			if (targetSkill?.CharinfoSkilldata?.SKillExtended != null) return;
			(MiyukiDecides && MiyukiInParty ? (Action)(() => targetSkill.CelestialUpgrade()) : () => targetSkill.NormalUpgrade())();
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
		[HarmonyPatch(typeof(CharStatV4))]
		[HarmonyPatch("OnEnable")]
		public static class CharStatV4_OnEnable_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(CharStatV4 __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated) return;
				MiyukiVisual.Instance.StartParticlesOnTransform(__instance.transform, true, MiyukiVisual.PauseSettings);
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
				MiyukiVisual.Instance.StopParticles();
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

				MiyukiVisual.Instance.StartParticlesOnTransform(__instance.transform, true, MiyukiVisual.PauseSettings);
				SetSprite(canvas.transform, "Can/Logo", RelativePath + "Logo");
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
		public static class Patch_PauseWindow_Miyuki
		{
			[HarmonyPostfix]
			public static void Postfix(PauseWindow __instance)
			{
				if (!MiyukiSaveManager.Instance.CurrentData.GameUpdated && !MiyukiData.PauseOpen) return;

				string pauseSprite = MiyukiData.PauseOpen ? $"Assets/Images/Skills/JustForYou/{MiyukiData.MiyukiArtIndex}" : RelativePath + "PauseBG";
				PlaySong();
				MiyukiVisual.Instance.StartParticlesOnTransform(__instance.transform, true, MiyukiVisual.PauseSettings);
				Transform root = __instance.transform;
				SetSprite(root, "Back", pauseSprite);
				SetSprite(root, "Main/Image", RelativePath + "Pause_Window");
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
				MiyukiVisual.Instance.StopParticles();
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
				ReplaceAllBackSprites(RelativePath + "OptionBG");
				SetSprite(root, "Image", RelativePath + "Option_Window");
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
				ReplaceAllBackSprites(RelativePath + "PauseBG");
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
				SetSprite(root, "Content", RelativePath + "Gameplay_Window");
				MakeTransparent(root, "Content/Back");
				ProcessToggleOptions(root, "Content/Layout");
				SetSprite(root, "Image (1)", RelativePath + "Gameplay_Window_Apply");
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
				SetSprite(root, "Content", RelativePath + "Sound_Window");
				MakeTransparent(root, "Content/Back");
				ProcessToggleOptions(root, "Content/Layout");
				SetSprite(root, "Image (1)", RelativePath + "Sound_Window_Apply");
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
				SetSprite(root, "Content", RelativePath + "Graphic_Window");
				MakeTransparent(root, "Content/Back");
				ProcessToggleOptions(root, "Content/Layout");
				SetSprite(root, "Image (1)", RelativePath + "Graphic_Window_Apply");
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
				SetSprite(root, "KeyObj/Content", RelativePath + "Control_Window");
				MakeTransparent(root, "KeyObj/Content/Back");

				// Настройки клавиш
				Transform layout = root.Find("KeyObj/Content/Scroll View/Viewport/Content/ToggleOptions");
				if (layout != null)
				{
					ProcessKeyGroup(layout.Find("MainKeyAlign"));
					ProcessKeyGroup(layout.Find("SubKeyAlign"));
				}

				SetSprite(root, "KeyObj/Image (1)", RelativePath + "Control_Window_Apply");
				MakeTransparent(root, "KeyObj/Image (1)/Image (1)");
				ProcessButtons(root, "KeyObj/Image (1)/Layout");

				// Попап
				SetSprite(root, "KeyObj/KeySetPopup", RelativePath + "Control_Window_Pop");
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
				SetSprite(root, "Back (1)", RelativePath + "PauseBG");
				SetSprite(root, "Image (1)", RelativePath + "Option_Window");
				MakeTransparent(root, "Image (1)/Image (1)");
				SetText(root, "Image (1)/Layout/TextMeshPro Text", null, 38, true);
				ProcessButtons(root, "Image (1)/Layout");
			}
		}
	}
}