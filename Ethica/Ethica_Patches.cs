using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Ethica
{
	[HarmonyPatch]
	public class Patches
	{

		[HarmonyPatch(typeof(BuffObject), "Update")]
		public class BuffObject_Patch
		{
			[HarmonyPostfix]
			public static void PostFix(BuffObject __instance)
			{
				if (__instance.MyBuff != null && __instance.MyBuff is IP_Ethica_Buff_Object ip_BuffObject_Updata)
				{
					ip_BuffObject_Updata.Ethica_Buff_Object(__instance);
				}
			}
		}

		[HarmonyPatch(typeof(BattleSystem), "ClearBattle")]
		public class ClearBattle_Patch
		{
			public static readonly List<ItemBase> RewardList = new List<ItemBase>();

			public static readonly List<string> CommonSkills = new List<string>()
			{
				ModItemKeys.Skill_S_Ethica_Common_Automation,
				ModItemKeys.Skill_S_Ethica_Common_BelieveinYou,
				ModItemKeys.Skill_S_Ethica_Common_Catastrophe,
				ModItemKeys.Skill_S_Ethica_Common_Coordinate,
				ModItemKeys.Skill_S_Ethica_Common_DarkShackles,
				ModItemKeys.Skill_S_Ethica_Common_Discovery,
				ModItemKeys.Skill_S_Ethica_Common_DramaticEntrance,
				ModItemKeys.Skill_S_Ethica_Common_Equilibrium,
				ModItemKeys.Skill_S_Ethica_Common_Fasten,
				ModItemKeys.Skill_S_Ethica_Common_Finesse,
				ModItemKeys.Skill_S_Ethica_Common_Fisticuffs,
				ModItemKeys.Skill_S_Ethica_Common_FlashofSteel,
				ModItemKeys.Skill_S_Ethica_Common_GangUp,
				ModItemKeys.Skill_S_Ethica_Common_HuddleUp,
				ModItemKeys.Skill_S_Ethica_Common_Impatience,
				ModItemKeys.Skill_S_Ethica_Common_Intercept,
				ModItemKeys.Skill_S_Ethica_Common_JackofAllTrades,
				ModItemKeys.Skill_S_Ethica_Common_Lift,
				ModItemKeys.Skill_S_Ethica_Common_MindBlast,
				ModItemKeys.Skill_S_Ethica_Common_Omnislice,
				ModItemKeys.Skill_S_Ethica_Common_Panache,
				ModItemKeys.Skill_S_Ethica_Common_PanicButton,
				ModItemKeys.Skill_S_Ethica_Common_PrepTime,
				ModItemKeys.Skill_S_Ethica_Common_Production,
				ModItemKeys.Skill_S_Ethica_Common_Prolong,
				ModItemKeys.Skill_S_Ethica_Common_Prowess,
				ModItemKeys.Skill_S_Ethica_Common_Purity,
				ModItemKeys.Skill_S_Ethica_Common_Restlessness,
				ModItemKeys.Skill_S_Ethica_Common_SeekerStrike,
				ModItemKeys.Skill_S_Ethica_Common_Shockwave,
				ModItemKeys.Skill_S_Ethica_Common_Splash,
				ModItemKeys.Skill_S_Ethica_Common_Stratagem,
				ModItemKeys.Skill_S_Ethica_Common_TheBomb,
				ModItemKeys.Skill_S_Ethica_Common_ThinkingAhead,
				ModItemKeys.Skill_S_Ethica_Common_ThrummingHatchet,
				ModItemKeys.Skill_S_Ethica_Common_UltimateDefend,
				ModItemKeys.Skill_S_Ethica_Common_UltimateStrike,
				ModItemKeys.Skill_S_Ethica_Common_Volley,
				ModItemKeys.Skill_S_Ethica_Common_Rare_Alchemize,
				ModItemKeys.Skill_S_Ethica_Common_Rare_Anointed,
				ModItemKeys.Skill_S_Ethica_Common_Rare_BeatDown,
				ModItemKeys.Skill_S_Ethica_Common_Rare_Bolas,
				ModItemKeys.Skill_S_Ethica_Common_Rare_Calamity,
			};

			[HarmonyPostfix]
			public static void Postfix(BattleSystem __instance)
			{
				if (Utils.EthicaInParty)
				{

				}

				Debug.Log("Fight is complete, proceeding rewards");
				CommonBookRewards(__instance);
			}

			public static void CommonBookRewards(BattleSystem __instance)
			{
				Debug.Log("Common book rewards are called");

				//if (RewardList.Count > 0) RewardList.Clear();

				//RewardList.Add(ItemBase.GetItem(new GDESkillData(GDEItemKeys.Skill_S_BombClown_DropSkill)));

				RewardList.Clear();
				for (int i = 0; i < 2; i++)
				{
					RewardList.Add(ItemBase.GetItem(new GDESkillData(CommonSkills.RandomElement())));
					//RewardList.Add(ItemBase.GetItem(new GDESkillData(PlayData.PublicRandomSkill())));
				}

				__instance.Reward.AddRange(RewardList);
			}
		}


		[HarmonyPatch(typeof(FieldStore), nameof(FieldStore.Init))]
		public static class FieldStore_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(FieldStore __instance)
			{
				for (int i = 0; i < 2; i++)
				{
					__instance.StoreItems.Add(ItemBase.GetItem(new GDESkillData(ClearBattle_Patch.CommonSkills.RandomElement())));
				}
			}
		}

		[HarmonyPatch(typeof(SpecialStore), nameof(SpecialStore.Start))]
		public static class SpecialStore_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(SpecialStore __instance)
			{
				for (int i = 0; i < 2; i++)
				{
					__instance.StoreItems.Add(ItemBase.GetItem(new GDESkillData(ClearBattle_Patch.CommonSkills.RandomElement())));
				}
			}
		}
	}
}
