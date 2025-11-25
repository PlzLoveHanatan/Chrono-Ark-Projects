using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;
using static EmotionSystem.Extended.EGO;

namespace EmotionSystem
{
	public class NaturalBuff
	{
		public class Abnormality
		{
			public class Lv1
			{
				public class Blades : Buff, IP_SkillUse_Target, IP_Awake
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 100).ToString());
					}

					public void Awake()
					{
						EmotionManager.InvertEmotionPoints(BChar);
					}

					public override void Init()
					{
						PlusStat.cri = 10;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage)
						{
							Utils.PlaySound("Floor_Art_Blades");
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Fragile, 1, Utils.ChanceDebuff(BChar, 100));
						}
					}
				}

				public class Blessing : Buff, IP_SkillUse_Target, IP_PlayerTurn, IP_Awake
				{
					private bool oncePerTurn;

					public override string DescExtended()
					{
						int healNum = (int)(BChar.GetStat.atk * 0.5f);
						return base.DescExtended().Replace("&a", healNum.ToString());
					}

					public void Awake()
					{
						EmotionManager.InvertEmotionPoints(BChar);
					}

					public override void Init()
					{
						PlusStat.cri = 20;
						PlusStat.hit = 20;
						PlusStat.crihit = 20;
					}

					public void Turn()
					{
						oncePerTurn = false;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage && Cri && !oncePerTurn)
						{
							Utils.PlaySound("Floor_Art_Blessing");
							int healNum = (int)(BChar.GetStat.atk * 0.5f);
							BChar.StartCoroutine(Utils.HealingParticle(BChar, Utils.DummyChar, healNum, true, false, false, true, true));
							oncePerTurn = true;
						}
					}
				}

				public class Despair : Buff, IP_SkillUse_Target, IP_Awake
				{
					public bool DespairDrawBack = true;

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 100).ToString());
					}

					public void Awake()
					{
						Scripts.LoseDrawBacks(BChar);
						EmotionManager.InvertEmotionPoints(BChar);
					}

					public override void Init()
					{
						PlusStat.cri = 20;
						PlusStat.hit = 20;
						PlusStat.crihit = DespairDrawBack ? 20 : 0;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage)
						{
							Utils.PlaySound("Floor_Art_Despair");
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Fragile, 2, Utils.ChanceDebuff(BChar, 100));
						}
					}
				}

				public class Hate : Buff, IP_Awake
				{
					public bool HateDrawBack = true;

					public void Awake()
					{
						Scripts.LoseDrawBacks(BChar);
						EmotionSystem_EGO_Button.instance?.AddEGOSkill(BChar, ModItemKeys.Skill_S_Abnormality_Natural_Hate);
					}

					public override void Init()
					{
						PlusPerStat.Heal = 20;
						PlusStat.AggroPer = HateDrawBack ? 100 : 0;
					}
				}

				public class Hate_0 : Buff
				{
					public override void Init()
					{
						PlusPerStat.Heal = -100;
					}
				}

				public class Justice : Buff, IP_PlayerTurn_1, IP_SkillUse_Target
				{
					private bool oncePerTurn;

					public override string DescExtended()
					{
						int healNum = (int)(BChar.GetStat.reg * 0.25);
						return base.DescExtended().Replace("&a", healNum.ToString());
					}

					public override void Init()
					{
						PlusPerStat.Heal = 10;
					}

					public void Turn1()
					{
						oncePerTurn = false;
						ApplyVillain();
					}

					public void ApplyVillain()
					{
						var enemies = Utils.EnemyTeam.AliveChars;
						int index = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, enemies.Count);
						var randomEnemy = enemies[index];

						Utils.PlaySound("Floor_Art_Justice");
						Utils.AddDebuff(randomEnemy, BChar, ModItemKeys.Buff_B_Abnormality_NaturalLv1_Justice_0);
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (Utils.ReturnBuff(hit, ModItemKeys.Buff_B_Abnormality_NaturalLv1_Justice_0) != null && !oncePerTurn)
						{
							Utils.PlaySound("Floor_Art_Love");
							int healNum = (int)(BChar.GetStat.reg * 0.25f);
							BChar.StartCoroutine(Utils.HealingParticle(null, BChar, healNum, true, false, true, true, true));
							oncePerTurn = true;
						}
					}
				}

				public class Justice_0 : Buff, IP_PlayerTurn
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

				public class Love : Buff, IP_SkillUse_Target, IP_PlayerTurn
				{
					public override string DescExtended()
					{
						int healNum = (int)(BChar.GetStat.reg * 0.5);
						return base.DescExtended().Replace("&a", healNum.ToString());
					}

					private bool oncePerTurn;

					public override void Init()
					{
						PlusPerStat.Heal = 10;
					}

					public void Turn()
					{
						oncePerTurn = false;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage && !oncePerTurn)
						{
							Utils.PlaySound("Floor_Art_Love");
							int healNum = (int)(BChar.GetStat.reg * 0.5f);
							BChar.StartCoroutine(Utils.HealingParticle(null, BChar, healNum, true, false, true, true, true));
							oncePerTurn = true;
						}
					}
				}
			}

			public class Lv2
			{
				public class KingGreed : Buff, IP_SkillUse_Target
				{
					protected virtual int CritDamage => 20;
					protected virtual int DamageTake => 0;
					protected virtual bool isGreed => false;


					public override void Init()
					{
						PlusStat.Weak = true;
						PlusStat.PlusCriDmg = CritDamage;
						PlusStat.DMGTaken = DamageTake;
						LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Abnormality_KingGreed);
					}

					public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
					{
						return AddedSkill.ExtendedFind<Extended.Abnormality.KingGreed>() == null
						&& AddedSkill.Master == BChar && AddedSkill.IsDamage
						&& (AddedSkill.MySkill.Target.Key == GDEItemKeys.s_targettype_enemy || AddedSkill.MySkill.Target.Key == GDEItemKeys.s_targettype_all_other);
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						bool canEarnMoney = BattleSystem.instance.TurnNum < BattleSystem.instance.FogTurn;
						int moneyEarn = 0;

						if (SP.SkillData.IsDamage && canEarnMoney)
						{
							Utils.PlaySound("Floor_Art_KingGreed");
							moneyEarn = hit.Info.Ally ? DMG : DMG / 4;

							if (isGreed)
							{
								moneyEarn *= 2;
							}
							PlayData.TSavedata._Gold += moneyEarn;
						}
					}
				}

				public class Companion : Buff, IP_PlayerTurn_1
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 125).ToString());
					}

					public override void Init()
					{
						PlusStat.HIT_CC = 20;
						PlusStat.HIT_DEBUFF = 20;
						PlusStat.HIT_DOT = 20;
					}

					public void Turn1()
					{
						if (Utils.AllyTeam.AliveChars.Count >= PlayData.TSavedata.Party.Count)
						{
							Utils.AllyTeam.Draw();
							Utils.AllyTeam.AP += 1;

							foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
							{
								Utils.ApplyErosion(enemy, BChar, 1, 125);
							}
						}
					}
				}

				public class Greed : KingGreed, IP_Awake
				{
					protected override int CritDamage => 40;
					protected override int DamageTake => GreedDrawBack ? 40 : 0;
					protected override bool isGreed => true;

					public bool GreedDrawBack = true;

					public void Awake()
					{
						Utils.PlaySound("Floor_Art_Greed");
						Scripts.LoseDrawBacks(BChar);
						EmotionSystem_EGO_Button.instance.AddEGOSkill(BChar, ModItemKeys.Skill_S_Abnormality_Natural_Intemperance);
						EmotionSystem_EGO_Button.instance.AddEGOSkill(BChar, ModItemKeys.Skill_S_Abnormality_Natural_Road);
					}
				}

				public class Intemperance : KingGreed, IP_Awake
				{
					public void Awake()
					{
						EmotionSystem_EGO_Button.instance.AddEGOSkill(BChar, ModItemKeys.Skill_S_Abnormality_Natural_Intemperance);
					}
				}

				public class Magical : Buff, IP_Hit, IP_Dodge, IP_DamageTake
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 125).ToString());
					}

					public override void Init()
					{
						PlusStat.AggroPer = 100;
						PlusStat.Strength = true;
					}

					public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
					{
						if (Dmg >= 1)
						{
							Utils.PlaySound("Floor_Art_MagicalGirls");
							foreach (var skill in Utils.AllyTeam.Skills.Where(s => s.Master == BChar))
							{
								if (Utils.ApplyExtended(skill, ModItemKeys.SkillExtended_Ex_Abnormality_MagicalGirls, false) is Extended.Abnormality.MagicalGirls ex)
								{
									ex.ManaReduction++;
								}
							}
						}
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

					private void ApplyDebuffs(BattleChar target)
					{
						Utils.ApplyErosion(target, BChar, 1, 125);
					}
				}

				public class Road : KingGreed, IP_Awake
				{
					protected override int DamageTake => -20;
					protected override int CritDamage => 0;
					public void Awake()
					{
						EmotionSystem_EGO_Button.instance.AddEGOSkill(BChar, ModItemKeys.Skill_S_Abnormality_Natural_Road);
					}
				}

				public class Road_0 : Buff
				{
					public override void Init()
					{
						PlusStat.DMGTaken = -100;
					}
				}

				public class Wrath : Buff, IP_SkillUse_User, IP_Awake
				{
					public bool WrathDrawBack = true;

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 125).ToString());
					}

					public void Awake()
					{
						Scripts.LoseDrawBacks(BChar);
					}

					public override void Init()
					{
						PlusStat.HIT_CC = 40;
						PlusStat.HIT_DEBUFF = 40;
						PlusStat.HIT_DOT = 40;
						PlusPerStat.Damage = 40;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.IsDamage && SkillD.Master == BChar)
						{
							Utils.ApplyErosion(Targets, BChar, 1, 125);

							if (!SkillD.FreeUse && !SkillD.BasicSkill && !SkillD.PlusHit && !WrathDrawBack)
							{
								Utils.PlaySound("Floor_Art_Wrath");
								Scripts.AttackRedirect(BChar, SkillD, Targets, false, 30);
							}							
						}
					}
				}
			}


			public class Lv3
			{
				public class Acidic : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 150).ToString());
					}

					public override void Init()
					{
						PlusStat.HIT_CC = 40;
						PlusStat.HIT_DEBUFF = 40;
						PlusStat.HIT_DOT = 40;
						PlusPerStat.Damage = 40;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
						{
							Utils.PlaySound("Floor_Art_Acidic");
							Utils.ApplyErosion(hit, BChar, 2, 150);
						}
					}
				}


				public class Nix : Buff, IP_Awake, IP_PlayerTurn
				{
					public bool noDrawBacks = false;

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 150).ToString())
							.Replace("&b", Utils.ChanceCC(BChar, 150).ToString());
					}

					public void Awake()
					{
						noDrawBacks = Scripts.LoseDrawBacks(BChar);
					}

					public void Turn()
					{
						if (noDrawBacks)
						{
							Utils.PlaySound("Floor_Art_MagicalGirls");

							foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
							{
								Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Feeble, 5, Utils.ChanceDebuff(BChar, 150));
								Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Disarm, 5, Utils.ChanceDebuff(BChar, 150));
								Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Bind, 5, Utils.ChanceCC(BChar, 150));
							}
						}
					}
				}

				public class Void : Buff
				{
					public int SkillsRemoved;

					public override void BuffStat()
					{
						PlusStat.atk = SkillsRemoved * 2;
						PlusStat.reg = SkillsRemoved * 2;
						PlusStat.def = SkillsRemoved * 5;
					}
				}
			}

			public class EGO
			{

			}
		}
	}
}
