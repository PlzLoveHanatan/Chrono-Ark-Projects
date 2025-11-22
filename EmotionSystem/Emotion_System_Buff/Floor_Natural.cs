using System;
using System.Collections;
using System.Collections.Generic;
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
				public class Blades : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar).ToString());
					}

					public override void Init()
					{
						PlusStat.cri = 10;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage)
						{
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Fragile, 1);
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
						PlusStat.def = -20;
					}

					public void Turn()
					{
						oncePerTurn = false;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage && Cri && !oncePerTurn)
						{
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
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar).ToString());
					}

					public void Awake()
					{
						Scripts.LoseDrawBacks(BChar);
						EmotionManager.InvertEmotionPoints(BChar);
					}

					public override void Init()
					{
						PlusStat.cri = 20;
						PlusStat.def = DespairDrawBack ? - 20 : 0;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage)
						{
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Fragile, 2);
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

				public class Justice : Buff, IP_PlayerTurn_1
				{
					public override void Init()
					{
						PlusPerStat.Heal = 10;
					}

					public void Turn1()
					{
						Utils.AddBuff(BChar, ModItemKeys.Buff_B_Abnormality_NaturalLv1_Justice_1);
					}
				}

				public class Justice_0 : Buff
				{
					public override void Init()
					{
						PlusStat.IgnoreTaunt_EnemySelf = true;
						PlusStat.DMGTaken = 10;
					}
				}

				public class Justice_1 : Buff, IP_SkillUse_Target
				{
					public override void Init()
					{
						base.Init();
						LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Abnormality_Justice);
					}

					public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
					{
						return AddedSkill.ExtendedFind<Extended.Abnormality.Justice>() == null && AddedSkill.Master == BChar && AddedSkill.IsDamage;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage && !hit.Info.Ally && !SP.SkillData.BasicSkill)
						{
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_Abnormality_NaturalLv1_Justice_0);
							SelfDestroy();
						}
					}
				}

				public class Love : Buff, IP_SkillUse_Target, IP_PlayerTurn
				{
					public override string DescExtended()
					{
						int healNum = (int)(BChar.GetStat.reg * 0.3);
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
							int healNum = (int)(BChar.GetStat.reg * 0.3f);
							BChar.StartCoroutine(Utils.HealingParticle(BChar, Utils.DummyChar, healNum, true, true, false, true, true));
							oncePerTurn = true;
						}
					}
				}
			}

			public class Lv2
			{
				public class KingGreed : Buff, IP_SkillUse_Target
				{
					protected virtual int GoldGain => 20;
					protected virtual int CritDamage => 20;
					protected virtual int DamageTake => 0;

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

						if (SP.SkillData.IsDamage && canEarnMoney)
						{
							PlayData.TSavedata._Gold += GoldGain;
						}
					}
				}

				public class Companion : Buff, IP_PlayerTurn_1
				{
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
						}
					}
				}

				public class Greed : KingGreed, IP_Awake
				{
					protected override int GoldGain => 40;
					protected override int CritDamage => 40;
					protected override int DamageTake => GreedDrawBack ? 40 : 0;

					public bool GreedDrawBack = true;

					public void Awake()
					{
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
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 25).ToString());
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
						Utils.ApplyErosion(target, BChar, 1, 25);
					}
				}

				public class Road : KingGreed, IP_Awake
				{
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
						if (!WrathDrawBack) return;

						if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.BasicSkill && !SkillD.PlusHit)
						{
							Scripts.AttackRedirect(BChar, SkillD, Targets, false, 30);
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
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 50).ToString());
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
							Utils.ApplyErosion(hit, BChar, 2, 50);
						}
					}
				}

				public class Nix : Buff, IP_Awake, IP_PlayerTurn
				{
					public bool noDrawBacks = false;

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 50).ToString()).Replace("&b", Utils.ChanceCC(BChar, 50).ToString());
					}

					public void Awake()
					{
						noDrawBacks = Scripts.LoseDrawBacks(BChar);
					}

					public void Turn()
					{
						if (noDrawBacks)
						{
							foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
							{
								Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Feeble, 5, 50);
								Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Disarm, 5, 50);
								Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EmotionSystem_Bind, 5, 50);
							}
						}
					}
				}

				public class Void : Buff, IP_Awake
				{
					public int SkillsRemoved;

					public void Awake()
					{
						EmotionSystem_EGO_Button.instance.AddEGOSkill(BChar, ModItemKeys.Skill_S_Abnormality_Natural_Void);
					}

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
