using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using System.Security.Cryptography;
using UnityEngine.AI;
using Spine;
using static UnityEngine.UI.GridLayoutGroup;
using EmotionSystem;
using EmotionalSystem;
using ChronoArkMod.ModData.Settings;
using static EnemyCastingLineV2;
using System.Security.Permissions;

namespace EmotionalSystemBuff
{
	public class EmotionsLucy : Buff, IP_PlayerTurn, IP_Awake
	{
		private List<Abnormality> DynamicAbnormalityList = new List<Abnormality>();
		private List<string> DynamicEGOList = new List<string>();

		public int LastEmotionalLevel;
		public int NextEGONum;
		public int EmotionalLevel
		{
			get
			{
				int sumLevel = 0;
				int sumCharacter = 0;
				foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
				{
					var emotion = ally.MyEmotion();
					if (emotion != null)
					{
						sumCharacter++;
						sumLevel += emotion.Level;
					}
				}
				if (sumLevel == 0) return 0;
				return sumLevel / sumCharacter;
			}
		}

		public int AllPosCoinNum
		{
			get
			{
				int num = 0;
				foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
				{
					var emotion = ally.MyEmotion();
					if (emotion != null)
					{
						num += emotion.AccumPosCoin;
					}
				}
				return num;
			}
		}

		public int AllNegCoinNum
		{
			get
			{
				int num = 0;
				foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
				{
					var emotion = ally.MyEmotion();
					if (emotion != null)
					{
						num += emotion.AccumNegCoin;
					}
				}
				return num;
			}
		}

		public void Awake()
		{
			LastEmotionalLevel = 0;
			NextEGONum = 0;

			DynamicAbnormalityList.Clear();
			DynamicAbnormalityList.AddRange(LibraryFloor.CurrentFloor.Abnormalities);

			DynamicEGOList.Clear();
			DynamicEGOList.AddRange(LibraryFloor.CurrentFloor.Egos);
		}

		public override void Init()
		{
			OnePassive = true;
		}

		public void Turn()
		{
			EmotionalSystem_EGO_Button.instance?.TurnUpdate();

			if (EmotionalLevel > LastEmotionalLevel)
			{
				//int levelsGained = EmotionalLevel - LastEmotionalLevel;
				for (int i = LastEmotionalLevel + 1; i <= EmotionalLevel; i++)
				{
					BattleSystem.DelayInputAfter(LucyEmotionLevelUp(i));

					if (i >= 3)
					{
						BattleSystem.DelayInputAfter(SelectEGO());
					}
				}

				LastEmotionalLevel = EmotionalLevel;
			}
		}

		public IEnumerator SelectEGO()
		{
			List<Skill> list = new List<Skill>();

			var availableEGO = DynamicEGOList.Random(BChar.GetRandomClass().SkillSelect, 2);
			if (availableEGO.Count > 0)
			{
				list.AddRange(availableEGO.Select(x => Skill.TempSkill(x, BChar, BChar.MyTeam)));

				yield return BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(SelectEgoOwner), ModLocalization.Select_EGO, false, true, true, false, true);
				yield break;
			}
		}

		public IEnumerator LucyEmotionLevelUp(int level, bool? getPositiveOnly = null, bool? getNegativeOnly = null)
		{
			// Если аргументы null, определяем по монетам
			bool positive = getPositiveOnly ?? (AllPosCoinNum > AllNegCoinNum);
			bool negative = getNegativeOnly ?? (AllPosCoinNum < AllNegCoinNum);

			List<Abnormality> selectionList = GetAbnormalitiesByLevel(level, positive, negative);

			if (selectionList == null || selectionList.Count == 0)
			{
				Debug.LogWarning($"[LucyEmotion] Нет доступных аномалий для выбора на уровне {level}");
				yield break;
			}

			var skillList = selectionList.Select(x => Skill.TempSkill(x.Name, BChar, BChar.MyTeam)).ToList();

			yield return BattleSystem.I_OtherSkillSelect(skillList,
				new SkillButton.SkillClickDel(GainAbnormality), ModLocalization.Select_Abnormality, false, false, true, false, true);
		}

