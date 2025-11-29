using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using GameDataEditor;
using I2.Loc;
using NLog.Targets;
using Spine;
using Steamworks;
using UnityEngine;

namespace EmotionSystem
{
	public class ArtBuff
	{
		public class Abnormality
		{
			public class Lv1
			{
				public class Echoes : Buff, IP_PlayerTurn_1
				{
					public override void Init()
					{
						PlusStat.cri = 10;
						PlusStat.Penetration = 25;
					}

					public void Turn1()
					{
						Utils.PlaySound("Floor_Art_Echoes");
						Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Abnormality_Art_Echoes, true);
					}
				}

				public class Elation : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDOT(BChar, 100).ToString());
					}

					public override void Init()
					{
						PlusStat.PlusCriDmg = 20;
						PlusPerStat.Damage = 20;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
						{
							Utils.PlaySound("Floor_Art_Elation");
							Utils.ApplyBleed(hit, BChar, 2, 100);
							bool neverLucky = RandomManager.RandomInt(RandomClassKey.InBattle, 0, 101) == 1;

							if (neverLucky)
							{
								Utils.PlaySound("Floor_Art_Elation_Dead");								
								Utils.ApplyBleed(hit, BChar, 10, 100);
								BChar.Dead(false, true);
								Scripts.ChargeLucyNeck();
							}
						}
					}
				}

				public class Pebble : Buff, IP_PlayerTurn_1
				{
					public override void Init()
					{
						PlusStat.DMGTaken = -20;
					}

					public void Turn1()
					{
						Utils.PlaySound("Floor_Art_Pebble");
						Utils.ApplyExtended(BChar.MyTeam.Skills.ToList(), ModItemKeys.SkillExtended_Ex_Abnormality_Pebble, null, null, false, 1, true);
					}
				}

				public class Powder : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDOT(BChar).ToString());
					}

					public override void Init()
					{
						PlusStat.PlusCriDmg = 20;
						PlusPerStat.Damage = 20;
						PlusStat.DMGTaken = 20;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
						{
							Utils.PlaySound("Floor_Art_Powder");
							Utils.ApplyBleed(hit, BChar, 1);
						}
					}
				}

				public class Tentacles : Buff, IP_SkillUse_Target, IP_PlayerTurn
				{
					private bool OncePerTurn;

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 100).ToString());
					}

					public override void Init()
					{
						PlusStat.cri = 10;
						PlusStat.Penetration = 25;
					}

					public void Turn()
					{
						OncePerTurn = false;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar && hit is BattleEnemy && !OncePerTurn)
						{
							OncePerTurn = true;
							Utils.PlaySound("Floor_Art_Tentacles");
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Disarm, 1, 100);
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Feeble, 1, 100);
						}
					}
				}

				public class Thorns : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDOT(BChar, 100).ToString());
					}

					public override void Init()
					{
						PlusStat.PlusCriDmg = 10;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
						{
							Utils.PlaySound("Floor_Art_Thorns");
							Utils.ApplyBleed(hit, BChar, 1, 100);
						}
					}
				}
			}

			public class Lv2
			{
				public class Autumuns : Buff, IP_Awake
				{
					public void Awake()
					{
						Utils.PlaySound("Floor_Art_Autumns");
					}

					public override void Init()
					{
						PlusPerStat.Heal = 20;
						PlusStat.dod = 20;
						PlusStat.DMGTaken = 20;
					}
				}

				public class Incomprehensible : Buff, IP_SkillUse_Target, IP_PlayerTurn
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 125).ToString());
					}

					private int debuffsPerTurn;

					public override void Init()
					{
						PlusStat.cri = 20;
						PlusStat.Penetration = 50;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar && hit is BattleEnemy && debuffsPerTurn < 2)
						{
							debuffsPerTurn++;
							Utils.PlaySound("Floor_Art_Incomprehensible");
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Disarm, 1, 125);
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Feeble, 1, 125);
						}
					}

					public void Turn()
					{
						debuffsPerTurn = 0;
					}
				}

				public class Perfomance : Buff, IP_Awake
				{
					public void Awake()
					{
						Utils.PlaySound("Floor_Art_Perfomance");
						EmotionSystem_EGO_Button.instance?.AddEGOSkill(BChar, ModItemKeys.Skill_S_Abnormality_Art_Perfomance);
					}
				}

				public class Perfomance_0 : Buff, IP_PlayerTurn
				{
					public void Turn()
					{
						Utils.AllyTeam.AP += 2;
						SelfDestroy();
					}
				}

				public class Petals : Buff, IP_Awake
				{
					public void Awake()
					{
						Utils.PlaySound("Floor_Art_Petals");
					}

					public override void Init()
					{
						PlusPerStat.Heal = 40;
						PlusStat.dod = 40;
						PlusStat.DMGTaken = 40;
					}
				}

				public class Teardrop : Buff, IP_DamageTake
				{
					public override void Init()
					{
						PlusStat.DMGTaken = -40;
					}

					public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
					{
						if (Dmg >= 1)
						{
							BattleSystem.DelayInputAfter(DeathDoorCheck());
						}
					}

					private IEnumerator DeathDoorCheck()
					{
						if (Utils.ReturnBuff(BChar, GDEItemKeys.Buff_B_Neardeath) != null)
						{
							Utils.PlaySound("Floor_Art_Teardrop");
							Utils.AllyTeam.AP -= 2;
						}
						yield break;
					}
				}

				public class Token : Buff, IP_TurnEnd
				{
					public override string DescExtended()
					{
						int heal = (int)(BChar.GetStat.maxhp * 0.2f);
						return base.DescExtended().Replace("&a", heal.ToString());
					}

					public override void Init()
					{
						PlusStat.DMGTaken = -20;
					}

					public void TurnEnd()
					{
						Utils.PlaySound("Floor_Art_Token");
						int heal = (int)(BChar.GetStat.maxhp * 0.2f);
						BattleSystem.DelayInputAfter(Utils.HealingParticle(BChar, Utils.DummyChar, heal, true, true));
					}
				}
			}

			public class Lv3
			{
				public class Adoration : Buff, IP_SkillUse_User
				{
					public override void Init()
					{
						PlusPerStat.Damage = 40;
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						bool alwaysLucky = RandomManager.RandomPer(RandomClassKey.Active, 100, 50);

						if (SkillD.Master != BChar || !alwaysLucky) return;

						bool edgeCase = SkillD.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy || SkillD.MySkill.Target.Key == GDEItemKeys.s_targettype_enemy_PlusRandom;

						if (edgeCase)
						{
							Targets.Clear();
							Targets.AddRange(BChar.MyTeam.AliveChars);
						}
						else
						{
							BattleChar target;

							if (BChar.MyTeam.AliveChars.Count > 1 && RandomManager.RandomPer(RandomClassKey.Active, 100, 50))
							{
								target = BChar.MyTeam.AliveChars.Random(RandomClassKey.Active);
							}
							else
							{
								target = BChar;
							}

							Targets.Clear();
							Targets.AddRange(BChar.BattleInfo.SkillTargetReturn(SkillD, target, null));
						}

						Scripts.ShowConfuseText(BChar);
					}
				}

				public class Adoration_0 : Buff, IP_PlayerTurn_1
				{
					public void Turn1()
					{
						Scripts.GlobalAbnormalitiesCheck(ModItemKeys.Buff_B_Abnormality_ArtLv3_Adoration, "Floor_Art_Adoration", false, true);
					}
				}

				public class Finale : Buff, IP_Awake
				{
					public void Awake()
					{
						BattleSystem.instance.StartCoroutine(GrandFinale());
					}

					private IEnumerator GrandFinale()
					{
						Utils.PlaySound("Floor_Art_Finale");
						yield return new WaitForSeconds(4);

						foreach (var ally in Utils.AllyTeam.AliveChars)
						{
							int heal = ally.GetStat.maxhp;
							BChar.StartCoroutine(Utils.HealingParticle(ally, Utils.DummyChar, heal, true, false));
						}

						foreach (var enemy in Utils.EnemyTeam.AliveChars)
						{
							int damage = enemy.GetStat.maxhp / 2;
							enemy.Damage(Utils.DummyChar, damage, false, true);
						}

						Utils.PlaySound("Floor_Art_Finale_Clap");
						yield break;
					}
				}

				public class Genesis : Buff, IP_Awake, IP_Hit, IP_Dodge
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", Utils.ChanceDebuff(BChar, 150).ToString());
					}

					public void Awake()
					{
						Utils.PlaySound("Floor_Art_Genesis");
					}

					public override void Init()
					{
						PlusPerStat.Heal = 20;
						PlusStat.dod = 20;
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
						Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_EGO_Art_Aroma, 1, 150);
					}
				}
			}
		}

		public class EGO
		{
			public class Aroma : Buff
			{
				public override void BuffStat()
				{
					PlusStat.DMGTaken = 20 * StackNum;
				}
			}

			public class Fragments : Buff
			{
				public override void Init()
				{
					PlusPerStat.Damage = -40;
				}
			}

			public class Fragments_0 : Common_Buff_Rest
			{

			}
		}
	}
}
