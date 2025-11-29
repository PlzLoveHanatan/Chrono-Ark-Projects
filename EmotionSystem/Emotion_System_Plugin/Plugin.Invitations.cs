using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace EmotionSystem
{
	public partial class EmotionlSystem_Plugin
	{
		[HarmonyPatch(typeof(BattleSystem), "BattleStart")]
		public class BossRushWavePatch
		{
			public static IEnumerator Postfix(IEnumerator __result, BattleSystem __instance)
			{
				while (__result.MoveNext())
				{
					yield return __result.Current;
				}

				if (Utils.DistortedBosses && !Utils.BossInvitations)
				{
					BattleSystem.instance.MainQueueData.CustomeFogTurn = (int)(BattleSystem.instance.FogTurn * 1.3f);
				}

				if (Utils.BossInvitations)
				{
					InvitationManager.Instance.SendReception(__instance);
				}
			}
		}


		[HarmonyPatch(typeof(BattleSystem), "ClearBattle")]
		public class BossRushRewardPatch
		{
			public static void Postfix(BattleSystem __instance)
			{
				if (Utils.BossInvitations && __instance.BossBattle)
				{
					if (InvitationManager.Instance.InvitationActive)
					{
						InvitationManager.Instance.PrepareReceptionRewards(InvitationManager.Instance.RewardMultiplier);
						__instance.Reward.AddRange(InvitationManager.Instance.ReceptionRewards);
						InvitationManager.Instance.InvitationActive = false;
					}
				}
			}
		}

		[HarmonyPatch(typeof(BloodyMist), "BattleEnd")]
		public static class WhiteGrave
		{
			[HarmonyPrefix]
			public static bool Prefix(BloodyMist __instance)
			{
				if (Utils.BossInvitations)
				{
					if (InvitationManager.Instance.SpecialCase)
					{
						FieldSystem.instance.BattleAfterDelegate = new FieldSystem.BattleAfterDel(InvitationManager.Instance.DoubleBattle);
						InvitationManager.Instance.SpecialCase = false;
						InvitationManager.Instance.SpecialReward = false;
						__instance.RuleChange.Shuffle = false;

						foreach (var ally in PlayData.TSavedata.Party)
						{
							if (ally.Incapacitated)
							{
								ally.Incapacitated = false;
								ally.Hp = 1;
								ally.Hp = (int)(ally.get_stat.maxhp * 0.5);
							}
						}
						//__instance.Level4DoubleBoss = true;
					}
					return false; // skip original BattleEnd
				}
				return true; // run original method
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
	}
}
