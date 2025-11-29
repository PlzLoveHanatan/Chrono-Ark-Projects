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
using static EmotionSystem.NaturalBuff.Abnormality.Lv3;

namespace EmotionSystem
{
	public class NaturalSkill
	{
		public class Abnormality
		{
			public class Hate : Ex_EGO
			{
				public override string DescExtended(string desc)
				{
					int damage = (int)(BChar.GetStat.reg);
					return base.DescExtended(desc).Replace("&a", damage.ToString());
				}

				public override bool Terms()
				{
					return BChar.Info.GetData.Role.Key == GDEItemKeys.CharRole_Role_Support && NowCooldown <= 0;
				}

				public override void Init()
				{
					Cooldown = 2;
				}

				public override void FixedUpdate()
				{
					SkillBasePlus.Target_BaseDMG = (int)(BChar.GetStat.reg);
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_Hate");
				}
			}

			public class KinGreed : Ex_EGO
			{
				protected virtual int Gold => 1000;
				protected virtual bool IsDestroyActions => false;

				protected virtual bool Intemperance => true;

				public override bool Terms()
				{
					return PlayData.TSavedata._Gold >= Gold;
				}

				public override void Init()
				{
					OncePerFight = true;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					PlayData.TSavedata._Gold -= Gold;

					if (IsDestroyActions)
					{
						Scripts.DestroyAllActions(Targets);
					}

					string sound = Intemperance ? "Floor_Art_Intemperance" : "Floor_Art_Road";
					Utils.PlaySound(sound);
				}
			}

			public class Intemperance : KinGreed
			{
				protected override int Gold => 1000;
				protected override bool IsDestroyActions => true;
			}

			public class Road : KinGreed
			{
				protected override int Gold => 800;
			}

			public class VoidAbno : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					EmotionSystem_EGO_Button.instance.AddEGOSkill(BChar, ModItemKeys.Skill_S_Abnormality_Natural_Void);
				}
			}

			public class Void : Ex_EGO
			{
				public override void Init()
				{
					Cooldown = 1;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_Void");
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
							Utils.RemoveSkill(skill, true);
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
				private bool NoDrawBack => Utils.ReturnBuff(BChar, ModItemKeys.Buff_B_Abnormality_NaturalLv3_Nix) is Nix nix && nix.noDrawBacks;

				public override string DescExtended(string desc)
				{
					string text = NoDrawBack ? "" : ModLocalization.EmotionSystem_EGO_Rage;
					return base.DescExtended(desc).Replace("Description", text.ToString());
				}

				public override void Init()
				{
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_Rage");

					if (!NoDrawBack)
					{
						Scripts.AttackRedirect(BChar, SkillD, Targets, false, 30);
					}					
					BattleSystem.DelayInput(Scripts.RecastSkillErosion(Targets[0], BChar, ModItemKeys.Skill_S_EGO_Natural_Rage, 2, 5, 300));
				}
			}

			public class Gold : Ex_EGO
			{
				public override string DescExtended(string desc)
				{
					int daamage = (int)(PlayData.TSavedata._Gold * 0.025f);
					return base.DescExtended(desc).Replace("&a", daamage.ToString());
				}

				public override void Init()
				{
					Cooldown = 3;
				}

				public override void FixedUpdate()
				{
					SkillBasePlus.Target_BaseDMG = (int)(PlayData.TSavedata._Gold * 0.025f);
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_Gold");
					BattleSystem.DelayInput(DeathCheck(Targets));
				}

				private IEnumerator DeathCheck(List<BattleChar> Targets)
				{

					foreach (var target in Targets)
					{
						if (target.IsDead)
						{
							PlayData.TSavedata._Gold += 300;
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
							PlusSkillPerFinal.Damage = 0;
							SkillParticleOff();
						}
					}
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Art_Love");
					Utils.PlaySound("Floor_Art_LoveHate");
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
					Utils.PlaySound("Floor_Art_Void");
					BattleSystem.DelayInputAfter(HandCheck());
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
					Utils.PlaySound("Floor_Art_Sword");
					Scripts.DestroyActions(Targets);
				}

				public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
				{
					if (Cri)
					{
						Utils.PlaySound("Floor_Art_Sword_Cry");
						Scripts.DestroyActions(hit);
					}
				}
			}
		}
	}
}
