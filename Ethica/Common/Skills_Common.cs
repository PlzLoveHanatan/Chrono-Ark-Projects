using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EItem;
using GameDataEditor;
using I2.Loc;
using NLog.Targets;
using UnityEngine;
using static Ethica.CommonSkills;

namespace Ethica
{
	partial class CommonSkills
	{
		public class Skills
		{
			public class Common
			{
				public class Catastrophe : Skill_Extended
				{
					public override void SkillUseSingle(Skill skill, List<BattleChar> targets)
					{
						var list = BattleSystem.instance.AllyTeam.Skills_Deck;
						if (list.Count > 0)
						{
							List<Skill> availableSkills = new List<Skill>(list);

							for (int i = 0; i < Math.Min(list.Count, 3); i++)
							{
								if (availableSkills.Count == 0) break;
								Skill randomSkill = availableSkills.RandomElement();
								availableSkills.Remove(randomSkill);
								BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { randomSkill }, b => BattleSystem.DelayInputAfter(CastSkill(b.Myskill)), ScriptLocalization.System_SkillSelect.UseSkillSelect, false, true, true, false, true));
							}
						}
						base.SkillUseSingle(skill, targets);
					}

					public IEnumerator CastSkill(Skill skill)
					{
						Skill Temp = skill.CloneSkill(true, skill.Master, null, false);
						BattleSystem.instance.AllyTeam.Skills_Deck.Remove(skill);
						if (!skill.isExcept && !skill.Disposable) BattleSystem.instance.AllyTeam.Skills_UsedDeck.Insert(BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count, skill);
						BattleSystem.instance.ActWindow.Draw(BattleSystem.instance.AllyTeam, false);
						yield return BattleSystem.instance.ActAfter();
						yield return BattleSystem.instance.SkillRandomUseIenum(skill.Master, Temp, false, false, false);
					}
				}

				public class Discovery : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						var list = new List<GDESkillData>();
						var skills = Utils.AllGameSkills.Where(s => (s.User == BChar.Info.KeyData || s.Category.Key == GDEItemKeys.SkillCategory_PublicSkill) && s.KeyID != SkillD.MySkill.KeyID && !s.NoDrop).ToList();

						if (skills.Count > 0)
						{
							for (int i = 0; i < Math.Min(skills.Count, 3); i++)
							{
								list.Add(skills.RandomElement());
							}
						}

