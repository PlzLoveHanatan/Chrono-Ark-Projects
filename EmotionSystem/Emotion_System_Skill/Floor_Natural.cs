using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using GameDataEditor;
using UnityEngine;
using static EmotionSystem.Extended.EGO;

namespace EmotionSystem
{
	public class NaturalSkill
	{
		public class Abnormality
		{
			public class Hate : Ex_EGO
			{
				public override bool Terms()
				{
					return BChar.Info.GetData.Role.Key == GDEItemKeys.CharRole_Role_Support;
				}

				public override void Init()
				{
					OncePerFight = true;
				}
			}

			public class Intemperance : Ex_EGO
			{
				public override bool Terms()
				{
					return PlayData.TSavedata._Gold >= 1000;
				}

				public override void Init()
				{
					OncePerFight = true;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					PlayData.TSavedata._Gold -= 1000;
				}
			}

			public class Road : Ex_EGO
			{
				public override bool Terms()
				{
					return PlayData.TSavedata._Gold >= 800;
				}

				public override void Init()
				{
					OncePerFight = true;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					PlayData.TSavedata._Gold -= 800;
				}
			}

			public class Void : Ex_EGO
			{
				public override void Init()
				{
					OncePerFight = true;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					BattleSystem.DelayInputAfter(RemoveSkills());
				}

				private IEnumerator RemoveSkills()
				{
					yield return new WaitForEndOfFrame();

					int statsUp = 0;
					var list = Utils.AllyTeam.Skills.ToList();

					foreach (var skill in list)
					{
						if (skill != null)
						{
							Utils.RemoveSkill(skill);
							statsUp++;
						}
					}

					if (Utils.ReturnBuff(BChar, ModItemKeys.Buff_B_Abnormality_NaturalLv3_Void) is NaturalBuff.Abnormality.Lv3.Void Void)
					{
						for (int i = 0; i < statsUp; i++)
						{
							Void.SkillsRemoved++;
						}
					}

					yield break;
				}
			}
		}

		public class EGO
		{
			public class Rage : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Scripts.AttackRedirect(BChar, SkillD, Targets, false, 30);
					BattleSystem.DelayInput(Scripts.RecastSkillErosion(Targets[0], BChar, ModItemKeys.Skill_S_EGO_Natural_Rage, 2, 5, 200));
				}
			}

			public class Gold : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					BattleSystem.DelayInput(DeathCheck(Targets));
				}

				private IEnumerator DeathCheck(List<BattleChar> Targets)
				{

					foreach (var target in Targets)
					{
						if (target.IsDead)
						{
							PlayData.TSavedata._Gold += 500;
						}
					}
					yield break;
				}
			}

			public class Love : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
					SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
				}

				public override void FixedUpdate()
				{
					foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
					{
						if (Utils.ReturnBuff(enemy, ModItemKeys.Buff_B_Abnormality_NaturalLv1_Justice_0) != null)
						{
							PlusSkillPerFinal.Damage = 50;
							SkillParticleOn();
						}
						else
						{
							SkillParticleOff();
						}
					}
				}
			}

			public class Nihil : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					BattleSystem.DelayInput(HandCheck());
				}

				private IEnumerator HandCheck()
				{
					yield return new WaitForEndOfFrame();

					if (Utils.AllyTeam.Skills.Count == 0)
					{
						Utils.AllyTeam.Draw(4);
					}
				}
			}

			public class Sword : Ex_EGO, IP_SkillUse_Target
			{
				public override void Init()
				{
					Cooldown = 3;
					OnePassive = true;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Scripts.DestroyActions(Targets);
				}

				public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
				{
					if (Cri)
					{
						Scripts.DestroyActions(hit);
					}
				}
			}
		}
	}
}
