using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using I2.Loc;
using NLog.LayoutRenderers;
using NLog.Targets;
using Spine;
using UnityEngine;

namespace QoH
{
	public class Skills
	{
		public class Justice : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (Targets.Count == 1)
				{
					Utils.AddDebuff(Targets[0], BChar, ModItemKeys.Buff_B_QoH_Shattered, 1, Utils.ChanceDOT(BChar, 100));
				}
			}
		}

		public class Radiant : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				foreach (var enemy in Utils.EnemyTeam.AliveChars)
				{
					Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_QoH_Chant, 1, Utils.ChanceDOT(BChar, 105));
				}
			}
		}

		public class Embrace : Skill_Extended
		{
			private int Heal => (int)(BChar.GetStat.reg * 0.4f);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString());
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				int recastHeal = 0;

				foreach (var enemy in Targets)
				{
					foreach (var buff in enemy.Buffs)
					{
						if (buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
						{
							foreach (var buffstack in buff.StackInfo)
							{
								recastHeal++;
							}
						}
					}
				}

				for (int i = 0; i < recastHeal; i++)
				{
					BattleSystem.instance.StartCoroutine(Utils.HealingParticle(null, BChar, Heal, true, false, true, true, true, false));
				}
			}
		}

		public class Miracle : Skill_Extended
		{
			private int Heal => (int)(BChar.GetStat.atk * 0.5f);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString());
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (Targets[0].GetBuffs(BattleChar.GETBUFFTYPE.DOT, false, false).Count != 0)
				{
					BattleSystem.DelayInput(ApplyDebuffs());
				}
			}

			public override void SkillKill(SkillParticle SP)
			{
				BattleSystem.DelayInput(ApplyDebuffs());
			}

			private IEnumerator ApplyDebuffs()
			{
				BattleSystem.instance.StartCoroutine(Utils.HealingParticle(null, BChar, Heal, true, false, true, true, true, false));

				var enemies = Utils.EnemyTeam.AliveChars;

				foreach (var enemy in enemies)
				{
					Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_QoH_Chant, 1, Utils.ChanceDOT(BChar, 105));
					Utils.AddDebuff(enemy, BChar, ModItemKeys.Buff_B_QoH_LoveHate, 1, Utils.ChanceDOT(BChar, 105));
				}
				yield break;
			}
		}

		public class Harmony : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (Targets[0].GetBuffs(BattleChar.GETBUFFTYPE.DOT, false, false).Count >= 3)
				{
					BattleSystem.instance.StartCoroutine(Scripts.RecastSkill(Targets[0], BChar, MySkill.MySkill.KeyID, 1));
				}
			}
		}

		public class HeartWave : Skill_Extended
		{
			private int Heal => (int)(BChar.GetStat.reg * 0.25f);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString()).Replace("&b", (Heal * 100).ToString());
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				int debuffTypes = GetDebuffTypes(Targets[0]);
				int additionalHeal = Heal * debuffTypes;

				if (debuffTypes > 0)
				{
					SkillBasePlus.Target_BaseHeal = additionalHeal;
				}

				foreach (var target in Targets)
				{
					if (!target.Info.Ally) continue;

					foreach (var debuff in target.GetBuffs(BattleChar.GETBUFFTYPE.DOT, false, false))
					{
						Utils.RemoveBuff(target, debuff.BuffData.Key);
					}
				}
			}

			private int GetDebuffTypes(BattleChar bchar)
			{
				int result = 0;

				foreach (var t in NegativeBuffTypes)
				{
					var buffs = bchar.GetBuffs(t, false, false);
					if (buffs != null && buffs.Count > 0)
					{
						result++;
					}
				}
				return result;
			}

			private readonly BattleChar.GETBUFFTYPE[] NegativeBuffTypes = new[]
			{
				BattleChar.GETBUFFTYPE.DOT,
				BattleChar.GETBUFFTYPE.DEBUFF,
				BattleChar.GETBUFFTYPE.CC
			};
		}

		public class Spiral : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				var enemies = Utils.EnemyTeam.AliveChars;
				var index = RandomManager.RandomInt(RandomClassKey.Curse, 0, enemies.Count);
				var target = enemies[index];

				if (target != null)
				{
					Utils.AddDebuff(target, BChar, ModItemKeys.Buff_B_QoH_Chant, 1, Utils.ChanceDOT(BChar, 105));
				}
			}
		}

		public class Promise : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				var enemies = Utils.EnemyTeam.AliveChars;
				var index = RandomManager.RandomInt(RandomClassKey.Curse, 0, enemies.Count);
				var target = enemies[index];

				if (target != null)
				{
					Utils.AddDebuff(target, BChar, ModItemKeys.Buff_B_QoH_LoveHate, 1, Utils.ChanceDOT(BChar, 105));
				}
			}
		}

		public class Blessing : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				base.SkillUseSingle(SkillD, Targets);
				if (Targets[0].Info.Ally)
				{
					if (BattleSystem.instance.AllyList.Count != 1)
					{
						int num = 0;
						for (int i = 0; i < BattleSystem.instance.AllyTeam.AliveChars.Count; i++)
						{
							if (BattleSystem.instance.AllyTeam.AliveChars[i] == Targets[0])
							{
								num = i;
							}
						}
						List<BattleAlly> allyList = BattleSystem.instance.AllyList;
						List<BattleChar> list = new List<BattleChar>();
						if (allyList.Count > num + 1)
						{
							list.Add(allyList[num + 1]);
						}
						else
						{
							list.Add(allyList[0]);
						}
						if (list.Count != 0)
						{
							Targets.AddRange(list);
							return;
						}
					}
				}
				else if (BattleSystem.instance.EnemyList.Count != 1)
				{
					int num2 = 0;
					List<BattleEnemy> list2 = (Targets[0] as BattleEnemy).EnemyPosNum(out num2);
					List<BattleChar> list3 = new List<BattleChar>();
					if (list2.Count > num2 + 1)
					{
						list3.Add(list2[num2 + 1]);
					}
					else
					{
						list3.Add(list2[0]);
					}
					if (list3.Count != 0)
					{
						Targets.AddRange(list3);
					}
				}
			}
		}

		public class SlaveRare : Skill_Extended
		{
			public override string DescExtended(string desc)
			{
				int damage = (int)(BChar.GetStat.atk * 0.5f);
				return base.DescExtended(desc).Replace("&a", damage.ToString());
			}

			public override void Special_PointerEnter(BattleChar Char)
			{
				base.Special_PointerEnter(Char);
				int damage = (int)(BChar.GetStat.atk * 0.5f);
				SkillBasePlusPreview.Target_BaseDMG = AdditionalDamage(Char) * damage;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				int damage = (int)(BChar.GetStat.atk * 0.5f);
				SkillBasePlus.Target_BaseDMG = AdditionalDamage(Targets[0]) * damage;
			}

			private int AdditionalDamage(BattleChar bchar)
			{
				int num = 0;
				List<Buff> buffs = bchar.GetBuffs(BattleChar.GETBUFFTYPE.DOT, false, false);

				foreach (Buff buff in buffs)
				{
					num += buff.StackNum;
				}
				return num;
			}
		}

		public class BeatsRare : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (Targets[0].GetBuffs(BattleChar.GETBUFFTYPE.DOT, false, false).Count > 0)
				{
					PlusSkillStat.cri = 100;
				}
			}
		}

		public class LucyLoveAttack : Skill_Extended
		{
			private readonly BattleChar Queen = Utils.AllyTeam.AliveChars.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_QoH);
			private int Heal => (int)(Queen?.GetStat.reg * 0.5f);

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", Heal.ToString()).Replace("&b", Queen.Info.Name.ToString());
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				List<Skill> strings = new List<Skill>
				{
					Skill.TempSkill(ModItemKeys.Skill_S_QoH_Lucy_0, Utils.AllyTeam.LucyAlly, Utils.AllyTeam),
					Skill.TempSkill(ModItemKeys.Skill_S_QoH_Lucy_1, Utils.AllyTeam.LucyAlly, Utils.AllyTeam),
				};

				if (Queen != null)
				{
					BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(strings, new SkillButton.SkillClickDel(Selection), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
				}
				else
				{
					Utils.AllyTeam.Draw(2);
				}
			}

			private void Selection(SkillButton Mybutton)
			{
				string key = Mybutton.Myskill.MySkill.KeyID;
				int drawNum = 2;

				if (key == ModItemKeys.Skill_S_QoH_Lucy_0)
				{
					foreach (var ally in Utils.AllyTeam.AliveChars)
					{
						BattleSystem.instance.StartCoroutine(Utils.HealingParticle(ally, Queen, Heal, true, false, false, true, true, false));
					}
				}
				else
				{
					foreach (var enemy in Utils.EnemyTeam.AliveChars)
					{
						foreach (var buff in enemy.Buffs)
						{
							if (buff.BuffData.Debuff && buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
							{
								foreach (StackBuff stackbuff in buff.StackInfo)
								{
									stackbuff.RemainTime++;
								}
							}
						}
					}
					drawNum = 3;
				}

				Utils.AllyTeam.Draw(drawNum);
			}
		}
	}
}