		private List<Abnormality> GetAbnormalitiesByLevel(int level, bool getPositiveOnly = false, bool getNegativeOnly = false)
		{
			List<Abnormality> list = new List<Abnormality>();

			// Берем все аномалии до третьего уровня включительно
			List<Abnormality> available = DynamicAbnormalityList.FindAll(abno => abno.Level <= 3);

			int posCount = 0, negCount = 0;
			int maxTotal = 3;

			// Определяем ограничения по количеству аномалий для уровня
			switch (level)
			{
				case 1:
					available = available.FindAll(abno => abno.Level == 1);
					posCount = negCount = 3;
					break;
				case 2:
					available = available.FindAll(abno => abno.Level == 1 || abno.Level == 2);
					posCount = 2; // lvl1
					negCount = 1; // lvl2
					break;
				case 3:
					available = available.FindAll(abno => abno.Level == 1 || abno.Level == 2);
					posCount = 1; // lvl1
					negCount = 2; // lvl2
					break;
				case 4:
					available = available.FindAll(abno => abno.Level == 2);
					posCount = negCount = available.Count; // берём все доступные
					break;
				case 5:
					available = available.FindAll(abno => abno.Level == 3);
					posCount = negCount = available.Count; // берём все доступные
					break;
			}

			if (available.Count == 0)
			{
				Debug.LogWarning($"[LucyEmotion] Нет доступных аномалий для уровня {level}");
				return list;
			}

			// Если явно выбран Pos или Neg
			if (getPositiveOnly)
			{
				List<Abnormality> availablePos = available.FindAll(a => a.Type == AbnoType.Pos);
				list.AddRange(availablePos.Random(BChar.GetRandomClass().SkillSelect, Math.Min(3, availablePos.Count)));
			}
			else if (getNegativeOnly)
			{
				List<Abnormality> availableNeg = available.FindAll(a => a.Type == AbnoType.Neg);
				list.AddRange(availableNeg.Random(BChar.GetRandomClass().SkillSelect, Math.Min(3, availableNeg.Count)));
			}
			else
			{
				// Обычная селекция по монетам
				List<Abnormality> availablePos = available.FindAll(a => a.Type == AbnoType.Pos);
				List<Abnormality> availableNeg = available.FindAll(a => a.Type == AbnoType.Neg);

				if (AllPosCoinNum >= AllNegCoinNum)
				{
					// Добавляем положительные аномалии
					list.AddRange(availablePos.Random(BChar.GetRandomClass().SkillSelect, Math.Min(posCount, availablePos.Count)));

					// Если после добавления положительных меньше 3, добавляем отрицательные, но не больше нужного
					if (list.Count < 3)
					{
						list.AddRange(availableNeg.Random(BChar.GetRandomClass().SkillSelect, Math.Min(3 - list.Count, availableNeg.Count)));
					}
				}
				else
				{
					// Переворачиваем порядок: сначала отрицательные
					list.AddRange(availableNeg.Random(BChar.GetRandomClass().SkillSelect, Math.Min(negCount, availableNeg.Count)));

					if (list.Count < 3)
					{
						list.AddRange(availablePos.Random(BChar.GetRandomClass().SkillSelect, Math.Min(3 - list.Count, availablePos.Count)));
					}
				}

			}

			// Дозабор до 3 абнормалити
			if (list.Count < 3)
			{
				List<Abnormality> remaining = available.Except(list).ToList();
				if (remaining.Count > 0)
				{
					list.AddRange(remaining.Random(BChar.GetRandomClass().SkillSelect, Math.Min(3 - list.Count, remaining.Count)));
				}
			}

			return list;
		}

		private void SelectEgoOwner(SkillButton button)
		{
			var alliesList = Utils.AlliesPreview(button.Myskill.MySkill.KeyID);
			BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(alliesList, new SkillButton.SkillClickDel(GainEGO), ModLocalization.SelectOwner_EGO, false, false, true, false, true));
		}

		private void GainEGO(SkillButton button)
		{
			string skillKey = button.Myskill.MySkill.KeyID;

			DynamicEGOList.RemoveAll(x => x == skillKey);
			EmotionalSystem_EGO_Button.instance?.AddEGOSkill(button.Myskill);
			Utils.UnlockSkillPreview(false, false, true);
		}

		private void GainAbnormality(SkillButton button)
		{
			string key = button.Myskill.MySkill.KeyID;

			DynamicAbnormalityList.RemoveAll(x => x.Name == key);
			Utils.UnlockSkillPreview(true);

			var instantCastAbnormalities = new[]
			{
				ModItemKeys.Skill_S_Abnormality_HistoryLv2_WorkerBee,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_Music,
			};

			if (instantCastAbnormalities.Contains(key))
			{
				ForceSkillCast(button.Myskill);
			}
			else
			{
				SelectAbnormalityOwner(button);
			}
		}

		private void SelectAbnormalityOwner(SkillButton button)
		{
			var alliesList = Utils.AlliesPreview(button.Myskill.MySkill.KeyID);
			BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(alliesList, new SkillButton.SkillClickDel(ApplyAbnormality), ModLocalization.SelectOwner_Abnormality, false, false, true, false, true));
		}

		private void ApplyAbnormality(SkillButton button)
		{
			ForceSkillCast(button.Myskill);
		}

		private void ForceSkillCast(Skill skill)
		{
			skill.isExcept = true;
			BattleSystem.instance.StartCoroutine(BattleSystem.instance.ForceAction(skill, skill.Master, false, false, true, null));
		}
	}
}
