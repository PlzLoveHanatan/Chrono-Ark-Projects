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
				if (Utils.GuestEmotions)
				{
					float timer = 1.3f;

					if (Utils.BossInvitations)
					{
						timer = 1.9f;

						if (Utils.DistortedBosses)
						{
							timer = 2.6f;
						}
					}

					PlayData.TSavedata.Timer *= timer;
				}
			}
		}
	}
}
