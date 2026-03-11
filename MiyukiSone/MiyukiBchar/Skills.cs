using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsScripts;
using static MiyukiSone.Affection;
using GameDataEditor;
using UnityEngine;
using static MiyukiSone.Skills.Class;
using NLog.Targets;
using static CharacterDocument;

namespace MiyukiSone
{
	public class Skills
	{
		public class Test : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				ChangeAffectionPoints(10);
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class Test2 : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				ChangeAffectionPoints(-10);
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class SacrificedKnowledge : Skill_Extended
		{
			public override bool Terms()
			{
				return false;
			}
		}

		public class Class
		{
			public class MiyukiSoneSkill : Skill_Extended
			{
				public override void Init()
				{
					MySkill.MiyukiInit(CurrentAffectionState);
					base.Init();
				}
			}

			public class EternalPromise : SkillExtended_LerynShield, IP_MiyukiSkillImgChange
			{
				private int Heal => (int)Misc.PerToNum(BChar.GetStat.reg, 180f);
				private bool effectTriggered = false;
				private readonly string EternalVow = ModItemKeys.Skill_S_EternalVow;

				public override void Init()
				{
					base.Init();
					MySkill.MiyukiInit(CurrentAffectionState);
					effectTriggered = false;
				}

				public override void SkillUseHand(BattleChar Target)
				{
					effectTriggered = false;
				}

				public override void FixedUpdate()
				{
					if (Bs != null) UpdateAPCost();
					if (effectTriggered || MySkill.MyButton == null || MySkill.BasicSkill || MySkill.MyButton.AlreadyWasted) return;

					int adjIndex = MySkill.AdjacentSkillIndex(EternalVow);
					if (adjIndex != -1)
					{
						int myIndex = AllyTeam.Skills.FindIndex(s => s == MySkill);
						if (myIndex < adjIndex)
						{
							effectTriggered = true;
							Skill otherSkill = AllyTeam.Skills[adjIndex];
							BattleSystem.DelayInput(MiyukiSoneSkillExtension.CreateEternalKiss(MySkill, otherSkill, BChar));
						}
					}
				}

				public override string DescExtended(string desc)
				{
					return base.DescExtended(desc).Replace("&a", Heal.ToString());
				}

				public override void AddShieldPersent()
				{
					base.AddShieldPersent();
					Percent = 140;
				}

				private void UpdateAPCost()
				{
					bool inDeck = AllyTeam.Skills_Deck.Contains(MySkill);
					MySkill.APChange = inDeck ? 4 : 0;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					if (Targets[0].HP >= 0) Percent = 140;
					else
					{
						Targets.ForEach(t => t.Heal(BChar, Heal, false, false, null));
						TargetBuff.Clear();
						Percent = 0;
					}
					base.SkillUseSingle(SkillD, Targets);
				}

				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}

			public class EternalVow : MissChainPBase
			{
				private bool effectTriggered = false;
				private readonly string EternalPromise = ModItemKeys.Skill_S_EternalPromise;

				public override void Init()
				{
					base.Init();
					MySkill.MiyukiInit(CurrentAffectionState);
					SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
					effectTriggered = false;
				}

				public override void SkillUseHand(BattleChar Target)
				{
					effectTriggered = false;
				}

				public override void FixedUpdate()
				{
					base.FixedUpdate();
					if (Bs != null) UpdateAPCost();
					if (effectTriggered || MySkill.MyButton == null || MySkill.BasicSkill || MySkill.MyButton.AlreadyWasted) return;

					int adjIndex = MySkill.AdjacentSkillIndex(EternalPromise);
					if (adjIndex != -1)
					{
						int myIndex = AllyTeam.Skills.FindIndex(s => s == MySkill);
						if (myIndex < adjIndex)
						{
							effectTriggered = true;
							Skill otherSkill = AllyTeam.Skills[adjIndex];
							BattleSystem.DelayInput(MiyukiSoneSkillExtension.CreateEternalKiss(MySkill, otherSkill, BChar));
						}
					}
				}

				private void UpdateAPCost()
				{
					bool inDiscardPile = AllyTeam.Skills_UsedDeck.Concat(AllyTeam.Skills_Deck).Contains(MySkill);
					MySkill.APChange = inDiscardPile ? 3 : 0;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					base.SkillUseSingle(SkillD, Targets);
					if (Fire) BattleSystem.DelayInput(IncreaseDebuffs());
				}

