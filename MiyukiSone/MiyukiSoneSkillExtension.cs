using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using NLog.Targets;
using UnityEngine;
using static CharacterDocument;
using static MiyukiSone.Skills;
using static MiyukiSone.Utils;
using static UnityEngine.UI.GridLayoutGroup;

namespace MiyukiSone
{
	public static class MiyukiSkillExtension
	{


		private static readonly Dictionary<string, string> RedirectMap = new Dictionary<string, string>
		{
			{ GDEItemKeys.s_targettype_enemy, GDEItemKeys.s_targettype_otherally },
			{ GDEItemKeys.s_targettype_all_enemy, GDEItemKeys.s_targettype_all_ally },
			{ GDEItemKeys.s_targettype_ally, GDEItemKeys.s_targettype_enemy },
			{ GDEItemKeys.s_targettype_all_ally, GDEItemKeys.s_targettype_all_enemy },
			{ GDEItemKeys.s_targettype_all_allyorenemy, GDEItemKeys.s_targettype_all_ally },
			{ GDEItemKeys.s_targettype_otherally, GDEItemKeys.s_targettype_enemy },
			{ GDEItemKeys.s_targettype_enemy_PlusRandom, GDEItemKeys.s_targettype_ally }
		};

		public static bool IsMiyukiOwner(this Skill skill)
		{
			return skill.Master.Info.KeyData == ModItemKeys.Character_Miyuki;
		}

		public static void MiyukiInit(this Skill skill, MiyukiAffection? state = null)
		{
			if (Bs == null || UIManager.AllUI.Any(ui => ui.name.Contains("Collection"))) return;

			if (!skill.IsMiyukiOwner())
			{
				Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Special_SacrificedKnowledge, skill.Master, skill.Master.MyTeam);
				skill.SkillChange(newSkill, false, false, true);
			}
			else
			{
				return;
				if (state == MiyukiAffection.Yandere)
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
			GetMiyukiPassive?.AvaliableCharacterDraw.Add(ModItemKeys.Skill_S_Miyuki_Draw_MiyukiHelp);
			yield return CheckEternalVow();
			Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Special_EternalKiss, owner, owner.MyTeam);
			if (newSkill != null) AllyTeam.Add(newSkill, true);
			yield break;
		}	
	}
}
