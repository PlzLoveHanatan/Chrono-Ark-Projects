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
	public enum MiyukiAffection
	{
		DereDere,
		Kuudere,
		Yandere
	}

	public static class Affection
	{
		private static MiyukiAffection? _currentAffection;
		public static MiyukiAffection CurrentAffection
		{
			get
			{
				if (_currentAffection == null)
				{
					int savedValue = MiyukiSaveManager.Instance.CurrentData.Affection;
					_currentAffection = (MiyukiAffection)savedValue;
				}
				return _currentAffection ?? MiyukiAffection.Kuudere;
			}

			set
			{
				if (_currentAffection != value)
				{
					_currentAffection = value;
					CheckIp();
					Debug.Log($"[Miyuki] Affection changed to: {value}");
				}
			}
		}

		public static int MiyukiResult(int baseValue)
		{
			if (IsKuudere) return 0;
			return IsDere ? baseValue : -baseValue;
		}

		public static bool MiyukiResult() => IsDere;
		public static bool MiyukiDecides => RandomManager.RandomPer("MiyukiDecision", 100, 50);
		public static bool MiyukiActing => MiyukiDecides && !IsKuudere;
		public static bool IsDere => CurrentAffection == MiyukiAffection.DereDere;
		public static bool IsKuudere => CurrentAffection == MiyukiAffection.Kuudere;
		public static bool IsYandere => CurrentAffection == MiyukiAffection.Yandere;

		//private static MiyukiAffection GetRandomActionState()
		//{ 
		//	if (MiyukiSaveManager.Instance.CurrentData.LockedState.HasValue) return (MiyukiAffection)MiyukiSaveManager.Instance.CurrentData.LockedState.Value;
		//	var values = Enum.GetValues(typeof(MiyukiAffection));
		//	return (MiyukiAffection)values.GetValue(RandomManager.RandomInt("MiyukiRandom", 0, values.Length));
		//}

		public static void MiyukiTurn()
		{
			//CreateDialogue(); return;

			if (!MiyukiSaveManager.Instance.CurrentData.EternalPromise)
			{
				CreateDialogue();
				return;
			}

			if (MiyukiDecides)
			{
				_currentAffection = GetRandomAffection();
				MiyukiSaveManager.Instance.CurrentData.Affection = (int)CurrentAffection;
				if (CurrentAffection == MiyukiAffection.Kuudere) CreateDialogue();
				else MiyukiAction();
			}

			CheckIp();
		}

		private static MiyukiAffection GetRandomAffection()
		{
			if (MiyukiSaveManager.Instance.CurrentData.LockedState.HasValue) return (MiyukiAffection)MiyukiSaveManager.Instance.CurrentData.LockedState.Value;

			var values = Enum.GetValues(typeof(MiyukiAffection));
			var availableIndices = new List<int>();

			for (int i = 0; i < values.Length; i++)
			{
				availableIndices.Add(i);
			}

			int lastIndex = MiyukiData.LastAffection;
			if (lastIndex != -1 && availableIndices.Count > 1) availableIndices.Remove(lastIndex);
			int randomIndex = RandomManager.RandomInt("MiyukiRandom", 0, availableIndices.Count);
			int selectedIndex = availableIndices[randomIndex];
			MiyukiData.LastAffection = selectedIndex;
			return (MiyukiAffection)values.GetValue(selectedIndex);
		}

		public static void CheckIp()
		{
			if (Bs == null) return;

			foreach (var skill in AllyTeam.Skills.Concat(AllyTeam.Skills_Deck).Concat(AllyTeam.Skills_UsedDeck))
			{
				foreach (var ex in skill.AllExtendeds)
				{
					if (ex is IP_MiyukiSkillImgChange handler)
					{
						handler.SkillImgChange(skill);
					}

					if (skill.Master.Info.KeyData == ModItemKeys.Character_Miyuki) ex.Init();
				}
			}

			foreach (var battleChar in BattleSystem.instance.AllyTeam.AliveChars)
			{
				Passive_Char passive = battleChar.Info.Passive;

				if (passive != null && passive is IP_MiyukiCharImgChange handler)
				{
					handler.CharImgChange();
				}
			}
		}
	}
}