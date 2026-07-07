using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using UnityEngine;
using static Ethica.CommonSkills;

namespace Ethica
{
	partial class CommonSkills
	{
		public class Buffs
		{
			#region Main Buffs
			public class Block : Buff
			{

			}

			public class Vigor : Buff, IP_DamageChange, IP_Ethica_Buff_Object, IP_DealDamage
			{
				public int VigorStacks = 5;

				public override string DescExtended()
				{
					return base.DescExtended().Replace("&a", (VigorStacks * 10).ToString());
				}

				public void Ethica_Buff_Object(BuffObject obj)
				{
					obj.StackText.text = VigorStacks.ToString();
				}

				public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
				{
					if (SkillD.Master == BChar && SkillD.IsDamage) return (int)(Damage * (1f + VigorStacks * 0.1f));
					else return Damage;
				}

				public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
				{
					SelfDestroy();
				}
			}

			public class Vulnerable : Buff
			{
				public override void Init()
				{
					base.Init();
					PlusStat.def = -50;
				}
			}

			public class Weak : Buff
			{
				public override void Init()
				{
					base.Init();
					PlusPerStat.Damage = -25;
				}
			}

			#endregion


			#region Common Buffs

			public class Common
			{
				public class Automation : Buff, IP_Draw, IP_Ethica_Buff_Object
				{
					private int drawCount;

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", drawCount.ToString());
					}

					public IEnumerator Draw(Skill Drawskill, bool NotDraw)
					{
						if (NotDraw) yield break;
						yield return null;
						if (++drawCount >= 10)
						{
							BattleSystem.instance.AllyTeam.AP++;
							drawCount = 0;
						}
					}

					public void Ethica_Buff_Object(BuffObject obj)
					{
						obj.StackText.text = drawCount.ToString();
					}
				}

				public class Coordinate : Buff
				{
					public override void Init()
					{
						PlusStat.IgnoreTaunt = true;
						PlusPerStat.Damage = 33;
						base.Init();
					}
				}

				public class DarkShackles : Buff, IP_PlayerTurn
				{
					public override void Init()
					{
						PlusPerStat.Damage = -75;
						base.Init();
					}

					public void Turn()
					{
						SelfDestroy();
					}
				}

				public class Equilibrium : Buff, IP_PlayerTurn
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", StackNum.ToString());
					}

					public void Turn()
					{
						BattleSystem.instance.AllyTeam.AP++;
						SelfDestroy();
					}
				}

				public class Fasten : Buff
				{
					public override void Init()
					{
						base.Init();
						PlusStat.HEALTaken = 25;
					}
				}

				public class Finesse : Buff
				{
					public override void Init()
					{
						base.Init();
						BarrierHP += 7;
					}
				}

				public class Intercept : Common_Buff_Protection
				{

				}

				public class Panache : Buff, IP_SkillUse_User, IP_Ethica_Buff_Object, IP_PlayerTurn
				{
					private int skillsPlayed;

					public void Ethica_Buff_Object(BuffObject obj)
					{
						obj.StackText.text = skillsPlayed.ToString();
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.Master == BChar) skillsPlayed++;
						if (skillsPlayed >= 5) BChar.StartCoroutine(DamageCo());
					}

					public void Turn()
					{
						skillsPlayed = 0;
					}

