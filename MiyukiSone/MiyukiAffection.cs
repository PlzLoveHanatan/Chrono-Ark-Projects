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
using static MiyukiSone.EventAction;
using UnityEngine.U2D.SpriteShapeClipperLib;

namespace MiyukiSone
{
	public static class Affection
	{
		public enum MiyukiAffectionState
		{
			adoration,  // 20+
			love,       // 10 -> 19
			indifference, // -9 -> 9
			hate,       // -10 -> -19
			eradication // -20-
		}

		private const string MiyukiRandom = "MiyukiRandom";

		private const int Adoration_Threshold = 20;
		private const int Love_Threshold = 10;
		private const int Hate_Threshold = -10;
		private const int Eradication_Threshold = -20;
		private const int Max_Affection = 20;
		private const int Min_Affection = -20;
		private const int Random_Chance = 5;

		public static int MiyukiPoints => MiyukiData.MiyukiAffectionPoints;

		public static MiyukiAffectionState CurrentAffection
		{
			get
			{
				int points = MiyukiPoints;

				if (points >= 20) return MiyukiAffectionState.adoration;
				if (points >= 10) return MiyukiAffectionState.love;				
				if (points <= -10) return MiyukiAffectionState.hate;
				if (points <= -20) return MiyukiAffectionState.eradication;

				return MiyukiAffectionState.indifference;
			}
		}

		public static void ChangePointsRandom()
		{
			int amount = RandomManager.RandomInt(MiyukiRandom, 0, 2);
			ChangePoints(-amount);
		}

		public static void ChangePoints(int amount)
		{
			MiyukiData.MiyukiAffectionPoints = Mathf.Clamp(MiyukiData.MiyukiAffectionPoints + amount, Min_Affection, Max_Affection);
			Debug.Log($"Current Miyuki affection is {MiyukiPoints}");
		}

		public static void MiyukiTurn()
		{
			//CreateDialogueBox(); return;

			bool alwaysLucky = RandomManager.RandomPer(MiyukiRandom, 100, 50);

			if (alwaysLucky && CurrentAffection != MiyukiAffectionState.indifference)
			{
				MiyukiAction();

				//switch (CurrentAffection)
				//{
				//	case MiyukiAffectionState.adoration: AdorationAction(5); break;
				//	case MiyukiAffectionState.eradication: EradicationAction(3); break;
				//	default: CreateDialogueBox();  break;
				//}
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
