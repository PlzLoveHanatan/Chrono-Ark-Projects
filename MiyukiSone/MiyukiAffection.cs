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
	public enum MiyukiAffectionState
	{
		DereDere,     // 15+
		Kuudere,       // -9 - 9
		Yandere         // -15-
	}

	public static class Affection
	{
		private const string MiyukiRandomKey = "MiyukiRandom";
		private const int MaxAffectionPoints = 25;
		private const int MinAffectionPoints = -25;
		private const int RandomChanceMultiplier = 5;
		public static int MiyukiPoints => MiyukiData.MiyukiAffectionPoints;

		public static int MiyukiOutcome(int baseValue)
		{
			if (MiyukiDecides) return 0;

			if (IsKuudere)
			{
				return MiyukiPoints >= 0 ? baseValue : -baseValue;
			}
			else if (IsDere || IsYandere)
			{
				int result = IsDere ? baseValue : -baseValue;

				if (MiyukiPoints == MaxAffectionPoints || MiyukiPoints == MinAffectionPoints)
				{
					int bonusChance = Mathf.Clamp(Math.Abs(MiyukiPoints) * RandomChanceMultiplier + 25, 0, 100);
					bool isDouble = RandomManager.RandomPer(IsDere ? "DereBonus" : "YandereBonus", 100, bonusChance);
					if (isDouble) return result * 2;
				}

				return result;
			}
			return 0;
		}

		public static bool MiyukiDecides => RandomManager.RandomPer("MiyukiBehaviour", 100, Math.Abs(MiyukiPoints * RandomChanceMultiplier + 25));

		public static bool MiyukiMood => IsDere || (IsKuudere && MiyukiPoints >= 0);

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
			Debug.Log($"[Miyuki] Affection points changed by {amount}. Current: {MiyukiPoints}");

			foreach (var skill in BattleSystem.instance.AllyTeam.Skills)
			{
				foreach (var ex in skill.AllExtendeds)
				{
					if (ex is IP_MiyukiSkillImgChange handler)
					{
						handler.SkillImgChange(skill);
					}
				}
			}
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
				bool isKissTriggered = RandomManager.RandomPer(MiyukiRandomKey, 100, 30) && IsDere;
				DialogueBoxState? dialogueState = null;
				if (isKissTriggered) dialogueState = DialogueBoxState.kiss;
				CreateDialogueBox(dialogueState);
			}
		}
	}
}
