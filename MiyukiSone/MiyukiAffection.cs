using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.DialogueBoxData;
using static MiyukiSone.EventData;
using static MiyukiSone.EventStringLoader;
using static MiyukiSone.DialogueBox;
using static MiyukiSone.EventBattle;
using UnityEngine.U2D.SpriteShapeClipperLib;

namespace MiyukiSone
{
	public static class Affection
	{
		public enum MiyukiAffectionState
		{
			adoration,  // 25+
			love,       // 10 -> 24
			indifference, // -9 -> 9
			hate,       // -10 -> -24
			eradication // -25-
		}

		private const string MiyukiRandom = "MiyukiRandom";
		private const int Max_Affection = 25;
		private const int Min_Affection = -25;
		private const int Random_Chance = 5;

		public static bool MiyukiActing => RandomManager.RandomPer("MiyukiBehaviour", 100, Math.Abs(MiyukiPoints)) && !IsIndifferent;

		public static int MiyukiPoints => MiyukiData.MiyukiAffectionPoints;
		public static bool IsLoving => CurrentAffection == MiyukiAffectionState.love || CurrentAffection == MiyukiAffectionState.adoration;
		public static bool IsHating => CurrentAffection == MiyukiAffectionState.hate || CurrentAffection == MiyukiAffectionState.eradication;
		public static bool IsIndifferent => CurrentAffection == MiyukiAffectionState.indifference;
		public static bool IsExtremeAffection => CurrentAffection == MiyukiAffectionState.adoration || CurrentAffection == MiyukiAffectionState.eradication;

		public static MiyukiAffectionState CurrentAffection
		{
			get
			{
				int points = MiyukiPoints;

				if (points >= 25) return MiyukiAffectionState.adoration;
				if (points >= 10) return MiyukiAffectionState.love;
				if (points <= -25) return MiyukiAffectionState.eradication;
				if (points <= -10) return MiyukiAffectionState.hate;

				return MiyukiAffectionState.indifference;
			}
		}

		public static void ChangeAffectionPointsRandom()
		{
			int amount = RandomManager.RandomInt(MiyukiRandom, 0, 2);
			ChangeAffectionPoints(-amount);
		}

		public static void ChangeAffectionPoints(int amount)
		{
			MiyukiData.MiyukiAffectionPoints = Mathf.Clamp(MiyukiData.MiyukiAffectionPoints + amount, Min_Affection, Max_Affection);
			Debug.Log($"Current Miyuki affection is {MiyukiPoints}");
		}

		public static void MiyukiTurn()
		{
			//CreateDialogueBox(DialogueBoxState.kiss); return;

			bool alwaysLucky = RandomManager.RandomPer(MiyukiRandom, 100, Math.Abs(MiyukiPoints * Random_Chance + 25));

			if (alwaysLucky)
			{
				if (!IsIndifferent)
				{
					MiyukiAction();
				}
				else
				{
					DialogueBoxState? state = null;
					if (CurrentAffection == MiyukiAffectionState.adoration && alwaysLucky) state = DialogueBoxState.kiss;
					CreateDialogueBox(state);
				}
			}
		}		
	}
}
