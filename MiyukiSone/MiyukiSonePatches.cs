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
using static MiyukiSone.MiyukiSonePatchesHelpers;
using static Spine.Unity.Examples.SpineboyFootplanter;

namespace MiyukiSone
{
	[HarmonyPatch]
	public static class Patches
	{
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(CharFace), "GetRandomSkill")]
		private static IEnumerable<CodeInstruction> CharacterUpgradeTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = instructions.ToList<CodeInstruction>();
			int num = list.FindLastIndex((CodeInstruction code) => code.opcode == OpCodes.Ldloc_0);
			list.InsertRange(num + 1, new List<CodeInstruction>
			{
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patches), nameof(MiyukiModifySkills)))
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
				{ new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patches), nameof(MiyukiModifySkills))) });
			}
			return codes.AsEnumerable();
		}

		public static List<Skill> MiyukiModifySkills(List<Skill> skills)
		{
			if (!MiyukiDecides && MiyukiSone_Plugin.MiyukiInParty()) return skills;

			UnityEngine.Debug.Log("[Miyuki] === ModifySkills START ===");

			if (skills == null || skills.Count == 0)
			{
				UnityEngine.Debug.Log("[Miyuki] skills == null or empty");
				return skills;
			}

			// Проверяем, есть ли у персонажа уже редкий скилл
			bool hasRare = skills.Any(s => s?.Master?.Info?.HasRareSkill() == true);
			if (hasRare && IsDere)
			{
				UnityEngine.Debug.Log("[Miyuki] Character already has rare skill, returning original skills");
				return skills;
			}

			UnityEngine.Debug.Log("[Miyuki] Skill count: " + skills.Count);

			var customSkillKeys = new List<string>
			{
				GDEItemKeys.Skill_S_DefultSkill_0,
				GDEItemKeys.Skill_S_DefultSkill_1,
				GDEItemKeys.Skill_S_DefultSkill_2,
				//GDEItemKeys.Skill_S_SacrificeSkill
			};

			var skillPoolKeys = IsYandere || !MiyukiSone_Plugin.MiyukiInParty() ? customSkillKeys : PlayData.ALLRARESKILLLIST.Select(s => s.KeyID).ToList();

			UnityEngine.Debug.Log("[Miyuki] Skill pool size: " + skillPoolKeys.Count);

			if (skillPoolKeys.Count == 0)
			{
				UnityEngine.Debug.Log("[Miyuki] skillPoolKeys is empty");
				return skills;
			}

			int slotIndex = RandomManager.RandomInt("MiyukiRandomIndex", 0, skills.Count);
			var oldSkill = skills[slotIndex];
			var newSkillKey = skillPoolKeys.Random("MiyukiRandomSkill");

			UnityEngine.Debug.Log($"[Miyuki] Replacing slot {slotIndex} | {oldSkill?.MySkill?.KeyID} -> {newSkillKey}");

			skills[slotIndex] = Skill.TempSkill(newSkillKey, oldSkill?.Master, oldSkill?.MyTeam);

			UnityEngine.Debug.Log("[Miyuki] === ModifySkills END ===");

			return skills;
		}

		private static bool HasRareSkill(this Character character)
		{
			if (character.BasicSkill?.SkillInfo?.Rare == true) return true;
			return character.SkillDatas?.Any(sd => sd.SkillInfo?.Rare == true) == true;
		}

		[HarmonyPatch(typeof(MainSceneScript))]
		[HarmonyPatch("Start")]
		class Patch_MainMenu_Overlay
		{
			[HarmonyPostfix]
			public static void Postfix(MainSceneScript __instance)
			{
				return; 
				// Создаем канвас если нет
				GameObject canvas = GameObject.Find("MiyukiOverlayCanvas");
				if (canvas == null)
				{
					canvas = new GameObject("MiyukiOverlayCanvas");
					Canvas cv = canvas.AddComponent<Canvas>();
					cv.renderMode = RenderMode.ScreenSpaceOverlay;
					cv.sortingOrder = 999; // поверх всего

					CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();
					scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

					canvas.AddComponent<GraphicRaycaster>();
				}

				// Создаем Image на весь экран
				GameObject overlay = new GameObject("MiyukiBackground");
				overlay.transform.SetParent(canvas.transform, false);

				Image img = overlay.AddComponent<Image>();
				img.raycastTarget = false; // чтобы не блокировать клики

				// Растягиваем на весь экран
				RectTransform rt = img.GetComponent<RectTransform>();
				rt.anchorMin = Vector2.zero;
				rt.anchorMax = Vector2.one;
				rt.offsetMin = Vector2.zero;
				rt.offsetMax = Vector2.zero;

				// Загружаем арт
				Sprite miyukiBg = UtilsUI.GetSprite("MiyukiVisual/Menu/.png");
				if (miyukiBg != null)
				{
					img.sprite = miyukiBg;
					img.color = Color.white;
					Debug.Log("[Miyuki] Fullscreen background overlay added");
				}
				else
				{
					// Если нет арта, делаем розовый фон
					//img.color = new Color(1f, 0.5f, 0.8f, 0.5f);
				}
			}
		}

		[HarmonyPatch(typeof(MainSceneScript))]
		[HarmonyPatch("Start")]
		class Patch_MainMenu
		{
			[HarmonyPostfix]
			public static void Postfix(MainSceneScript __instance)
			{
				// Находим корневые объекты
				GameObject canvas = GameObject.Find("Canvas");
				GameObject canvas2 = GameObject.Find("Canvas (2)");

				if (canvas == null) return;

				// 1. Меняем логотип
				Transform logo = canvas.transform.Find("Can/Logo");
				if (logo != null)
				{
					Image logoImg = logo.GetComponent<Image>();
					if (logoImg != null)
					{
						Sprite miyukiLogo = UtilsUI.GetSprite("MiyukiVisual/Menu/logo.png");
						if (miyukiLogo != null)
						{
							logoImg.sprite = miyukiLogo;
							Debug.Log("[Miyuki] Logo replaced");
						}
					}
				}

				// 2. Меняем текст "Press Any Key"
				Transform pressKey = canvas.transform.Find("Can/PressAnyKey/Text");
				if (pressKey != null)
				{
					TextMeshProUGUI tmp = pressKey.GetComponent<TextMeshProUGUI>();
					if (tmp != null)
					{
						//tmp.text = "Press Any Key...";
						tmp.color = new Color(1f, 0.5f, 0.8f);
						tmp.fontStyle = FontStyles.Bold;
					}
				}

				// 3. Меняем все кнопки главного меню
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

				// 4. Меняем версию
				if (canvas2 != null)
				{
					Transform version = canvas2.transform.Find("TextMeshPro Text");
					if (version != null)
					{
						TextMeshProUGUI tmp = version.GetComponent<TextMeshProUGUI>();
						if (tmp != null)
						{
							//tmp.text += " | with Miyuki";
							tmp.color = new Color(1f, 0.5f, 0.8f);
						}
					}

					// 5. Меняем цвет SNS иконок
					Transform twitter = canvas2.transform.Find("SNSAlign/Twitter");
					if (twitter != null)
					{
						Image img = twitter.GetComponent<Image>();
						if (img != null) img.color = new Color(1f, 0.5f, 0.8f);
					}

					Transform discord = canvas2.transform.Find("SNSAlign/Discord");
					if (discord != null)
					{
						Image img = discord.GetComponent<Image>();
						if (img != null) img.color = new Color(1f, 0.5f, 0.8f);
					}
				}

				Debug.Log("[Miyuki] Main menu fully styled!");
			}
		}

		[HarmonyPatch(typeof(PauseWindow))]
		[HarmonyPatch(nameof(PauseWindow.Start))]
		class Patch_PauseWindow_Miyuki
		{
			[HarmonyPostfix]
			public static void Postfix(PauseWindow __instance)
			{
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

		[HarmonyPatch(typeof(MainOptionMenu))]
		[HarmonyPatch("Start")]
		class Patch_MainOptionMenu
		{
			[HarmonyPostfix]
			public static void Postfix(MainOptionMenu __instance)
			{
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
				ReplaceAllBackSprites("MiyukiVisual/Menu/pause.png");
			}
		}

		// ==================== GAME PLAY OPTION ====================
		[HarmonyPatch(typeof(GamePlayOption))]
		[HarmonyPatch("Open")]
		class Patch_GamePlayOption
		{
			[HarmonyPostfix]
			public static void Postfix(GamePlayOption __instance)
			{
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

		// ==================== SOUND OPTION ====================
		[HarmonyPatch(typeof(SoundOption))]
		[HarmonyPatch("Open")]
		class Patch_SoundOption
		{
			[HarmonyPostfix]
			public static void Postfix(SoundOption __instance)
			{
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

		// ==================== GRAPHIC OPTION ====================
		[HarmonyPatch(typeof(GraphicOption))]
		[HarmonyPatch("Open")]
		class Patch_GraphicOption
		{
			[HarmonyPostfix]
			public static void Postfix(GraphicOption __instance)
			{
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

		// ==================== CONTROL OPTION ====================
		[HarmonyPatch(typeof(ControlOption))]
		[HarmonyPatch("Open")]
		class Patch_ControlOption
		{
			[HarmonyPostfix]
			public static void Postfix(ControlOption __instance)
			{
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