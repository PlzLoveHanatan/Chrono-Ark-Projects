using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModEditor;
using I2.Loc;
using Meeyu;
using Spine;
using UnityEngine;

namespace Meeyu
{
	public class Skills
	{
		public class LucySkill : Skill_Extended
		{
			private int usedSkills;

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				var selectedSkill = new List<Skill>();
				string skillKey;
				int randomSkill = RandomManager.RandomInt(RandomClassKey.LucyDraw, 0, 3);

				switch (randomSkill)
				{
					case 0: skillKey = ModItemKeys.Skill_S_Meeyu_Lucy_0; break;
					case 1: skillKey = ModItemKeys.Skill_S_Meeyu_Lucy_1; break;
					case 2: skillKey = ModItemKeys.Skill_S_Meeyu_Lucy_2; break;
						default: skillKey = ""; break;
				}

				if (string.IsNullOrEmpty(skillKey)) return;

				selectedSkill.Add(Skill.TempSkill(skillKey, BChar, BChar.MyTeam));

				BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(selectedSkill, Selection, ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true));
			}

			private void Selection(SkillButton button)
			{
				int drawNum = 0;
				var skillKey = button.Myskill.MySkill.KeyID;

				if (skillKey == ModItemKeys.Skill_S_Meeyu_Lucy_0)
				{
					BattleSystem.instance.AllyTeam.AP += 1;
					drawNum = 2;
				}
				else if (skillKey == ModItemKeys.Skill_S_Meeyu_Lucy_1)
				{
					foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
					{
						if (ally != null)
						{
							ally.Overload = 0;
						}
					}
					BattleSystem.instance.AllyTeam.LucyAlly.Overload = 0;
					drawNum = 2;
				}
				else
				{
					usedSkills = 2;
					BattleSystem.DelayInput(DrawUseAction());
				}

				if (drawNum > 0)
				{
					BattleSystem.instance.AllyTeam.Draw(drawNum);
				}
			}

			public void CastDrawnSkill(Skill skill)
			{
				BattleTeam.SkillRandomUse(BChar, skill, false, false, true);
				BattleSystem.DelayInput(DrawUseAction());
			}

			private IEnumerator DrawUseAction()
			{
				if (usedSkills-- > 0)
				{
					BChar.MyTeam.Draw(new BattleTeam.DrawInput(CastDrawnSkill));
				}
				yield return null;
			}
		}

		public class EliseSkill : Skill_Extended
		{
			public override void Init()
			{
				CanUseStun = true;
			}
		}

		public class MaidDuty : Skill_Extended
		{
			public override string DescExtended(string desc)
			{
				int barrier = (int)(BChar.GetStat.maxhp * 0.3f);
				return base.DescExtended(desc).Replace("&a", barrier.ToString());
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				int barrier = (int)(BChar.GetStat.maxhp * 0.3f);
				Utils.AddBarrier(Targets[0], BChar, ModItemKeys.Buff_B_Meeyu_PleasureBarrier, barrier);
			}
		}

		public class BlowjobTraining : Skill_Extended
		{
			public override string DescExtended(string desc)
			{
				int barrier = (int)(BChar.GetStat.maxhp * 0.3);
				return base.DescExtended(desc).Replace("&a", barrier.ToString());
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				int barrier = (int)(BChar.GetStat.maxhp * 0.3f);
				Utils.AddBarrier(Targets[0], BChar, ModItemKeys.Buff_B_Meeyu_PleasureBarrier, barrier);

				var skills = new List<Skill>
				{
					Skill.TempSkill(ModItemKeys.Skill_S_Meeyu_Blowjob_0, BChar, BChar.MyTeam),
					Skill.TempSkill(ModItemKeys.Skill_S_Meeyu_Blowjob_1, BChar, BChar.MyTeam),
					Skill.TempSkill(ModItemKeys.Skill_S_Meeyu_Blowjob_2, BChar, BChar.MyTeam),
				};

				BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(skills, Selection, ScriptLocalization.System_SkillSelect.CreateSkill, false, true));
			}

			private void Selection(SkillButton button)
			{
				Utils.CreateSkill(BChar, button.Myskill.MySkill.KeyID, true);
			}
		}

		public class ElisesHelpMe : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_EliseHelpMe_0, true);
			}
		}

		public class MeeyusSlimeTraining : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_MeeyuSlimeTraining_0, true);
			}
		}

		public class MeeyusSacrifice : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_MeeyusSacrifice_0, true);
			}
		}

		public class MeeyusTactics : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_MeeyusTactics_0, true);
			}
		}

		public class PrincessStrikes : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_PrincessStrikes_0, true);
			}
		}

		public class Warmup : Skill_Extended
		{
			public override string DescExtended(string desc)
			{
				int barrier = (int)(BChar.GetStat.maxhp * 0.4f);
				return base.DescExtended(desc).Replace("&a", barrier.ToString());
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				int barrier = (int)(BChar.GetStat.maxhp * 0.4f);
				Utils.AddBarrier(Targets[0], BChar, ModItemKeys.Buff_B_Meeyu_PleasureBarrier, barrier);
				Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_WarmUp_0, true);
			}
		}

		public class Warmup_0 : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_WarmUp_1, true);
			}
		}

		public class Warmup_1 : Skill_Extended
		{
			public override string DescExtended(string desc)
			{
				int damage = (int)(BChar.GetStat.maxhp * 0.2f);
				return base.DescExtended(desc).Replace("&a", damage.ToString());
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				int damage = (int)(BChar.GetStat.maxhp * 0.2f);
				Utils.TakeNonLethalDamage(BChar, damage);
			}
		}

		public class Rare
		{
			public class PerfectPartner : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_RarePerfectPartner_0, true);
				}
			}

			public class PerfectPartner_0 : EliseSkill
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_RarePerfectPartner_1, true);
				}
			}

			public class MakiCaptureTechnique : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					foreach (BattleChar battleChar in Targets)
					{
						battleChar.Recovery = BChar.GetStat.maxhp;

						if (battleChar.HP < battleChar.Recovery)
						{
							int num = battleChar.Recovery - battleChar.HP;
							battleChar.Heal(BChar, num, false, false, null);
						}
						
					}
				}
			}

			public class MeeyusRestraint : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Meeyu_Rare_MeeyusRestraint_0, true);
				}
			}
		}
	}
}
