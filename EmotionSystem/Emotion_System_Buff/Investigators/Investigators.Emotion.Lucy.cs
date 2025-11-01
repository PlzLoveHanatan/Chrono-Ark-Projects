using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Investigators
	{
		public class EmotionLucy : Buff, IP_PlayerTurn, IP_Awake
		{
			private readonly List<DataStore.Abnormality> DynamicAbnormalityList = new List<DataStore.Abnormality>();
			private readonly List<string> DynamicEGOList = new List<string>();

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
				DynamicAbnormalityList.AddRange(DataStore.LibraryFloor.CurrentFloor.Abnormalities);

				DynamicEGOList.Clear();
				DynamicEGOList.AddRange(DataStore.LibraryFloor.CurrentFloor.Egos);
			}


			public override void Init()
			{
				OnePassive = true;
			}

			public void Turn()
			{
				EmotionSystem_EGO_Button.instance?.TurnUpdate();

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

			public IEnumerator LucyEmotionLevelUp(int level, bool? getPositiveOnly = null, bool? getNegativeOnly = null, bool? getRandomAbnormality = null)
			{
				List<DataStore.Abnormality> selectionList = GetAbnormalitiesByLevel(level, getPositiveOnly, getNegativeOnly, getRandomAbnormality);

				if (selectionList == null || selectionList.Count == 0)
				{
					Debug.LogWarning($"[LucyEmotion] Нет доступных аномалий для выбора на уровне {level}");
					yield break;
				}

				bool isCancel = (getNegativeOnly.GetValueOrDefault() || getPositiveOnly.GetValueOrDefault() || getRandomAbnormality.GetValueOrDefault());


				var skillList = selectionList.Select(x => Skill.TempSkill(x.Name, BChar, BChar.MyTeam)).ToList();

				yield return BattleSystem.I_OtherSkillSelect(skillList,
					new SkillButton.SkillClickDel(GainAbnormality), ModLocalization.Select_Abnormality, isCancel, false, true, false, true);
			}

			private List<DataStore.Abnormality> GetAbnormalitiesByLevel(int level, bool? getPositiveOnly = null, bool? getNegativeOnly = null, bool? randomAbnormality = null)
			{
				List<DataStore.Abnormality> list = new List<DataStore.Abnormality>();

				int posAbnoCount = 0, negAbnoCount = 0; int AbnoLevel = 0;

				if (getPositiveOnly == true)
				{
					var allPositiveAbnormality = DynamicAbnormalityList.FindAll(a => a.Level == level && a.Type == DataStore.AbnoType.Pos);
					list.AddRange(allPositiveAbnormality);
					return list;
				}
				else if (getNegativeOnly == true)
				{
					var allNegativeAbnormality = DynamicAbnormalityList.FindAll(a => a.Level == level && a.Type == DataStore.AbnoType.Neg);
					list.AddRange(allNegativeAbnormality);
					return list;
				}
				else if (randomAbnormality == true)
				{
					var allRandomAbnormality = DynamicAbnormalityList.FindAll(a => a.Level == level && (a.Type == DataStore.AbnoType.Neg || a.Type == DataStore.AbnoType.Pos));
					list.AddRange(allRandomAbnormality);
					return list;
				}
				else
				{
					switch (level)
					{
						case 1: posAbnoCount = negAbnoCount = 3; AbnoLevel = 1; break;
						case 2: posAbnoCount = 2; negAbnoCount = 1; AbnoLevel = 1; break;
						case 3: posAbnoCount = 2; negAbnoCount = 1; AbnoLevel = 2; break;
						case 4: posAbnoCount = negAbnoCount = 3; AbnoLevel = 2; break;
						case 5: posAbnoCount = negAbnoCount = 3; AbnoLevel = 3; break;
					}

					// Определяем доступные аномалии по типу для текущего AbnoLevel
					var availablePos = DynamicAbnormalityList.Where(a => a.Level == AbnoLevel && a.Type == DataStore.AbnoType.Pos).ToList();
					var availableNeg = DynamicAbnormalityList.Where(a => a.Level == AbnoLevel && a.Type == DataStore.AbnoType.Neg).ToList();
					var randomSeed = BChar.GetRandomClass().SkillSelect;

					// Селекция
					if (AllPosCoinNum >= AllNegCoinNum)
					{
						list.AddRange(availablePos.Random(randomSeed, Math.Min(posAbnoCount, availablePos.Count)));

						if (list.Count < 3)
						{
							list.AddRange(availableNeg.Random(randomSeed, Math.Min(3 - list.Count, availableNeg.Count)));
						}
					}
					else
					{
						if (level == 2 || level == 3)
						{
							negAbnoCount = posAbnoCount;
						}

						list.AddRange(availableNeg.Random(randomSeed, Math.Min(negAbnoCount, availableNeg.Count)));

						if (list.Count < 3)
						{
							list.AddRange(availablePos.Random(randomSeed, Math.Min(3 - list.Count, availablePos.Count)));
						}
					}

					if (list.Count < 3)
					{
						FillAbnormalityList(list);
					}
				}

				return list;
			}

			private void FillAbnormalityList(List<DataStore.Abnormality> list)
			{
				if (list.Count >= 3) return;

				var remaining = DynamicAbnormalityList.Where(a => a.Level <= 2 && !list.Contains(a)).ToList();

				while (list.Count < 3 && list.Count > 0)
				{
					var pick = remaining.Random(1).First();
					list.Add(pick);
					remaining.Remove(pick);
				}
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
				EmotionSystem_EGO_Button.instance?.AddEGOSkill(button.Myskill);
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
					ModItemKeys.Skill_S_Abnormality_Literature_Lv3_LovingFamily,
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
}