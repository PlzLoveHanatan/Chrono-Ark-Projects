using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using HarmonyLib;
using UnityEngine;

namespace EmotionSystem
{
	public partial class EmotionlSystem_Plugin
	{
		[HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.EnemyTurn))]
		public class EndTurnPatch
		{
			// This patch will switch the hand back if EGO is active when turn ends
			public static IEnumerator Postfix(IEnumerator result, bool EndButton)
			{
				if (EmotionSystem_EGO_Button.instance != null && EmotionSystem_EGO_Button.instance.OpenEGOHand)
				{
					try
					{
						EmotionSystem_EGO_Button.instance.ChangeHand();
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
	}
}
