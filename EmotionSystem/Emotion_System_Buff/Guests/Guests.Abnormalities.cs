using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Guests
	{
		public class Abnormalities
		{
			public class Lv1
			{
				public class Despair : Buff
				{
					public override void BuffStat()
					{
						PlusStat.cri = 5 * BChar.EmotionLevel();
					}

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", (BChar.EmotionLevel() * 5).ToString());
					}
				}

				public class GiantMushroom : Buff
				{
					public override void BuffStat()
					{
						PlusStat.RES_CC = 5 * BChar.EmotionLevel();
						PlusStat.RES_DEBUFF = 5 * BChar.EmotionLevel();
						PlusStat.RES_DOT = 5 * BChar.EmotionLevel();
					}

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", (BChar.EmotionLevel() * 5).ToString());
					}
				}

				public class Strengthen : Buff
				{
					public override void BuffStat()
					{
						PlusPerStat.Damage = 5 * BChar.EmotionLevel();
					}

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", (BChar.EmotionLevel() * 5).ToString());
					}
				}

				public class Stress : Buff
				{
					public override void BuffStat()
					{
						PlusStat.def = 5 * BChar.EmotionLevel();
					}

					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", (BChar.EmotionLevel() * 5).ToString());
					}
				}

				public class Unity : Buff, IP_Awake, IP_TurnEnd
				{
					public override void Init()
					{
						PlusStat.dod = 15;
					}

					public override string DescExtended()
					{
						int heal = BChar.EmotionLevel() * 5;
						return base.DescExtended().Replace("&a", heal.ToString());
					}

					public void Awake()
					{
						foreach (var ally in BChar.MyTeam.AliveChars)
						{
							if (ally == BChar)
							{
								Utils.RemoveBuff(BChar, GDEItemKeys.Buff_B_EnemyTaunt, true);
							}
							else
							{
								Utils.AddBuff(ally, GDEItemKeys.Buff_B_EnemyTaunt);
							}
						}
					}

					public void TurnEnd()
					{
						foreach (var ally in BChar.MyTeam.AliveChars)
						{
							int heal = BChar.EmotionLevel() * 5;
							BattleSystem.DelayInput(Utils.HealingParticle(ally, Utils.DummyChar, heal, true, false));
						}
					}
				}

				public class YouMustbeHappy : Buff
				{
					public override void Init()
					{
						PlusStat.DMGTaken = -15;
					}
				}
			}

			public class Lv2
			{
				public class BehaviourAdjustment : Buff, IP_Awake, IP_PlayerTurn
				{
					public bool DodgedOrDebuffBlocked;

					public void Awake()
					{
						Utils.AddBuff(BChar, ModItemKeys.Buff_B_Abnormality_GuestLv2_BehaviourAdjustment_0);
					}

					public override string DescExtended()
					{
						string text = DodgedOrDebuffBlocked ? ModLocalization.EmotionSystem_Status_Inactive : ModLocalization.EmotionSystem_Status_Active;
						return base.DescExtended().Replace("&a", text.ToString());
					}

					public void Turn()
					{
						if (DodgedOrDebuffBlocked)
						{
							Utils.AddBuff(BChar, ModItemKeys.Buff_B_Abnormality_GuestLv2_BehaviourAdjustment_0);
							DodgedOrDebuffBlocked = false;
						}
					}
				}

				public class BehaviourAdjustment_0 : Buff, IP_BuffAdd, IP_Dodge
				{
					public override void Init()
					{
						PlusStat.PerfectDodge = true;
					}

					public void Dodge(BattleChar Char, SkillParticle SP)
					{
						if (Char == BChar)
						{
							if (SP.SkillData.IsDamage && !SP.UseStatus.IsLucy && !SP.UseStatus.Dummy)
							{
								ChangeBoolState();
								SelfDestroy();
							}
						}
					}

					public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
					{
						GDEBuffData gdebuffData = new GDEBuffData(addedbuff.BuffData.Key);
						if (BuffTaker == BChar && gdebuffData.BuffTag.Key != "null" && gdebuffData.Debuff)
						{
							addedbuff.SelfDestroy();
							BuffTaker.SimpleTextOut(ScriptLocalization.UI_Battle.DebuffGuard);
							ChangeBoolState();
							SelfDestroy();
						}
					}

					public void ChangeBoolState()
					{
						if (Utils.ReturnBuff(BChar, ModItemKeys.Buff_B_Abnormality_GuestLv2_BehaviourAdjustment) is BehaviourAdjustment buff)
						{
							buff.DodgedOrDebuffBlocked = true;
						}
					}
				}

				public class EnergyConversion : Buff, IP_DamageTake, IP_BuffObject_Updata
				{
					public override string DescExtended()
					{
						return base.DescExtended().Replace("&a", threshold.ToString());
					}

					private int threshold;

					public override void Init()
					{
						base.Init();
						this.OnePassive = true;

						int hp = BChar.GetStat.maxhp;
						int step = hp / (BChar is BattleEnemy enemy && enemy.Boss ? 5 : 2);
						int damageTaken = BChar.GetStat.maxhp - BChar.HP;

						threshold = damageTaken < step ? step - damageTaken : 1;
					}

					public void BuffObject_Updata(BuffObject obj)
					{
						string num = threshold.ToString();
						obj.StackText.text = num;
					}


					public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
					{
						if (Dmg > BChar.HP)
						{
							threshold -= BChar.HP;
						}
						else
						{
							threshold -= Dmg;
						}

						while (threshold < 1)
						{
							if (BChar is BattleEnemy enemy && enemy.Boss)
							{
								threshold += BChar.GetStat.maxhp / 5;
							}
							else
							{
								threshold += BChar.GetStat.maxhp / 2;
							}

							BattleSystem.instance.AllyTeam.AP -= 1;
						}
					}
				}

				public class MirrorAdjustment : Buff, IP_PlayerTurn, IP_Awake
				{
					public bool ReflectedDamage;

					public void Awake()
					{
						Utils.AddBuff(BChar, ModItemKeys.Buff_B_Abnormality_GuestLv2_MirrorAdjustment_0);
					}

					public override string DescExtended()
					{
						string text = ReflectedDamage ? ModLocalization.EmotionSystem_Status_Inactive : ModLocalization.EmotionSystem_Status_Active;
						return base.DescExtended().Replace("&a", text.ToString());
					}

					public void Turn()
					{
						if (ReflectedDamage)
						{
							Utils.AddBuff(BChar, ModItemKeys.Buff_B_Abnormality_GuestLv2_MirrorAdjustment_0);
							ReflectedDamage = false;
						}
					}
				}

				public class MirrorAdjustment_0 : Buff, IP_Hit
				{
					public void Hit(SkillParticle SP, int Dmg, bool Cri)
					{
						if (!SP.UseStatus.IsLucy && !SP.UseStatus.Dummy && Dmg > 1 && !SP.SkillData.PlusHit)
						{
							Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Abnormality_Guest_Mirror, BChar, BChar.MyTeam);

							int damage = (int)(Dmg * 0.8);

							Utils.TakeNonLethalDamage(SP.UseStatus, damage);
							//SP.UseStatus.Damage(SP.UseStatus, 0, false, true, false, 0, false, false, false);
							BChar.ParticleOut(skill, SP.UseStatus);
							Utils.RemoveBuff(BChar, ModItemKeys.Buff_B_Abnormality_GuestLv2_MirrorAdjustment_0, true);

							if (Utils.ReturnBuff(BChar, ModItemKeys.Buff_B_Abnormality_GuestLv2_MirrorAdjustment) is MirrorAdjustment buff)
							{
								buff.ReflectedDamage = true;
							}
							SelfDestroy();
						}
					}
				}

				public class Present : Buff, IP_PlayerTurn, IP_Awake
				{
					public void Awake()
					{
						ShufflePresent();
					}

					public void Turn()
					{
						ShufflePresent();
					}

					public void ShufflePresent()
					{
						var skill = Utils.CreateSkill(Utils.AllyTeam.LucyAlly, ModItemKeys.Skill_S_Abnormality_Guest_Present, false);
						Utils.InsertSkillInDeck(skill);
					}
				}

				public class Shelter : Buff, IP_HPChange
				{
					public override void Init()
					{
						OnePassive = true;
					}

					public void HPChange(BattleChar Char, bool Healed)
					{
						if (Char.HP <= 0)
						{
							Char.HP = 1;
							Char.IsDead = false;
							EffectView.SimpleTextout(Char.GetPos(), ScriptLocalization.UI_Battle.Endure, true, 1f, false, 1f);
							SelfDestroy();
							Utils.AddBuff(BChar, ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter_0);
						}
					}
				}

				public class Shelter_0 : Buff, IP_DamageTake, IP_Awake, IP_PlayerTurn
				{
					private int turnsBeforeRemove;

					public void Awake()
					{
						turnsBeforeRemove = 2;
					}

					public override string DescExtended()
					{
						int thershold = BChar.EmotionLevel() * 15;
						return base.DescExtended().Replace("&a", thershold.ToString()).Replace("&b", turnsBeforeRemove.ToString());
					}

					public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
					{
						int thershold = BChar.EmotionLevel() * 15;

						if (Dmg >= thershold)
						{
							BChar.Dead();
							SelfDestroy();
						}
						else
						{
							resist = true;
						}
					}

					public void Turn()
					{
						turnsBeforeRemove--;

						if (turnsBeforeRemove <= 0)
						{
							SelfDestroy();
						}
					}
				}

				public class Storytime : Buff, IP_Awake, IP_PlayerTurn_1
				{
					public void Awake()
					{
						SelectAlly();
					}

					public void Turn1()
					{
						SelectAlly();
					}

					private void SelectAlly()
					{
						var allyList = Utils.AlliesPreview(ModItemKeys.Skill_S_Abnormality_Guest_Storytime);
						BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(allyList, new SkillButton.SkillClickDel(ApplyAbnormality), "", false, false, true, false, true));
					}

					private void ApplyAbnormality(SkillButton Mybutton)
					{
						Mybutton.CharData.BuffAdd(ModItemKeys.Buff_B_Abnormality_GuestLv2_Storytime_0, BChar, false, 0, false, -1, false);
					}
				}

				public class Storytime_0 : Buff, IP_Healed, IP_DamageTake
				{
					public override void Init()
					{
						PlusStat.Stun = true;
					}

					public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
					{
						if (HealNum >= 1)
						{
							SelfDestroy();
						}
					}

					public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
					{
						if (Dmg >= 1)
						{
							SelfDestroy();
						}
					}
				}
			}

			public class Lv3
			{
				public class DimensionalRefraction : Buff, IP_PlayerTurn_1, IP_Awake
				{
					private Skill SavedSkill;
					private Skill DimensionSkill;

					public override void SelfdestroyPlus()
					{
						var index = 0;
						var dimensionSkill = Utils.AllyTeam.Skills.Where(s => s?.MySkill == DimensionSkill?.MySkill).FirstOrDefault();

						if (dimensionSkill != null)
						{
							index = Utils.AllyTeam.Skills.IndexOf(DimensionSkill);
							BattleSystem.DelayInput(Utils.RemoveSkillCoroutine(DimensionSkill, true));
							Utils.CreateSkill(SavedSkill.Master, SavedSkill.MySkill.KeyID, false, true, index);
						}
					}

					public void Awake()
					{
						ChangeSkill();
					}

					public void Turn1()
					{
						ChangeSkill();
					}

					private void ChangeSkill()
					{
						try
						{
							Utils.AllyTeam.AP -= 1;

							var index = 0;
							var dimensionSkill = Utils.AllyTeam.Skills.Where(s => s?.MySkill == DimensionSkill?.MySkill).FirstOrDefault();

							if (dimensionSkill != null)
							{
								index = Utils.AllyTeam.Skills.IndexOf(DimensionSkill);
								BattleSystem.DelayInput(Utils.RemoveSkillCoroutine(DimensionSkill, true));
								Utils.CreateSkill(SavedSkill.Master, SavedSkill.MySkill.KeyID, false, true, index);
							}

							var skillList = Utils.AllyTeam.Skills.ToList();

							if (skillList.Count > 0)
							{
								var indexList = skillList.Select((v, i) => new { v, i }).
									Where(x => x.v.MySkill != DimensionSkill?.MySkill).Select(x => x.i).ToList();

								int randomIndex = RandomManager.RandomInt(BChar.GetRandomClass().SkillSelect, 0, indexList.Count);
								var skill = skillList[indexList[randomIndex]];
								SavedSkill = skill;

								BattleSystem.DelayInput(Utils.RemoveSkillCoroutine(skill, true));

								DimensionSkill = Utils.CreateSkill(skill.Master, ModItemKeys.Skill_S_Abnormality_Guest_DimensionalRefraction, false, true, indexList[randomIndex]);

								string baseName = skill.MySkill != null ? new GDESkillData(skill.MySkill.KeyID).Name : "Unknown Skill";
								string baseDescription = skill.MySkill != null ? new GDESkillData(skill.MySkill.KeyID).Description : "Unknown Skill";

								DimensionSkill.MySkill.Name = $"{baseName}";
								DimensionSkill.MySkill.Description = $"{baseDescription}";
								DimensionSkill.AP = skill.MySkill.UseAp;
								DimensionSkill.MyButton?.InputData(skill, null, false);
							}
						}
						catch (Exception e)
						{
							Debug.LogException(e);
						}
					}
				}

				public class Bait : Buff, IP_PlayerTurn_1, IP_Awake
				{
					public void Awake()
					{
						DiscardSkill();
					}

					public void Turn1()
					{
						DiscardSkill();
					}

					private void DiscardSkill()
					{
						Utils.AllyTeam.AP -= 1;

						var skillList = Utils.AllyTeam.Skills?.ToList();
						int randomIndex = RandomManager.RandomInt(BChar.GetRandomClass().SkillSelect, 0, skillList.Count);
						var skill = skillList[randomIndex];

						Utils.RemoveSkill(skill);
						Utils.InsertSkillInDeck(skill);
					}
				}

				public class CycleCurse : Buff, IP_PlayerTurn_1, IP_Awake
				{
					public override string DescExtended()
					{
						int damage = 5 + (PlayData.TSavedata.StageNum * 5);
						return base.DescExtended().Replace("&a", damage.ToString());
					}

					public void Awake()
					{
						RemoveSkill();
					}

					public void Turn1()
					{
						RemoveSkill();
					}

					private void RemoveSkill()
					{
						int damage = 5 + (PlayData.TSavedata.StageNum * 5);
						var skillList = Utils.AllyTeam.Skills?.ToList();
						BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(skillList, new SkillButton.SkillClickDel(Selection), ModLocalization.Abnormality_Guest_CycleCurse.Replace("&a", damage.ToString()), false, true, true, false, true));
					}

					private void Selection(SkillButton selectedSkill)
					{
						if (selectedSkill != null)
						{
							Utils.RemoveSkill(selectedSkill.Myskill, true);

							if (selectedSkill.CharData == Utils.AllyTeam.LucyAlly)
							{
								Utils.AllyTeam.AP -= 2;
							}
							else
							{
								int damage = 5 + (PlayData.TSavedata.StageNum * 5);
								Utils.TakeNonLethalDamage(selectedSkill.CharData, damage, true);
							}
						}
					}
				}
			}
		}
	}
}
