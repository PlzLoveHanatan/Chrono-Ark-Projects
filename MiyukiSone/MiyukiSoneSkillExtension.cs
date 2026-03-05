using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;
using static MiyukiSone.Skills.Class;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public static class MiyukiSoneSkillExtension
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
			}
		}
	}
}
