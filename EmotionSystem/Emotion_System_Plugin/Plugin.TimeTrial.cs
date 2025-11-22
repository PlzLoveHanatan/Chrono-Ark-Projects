using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace EmotionSystem
{
	public partial class EmotionlSystem_Plugin
	{
		[HarmonyPatch(typeof(EventBattle_TrialofTime))]
		[HarmonyPatch(nameof(EventBattle_TrialofTime.BattleStartUIOnBefore))]
		class TimeTrialPatch
		{
			[HarmonyPostfix]
			static void Postfix()
			{
				// локальные добавки к таймеру
				float add_GuestEmotions = 0.5f;
				float add_BossInvitations = 1.0f;
				float add_DistortedBosses = 1.0f;

				// стартовое значение
				float TimeRate = 1f;

				// если флаг включён — добавляем
				if (Utils.GuestEmotions)
				{
					TimeRate += add_GuestEmotions;
				}

				if (Utils.BossInvitations)
				{
					TimeRate += add_BossInvitations;
				}

				if (Utils.DistortedBosses)
				{
					TimeRate += add_DistortedBosses;
				}

				PlayData.TSavedata.Timer *= TimeRate;
			}
		}
	}
}
