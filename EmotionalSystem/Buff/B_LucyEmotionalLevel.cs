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
using static EmotionalSystem.B_LucyEmotionalLevel;
using static EnemyCastingLineV2;

namespace EmotionalSystem
{
	public class B_LucyEmotionalLevel : Buff, IP_PlayerTurn, IP_Awake
	{
		private List<Abnormality> DynamicAbnormalityList = new List<Abnormality>();
		private List<string> DynamicEGOList = new List<string>();

		private bool FirstAwake = false;

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
			if (!FirstAwake)
			{
				LastEmotionalLevel = 0;
				NextEGONum = 0;
				FirstAwake = true;

				var nowFloorInfo = EmotionalSystem_LibraryFloor.CurrentFloor;

				DynamicAbnormalityList.Clear();
				DynamicAbnormalityList.AddRange(nowFloorInfo.Abnormalities);

				DynamicEGOList.Clear();
				DynamicEGOList.AddRange(nowFloorInfo.Egos);
			}
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

		private IEnumerator SelectEGO()
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

		private IEnumerator LucyEmotionLevelUp(int level)
		{
			// when you level up, add a new abno skill
			// this abno skill is based your next level
			List<Abnormality> selectionList = new List<Abnormality>();
			switch (level)
			{
				case 1:
					if (AllPosCoinNum > AllNegCoinNum)
					{
						var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
						var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);
						//var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 1);

						selectionList.AddRange(selectedPosAbnoLv1);
						//selectionList.AddRange(selectedNegAbnoLv1);
					}
					else if (AllPosCoinNum < AllNegCoinNum)
					{
						var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
						var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 3);
						//var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 1);

						selectionList.AddRange(selectedNegAbnoLv1);
						//selectionList.AddRange(selectedPosAbnoLv1);
					}
					else
					{
						var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
						var selectedRandomAbno = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);

						selectionList.AddRange(selectedRandomAbno);
					}
					break;

				case 2:
					if (AllPosCoinNum > AllNegCoinNum)
					{
						var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
						var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 2);
						var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 1);

						selectionList.AddRange(selectedPosAbnoLv1);
						selectionList.AddRange(selectedNegAbnoLv1);
					}
					else if (AllPosCoinNum < AllNegCoinNum)
					{
						var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
						var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 2);
						var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 1);

						selectionList.AddRange(selectedNegAbnoLv1);
						selectionList.AddRange(selectedPosAbnoLv1);
					}
					else
					{
						var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
						var selectedRandomAbno = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);

						selectionList.AddRange(selectedRandomAbno);
					}
					break;
				case 3:
					if (AllPosCoinNum > AllNegCoinNum)
					{
						var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
						var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2);
						var selectedPosAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 2);
						var selectedPosAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 1);

						selectionList.AddRange(selectedPosAbnoLv2);
						selectionList.AddRange(selectedPosAbnoLv1);
					}
					else if (AllPosCoinNum < AllNegCoinNum)
					{
						var availableAbnoLv1 = DynamicAbnormalityList.FindAll(abno => abno.Level == 1);
						var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2);
						var selectedNegAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 2);
						var selectedNegAbnoLv1 = availableAbnoLv1.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 1);

						selectionList.AddRange(selectedNegAbnoLv2);
						selectionList.AddRange(selectedNegAbnoLv1);
					}
					else
					{
						var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2 || abno.Level == 1);
						var selectedRandomAbno = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);

						selectionList.AddRange(selectedRandomAbno);
					}
					break;
				case 4:
					if (AllPosCoinNum > AllNegCoinNum)
					{
						var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2);
						var selectedPosAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 2);
						var selectedNegAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 1);

						selectionList.AddRange(selectedPosAbnoLv2);
						selectionList.AddRange(selectedNegAbnoLv2);
					}
					else if (AllPosCoinNum < AllNegCoinNum)
					{
						var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2);
						var selectedNegAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg).Random(BChar.GetRandomClass().SkillSelect, 2);
						var selectedPosAbnoLv2 = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 1);

						selectionList.AddRange(selectedNegAbnoLv2);
						selectionList.AddRange(selectedPosAbnoLv2);
					}
					else
					{
						var availableAbnoLv2 = DynamicAbnormalityList.FindAll(abno => abno.Level == 2 || abno.Level == 1);
						var selectedRandomAbno = availableAbnoLv2.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);

						selectionList.AddRange(selectedRandomAbno);
					}
					break;
				case 5:
					var availableAbnoLv3 = DynamicAbnormalityList.FindAll(abno => abno.Level == 3);
					var abnoLv3 = availableAbnoLv3.FindAll(abno => abno.Type == AbnoType.Neg || abno.Type == AbnoType.Pos).Random(BChar.GetRandomClass().SkillSelect, 3);
					selectionList.AddRange(abnoLv3);
					break;
				default:
					break;
			}

			if (selectionList.Count == 0) yield break;

			var skillList = selectionList.Select(x => Skill.TempSkill(x.Name, BChar, BChar.MyTeam)).ToList();

			yield return BattleSystem.I_OtherSkillSelect(skillList, new SkillButton.SkillClickDel(GainAbnormality), ModLocalization.Select_Abnormality, false, false, true, false, true);
		}

		private void SelectEgoOwner(SkillButton button)
		{
			var alliesList = AlliesPreview(button.Myskill.MySkill.KeyID);
			BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(alliesList, new SkillButton.SkillClickDel(GainEGO), ModLocalization.SelectOwner_EGO, false, false, true, false, true));
		}

		private void GainEGO(SkillButton button)
		{
			string skillKey = button.Myskill.MySkill.KeyID;

			DynamicEGOList.RemoveAll(x => x == skillKey);
			EmotionalSystem_EGO_Button.instance?.AddEGOSkill(button.Myskill);
			Utils.UnlockSkillPreview(skillKey);
		}

		public void GainAbnormality(SkillButton button)
		{
			string key = button.Myskill.MySkill.KeyID;

			DynamicAbnormalityList.RemoveAll(x => x.Name == key);

			var instantCastAbnormalities = new[]
			{
				ModItemKeys.Buff_B_Abnormality_HistoryLv2_WorkerBee,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_Music_Neg,
			};

			if (instantCastAbnormalities.Contains(key))
			{
				ForceSkill(button.Myskill);
			}
			else
			{
				SelectAbnormalityOwner(button);
			}
		}

		private void SelectAbnormalityOwner(SkillButton button)
		{
			var alliesList = AlliesPreview(button.Myskill.MySkill.KeyID);
			BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(alliesList, new SkillButton.SkillClickDel(ApplyAbnormality), ModLocalization.SelectOwner_Abnormality, false, false, true, false, true));
		}

		public void ApplyAbnormality(SkillButton button)
		{
			ForceSkill(button.Myskill);
		}

		private void ForceSkill(Skill skill)
		{
			skill.isExcept = true;
			BattleSystem.instance.StartCoroutine(BattleSystem.instance.ForceAction(skill, skill.Master, false, false, true, null));
			Utils.UnlockSkillPreview(skill.MySkill.KeyID);
		}

		private List<Skill> AlliesPreview(string key)
		{
			var alliesList = new List<Skill>();

			foreach (var ally in BattleSystem.instance.AllyList)
			{
				var skill = Skill.TempSkill(key, ally, ally.MyTeam);
				if (skill != null)
				{
					alliesList.Add(skill);
				}
			}
			return alliesList;
		}
	}
}
