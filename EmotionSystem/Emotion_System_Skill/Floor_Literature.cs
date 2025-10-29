using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmotionSystem.Extended.EGO;

namespace EmotionSystem
{
	public class LiteratureSkill
	{
		public class Abnormality
		{

		}


		public class EGO
		{
			public class TodayExpression : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}
			}

			public class SanguineDesire : Ex_EGO, IP_SkillCastingStart, IP_SkillCastingQuit
			{
				public override void Init()
				{
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.ApplyBleed(Targets[0], BChar, 10);
				}


				public void SkillCasting(CastingSkill ThisSkill)
				{
					foreach (var ally in Utils.AllyTeam.AliveChars)
					{
						Utils.AddBuff(BChar, ally, ModItemKeys.Buff_B_EGO_Literature_SanguineDesire);
					}

				}

				public void SkillCastingQuit(CastingSkill ThisSkill)
				{
					BattleSystem.DelayInputAfter(BuffRemove(ThisSkill));
				}

				public IEnumerator BuffRemove(CastingSkill ThisSkill)
				{
					var castList = BattleSystem.instance.CastSkills.ToList();
					castList.AddRange(BattleSystem.instance.SaveSkill);
					castList = castList.FindAll(cs => cs != ThisSkill && cs.skill.MySkill.KeyID == ModItemKeys.Skill_S_EGO_Literature_SanguineDesire);

					if (castList.Any())
					{
						yield break;
					}
					else
					{
						foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
						{
							battleChar.BuffRemove(ModItemKeys.Buff_B_EGO_Literature_SanguineDesire);
						}
						yield break;
					}
				}
			}

			public class RedEyes : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}
			}

			public class Laetitia : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.ApplyExtended(BChar.MyTeam.Skills, ModItemKeys.SkillExtended_Ex_Abnormality_Friend, true, true, true, 5, true);
				}
			}

			public class BlackSwan : Ex_EGO
			{
				public override string DescExtended(string desc)
				{
					int chanceWeak = (int)(BChar.GetStat.HIT_DEBUFF + 100);
					return base.DescExtended(desc).Replace("&a", chanceWeak.ToString());
				}

				public override void Init()
				{
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					foreach (var enemy in Targets)
					{
						Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Paralysis, 5);
						Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Fragile, 5);
						Utils.ApplyBleed(enemy, BChar, 5);
					}

					foreach (var ally in Utils.AllyTeam.AliveChars)
					{
						Utils.AddBuff(BChar, ally, ModItemKeys.Buff_B_Abnormality_LiteratureLv3_LovingFamily);
					}
				}
			}
		}
	}
}
