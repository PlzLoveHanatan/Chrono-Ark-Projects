using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using UnityEngine;
using static EmotionSystem.Scripts;

namespace EmotionSystem
{
	public class HistoryBuff
	{
		public class Abnormality
		{
			public class Lv1
			{
				public class Ashes : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						int chance = (int)(BChar.GetStat.HIT_DOT + 100);
						return base.DescExtended().Replace("&a", chance.ToString());
					}

					public override void BuffStat()
					{
						PlusStat.HIT_CC = 10f;
						PlusStat.HIT_DEBUFF = 10f;
						PlusStat.HIT_DOT = 10f;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
						{
							Utils.PlaySound("Floor_History_Ashes");
							Utils.ApplyBurn(hit, BChar);
						}
					}
				}

				public class DisplayAffection : Buff, IP_SkillUse_User, IP_DamageChange
				{
					public override void Init()
					{
						PlusStat.IgnoreTaunt = true;
						PlusStat.hit = 20;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.IsDamage && SkillD.Master == BChar)
						{
							Utils.PlaySound("Floor_History_DisplayAffection");
						}
					}

					public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
					{
						if (SkillD.IsDamage && SkillD.Master == BChar && Target is BattleEnemy enemy)
						{
							bool additionalDamage = enemy.SkillQueue[0].CastSpeed == 0 || enemy.SkillQueue[0].CastSpeed == 1 || enemy.SkillQueue[0].CastSpeed == 2 || enemy.SkillQueue[0].CastSpeed >= 9;

							if (enemy.SkillQueue.Count > 0 && additionalDamage)
							{
								Damage += (int)(Damage * 0.2);
							}
							else
							{
								Damage -= (int)(Damage * 0.4);
							}
						}
						return Damage;
					}
				}

				public class HappyMemories : Buff, IP_PlayerTurn_1
				{
					public override void Init()
					{
						PlusStat.hit = 10;
					}

					public void Turn1()
					{
						Utils.AddBuff(BChar, ModItemKeys.Buff_B_Abnormality_HistoryLv1_HappyMemories_0);
					}
				}

				public class HappyMemories_0 : Buff, IP_SkillUseHand_Team
				{
					public override void Init()
					{
						base.Init();
						LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Abnormality_HappyMemories);
					}

					public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
					{
						return AddedSkill.ExtendedFind<Extended.Abnormality.HappyMemories>() == null && AddedSkill.Master == BChar;
					}

