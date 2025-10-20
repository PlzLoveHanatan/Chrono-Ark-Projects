using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalSystem;
using EmotionalSystemSkillExtended;
using GameDataEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using static EmotionalSystem.DataStore.Synchronize;
using static EmotionalSystem.Scripts;
using static EmotionalSystemSkillExtended.ExAbnormality;

namespace EmotionalSystemBuff
{
	public class FloorTechnological
	{
		public class Abnormality
		{
			public class Lv1
			{
				public class Lament : Buff, IP_TurnEnd
				{
					public override string DescExtended()
					{
						int damage = (int)(BChar.GetStat.maxhp * 0.1f);
						return base.DescExtended().Replace("&a", damage.ToString());
					}

					public override void Init()
					{
						PlusPerStat.Damage = 20;
						PlusStat.HIT_CC = 20f;
						PlusStat.HIT_DEBUFF = 20f;
						PlusStat.HIT_DOT = 20f;
					}

					public void TurnEnd()
					{
						Utils.PlaySound("Floor_Technological_Lament");
						int damage = (int)(BChar.GetStat.maxhp * 0.1f);
						Utils.TakeNonLethalDamage(BChar, damage);
					}
				}

				public class MetallicRinging : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						int chance = (int)(BChar.GetStat.HIT_DEBUFF + 100);
						return base.DescExtended().Replace("&a", chance.ToString());
					}

