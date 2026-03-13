using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;
using static CharacterDocument;
using static MiyukiSone.Skills.Class;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public static class MiyukiSkillExtension
	{
		public static bool IsMiyukiOwner(this Skill skill)
		{
			return skill.Master.Info.KeyData == ModItemKeys.Character_Miyuki;
		}

		public static void MiyukiInit(this Skill skill, MiyukiAffectionState? state = null)
		{
			if (Bs == null || UIManager.AllUI.Any(ui => ui.name.Contains("Collection"))) return;

			if (skill.IsMiyukiOwner()) goto statecheck;
			else
			{
				Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_S_SacrificedKnowledge, skill.Master, skill.Master.MyTeam);
				skill.SkillChange(newSkill, false, false, true);
				return;
			}

		statecheck:;
			if (state == MiyukiAffectionState.Yandere)
			{
				var targetKey = skill.MySkill.Target.Key;

				if (targetKey == GDEItemKeys.s_targettype_enemy) skill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_otherally);
				else if (targetKey == GDEItemKeys.s_targettype_all_enemy) skill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_ally);
				else if (targetKey == GDEItemKeys.s_targettype_ally) skill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_enemy);
				else if (targetKey == GDEItemKeys.s_targettype_all_ally) skill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_enemy);
				else if (targetKey == GDEItemKeys.s_targettype_all_allyorenemy) skill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_ally);
				else if (targetKey == GDEItemKeys.s_targettype_otherally) skill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_enemy);
			}
		}

		public static int AdjacentSkillIndex(this Skill skill, string targetSkillID)
		{
			int myIndex = AllyTeam.Skills.FindIndex(s => s == skill);
			if (myIndex == -1) return -1;

			int[] adjacentPositions = new int[] { myIndex - 1, myIndex + 1 };

			foreach (int pos in adjacentPositions)
			{
				if (pos >= 0 && pos < AllyTeam.Skills.Count)
				{
					if (AllyTeam.Skills[pos].MySkill.KeyID == targetSkillID) return pos;
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

			Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_S_Special_EternalKiss, owner, owner.MyTeam);
			if (newSkill != null) AllyTeam.Add(newSkill, true);
			yield break;
		}
	}
}
