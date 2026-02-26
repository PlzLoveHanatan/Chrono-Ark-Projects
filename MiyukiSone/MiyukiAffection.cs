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
using static MiyukiSone.DialogueBox;
using static MiyukiSone.Event;
using UnityEngine.U2D.SpriteShapeClipperLib;

namespace MiyukiSone
{
	public static class Affection
	{
		public enum MiyukiAffectionState
		{
			DereDere,     // 15+
			Kuudere,       // -9 - 9
			Yandere         // -15-
		}

		private const string MiyukiRandomKey = "MiyukiRandom";
		private const int MaxAffectionPoints = 25;
		private const int MinAffectionPoints = -25;
		private const int RandomChanceMultiplier = 5;

		public static bool MiyukiDecides => RandomManager.RandomPer("MiyukiBehaviour", 100, Math.Abs(MiyukiPoints * RandomChanceMultiplier + 25)) && !IsKuudere;

		public static int MiyukiPoints => MiyukiData.MiyukiAffectionPoints;

		public static bool IsDere => CurrentAffectionState == MiyukiAffectionState.DereDere;

		public static bool IsKuudere => CurrentAffectionState == MiyukiAffectionState.Kuudere;

		public static bool IsYandere => CurrentAffectionState == MiyukiAffectionState.Yandere;


		public static MiyukiAffectionState CurrentAffectionState
		{
			get
			{
				int points = MiyukiPoints;

				if (points >= 15) return MiyukiAffectionState.DereDere;
				if (points <= -15) return MiyukiAffectionState.Yandere;

				return MiyukiAffectionState.Kuudere;
			}
		}

		public static void ChangeAffectionPointsRandom()
		{
			int amount = RandomManager.RandomInt(MiyukiRandomKey, 0, 2);
			ChangeAffectionPoints(-amount);
		}

		public static void ChangeAffectionPoints(int amount)
		{
			MiyukiData.MiyukiAffectionPoints = Mathf.Clamp(MiyukiData.MiyukiAffectionPoints + amount, MinAffectionPoints, MaxAffectionPoints);
			Debug.Log($"Current Miyuki affection is {MiyukiPoints}");
		}

		public static void MiyukiTurn()
		{
			//CreateDialogueBox(); return;

			if (MiyukiDecides)
			{
				MiyukiAction();
			}
			else
			{
				bool isKissTriggered = RandomManager.RandomPer(MiyukiRandomKey, 100, 30) && IsDere;
				DialogueBoxState? dialogueState = null;
				if (isKissTriggered) dialogueState = DialogueBoxState.kiss;
				CreateDialogueBox(dialogueState);
			}
		}
	}
}