				public IEnumerator IncreaseDebuffs()
				{
					var debuffs = BattleSystem.instance.EnemyList
						.SelectMany(enemy => enemy.GetBuffs(BattleChar.GETBUFFTYPE.DOT, false, false)
						.Concat(enemy.GetBuffs(BattleChar.GETBUFFTYPE.DEBUFF, false, false)));

					foreach (var debuff in debuffs)
					{
						foreach (var stack in debuff.StackInfo)
						{
							if (stack.RemainTime != 0) stack.RemainTime++;
						}
					}

					yield return null;
					yield break;
				}
			}

			public class GlitchingPhone : Skill_Extended
			{
				private float timer = 0f;
				private readonly float interval = 3f;

				public override void FixedUpdate()
				{
					base.FixedUpdate();

					timer += Time.fixedDeltaTime;

					if (timer >= interval)
					{
						timer = 0f;
						ChangeImg();
					}
				}

				private void ChangeImg()
				{
					string[] suffixes = { "01", "02", "03" };
					var available = Enumerable.Range(0, suffixes.Length).Where(i => i != MiyukiData.LastPhoneImage || suffixes.Length == 1).ToList();
					int selected = available[RandomManager.RandomInt("MiyukiPhone", 0, available.Count)];
					MiyukiData.LastPhoneImage = selected;
					string path = $"Assets/Images/Skills/GlitchingPhone/{suffixes[selected]}/";
					MySkill.ChangeSkillImage(path + "skill", path + "button", path + "basic", isGlicthEffect: true);
				}
			}

			public class GracefulSwing : PriestPBase, IP_DamageChange
			{
				private int Damage => (int)(BChar.GetStat.atk * 1.5f);
				private bool _lastPassiveDraw;

				private bool Conditions(BattleChar target)
				{
					return target.GetBuffs(BattleChar.GETBUFFTYPE.DEBUFF, false, false).Count != 0 || BChar.HP <= BChar.GetStat.maxhp / 2;
				}


				public override string DescExtended(string desc)
				{
					return base.DescExtended(desc).Replace("&a", Damage.ToString());
				}

				public override bool CanIgnoreTauntTarget(BattleChar IgnoreTauntTarget)
				{
					return Conditions(IgnoreTauntTarget) || base.CanIgnoreTauntTarget(IgnoreTauntTarget);
				}

				public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
				{
					if (Conditions(Target)) Cri = true;
					return Damage;
				}

				public override void Init()
				{
					base.Init();
					MySkill.MiyukiInit(CurrentAffectionState);
				}

				public override void FixedUpdate()
				{
					if (PassiveDraw != _lastPassiveDraw)
					{
						_lastPassiveDraw = PassiveDraw;

						if (PassiveDraw)
						{
							string path = "Assets/Images/Skills/GracefulSwing/Prophecy/";
							SkillBasePlus.Target_BaseDMG = Damage;
							MySkill.ChangeSkillImage(path + "skill", path + "button", path + "basic", isGlicthEffect: true);
						}
						else MySkill.ChangeSkillImage(isRestoreImg: true);
					}
					base.FixedUpdate();
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					if (PassiveDraw) SkillBasePlus.Target_BaseDMG = Damage;
					base.SkillUseSingle(SkillD, Targets);
				}
			}

			public class HappyBirthday : SkillExtedned_IlyaP, IP_MiyukiSkillImgChange
			{
				private int BonusDamage => (int)(BChar.GetStat.atk * 1.4f);
				private bool AllyStunned => BChar.MyTeam.AliveChars.Any(a => a.GetStat.Stun);

				public override string DescExtended(string desc)
				{
					return base.DescExtended(desc).Replace("&a", BonusDamage.ToString());
				}

				private bool HasCC(BattleChar target)
				{
					return target.GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Any();
				}

				public override void Init()
				{
					base.Init();
					MySkill.MiyukiInit(CurrentAffectionState);
					SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
				}

				public override void IlyaWaste()
				{
					BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(BChar, MySkill, false, true, false));
					base.IlyaWaste();
				}

				public override void FixedUpdate()
				{
					base.FixedUpdate();

					if (AllyStunned)
					{
						SkillParticleOn();
						SkillBasePlusPreview.Target_BaseDMG = BonusDamage;
					}
					else
					{
						SkillParticleOff();
						SkillBasePlusPreview.Target_BaseDMG = 0;
					}
				}

				public override void Special_PointerEnter(BattleChar Char)
				{
					base.Special_PointerEnter(Char);
					SkillBasePlusPreview.Target_BaseDMG = HasCC(Char) ? BonusDamage : 0;
				}

