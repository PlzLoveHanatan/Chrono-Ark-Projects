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
			Adoration,     // 25+
			Love,          // 10-24
			Neutral,       // -9 - 9
			Annoyance,     // -10 - -24
			Hatred         // -25-
		}

		private const string MiyukiRandomKey = "MiyukiRandom";
		private const int MaxAffectionPoints = 25;
		private const int MinAffectionPoints = -25;
		private const int RandomChanceMultiplier = 5;

		public static bool MiyukiDecides => RandomManager.RandomPer("MiyukiBehaviour", 100, Math.Abs(MiyukiPoints * RandomChanceMultiplier + 25)) && !IsNeutral;

		public static int MiyukiPoints => MiyukiData.MiyukiAffectionPoints;

		public static bool IsAdoring => CurrentAffectionState == MiyukiAffectionState.Adoration;

		public static bool IsLoving => CurrentAffectionState == MiyukiAffectionState.Love;

		public static bool IsInLove => CurrentAffectionState == MiyukiAffectionState.Adoration || CurrentAffectionState == MiyukiAffectionState.Love;

		public static bool IsNeutral => CurrentAffectionState == MiyukiAffectionState.Neutral;

		public static bool IsAnnoyed => CurrentAffectionState == MiyukiAffectionState.Annoyance;

		public static bool IsHating => CurrentAffectionState == MiyukiAffectionState.Hatred;

		public static bool IsHostile => CurrentAffectionState == MiyukiAffectionState.Annoyance || CurrentAffectionState == MiyukiAffectionState.Hatred;

		public static MiyukiAffectionState CurrentAffectionState
		{
			get
			{
				int points = MiyukiPoints;

				if (points >= 25) return MiyukiAffectionState.Adoration;
				if (points >= 10) return MiyukiAffectionState.Love;
				if (points <= -25) return MiyukiAffectionState.Hatred;
				if (points <= -10) return MiyukiAffectionState.Annoyance;

				return MiyukiAffectionState.Neutral;
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
			CreateDialogueBox(); return;

			if (MiyukiDecides)
			{
				MiyukiAction();
			}
			else
			{
				bool isKissTriggered = RandomManager.RandomPer(MiyukiRandomKey, 100, 30);
				DialogueBoxState? dialogueState = null;
				if (isKissTriggered) dialogueState = DialogueBoxState.kiss;
				CreateDialogueBox(dialogueState);
			}
		}
	}
}