					private IEnumerator DamageCo()
					{
						yield return null;
						BattleSystem.instance.EnemyTeam.AliveChars.ForEach(e => e.Damage(BattleSystem.instance.DummyChar, 14, false, PlusPenetration: 100));
						skillsPlayed = 0;

					}
				}

				public class PanicButton : Buff
				{
					public override void Init()
					{
						base.Init();
						PlusStat.HEALTaken = -100;
						BarrierHP += 40;
					}
				}

				public class PrepTime : Buff, IP_PlayerTurn
				{

					public override void BuffOneAwake()
					{
						base.BuffOneAwake();
						BChar.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_Vigor, BattleSystem.instance.AllyTeam.DummyChar);
					}

					public void Turn()
					{
						if (BChar.BuffReturn(ModItemKeys.Buff_B_Ethica_Common_Vigor, false) is Vigor vigor) vigor.VigorStacks += 5;
						else BChar.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_Vigor, BattleSystem.instance.DummyChar);
					}
				}

				public class Prowess : Buff
				{
					public override void Init()
					{
						base.Init();
						PlusPerStat.Damage = 15;
						PlusPerStat.Heal = 15;
						PlusStat.dod = 15;
					}
				}

				public class Stratagem : Buff, IP_DrawNumChange, IP_PlayerTurn
				{
					public void DrawNumChange(int DrawNum, out int OutNum)
					{
						OutNum = DrawNum - 2;
					}

					public void Turn()
					{
						var list = BattleSystem.instance.AllyTeam.Skills_Deck.ToList().Let(l => Misc.Shuffle(l));
						for (int i = 0; i < Math.Min(list.Count, 2); i++)
						{
							BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(list, b => { BattleSystem.instance.AllyTeam.ForceDraw(b.Myskill); list.Remove(b.Myskill); },
								ModLocalization.S_Common_Stratagem, false, true, true, false, true));
						}
					}
				}

				public class TheBomb : Buff
				{
					public override void SelfdestroyPlus()
					{
						base.SelfdestroyPlus();
						BattleSystem.instance.EnemyTeam.AliveChars.ForEach(e => e.Damage(BattleSystem.instance.AllyTeam.DummyChar, 50, false, PlusPenetration: 100));
					}
				}

				public class ThrummingHatchet : Buff, IP_PlayerTurn
				{
					public void Turn()
					{
						BattleSystem.instance.AllyTeam.Skills_UsedDeck.Where(s => s.Master == BChar && s.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_Common_ThrummingHatchet) is Ex.ThrummingHatchet hatchet && hatchet.SkillPLayed).ToList().ForEach(s => Utils.ForceDraw(s, fromDeck: false));
						SelfDestroy();
					}
				} 

				public class UltimateDefend : Buff
				{
					public override void Init()
					{
						base.Init();
						BarrierHP += 15;
					}
				}
			}

			public class Rare
			{
				public class Alchemize : Buff
				{

				}

				public class Anointed : Buff
				{

				}

				public class BeatDown : Buff
				{

				}

				public class Bolas : Buff, IP_PlayerTurn
				{
					public void Turn()
					{
						BattleSystem.instance.AllyTeam.Skills_UsedDeck.Concat(BattleSystem.instance.AllyTeam.Skills_Deck).Where(s => s.Master == BChar && s.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_Common_Rare_Bolas) is Ex.Bolas bolas && bolas.SkillPLayed).ToList().ForEach(s => Utils.ForceDraw(s));
						SelfDestroy();
					}
				}

				public class Calamity : Buff, IP_SkillUse_User
				{
					private static readonly List<GDESkillData> Skills = new List<GDESkillData>();


					public override void BuffOneAwake()
					{
						base.BuffOneAwake();
						var skills = Utils.AllGameSkills.Where(s => s.User == BChar.Info.KeyData && (s.Effect_Target.DMG_Base >= 1 || s.Effect_Target.DMG_Per >= 1));
						foreach (var skill in skills)
						{
							Skills.Add(skill);
						}
					}

					public void SkillUse(Skill SkillD, List<BattleChar> Targets)
					{
						if (SkillD.Master == BChar && SkillD.IsDamage && !SkillD.BasicSkill)
						{
							var skill = Skill.TempSkill(Skills.RandomElement().Key, BChar, BChar.MyTeam);
							skill.isExcept = true;
							skill.AutoDelete = 2;
							BattleSystem.instance.AllyTeam.Add(skill, false);
						}
					}
				}

				public class Entropy : Buff, IP_PlayerTurn_1
				{
					public void Turn1()
					{
						BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(BattleSystem.instance.AllyTeam.Skills, OnSelect, "", false));
					}

					private void OnSelect(SkillButton button)
					{
						int index = BattleSystem.instance.AllyTeam.Skills.IndexOf(button.Myskill);
						//var master = string.IsNullOrEmpty(button.Myskill.MySkill.User) ? button.Myskill.Master.Info.KeyData : button.Myskill.MySkill.User; // randomize skill by original skill user
						var master = button.Myskill.Master.Info.KeyData ?? button.Myskill.MySkill.User; // randomize skill by original skill owner
						var skill = Skill.TempSkill(Utils.AllGameSkills.Where(s => s.User == master && s.User != "" && !s.Lock && !s.NoDrop).RandomElement().KeyID, button.Myskill.Master, button.Myskill.Master.MyTeam);
						//skill.isExcept = button.Myskill.isExcept;
						button.Myskill.Except();
						BattleSystem.instance.ActWindow.Draw(BattleSystem.instance.AllyTeam, false);
						BattleSystem.instance.AllyTeam.Skills.Insert(index, skill);
						//BattleSystem.instance.AllyTeam.Add(skill, false);
					}
				}

				public class EternalArmor : Buff, IP_PlayerTurn
				{
					public void Turn()
					{
						BChar.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_Rare_EternalArmor_0, BattleSystem.instance.AllyTeam.DummyChar);
					}
				}

				public class EternalArmor_0 : Buff
				{
					public override void Init()
					{
						base.Init();
						PlusStat.Strength = true;
						BarrierHP = 15;
					}
				}

				public class HandofGreed : Buff
				{

				}

				public class HiddenGem : Buff
				{

				}

				public class Jackpot : Buff
				{

				}

				public class MasterofStrategy : Buff
				{

				}

				public class Mayhem : Buff
				{

				}

				public class Nostalgia : Buff
				{

				}

				public class Rend : Buff
				{

				}

				public class RollingBoulder : Buff
				{

				}

				public class Salvo : Buff
				{

				}

				public class Scrawl : Buff
				{

				}

				public class SecretTechnique : Buff
				{

				}

				public class SecretWeapon : Buff
				{

				}

				public class TheGambit : Buff
				{

				}
			}
			#endregion
		}
	}
}
