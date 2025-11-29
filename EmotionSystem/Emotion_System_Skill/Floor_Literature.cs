using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
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

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Literature_LookDay");
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
					Utils.PlaySound("Floor_Literature_SanguineDesire");
					Utils.ApplyBleed(Targets[0], BChar, 10, 300);
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

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Literature_RedEyes");
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
					BattleSystem.DelayInputAfter(ApplyExtended());
				}

				private IEnumerator ApplyExtended()
				{
					yield return new WaitForEndOfFrame();
					yield return new WaitForEndOfFrame();
					Utils.PlaySound("Floor_Literature_Laetitia");
					Utils.ApplyExtended(BChar.MyTeam.Skills.Where(s => s.Master == BChar).ToList(), ModItemKeys.SkillExtended_Ex_Abnormality_Friend_1, true, true, true, 3);
					yield break;
				}
			}

			public class BlackSwan : Ex_EGO
			{
				public override string DescExtended(string desc)
				{
					return base.DescExtended(desc).Replace("&a", Utils.ChanceDebuff(BChar, 150).ToString());
				}

				public override void Init()
				{
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Literature_BlackSwan");

					foreach (var enemy in Targets)
					{
						Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Paralysis, 5, Utils.ChanceDebuff(BChar, 150));
						Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Fragile, 5, Utils.ChanceDebuff(BChar, 150));
						Utils.ApplyBleed(enemy, BChar, 5, 150);
					}

					foreach (var ally in Utils.AllyTeam.AliveChars)
					{
						Utils.AddBuff(BChar, ally, ModItemKeys.Buff_B_Abnormality_LiteratureLv3_LovingFamily_0);
					}
				}
			}
		}
	}
}