						var skillList = list.Select(s => { var skill = Skill.TempSkill(s.Key, BChar, BChar.MyTeam); /*skill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_Discovery);*/ skill.isExcept = true; skill.APChange = -2; return skill; }).ToList();
						BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(skillList, new SkillButton.SkillClickDel(b => b.Myskill.Master.MyTeam.Add(b.Myskill, false)), ScriptLocalization.System_SkillSelect.CreateSkill, true, true, true, false, true));
						//BChar.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_Discovery, BattleSystem.instance.AllyTeam.DummyChar);
						base.SkillUseSingle(SkillD, Targets);
					}
				}

				public class DramaticEntrance : Skill_Extended
				{
					public override void BattleStartDeck(List<Skill> Skills_Deck)
					{
						Skills_Deck.Remove(MySkill);
						Skills_Deck.Insert(0, MySkill);
						base.BattleStartDeck(Skills_Deck);
					}
				}

				public class Finesse : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						BattleSystem.instance.AllyTeam.Draw();
						//BChar.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_Block, BattleSystem.instance.AllyTeam.DummyChar).BarrierHP = 7;
					}
				}

				public class Fisticuffs : Skill_Extended
				{
					public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
					{
						base.AttackEffectSingle(hit, SP, DMG, Heal);
						BChar.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_Block, BattleSystem.instance.AllyTeam.DummyChar).BarrierHP += DMG;
					}
				}

				public class FlashofSteel : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						BattleSystem.instance.AllyTeam.Draw();
					}
				}

				public class GangUp : Skill_Extended, IP_SkillUse_Team, IP_PlayerTurn
				{
					private int skillPlayed;
					private int AdditionalDamage => skillPlayed * 4;

					public override string DescExtended(string desc)
					{
						return base.DescExtended(desc).Replace("&a", skillPlayed.ToString());
					}

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						SkillBasePlus.Target_BaseDMG = AdditionalDamage;
					}

					public override void Special_PointerEnter(BattleChar Char)
					{
						base.Special_PointerEnter(Char);
						SkillBasePlusPreview.Target_BaseDMG = AdditionalDamage;
					}

					public void SkillUseTeam(Skill skill)
					{
						if (skill.Master != BChar /*&& !skill.Master.IsLucy*/) skillPlayed++;
					}

					public void Turn()
					{
						skillPlayed = 0;
					}
				}

				public class HuddleUp : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						BattleSystem.instance.AllyTeam.Draw(2);

					}
				}

				public class Impatience : Skill_Extended
				{
					private bool CanDrawSkills => BattleSystem.instance.AllyTeam.Skills.Where(s => s.IsDamage).Count() == 0 && !MySkill.BasicSkill;

					public override void Init()
					{
						base.Init();
						SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
					}

					public override void FixedUpdate()
					{
						if (!BattleSystem.instance.ActWindow.CanAnyMove) return;
						(CanDrawSkills ? (Action)SkillParticleOn : SkillParticleOff)();
					}

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						if (CanDrawSkills)
						{
							for (int i = 0; i < 2; i++)
							{
								BattleSystem.DelayInputAfter(DrawCo());
							}
						}
					}

					private IEnumerator DrawCo()
					{
						yield return null;
						Skill skill = BattleSystem.instance.AllyTeam.Skills_Deck.Find(s => s.IsDamage && s.Master == BChar);
						if (skill != null) BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(skill, null));
						else BattleSystem.instance.AllyTeam.Draw(skill);
					}
				}

				public class JackofAllTrades : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						var skills = PlayData.ALLSKILLLIST.Where(s => s.Category.Key == GDEItemKeys.SkillCategory_PublicSkill /*&& s.KeyID != SkillD.MySkill.KeyID */&& !s.NoDrop).ToList();

						if (skills.Count > 0)
						{
							for (int i = 0; i < Math.Min(skills.Count, 3); i++)
							{
								var skill = Skill.TempSkill(skills.RandomElement().KeyID, BChar, BChar.MyTeam);
								BattleSystem.instance.AllyTeam.Add(skill, false);
								skill.isExcept = true;
								skill.AutoDelete = 2;
							}
						}

						base.SkillUseSingle(SkillD, Targets);
					}
				}

				public class Lift : Skill_Extended
				{
					private int Barrier => (int)(BChar.GetStat.maxhp * 0.7f);

					public override string DescExtended(string desc)
					{
						return base.DescExtended(desc).Replace("&a", Barrier.ToString());
					}

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						Targets.ForEach(t => t.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_Block, BChar).BarrierHP += Barrier);
					}
				}

				public class MindBlast : Skill_Extended
				{
					private int AdditionalDamage => BattleSystem.instance?.AllyTeam?.Skills_Deck.Count ?? 0;

					public override string DescExtended(string desc)
					{
						return base.DescExtended(desc).Replace("&a", AdditionalDamage.ToString());
					}

					public override void BattleStartDeck(List<Skill> Skills_Deck)
					{
						base.BattleStartDeck(Skills_Deck);
						Skills_Deck.Remove(MySkill);
						Skills_Deck.Insert(0, MySkill);
					}

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						SkillBasePlus.Target_BaseDMG = AdditionalDamage;
					}

					public override void Special_PointerEnter(BattleChar Char)
					{
						base.Special_PointerEnter(Char);
						SkillBasePlusPreview.Target_BaseDMG = AdditionalDamage;
					}
				}

				public class Omnislice : Skill_Extended
				{
					public override void Init()
					{
						base.Init();
						PlusSkillStat.Penetration = 100;
					}

					public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
					{
						base.AttackEffectSingle(hit, SP, DMG, Heal);
						BattleSystem.DelayInput(DamageCo(DMG, hit));
					}

					private IEnumerator DamageCo(int damage, BattleChar target)
					{
						yield return null;
						BattleSystem.instance.EnemyTeam.AliveChars.Where(e => e != target).ToList().ForEach(e => e.Damage(BChar, damage, false, PlusPenetration: 100));
					}
				}

				public class Production : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						BattleSystem.instance.AllyTeam.AP += 3;
					}
				}

				public class Prolong : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						Targets.ForEach(t => t.Buffs.Where(b => b.BuffData.LifeTime != 0 && !b.BuffData.Hide).ToList().SelectMany(b => b.StackInfo).ToList().ForEach(s => s.RemainTime += 2));
					}
				}

				public class Purity : Skill_Extended
				{
					private readonly List<Skill> skillList = new List<Skill>();

					private int count;

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						skillList.AddRange(BattleSystem.instance.AllyTeam.Skills.Where(s => s != MySkill));
						SelectionCo();
					}

					private void SelectionCo()
					{
						if (++count > 3 || skillList.Count == 0) return;
						BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(skillList, OnSelect, ModLocalization.S_Common_Purity, true));
					}

					public void OnSelect(SkillButton button)
					{
						button.Myskill.Except();
						skillList.Remove(button.Myskill);
						BattleSystem.instance.ActWindow.Draw(BattleSystem.instance.AllyTeam, false);
						SelectionCo();
					}
				}

				public class Restlessness : Skill_Extended
				{
					private bool CanDrawSkills => BattleSystem.instance.AllyTeam.Skills.Where(s => s != MySkill).Count() == 0 && !MySkill.BasicSkill;

					public override void Init()
					{
						base.Init();
						SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
					}

					public override void FixedUpdate()
					{
						if (!BattleSystem.instance.ActWindow.CanAnyMove) return;
						(CanDrawSkills ? (Action)SkillParticleOn : SkillParticleOff)();
					}

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						if (CanDrawSkills)
						{
							BattleSystem.instance.AllyTeam.Draw(3);
							BattleSystem.instance.AllyTeam.AP += 3;
						}
					}
				}

				public class SeekerStrike : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						var list = BattleSystem.instance.AllyTeam.Skills_Deck;
						if (list.Count > 0)
						{
							List<Skill> availableSkills = new List<Skill>(list);
							List<Skill> seekerList = new List<Skill>();

							for (int i = 0; i < Math.Min(list.Count, 3); i++)
							{
								if (availableSkills.Count == 0) break;
								Skill randomSkill = availableSkills.RandomElement();
								availableSkills.Remove(randomSkill);
								seekerList.Add(randomSkill);
							}

							BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(seekerList, OnSelect, ScriptLocalization.System_SkillSelect.DrawSkill, false, true, true, false, true));
						}
					}

					private void OnSelect(SkillButton button)
					{
						BattleSystem.instance.AllyTeam.Skills_Deck.Remove(button.Myskill);
						BattleSystem.instance.AllyTeam.Add(button.Myskill, false);
					}
				}

				public class Splash : Skill_Extended
				{
					private static readonly List<string> CharacterList = new List<string>()
					{
						GDEItemKeys.Character_Azar,
						GDEItemKeys.Character_Control,
						GDEItemKeys.Character_Hein,
						GDEItemKeys.Character_Ilya,
						GDEItemKeys.Character_Joey,
						GDEItemKeys.Character_Leryn,
						GDEItemKeys.Character_Lian,
						GDEItemKeys.Character_LucyC,
						GDEItemKeys.Character_Mement,
						GDEItemKeys.Character_MissChain,
						GDEItemKeys.Character_Momori,
						GDEItemKeys.Character_Phoenix,
						GDEItemKeys.Character_Priest,
						GDEItemKeys.Character_Prime,
						GDEItemKeys.Character_Queen,
						GDEItemKeys.Character_ShadowPriest,
						GDEItemKeys.Character_SilverStein,
						GDEItemKeys.Character_Sizz,
					};

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						// i.User != "" && !i.Lock
						var list = new List<GDESkillData>();
						var skills = PlayData.ALLSKILLLIST.Concat(PlayData.ALLRARESKILLLIST).Where(s => CharacterList.Contains(s.User) && (s.Effect_Target.DMG_Base >= 1 || s.Effect_Target.DMG_Per >= 1) && !s.NoDrop).ToList();

						if (skills.Count > 0)
						{

							for (int i = 0; i < Math.Min(skills.Count, 3); i++)
							{
								list.Add(skills.RandomElement());
							}
						}

						var skillList = list.Select(s => { var skill = Skill.TempSkill(s.Key, BChar, BChar.MyTeam); skill.isExcept = true; skill.APChange = -2; return skill; }).ToList();
						BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(skillList, new SkillButton.SkillClickDel(b => b.Myskill.Master.MyTeam.Add(b.Myskill, false)), ScriptLocalization.System_SkillSelect.CreateSkill, true, true, true, false, true));
						base.SkillUseSingle(SkillD, Targets);
					}
				}

				public class ThinkingAhead : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						BattleSystem.instance.AllyTeam.Draw(2);
						BattleSystem.DelayInput(SelectionCo());
					}

					private IEnumerator SelectionCo()
					{
						yield return null;
						BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(BattleSystem.instance.AllyTeam.Skills.Where(s => s != MySkill).ToList(), OnSelect, ModLocalization.S_Common_ThinkingAhead, false, true, true, false, true));
					}

					private void OnSelect(SkillButton button)
					{
						BattleSystem.instance.AllyTeam.Skills.Remove(button.Myskill);
						BattleSystem.instance.AllyTeam.Skills_Deck.Insert(0, button.Myskill);
						BattleSystem.instance.ActWindow.Draw(BattleSystem.instance.AllyTeam, false);
					}
				}

				public class ThrummingHatchet : Skill_Extended
				{
					public override IEnumerator DrawAction()
					{
						if (MySkill.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_Common_ThrummingHatchet) == null) MySkill.ExtendedAdd_Battle(ModItemKeys.SkillExtended_Ex_Common_ThrummingHatchet);
						return base.DrawAction();
					}

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						if (!MySkill.BasicSkill && !MySkill.FreeUse)
						{
							BChar.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_ThrummingHatchet, BattleSystem.instance.AllyTeam.DummyChar);
							if (MySkill.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_Common_ThrummingHatchet) is Ex.ThrummingHatchet hatchet && hatchet != null) hatchet.SkillPLayed = true;
						}
					}

					//public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					//{
					//	base.SkillUseSingle(SkillD, Targets);
					//	if (!MySkill.BasicSkill) BChar.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_ThrummingHatchet, BattleSystem.instance.AllyTeam.DummyChar);
					//}
				}

				public class Volley : Skill_Extended
				{
					public override void FixedUpdate()
					{
						base.FixedUpdate();
						MySkill.APChange = BattleSystem.instance.AllyTeam.AP;
					}

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						BattleSystem.DelayInputAfter(RecastCo(Targets[0], MySkill.AP));
					}

					private IEnumerator RecastCo(BattleChar target, int num)
					{
						yield return null;
						for (int i = 0; i < num; i++)
						{
							yield return new WaitForSecondsRealtime(0.25f);
							Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Ethica_Common_Volley, BChar, BChar.MyTeam);
							skill.FreeUse = true;
							skill.PlusHit = true;
							BChar.ParticleOut(skill, target ?? BChar.BattleInfo.EnemyList.RandomElement());
						}
					}
				}
			}

			public class Rare
			{

				public class RareBase : Skill_Extended
				{
					public override void Init()
					{
						base.Init();
						MySkill.ToolTip();
					}
				}

				public class Alchemize : RareBase
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						BattleSystem.instance.AllyTeam.MaxPotionNum++;
						InventoryManager.Reward(ItemBase.GetPotionRandom());
					}
				}

				public class Anointed : RareBase
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						BattleSystem.DelayInputAfter(DrawCo());
					}

					private IEnumerator DrawCo()
					{
						yield return null;

						var rareSkills = BattleSystem.instance.AllyTeam.Skills_Deck.Where(s => s.MySkill.Rare).ToList();

						foreach (var skill in rareSkills)
						{
							if (BattleSystem.instance.AllyTeam.Skills_Deck.Contains(skill))
							{
								yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(skill, null));
							}
						}
					}
				}

				public class BeatDown : RareBase
				{
					public override void SkillUseSingle(Skill skill, List<BattleChar> targets)
					{
						var list = BattleSystem.instance.AllyTeam.Skills_UsedDeck.Where(s => s.IsDamage).ToList();
						if (list.Count > 0)
						{
							List<Skill> availableSkills = new List<Skill>(list);

							for (int i = 0; i < Math.Min(list.Count, 3); i++)
							{
								if (availableSkills.Count == 0) break;
								Skill randomSkill = availableSkills.RandomElement();
								availableSkills.Remove(randomSkill);
								BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(new List<Skill> { randomSkill }, b => BattleSystem.DelayInputAfter(CastSkill(b.Myskill)), ScriptLocalization.System_SkillSelect.UseSkillSelect, false, true, true, false, true));
							}
						}
						base.SkillUseSingle(skill, targets);
					}

					public IEnumerator CastSkill(Skill skill)
					{
						Skill Temp = skill.CloneSkill(true, skill.Master, null, false);
						BattleSystem.instance.ActWindow.Draw(BattleSystem.instance.AllyTeam, false);
						yield return BattleSystem.instance.ActAfter();
						yield return BattleSystem.instance.SkillRandomUseIenum(skill.Master, Temp, false, false, false);
					}
				}

				public class Bolas : RareBase
				{
					public override IEnumerator DrawAction()
					{
						if (MySkill.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_Common_Rare_Bolas) == null) MySkill.ExtendedAdd_Battle(ModItemKeys.SkillExtended_Ex_Common_Rare_Bolas);
						return base.DrawAction();
					}

					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						if (!MySkill.BasicSkill && !MySkill.FreeUse)
						{
							BChar.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_Rare_Bolas, BattleSystem.instance.AllyTeam.DummyChar);
							if (MySkill.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_Common_Rare_Bolas) is Ex.Bolas bolas && bolas != null) bolas.SkillPLayed = true;
						}
					}
				}

				public class Calamity : RareBase
				{

				}

				public class Entropy : RareBase
				{

				}

				public class EternalArmor : RareBase
				{

				}

				public class GoldAxe : RareBase
				{

					public override string DescExtended(string desc)
					{
						return base.DescExtended(desc).Replace("&a", Utils.EthicaBV.GoldAxeDamage.ToString());
					}
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						SkillBasePlus.Target_BaseDMG = Utils.EthicaBV.GoldAxeDamage;
					}

					public override void Special_PointerEnter(BattleChar Char)
					{
						base.Special_PointerEnter(Char);
						SkillBasePlusPreview.Target_BaseDMG = Utils.EthicaBV.GoldAxeDamage;
					}
				}

				public class HandofGreed : RareBase
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						base.SkillUseSingle(SkillD, Targets);
						BattleSystem.DelayInputAfter(MoneyCo(Targets));
					}

					private IEnumerator MoneyCo(List<BattleChar> targets)
					{
						yield return null;
						List<ItemBase> list = new List<ItemBase>();
						targets.Where(t => t.IsDead).ToList().ForEach(_ => list.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 250)));
						if (targets.Any(t => t.IsDead)) InventoryManager.Reward(list);
					}
				}

				public class HiddenGem : RareBase
				{

				}

				public class Jackpot : RareBase
				{

				}

				public class MasterofStrategy : RareBase
				{

				}

				public class Mayhem : RareBase
				{

				}

				public class Nostalgia : RareBase
				{

				}

				public class Rend : RareBase
				{

				}

				public class RollingBoulder : RareBase
				{

				}

				public class Salvo : RareBase
				{

				}

				public class Scrawl : RareBase
				{

				}

				public class SecretTechnique : RareBase
				{

				}

				public class SecretWeapon : RareBase
				{

				}

				public class TheGambit : RareBase
				{

				}
			}
		}
	}
}