					public override void Init()
					{
						PlusPerStat.Damage = 10;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage)
						{
							Utils.PlaySound("Floor_Technological_Metallic");
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionalSystem_Paralysis);
						}
					}
				}

				public class RepetitivePattern : Buff, IP_SkillUse_User, IP_PlayerTurn
				{
					private bool ManaNextTurn;

					public override void Init()
					{
						PlusStat.cri = 10;
					}

					public void Turn()
					{
						if (ManaNextTurn)
						{
							Utils.AllyTeam.AP += 1;
						}
						ManaNextTurn = false;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.Master == BChar && SkillD.IsDamage)
						{
							Utils.PlaySound("Floor_Technological_Repetitive");
							ManaNextTurn = true;
						}
					}
				}

				public class Request : Buff, IP_PlayerTurn_1
				{
					public override void Init()
					{
						PlusStat.hit = 10;
					}

					public void Turn1()
					{
						ApplyRequestedTarget();
					}

					public void ApplyRequestedTarget()
					{
						var enemies = Utils.EnemyTeam.AliveChars;
						int index = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, enemies.Count);
						var randomEnemy = enemies[index];

						Utils.PlaySound("Floor_Technological_Request");
						Utils.AddDebuff(randomEnemy, BChar, ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Request_0);
					}
				}

				public class Request_0 : Buff, IP_PlayerTurn
				{
					public override void Init()
					{
						PlusStat.IgnoreTaunt_EnemySelf = true;
						PlusStat.DMGTaken = 10;
					}

					public void Turn()
					{
						SelfDestroy();
					}
				}

				public class Rhythm : Buff, IP_SkillUse_User
				{
					public override void Init()
					{
						PlusPerStat.Damage = 20;
						PlusStat.DMGTaken = 20;
						PlusStat.PlusCriDmg = 20;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.Master == BChar && SkillD.IsDamage)
						{
							Utils.PlaySound("Floor_Technological_Rhythm");
						}
					}
				}

				public class Violence : Buff, IP_DamageChange, IP_SkillUse_User
				{
					public override void Init()
					{
						PlusPerStat.Damage = 20;
					}

					public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
					{
						if (SkillD.IsDamage && SkillD.Master == BChar)
						{
							int randomNum = RandomManager.RandomInt(BChar.GetRandomClass().Target, 0, 7);

							switch (randomNum)
							{
								case 0: Damage = (Damage * 0); break;
								case 1: Damage = (int)(Damage * 0.5f); break;
								case 2: Damage = (int)(Damage * 1.5f); break;
								case 3: Damage = (int)(Damage * 2f); break;
								case 4: Damage = (int)(Damage * 2.5f); break;
								case 5: Damage = (int)(Damage * 3f); break;
								default:
									// No damage change
									break;
							}
						}
						return Damage;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.Master == BChar && SkillD.IsDamage)
						{
							Utils.PlaySound("Floor_Technological_Violence");
						}
					}
				}
			}

			public class Lv2
			{
				public class ChainedWrath : Buff, IP_SkillUse_User
				{
					public override void Init()
					{
						PlusPerStat.Damage = 40;
						PlusStat.spd = -2;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.Master == BChar && SkillD.IsDamage)
						{
							Utils.PlaySound("Floor_Technological_ChainedWrath");
						}
					}
				}

				public class Clean : Buff, IP_SkillUse_User, IP_DealDamage, IP_PlayerTurn
				{
					private bool oncePerturn;

					public override void Init()
					{
						PlusStat.cri = 20;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.Master == BChar && SkillD.IsDamage)
						{
							Utils.PlaySound("Floor_Technological_Clean");
						}
					}

					public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
					{
						if (IsCri && !oncePerturn)
						{
							Utils.AllyTeam.AP += 1;
							oncePerturn = true;
						}
					}

					public void Turn()
					{
						oncePerturn = false;
					}
				}

				public class EternalRest : Buff, IP_DealDamage
				{
					public override string DescExtended()
					{
						int chance = (int)(BChar.GetStat.HIT_CC + 100);
						return base.DescExtended().Replace("&a", chance.ToString());
					}

					public override void Init()
					{
						PlusStat.HIT_CC = 20f;
						PlusStat.HIT_DEBUFF = 20f;
						PlusStat.HIT_DOT = 20f;
					}

					public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
					{
						if (Take is BattleEnemy enemy)
						{
							int threshold = (int)(enemy.GetStat.maxhp * 0.1);

							if (Damage >= threshold)
							{
								int chance = (int)(BChar.GetStat.HIT_CC + 100);
								Utils.PlaySound("Floor_Technological_EternalRest");
								Utils.AddDebuff(enemy, BChar, GDEItemKeys.Buff_B_Common_Rest, 1, chance);
							}
						}
					}
				}

				public class MusicalAddiction : Buff, IP_SkillUse_User
				{
					public override void Init()
					{
						PlusPerStat.Damage = 40;
						PlusStat.DMGTaken = 40;
						PlusStat.PlusCriDmg = 40;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.Master == BChar && SkillD.IsDamage)
						{
							Utils.PlaySound("Floor_Technological_Musical");
						}
					}
				}

				public class Recharge : Buff, IP_PlayerTurn, IP_Awake
				{
					public override void Init()
					{
						PlusStat.cri = 20;
						//PlusStat.hit = 20;
					}

					public void Turn()
					{
						Utils.PlaySound("Floor_Technological_Repetitive");
						Utils.AllyTeam.AP += 1;
					}

					public void KillEffect(SkillParticle SP)
					{
						foreach (var target in SP.TargetChar)
						{
							if (target is BattleEnemy enemy && enemy.IsDead && SP.SkillData.Master == BChar)
							{
								Utils.PlaySound("Floor_Technological_Repetitive");
								Utils.AllyTeam.AP += 2;
							}
						}
					}

					public void Awake()
					{
						Utils.PlaySound("Floor_Technological_Repetitive");
						Utils.AllyTeam.AP += 1;
					}
				}

				public class SeventhBullet : Buff, IP_SkillUse_User
				{
					private int AttackUses;

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", AttackUses.ToString());
					}

					public override void Init()
					{
						PlusPerStat.Damage = 40;
						PlusStat.hit = 40;
						PlusStat.HitMaximum = true;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.BasicSkill)
						{
							AttackUses++;

							if (AttackUses == 6)
							{
								Utils.AddBuff(BChar, ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_SeventhBullet_0);
							}

							if (AttackUses >= 7)
							{
								Utils.PlaySound("Floor_Technological_TheSeventhBullet");
								AttackRedirect(BChar, SkillD, Targets, SkillD.TargetDamage);
								Utils.RemoveBuff(BChar, ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_SeventhBullet_0, true);
								AttackUses = 0;
							}
						}
					}
				}

				public class SeventhBullet_0 : Buff
				{
					public override void Init()
					{
						base.Init();
						LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Abnormality_SeventhBullet);
					}

					public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
					{
						return AddedSkill.ExtendedFind<ExAbnormality.SeventhBullet>() == null && AddedSkill.Master == BChar && AddedSkill.IsDamage;
					}
				}

				public class SeventhBullet_1 : Buff, IP_DamageTake
				{
					public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
					{
						if (Dmg >= 1)
						{
							resist = true;
							SelfDestroy();
						}
					}
				}
			}

			public class Lv3
			{
				public class Coffin : Buff, IP_SkillUse_User, IP_PlayerTurn
				{
					private bool OncePerTurn;

					public override void Init()
					{
						PlusStat.HIT_CC = 40;
						PlusStat.HIT_DEBUFF = 40;
						PlusStat.HIT_DOT = 40;
					}

					public override string DescExtended()
					{
						string text = OncePerTurn ? "Inactive" : "Active";
						return base.DescExtended().Replace("&a", text.ToString());
					}

					public void Turn()
					{
						OncePerTurn = false;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.IsDamage && SkillD.Master == BChar && !OncePerTurn)
						{
							DestroyActions(Targets[0]);
							Utils.PlaySound("Floor_Technological_Coffin");
							OncePerTurn = true;
						}
					}
				}

				public class DarkFlame : Buff, IP_Awake
				{
					public override void Init()
					{
						PlusPerStat.Damage = 80;
						PlusStat.hit = 80;
					}

					public void Awake()
					{
						foreach (var target in Utils.AllyTeam.AliveChars.Concat(Utils.EnemyTeam.AliveChars_Vanish))
						{
							Utils.AddBuff(target, ModItemKeys.Buff_B_Abnormality_TechnologicalLv3_DarkFlame_0);
						}
					}
				}

				public class DarkFlame_0 : Buff
				{
					public override void Init()
					{
						PlusStat.RES_CC = -300;
						PlusStat.RES_DEBUFF = -300;
						PlusStat.RES_DOT = -300;
					}
				}

				public class Music : Buff
				{
					public override void Init()
					{
						PlusPerStat.Damage = 80;
						PlusStat.DMGTaken = 80;
					}
				}
			}
		}

		public class EGO
		{
			public class Harmony : Buff
			{
				public override void Init()
				{
					PlusStat.DMGTaken = 25;
					PlusStat.CRIGetDMG = 25;
				}
			}

			public class MagicBullet : Buff, IP_Awake, IP_SkillUse_User
			{
				public override void SelfdestroyPlus()
				{
					DeSynchronize(BChar);
					SelfDestroy();
				}

				public void Awake()
				{
					SynchronizeWithEGO(BChar, ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_Desynchronize, DataStore.Instance.Synchronization.DerSkills);
				}

				public void SkillUse(Skill SkillD, List<BattleChar> Targets)
				{
					if (SkillD.Master == BChar && SkillD.IsDamage)
					{
						Utils.PlaySound("Floor_Technological_Request");
					}
				}
			}
		}
	}
}