					public void SKillUseHand_Team(Skill skill)
					{
						if (skill.Master == BChar && !skill.FreeUse)
						{
							Utils.PlaySound("Floor_History_HappyMemories");
							SelfDestroy();
						}
					}
				}

				public class Matchlight : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						int damage = (int)(BChar.GetStat.maxhp * 0.15f);
						int chance = (int)(BChar.GetStat.HIT_DOT + 100);
						return base.DescExtended().Replace("&a", damage.ToString()).Replace("&b", chance.ToString());
					}

					public override void Init()
					{
						PlusPerStat.Damage = 20;
						PlusStat.HIT_CC = 20f;
						PlusStat.HIT_DEBUFF = 20f;
						PlusStat.HIT_DOT = 20f;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage)
						{
							Utils.PlaySound("Floor_History_Matchlight");
							Utils.ApplyBurn(hit, BChar, 2);

							bool neverLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 20);

							if (neverLucky)
							{
								int damage = (int)(BChar.GetStat.maxhp * 0.15f);
								Utils.TakeNonLethalDamage(BChar, damage);
								Utils.PlaySound("Floor_History_Explode");
							}
						}
					}
				}

				public class NostalgicEmbrace : Buff, IP_SkillUse_Target, IP_PlayerTurn
				{
					private bool OncePerTurn;

					public override string DescExtended()
					{
						int chance = (int)(BChar.GetStat.HIT_CC + 100);
						return base.DescExtended().Replace("&a", chance.ToString());
					}

					public override void Init()
					{
						PlusStat.hit = 10;
					}

					public void Turn()
					{
						OncePerTurn = false;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && !OncePerTurn)
						{
							bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 50);

							if (alwaysLucky)
							{
								int chance = (int)(BChar.GetStat.HIT_CC + 100);
								Utils.PlaySound("Floor_History_Embrace");
								Utils.AddDebuff(hit, BChar, GDEItemKeys.Buff_B_Common_Rest, chance);
								OncePerTurn = true;
							}
						}
					}
				}

				public class FairiesCare : Buff
				{
					public override void Init()
					{
						PlusStat.DMGTaken = 20;
						PlusPerStat.Damage = 20;
						PlusStat.PlusCriDmg = 20;
					}
				}
			}

			public class Lv2
			{
				public class Footfalls : Buff, IP_SkillUse_Target
				{
					public override void Init()
					{
						PlusPerStat.Damage = 40;
						PlusStat.HIT_CC = 40f;
						PlusStat.HIT_DEBUFF = 40f;
						PlusStat.HIT_DOT = 40f;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						bool lowHp = BChar.HP <= BChar.GetStat.maxhp * 0.2f;

						if (SP.SkillData.Master == BChar && !hit.Info.Ally && lowHp)
						{
							BattleSystem.DelayInput(StartCrying(hit));
						}
					}

					private IEnumerator StartCrying(BattleChar hit)
					{
						Utils.PlaySound("Floor_History_Cry");
						BattleSystem.instance.BlackBackground.SetBackgroundDirect(0.5f, 1.5f);
						yield return new WaitForSecondsRealtime(3f);
						yield return new WaitForSecondsRealtime(2f);
						BattleSystem.DelayInput(Explode(hit));
					}

					private IEnumerator Explode(BattleChar hit)
					{
						int damage = Mathf.Min(80, (int)(hit.GetStat.maxhp * 0.8f));
						hit.Damage(BChar, damage, false, true, false, 0, false, false, false);
						Utils.ApplyBurn(hit, BChar, 20);
						Utils.PlaySound("Floor_History_Explode");
						BChar.Dead();
						ChargeLucyNeck();
						yield return BackgroundOff();
					}

					public IEnumerator BackgroundOff()
					{
						BattleSystem.instance.BlackBackground.SetBackgroundDirectOff();
						yield return null;
						yield break;
					}
				}

				public class Gluttony : Buff, IP_DealDamage
				{
					public override void Init()
					{
						PlusStat.PlusCriDmg = 20f;
					}

					public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
					{
						if (Damage >= 1)
						{
							Utils.PlaySound("Floor_History_Gluttony");
							BChar.Heal(BChar, (int)(Damage * 0.2), false, false, null);
						}
					}
				}

				public class Predation : Buff
				{
					public override void Init()
					{
						PlusPerStat.Damage = 40;
						PlusStat.PlusCriDmg = 40;
						PlusStat.DMGTaken = 40;
					}
				}

				public class Spores : Buff, IP_Hit, IP_Dodge
				{
					public override string DescExtended()
					{
						int chance = (int)(BChar.GetStat.HIT_DOT + 100);
						return base.DescExtended().Replace("&a", chance.ToString());
					}

					public override void Init()
					{
						PlusStat.Strength = true;
						PlusStat.AggroPer = 60;
					}

					public void Hit(SkillParticle SP, int Dmg, bool Cri)
					{
						if (SP.SkillData.IsDamage)
						{
							ApplyDebuffs(SP.UseStatus);
						}
					}

					public void Dodge(BattleChar Char, SkillParticle SP)
					{
						if (Char == BChar && SP.SkillData.IsDamage)
						{
							ApplyDebuffs(SP.UseStatus);
						}
					}

					private void ApplyDebuffs(BattleChar enemy)
					{
						Utils.PlaySound("Floor_History_Spores");
						Utils.ApplyBleed(enemy, BChar, 4);
						Utils.ApplyBurn(enemy, BChar, 4);
					}
				}

				public class Vines : Buff
				{
					public override void Init()
					{
						PlusStat.DMGTaken = -20;
						PlusStat.RES_CC = 20f;
						PlusStat.RES_DEBUFF = 20f;
						PlusStat.RES_DOT = 20f;
					}
				}

				public class WorkerBee : Buff, IP_Hit, IP_Dodge
				{
					public override string DescExtended()
					{
						int chance = (int)(BChar.GetStat.HIT_DEBUFF + 150);
						return base.DescExtended().Replace("&a", chance.ToString());
					}

					public override void BuffStat()
					{
						PlusStat.DMGTaken = 20f;
						PlusStat.Strength = true;
					}

					public void Hit(SkillParticle SP, int Dmg, bool Cri)
					{
						if (SP.SkillData.IsDamage)
						{
							ApplyDebuffs(SP.UseStatus);
						}
					}

					public void Dodge(BattleChar Char, SkillParticle SP)
					{
						if (Char == BChar && SP.SkillData.IsDamage)
						{
							ApplyDebuffs(SP.UseStatus);
						}
					}

					private void ApplyDebuffs(BattleChar enemy)
					{
						Utils.PlaySound("Floor_History_WorkerBee");
						Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_Abnormality_HistoryLv2_WorkerBee_0);
					}
				}

				public class WorkerBee_0 : Buff
				{
					public override void BuffStat()
					{
						PlusStat.DMGTaken = 40f * StackNum;
					}
				}
			}

			public class Lv3
			{
				public class BarrierThorns : Buff, IP_Awake
				{
					public override void Init()
					{
						PlusStat.DMGTaken = -40;
						PlusStat.RES_CC = 40f;
						PlusStat.RES_DEBUFF = 40f;
						PlusStat.RES_DOT = 40f;
					}

					public void Awake()
					{
						Utils.PlaySound("Floor_History_BarrierThorns");
					}
				}

				public class Loyalty : Buff
				{
					public override void BuffStat()
					{
						PlusStat.DMGTaken = 40;
						PlusPerStat.Damage = 80;
						PlusPerStat.Heal = 80;
						PlusStat.Strength = true;
					}
				}
				public class Malice : Buff, IP_SkillUse_User
				{
					public override void Init()
					{
						PlusPerStat.Damage = 40;
						PlusStat.cri = 40;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.Master == BChar && SkillD.IsDamage && !SkillD.FreeUse)
						{
							Utils.PlaySound("Floor_History_Malice");
						}
					}
				}
			}
		}

		public class EGO
		{
			public class HornetSting : Buff
			{
				public override void BuffStat()
				{
					PlusStat.def = -50;
				}
			}

			public class GreenStem : Buff
			{
				public override void Init()
				{
					PlusPerStat.Damage = -25;
					PlusStat.def = -25;
				}
			}
		}
	}
}
