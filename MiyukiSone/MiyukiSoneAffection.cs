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
using static MiyukiSone.DialogueData;
using static MiyukiSone.EventData;
using static MiyukiSone.Dialogue;
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

	public static class MiyukiAffection
	{
		private const string MiyukiRandomKey = "MiyukiRandom";
		private const int MaxAffectionPoints = 100;
		private const int MinAffectionPoints = -100;
		public static int MiyukiPoints => MiyukiData.MiyukiAffectionPoints;

		public static int MiyukiResult(int baseValue)
		{
			if (!MiyukiDecides) return 0;

			if (IsKuudere)
			{
				return MiyukiPoints >= 0 ? baseValue : -baseValue;
			}
			else if (IsDere || IsYandere)
			{
				int result = IsDere ? baseValue : -baseValue;

				if (MiyukiPoints == MaxAffectionPoints || MiyukiPoints == MinAffectionPoints)
				{
					int bonusChance = Mathf.Clamp(Math.Abs(MiyukiPoints) + 25, 0, 100);
					bool isDouble = RandomManager.RandomPer(IsDere ? "MiyukiDere" : "MiyukiYandere", 100, bonusChance);
					if (isDouble) return result * 2;
				}

				return result;
			}
			return 0;
		}

		public static bool MiyukiDecides => RandomManager.RandomPer("MiyukiBehaviour", 100, Mathf.Clamp(Math.Abs(MiyukiPoints) + 25, 0, 100));

		public static bool MiyukiInMood => IsDere || (IsKuudere && MiyukiPoints >= 0);

		public static bool IsDere => CurrentAffectionState == MiyukiAffectionState.DereDere;

		public static bool IsKuudere => CurrentAffectionState == MiyukiAffectionState.Kuudere;

		public static bool IsYandere => CurrentAffectionState == MiyukiAffectionState.Yandere;


		public static MiyukiAffectionState CurrentAffectionState
		{
			get
			{
				if (MiyukiSaveManager.Instance.CurrentData.LockedState.HasValue)
				{
					return (MiyukiAffectionState)MiyukiSaveManager.Instance.CurrentData.LockedState.Value;
				}

				int points = MiyukiPoints;

				if (points >= 15) return MiyukiAffectionState.DereDere;
				if (points <= -15) return MiyukiAffectionState.Yandere;

				return MiyukiAffectionState.Kuudere;
			}
		}

		public static void ChangeAffectionPoints()
		{
			ChangeAffectionPoints(1);
		}

		public static void ChangeAffectionPointsRandom()
		{
			int amount = MiyukiSaveManager.Instance.CurrentData.GameUpdated ? 1 : RandomManager.RandomInt(MiyukiRandomKey, 0, 2);
			ChangeAffectionPoints(-amount);				
		}

		public static void ChangeAffectionPoints(int amount)
		{
			if (amount < 0 && MiyukiSaveManager.Instance.CurrentData.GameUpdated) amount *= 2;
			MiyukiData.MiyukiAffectionPoints = Mathf.Clamp(MiyukiData.MiyukiAffectionPoints + amount, MinAffectionPoints, MaxAffectionPoints);

			MiyukiSaveManager.Instance.CurrentData.AffectionPoints = MiyukiPoints;
			MiyukiSaveManager.Instance.Save();

			Debug.Log($"[Miyuki] Affection points changed by {amount}. Current: {MiyukiPoints}");

			foreach (var skill in AllyTeam.Skills.Concat(AllyTeam.Skills_Deck).Concat(AllyTeam.Skills_UsedDeck))
			{
				foreach (var ex in skill.AllExtendeds)
				{
					if (ex is IP_MiyukiSkillImgChange handler)
					{
						handler.SkillImgChange(skill);
					}
				}
			}

			foreach (var battleChar in BattleSystem.instance.AllyTeam.AliveChars)
			{
				Passive_Char passive = battleChar.Info.Passive;

				if (passive != null && passive is IP_MiyukiMoodChange handler)
				{
					handler.MiyukiMoodChange();
				}
			}
		}

		public static void MiyukiTurn()
		{
			CreateDialogue(); return;

			if (MiyukiDecides)
			{
				MiyukiAction();
			}
			else
			{
				bool isKissTriggered = RandomManager.RandomPer(MiyukiRandomKey, 100, 30) && IsDere;
				DialogueState? dialogueState = null;
				if (isKissTriggered) dialogueState = DialogueState.kiss;
				CreateDialogue(dialogueState);
			}
		}
	}
}