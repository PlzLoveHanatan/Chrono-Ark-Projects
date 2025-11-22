using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using GameDataEditor;
using UnityEngine;
using static EmotionSystem.Extended.EGO;

namespace EmotionSystem
{
	public class ArtSkill
	{
		public class Perfomance : Ex_EGO
		{
			public override void Init()
			{
				base.Init();
				OncePerFight = true;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.PlaySound("Floor_Art_Perfomance_Skill");
				Utils.AddBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Abnormality_ArtLv2_Perfomance_0);
				BattleSystem.DelayInputAfter(EndTurn());
			}

			private IEnumerator EndTurn()
			{
				foreach (var ally in Utils.AllyTeam.AliveChars)
				{
					int heal = ally.GetStat.maxhp / 2;
					BChar.StartCoroutine(Utils.HealingParticle(ally, Utils.DummyChar, heal, true, false));
				}
				yield return Scripts.ForceTurnEnd();
				yield break;
			}
		}

		public class Abnormality
		{

		}

		public class EGO
		{

			public class Aroma : Ex_EGO
			{
				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_Aroma");
				}
			}

			public class DaCapo : Ex_EGO
			{
				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_DaCapo");

					foreach (var ally in Utils.AllyTeam.AliveChars)
					{
						foreach (Buff buff in ally.Buffs)
						{
							if (!buff.BuffData.Hide)
							{
								if (buff.BuffData.Debuff)
								{
									buff.TurnUpdate();
								}
								else if (buff.BuffData.LifeTime != 0f)
								{
									foreach (StackBuff stackBuff in buff.StackInfo)
									{
										stackBuff.RemainTime++;
									}
								}
							}
						}
					}

					Utils.AddBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Abnormality_ArtLv2_Perfomance_0);
					BattleSystem.DelayInputAfter(Scripts.ForceTurnEnd());
				}
			}

			public class Fragments : Ex_EGO
			{
				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_Fragments");
				}
			}

			public class Galaxy : Ex_EGO
			{
				public override string DescExtended(string desc)
				{
					int heal = (int)(BChar.GetStat.reg / 2);
					return base.DescExtended(desc).Replace("&a", heal.ToString());
				}

				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_Galaxy");
					int heal = (int)(BChar.GetStat.reg / 2);
					BattleSystem.DelayInput(Scripts.RecastSkill(Targets[0], BChar, ModItemKeys.Skill_S_EGO_Art_Galaxy, 3, heal, true, true));
				}
			}

			public class Pleasure : Ex_EGO
			{
				public override string DescExtended(string desc)
				{
					int damage = BChar.EmotionLevel() * 5;
					return base.DescExtended(desc).Replace("&a", damage.ToString());
				}

				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_Pleasure");
					int damage = BChar.EmotionLevel() * 5;
					PlusSkillPerFinal.Damage = damage;
					BattleSystem.DelayInput(Scripts.RecastSkillBleed(Targets[0], BChar, ModItemKeys.Skill_S_EGO_Art_Pleasure, 2, 5, 200));
				}
			}
		}
	}
}

