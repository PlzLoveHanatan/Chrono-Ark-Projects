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
using static MiyukiSone.EventsData;
using static MiyukiSone.Dialogue;
using static MiyukiSone.Events;
using UnityEngine.U2D.SpriteShapeClipperLib;
using UnityEngine.Internal;

namespace MiyukiSone
{
	public enum MiyukiAffection
	{
		Kuudere,
		DereDere,
		Yandere
	}

	public static class Affection
	{
		private static MiyukiAffection? _currentAffection;
		public static MiyukiAffection CurrentAffection
		{
			get
			{
				if (MiyukiSaveManager.Instance.CurrentData.LockedState.HasValue)
				{
					return (MiyukiAffection)MiyukiSaveManager.Instance.CurrentData.LockedState.Value;
				}

				if (_currentAffection == null)
				{
					if (MiyukiData.Affection != -1) _currentAffection = (MiyukiAffection)MiyukiData.Affection;
					else _currentAffection = MiyukiAffection.Kuudere;
				}
				return _currentAffection.Value;
			}

			set
			{
				if (_currentAffection != value)
				{
					_currentAffection = value;
					MiyukiData.Affection = (int)value;			
					Debug.Log($"Miyuki Affection now {value}");
				}
				else
				{
					Debug.Log($"Miyuki Affection still {value}");
				}
				CheckIp();
			}
		}

		// isPositive = true: Dere = +value, Yandere = -value
		// isPositive = false: Dere = -value, Yandere = +value
		public static int MiyukiResult(int baseValue, bool isPositive = true)
		{
			if (IsKuudere) return 0;
			bool shouldBePositive = isPositive ? IsDere : !IsDere;
			return shouldBePositive ? baseValue : -baseValue;
		}

		public static int MiyukiRandomResult(int maxValue)
		{
			return MiyukiRandomResult(maxValue, null);
		}

		public static int MiyukiRandomResult(int maxValue, [DefaultValue("1")] int? minValue = null)
		{
			if (IsKuudere) return 0;
			return RandomManager.RandomInt("MiyukiRandomInt", minValue ?? 1, maxValue + 1);
		}

		public static bool MiyukiResult() => IsDere;
		public static bool MiyukiDecides => RandomManager.RandomPer("MiyukiDecision", 100, 50);
		public static bool MiyukiForces => RandomManager.RandomPer("MiyukiForce", 100, 75);
		public static bool IsDere => MiyukiInParty && CurrentAffection == MiyukiAffection.DereDere;
		public static bool IsKuudere => MiyukiInParty && CurrentAffection == MiyukiAffection.Kuudere;
		public static bool IsYandere => !MiyukiInParty || CurrentAffection == MiyukiAffection.Yandere;

		//private static MiyukiAffection GetRandomActionState()
		//{ 
		//	if (MiyukiSaveManager.Instance.CurrentData.LockedState.HasValue) return (MiyukiAffection)MiyukiSaveManager.Instance.CurrentData.LockedState.Value;
		//	var values = Enum.GetValues(typeof(MiyukiAffection));
		//	return (MiyukiAffection)values.GetValue(RandomManager.RandomInt("MiyukiRandom", 0, values.Length));
		//}

		public static void MiyukiTurn()
		{
			if (!MiyukiSaveManager.Instance.CurrentData.EternalPromise)
			{
				CreateDialogue(DialogueState.love, amount: 1, isDoubleButton: true);
				return;
			}

			if (MiyukiDecides) GetRandomAffection();

			if (MiyukiForces)
			{
				CreateDialogue();
				MiyukiTurnAction();
			}
		}

		public static void GetRandomAffection()
		{
			if (MiyukiSaveManager.Instance.CurrentData.LockedState.HasValue) CurrentAffection = (MiyukiAffection)MiyukiSaveManager.Instance.CurrentData.LockedState.Value;

			var values = Enum.GetValues(typeof(MiyukiAffection));
			var availableIndices = new List<int>();

			for (int i = 0; i < values.Length; i++)
			{
				availableIndices.Add(i);
			}

			int lastIndex = MiyukiData.LastAffection;
			if (lastIndex != -1 && availableIndices.Count > 1) availableIndices.Remove(lastIndex);
			int randomIndex = RandomManager.RandomInt("MiyukiRandomIndex", 0, availableIndices.Count);
			int selectedIndex = availableIndices[randomIndex];
			MiyukiData.LastAffection = selectedIndex;
			CurrentAffection = (MiyukiAffection)values.GetValue(selectedIndex);
		}

		public static void CheckIp()
		{
			if (BattleSystem.instance != null)
			{
				foreach (var skill in BattleSystem.instance.AllyTeam.Skills.Concat(BattleSystem.instance.AllyTeam.Skills_Deck).Concat(BattleSystem.instance.AllyTeam.Skills_UsedDeck))
				{
					foreach (var ex in skill.AllExtendeds)
					{
						if (ex is IP_MiyukiSkillImgChange handler) handler.SkillImgChange(skill);
						if (skill.Master.Info.KeyData == ModItemKeys.Character_Miyuki) ex.Init();
					}
				}

				//foreach (var battleChar in BattleSystem.instance.AllyTeam.AliveChars)
				//{
				//	Passive_Char passive = battleChar.Info.Passive;
				//	if (passive != null && passive is IP_MiyukiCharImgChange handler) handler.CharImgChange();
				//}

				BattleSystem.instance.AllyTeam.AliveChars.ForEach(a => { if (a.BuffReturn(ModItemKeys.Buff_B_Miyuki_Buff_Ally, false) is Buffs.AllyConstantStats b) b.Init(); });
			}

			MiyukiCharImg.UpdateCharacterImage();

			//var buff = AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != ModItemKeys.Character_Miyuki).Select(a => a.Buffs).OfType<Buffs.MiyukiBuff>().ToList();
			//buff.ForEach(b => b.Init());
		}
	}
}