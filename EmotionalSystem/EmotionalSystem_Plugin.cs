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
using EmotionalSystem;
using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;
using ChronoArkMod.ModData.Settings;
using System.Text;
using static EmotionalSystem.LibraryFloor;
using TileTypes;
using System.ComponentModel;
using System.Reflection.Emit;
using TMPro;
using JetBrains.Annotations;
namespace EmotionalSystem
{
	public class EmotionalSystem_Plugin : ChronoArkPlugin
	{
		public const string modname = "EmotionalSystem";

		public const string version = "1.0";

		public const string author = "Midana && surprise4u";

		public static ModInfo ThisMod => ModManager.getModInfo(modname);

		[HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.EnemyTurn))]
		public class EndTurnPatch
		{
			// This patch will switch the hand back if EGO is active when turn ends
			public static IEnumerator Postfix(IEnumerator result, bool EndButton)
			{
				if (EmotionalSystem_EGO_Button.instance != null && EmotionalSystem_EGO_Button.instance.ActiveEGOHand)
				{
					try
					{
						EmotionalSystem_EGO_Button.instance.ChangeHand();
					}
					catch { }
					yield return new WaitForSecondsRealtime(0.2f);
				}
				while (result.MoveNext())
				{
					yield return result.Current;
				}
			}
		}

		Harmony harmony = new Harmony("EmotionalSystem");

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
				Debug.Log("EmotionalSystem: Patch Catch: " + e.ToString());
			}
		}

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
				   key == GDEItemKeys.Stage_Stage3_Camp;
		}

		[HarmonyPatch(typeof(ResultUI))]
		[HarmonyPatch(nameof(ResultUI.Init))]
		class ResultScreenPatch
		{
			[HarmonyPostfix]
			static void Postfix(ResultUI __instance)
			{
				if (PlayData.TSavedata.bMist == null)
				{
					PlayData.TSavedata.bMist = new BloodyMist();
					PlayData.TSavedata.bMist.Level = 4;
					PlayData.TSavedata.bMist.Level3_Option = 0;

					__instance.DifficultyObj.SetActive(true);
					__instance.BloodyMistObj.SetActive(true);

					Sprite sprite = AddressableLoadManager.LoadAsyncCompletion<Sprite>(
						new GDEImageDatasData(GDEItemKeys.ImageDatas_Image_BloodyMist).Sprites_Path[3],
						AddressableLoadManager.ManageType.Stage
					);
					__instance.BloodyMistImage.sprite = sprite;

					__instance.BloodyMistText.text = "";
				}

				StringBuilder sb = new StringBuilder();

				if (Utils.AllyEmotions)
				{
					sb.AppendLine("+" + ModLocalization.EmotionalSystem_EmotionsAlly);
				}

				if (Utils.EnemyEmotions)
				{
					sb.AppendLine("+" + ModLocalization.EmotionalSystem_EmotionsEnemy);
				}

				if (Utils.BossInvitations)
				{
					sb.AppendLine("+" + ModLocalization.EmotionalSystem_Boss_Invitations);
				}

				string floorName = GetLibraryFloor();
				if (CurrentFloor != null && !string.IsNullOrEmpty(floorName))
				{
					sb.AppendLine("+" + floorName);
				}

				string originalText = __instance.BloodyMistText.text;
				__instance.BloodyMistText.text = originalText + "\n" + ModLocalization.EmotionalSystem + "\n" + sb.ToString();
			}

			public static string GetLibraryFloor()
			{
				switch (CurrentFloorType)
				{
					case LibraryFloorType.History:
						return ModLocalization.EmotionalSystem_Floor_History;
					case LibraryFloorType.Technological:
						return ModLocalization.EmotionalSystem_Floor_Technological;
					default:
						return "";
				}
			}
		}

		[HarmonyPatch(typeof(BuffObject))]
		public class BuffObjectUpdatePlugin
		{
			[HarmonyPatch("Update")]
			[HarmonyPostfix]
			public static void Update_Patch(BuffObject __instance)
			{
				if (__instance.MyBuff != null)
				{
					IP_BuffObject_Updata ip_BuffObject_Updata = __instance.MyBuff as IP_BuffObject_Updata;
					if (ip_BuffObject_Updata != null)
					{
						ip_BuffObject_Updata.BuffObject_Updata(__instance);
					}
				}
			}
		}


		[HarmonyPatch(typeof(EventBattle_TrialofTime))]
		[HarmonyPatch(nameof(EventBattle_TrialofTime.BattleStartUIOnBefore))]
		class TimeTrialPatch
		{
			[HarmonyPostfix]
			static void Postfix()
			{
				if (Utils.EnemyEmotions)
				{
					float timer = 1.3f;

					if (Utils.BossInvitations)
					{
						timer = 1.9f;
					}

					PlayData.TSavedata.Timer *= timer;
				}
			}
		}

		[HarmonyPatch(typeof(BattleSystem), "BattleStart")]
		public class BossRushWavePatch
		{
			public static IEnumerator Postfix(IEnumerator __result, BattleSystem __instance)
			{
				while (__result.MoveNext())
				{
					yield return __result.Current;
				}

				if (Utils.BossInvitations)
				{
					InvitationManager.Instance.ApplyReception(__instance);
				}
			}
		}


		[HarmonyPatch(typeof(BattleSystem), "ClearBattle")]
		public class BossRushRewardPatch
		{
			public static void Postfix(BattleSystem __instance)
			{
				if (Utils.BossInvitations)
				{
					var instance = InvitationManager.Instance;

					if (instance.InvitationActive)
					{
						int rewardsNum = instance.RewardMultiplier;
						instance.PrepareReceptionRewards(rewardsNum);
						var rewards = instance.ReceptionRewards;
						__instance.Reward.AddRange(rewards);
						instance.InvitationActive = false;
					}
				}
			}
		}


		[HarmonyPatch(typeof(BloodyMist), "DoubleBattle")]
		public static class WhiteGrave
		{
			[HarmonyPrefix]
			public static bool Prefix(BloodyMist __instance)
			{
				if (Utils.BossInvitations)
				{
					__instance.Level4DoubleBoss = true;

					var instance = InvitationManager.Instance;
					List<ItemBase> items = new List<ItemBase>();

					if (InvitationManager.Instance.SpecialCase)
					{
						InvitationManager.Instance.SpecialCase = false;
						instance.PrepareReceptionRewards(3);
						var rewards = instance.ReceptionRewards;
						items.AddRange(rewards);
						InvitationManager.Instance.StartNewReception(GDEItemKeys.EnemyQueue_Queue_S3_Reaper);
					}

					if (items.Count > 0)
					{
						InventoryManager.Reward(items);
					}
					
					return false;
				}
				return true;
			}
		}

		[HarmonyPatch(typeof(Buff), nameof(Buff.SelfDestroy))]
		public static class ReceptionCleaing
		{
			public static void Postfix(Buff __instance)
			{
				if (Utils.BossInvitations)
				{
					var buffType = __instance.GetType();

					if (InvitationManager.Instance.ReceptionCleaning.TryGetValue(buffType, out var action))
					{
						action?.Invoke();
					}
				}
			}
		}

		[HarmonyPatch(typeof(P_AllyDoll), "DeadResist")]
		public static class Patch_AllyDoll_DeadResist
		{
			public static bool Prefix(ref bool __result)
			{
				if (InvitationManager.Instance.CanKillBurningStake)
				{
					__result = false;
					return false;
				}
				return true;
			}
		}

		[HarmonyPatch(typeof(BattleTeam), "CheckFullHand")]
		public static class BattleTeam_CheckFullHand_Patch
		{
			// Твоё новое значение лимита
			public static int customHandLimit = Utils.AdditionalSkill ? 11 : 10;
			public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
			{
				var codes = new List<CodeInstruction>(instructions);
				for (int i = 0; i < codes.Count; i++)
				{
					// Ищем инструкцию, которая загружает константу 10 (ldc.i4.s 10 или ldc.i4 10)
					if ((codes[i].opcode == OpCodes.Ldc_I4_S && (sbyte)codes[i].operand == 10) ||
						(codes[i].opcode == OpCodes.Ldc_I4 && (int)codes[i].operand == 10))
					{
						// Заменяем на нужное значение
						codes[i] = new CodeInstruction(OpCodes.Ldc_I4, customHandLimit);
					}
				}
				return codes.AsEnumerable();
			}
		}

		[HarmonyPatch(typeof(SkillWindow), "Awake")]
		public static class SkillWindow_Awake_Patch
		{
			public static void Postfix(SkillWindow __instance)
			{
				if (__instance != null && Utils.AdditionalSkill)
				{
					RectTransform rt = __instance.GetComponent<RectTransform>();
					if (rt != null)
					{
						// Уменьшаем весь SkillWindow
						rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y * 0.95f);
						//rt.localScale = new Vector3(1f, 0.9f, 1f);
					}
				}
			}
		}

		//[HarmonyPatch(typeof(SkillButton), "InputData")]
		//public static class SkillButton_InputData_Patch
		//{
		//	public static void Postfix(SkillButton __instance)
		//	{
		//		RectTransform rt = __instance.GetComponent<RectTransform>();
		//		if (rt != null)
		//		{
		//			// Уменьшаем масштаб
		//			rt.localScale = new Vector3(0.8f, 0.8f, 0.8f);

		//			// Сдвигаем кнопку вниз относительно её индекса в родителе
		//			int index = rt.GetSiblingIndex();
		//			rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -index * 40f);
		//			// 40f можно менять под свой размер кнопки
		//		}
		//	}
		//}
	}
}