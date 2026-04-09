using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using NLog.Targets;
using UnityEngine;
using static CharacterDocument;
using static MiyukiSone.Skills;
using static MiyukiSone.Utils;
using static UnityEngine.UI.GridLayoutGroup;

namespace MiyukiSone
{
	public static class MiyukiExtension
	{
		private static readonly List<string> ExceptSkillKeys = new List<string>()
		{
			ModItemKeys.Skill_S_Miyuki_Rare_FinalView,
			ModItemKeys.Skill_S_Miyuki_Rare_GameUpdate,
			ModItemKeys.Skill_S_Miyuki_Rare_JustforYOU,
		};

		public static void MiyukiInit(this Skill skill)
		{
			var miyukiMaster = skill.Master.Info.KeyData == ModItemKeys.Character_Miyuki;
			bool isInCollection = UIManager.AllUI.Any(ui => ui.name.Contains("Collection"));

			if (miyukiMaster || skill.Master.Dummy /*&& BattleSystem.instance != null || isInCollection || FieldSystem.instance != null || skill.MySkill.KeyID == ModItemKeys.Skill_S_Miyuki_Special_EternalKiss*/)
			{
				if (ExceptSkillKeys.Contains(skill.MySkill.KeyID)) return;

				var keyWord = skill.MySkill.PlusKeyWords.FirstOrDefault(k => k.Key == ModItemKeys.SkillKeyword_KeyWord_RealityWarping);

				if (keyWord == null)
				{
					keyWord = new GDESkillKeywordData(ModItemKeys.SkillKeyword_KeyWord_RealityWarping);
					skill.MySkill.PlusKeyWords.Add(keyWord);
				}

				if (MiyukiPassive.CharacterDrawList.TryGetValue(skill.MySkill.KeyID, out string skillKey) && !string.IsNullOrEmpty(skillKey))
				{
					string skillName = new GDESkillData(skillKey).Name ?? skillKey;
					string quotes = LocalizationManager.CurrentLanguage == "English" ? "'" : "";
					keyWord.Desc = ModLocalization.RealityWarpingDesc.Replace("&a", quotes + skillName + quotes);
				}
				RefreshMiyukiCharacterDraw();
			}
			else if (!miyukiMaster && !isInCollection)
			{
				Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Special_SacrificedKnowledge, skill.Master, skill.Master.MyTeam);
				skill.SkillChange(newSkill, false, false, true);
			}
		}

		public static Skill MiyukiSkillPreview(this Skill skill, string customKey = null)
		{
			string skillKey = customKey;

			if (string.IsNullOrEmpty(skillKey))
			{
				if (!MiyukiPassive.CharacterDrawList.TryGetValue(skill.MySkill.KeyID, out skillKey) || string.IsNullOrEmpty(skillKey) || skillKey == "???")
				{
					return null;
				}
			}

			try
			{
				Skill previewSkill = Skill.TempSkill(skillKey, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam);
				if (!string.IsNullOrEmpty(skill.MySkill.PlusSkillView)) previewSkill.MySkill.PlusSkillView = skill.MySkill.PlusSkillView;
				return previewSkill;
			}
			catch (Exception e)
			{
				Debug.LogError($"[Miyuki] Failed to create preview for {skillKey}: {e.Message}");
				return null;
			}
		}

		public static void CheckMiyukiDraw(bool createCharDraw = false, bool changeIcon = false)
		{
			MiyukiPassive.CreateCharacterDraw = createCharDraw;
			MiyukiBuff?.Init();
			if (changeIcon) MiyukiBuff?.ChangeIcon();
		}

		public static void RefreshMiyukiCharacterDraw()
		{
			if (BattleSystem.instance == null || MiyukiBchar == null || MiyukiChar == null) return;

			var miyukiSkills = BattleSystem.instance.AllyTeam.Skills.Concat(BattleSystem.instance.AllyTeam.Skills_Deck).Concat(BattleSystem.instance.AllyTeam.Skills_UsedDeck).Where(s => s.Master == MiyukiBchar && s.MySkill.KeyID != ModItemKeys.Skill_S_Miyuki_GlitchingPhone)?.Select(s => s.MySkill.KeyID).ToList();
			var shouldHaveDraws = miyukiSkills.Where(MiyukiPassive.CharacterDrawList.ContainsKey).Select(s => MiyukiPassive.CharacterDrawList[s]).Where(s => s != "???").Distinct().ToList();
			MiyukiPassive.AvaliableCharacterDraw.RemoveAll(draw => !shouldHaveDraws.Contains(draw));
			shouldHaveDraws.ForEach(s => { if (!MiyukiPassive.AvaliableCharacterDraw.Contains(s)) MiyukiPassive.AvaliableCharacterDraw.Add(s); });

			Dictionary<string, string> dic = new Dictionary<string, string>()
			{
				{ ModItemKeys.Skill_S_Miyuki_Special_Close, GDEItemKeys.Skill_S_Mement_LucyDraw },
				{ ModItemKeys.Skill_S_Miyuki_Special_Might, GDEItemKeys.Skill_S_Phoenix_Draw }
			};

			Skill basicSkill = MiyukiChar?.MyTeam.Skills_Basic[MiyukiBchar.MyTeam.Chars.IndexOf(MiyukiBchar)];
			if (basicSkill != null && dic.TryGetValue(basicSkill.MySkill.KeyID, out string drawKey))
			{
				if (!MiyukiPassive.AvaliableCharacterDraw.Contains(drawKey)) MiyukiPassive.AvaliableCharacterDraw.Add(drawKey);
			}
			if (MiyukiPassive.AvaliableCharacterDraw.Count == 0) CheckMiyukiDraw(false, true);
		}

		public static int AdjacentSkillIndex(this Skill skill, string targetSkillID)
		{
			int myIndex = BattleSystem.instance.AllyTeam.Skills.FindIndex(s => s == skill);
			if (myIndex == -1) return -1;

			int[] adjacentPositions = new int[] { myIndex - 1, myIndex + 1 };

			foreach (int pos in adjacentPositions)
			{
				if (pos >= 0 && pos < BattleSystem.instance.AllyTeam.Skills.Count)
				{
					if (BattleSystem.instance.AllyTeam.Skills[pos].MySkill.KeyID == targetSkillID) return pos;
				}
			}
			return -1;
		}

		public static IEnumerator CreateEternalKiss(Skill skill1, Skill skill2, BattleChar owner)
		{
			yield return null;
			yield return new WaitForFixedUpdate();
			yield return new WaitForSecondsRealtime(0.2f);

			if (owner.Info.KeyData != ModItemKeys.Character_Miyuki) yield break;
			if (skill1.MyButton != null && !skill1.MyButton.AlreadyWasted) skill1.Except();
			if (skill2.MyButton != null && !skill2.MyButton.AlreadyWasted) skill2.Except();
			Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Special_EternalKiss, owner, owner.MyTeam);
			if (newSkill != null) BattleSystem.instance.AllyTeam.Add(newSkill, true);
			MiyukiPassive.AvaliableCharacterDraw.Add(ModItemKeys.Skill_S_Miyuki_LucyDraw_MiyukiHelp);
			CheckMiyukiDraw(true, true);
			yield break;
		}
	}
}