				public override void Special_PointerExit()
				{
					base.Special_PointerExit();
					SkillBasePlusPreview.Target_BaseDMG = 0;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					base.SkillUseSingle(SkillD, Targets);
					SkillBasePlus.Target_BaseDMG = 0;
					if (AllyStunned) SkillBasePlus.Target_BaseDMG = BonusDamage;
					else if (HasCC(Targets[0])) SkillBasePlus.Target_BaseDMG = BonusDamage;

					if (!Bs.AllyTeam.AliveChars.Any(a => a.Info.KeyData == GDEItemKeys.Character_SilverStein))
					{
						BuffTag buffTag = new BuffTag
						{
							BuffData = new GDEBuffData(GDEItemKeys.Buff_B_SilverStein_P_1),
							User = BChar,
							PlusTagPer = 300
						};
						TargetBuff.Clear();
						TargetBuff.Add(buffTag);
					}
				}

				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}

			public class MeasuredLove : S_Lian_12, IP_MiyukiSkillImgChange
			{
				public override void Init()
				{
					MySkill.MiyukiInit(CurrentAffectionState);
					base.Init();
				}

				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Targets.ForEach(t => t.Damage(DummyChar, t.GetStat.maxhp / 2, false, true));
					base.SkillUseSingle(SkillD, Targets);
				}
			}

			public class Pandemonium : S_Azar_10
			{
				public override void Init()
				{
					base.Init();
					MySkill.MiyukiInit(CurrentAffectionState);
					if (MySkill.MySkill.Name.Contains("!")) return;
					MySkill.MySkill.Name += new string('!', RandomManager.RandomInt("MiyukiRandomExclamation", 1, 4));
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					if (Targets.Any(t => t.BuffFind(GDEItemKeys.Buff_B_SilverStein_P_1, false))) MySkill.MySkill.NODOD = true;
					base.SkillUseSingle(SkillD, Targets);
				}
			}

			public class QueenBee : MiyukiSoneSkill
			{

			}

			public class StepToward : Extended_Azar_0
			{
				private int Heal => (int)(BChar.GetStat.reg * 0.18f);

				public override string DescExtended(string desc)
				{
					return base.DescExtended(desc).Replace("&a", Heal.ToString());
				}

				public override void Init()
				{
					base.Init();
					MySkill.MiyukiInit(CurrentAffectionState);
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					base.SkillUseSingle(SkillD, Targets);
					SkillBasePlus.Target_BaseHeal = AllyTeam.Skills.Where(s => s != MySkill).Count() * Heal;
				}
			}

			public class SweetRestraint : S_Queen_13
			{
				public override void Init()
				{
					MySkill.MiyukiInit(CurrentAffectionState);
					base.Init();
				}
			}

			public class WarningStrike : Skill_Momori, IP_MiyukiSkillImgChange
			{
				private int Heal => (int)BChar.GetStat.reg;

				public override string DescExtended(string desc)
				{
					return base.DescExtended(desc).Replace("&a", Heal.ToString());
				}

				public override void Init()
				{
					MySkill.MiyukiInit();
					base.Init();
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					var momoriPskill = GetMomoriPskill();
					if (momoriPskill.Any())
					{
						var p = momoriPskill.First();
						p.SaveDMG = Math.Max(0, p.SaveDMG - Heal);
					}
					base.SkillUseSingle(SkillD, Targets);
				}

				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}
		}

		public class Rare
		{
			public class JustforYOU : MiyukiSoneSkill, IP_MiyukiSkillImgChange
			{
				public override bool Terms()
				{
					return !IsYandere;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Targets.ForEach(t =>
					{
						var debuffs = t.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false).ToList();
						if (debuffs.Any()) debuffs.Random("MiyukiRandomDebuff").SelfDestroy();
					});
					DialogueState state = IsDere && MiyukiPoints >= 30 ? DialogueState.kiss : DialogueState.love;
					Dialogue.CreateDialogue(state);
					base.SkillUseSingle(SkillD, Targets);
				}

				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}

			public class GameUpdate : MiyukiSoneSkill, IP_MiyukiSkillImgChange
			{
				public override bool Terms()
				{
					return !MiyukiSoneSaveManager.Instance.CurrentData.GameUpdated;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					MiyukiSoneSaveManager.Instance.CurrentData.LockedState = (int)CurrentAffectionState;
					MiyukiSoneSaveManager.Instance.CurrentData.GameUpdated = true;
					MiyukiSoneSaveManager.Instance.Save();
					EventRandom.RestartCurrentStage(PlayData.TSavedata.StageNum);
					base.SkillUseSingle(SkillD, Targets);
				}

				public void SkillImgChange(Skill mySkill)
				{
					MySkill.ChangeImg();
				}
			}
		}

		public class Lucy
		{

		}
	}
}
