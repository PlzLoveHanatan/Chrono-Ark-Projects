using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using EmotionSystem;
using GameDataEditor;

namespace QoH
{
	public class Extendeds
	{
		public class Hysteria : Skill_Extended
		{
			public override bool Terms()
			{
				bool result = true;

				if (MySkill.Master.Info.KeyData == ModItemKeys.Character_QoH)
				{
					if (Utils.ReturnBuff(BChar, ModItemKeys.Buff_B_QoH_Sanity) is Buffs.QoHSanity sanity)
					{
						result = (sanity.MagicalGirlMode && MySkill.IsHeal) || (!sanity.MagicalGirlMode && MySkill.IsDamage) || (!MySkill.IsHeal && !MySkill.IsDamage);
					}
				}

				return result;
			}
		}

		public class Ex_0 : Skill_Extended
		{
			public override bool CanSkillEnforce(Skill MainSkill)
			{
				return MainSkill.AP >= 1;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				var queen = Utils.AllyTeam.AliveChars.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_QoH);
				int heal = (int)(queen.GetStat.reg * 0.75);

				if (queen != null)
				{
					BattleSystem.instance.StartCoroutine(Utils.HealingParticle(null, Utils.DummyChar, heal, true, false, true, false, false, false));
				}

				if (Utils.ReturnBuff(queen, ModItemKeys.Buff_B_QoH_Sanity) is Buffs.QoHSanity sanity)
				{
					sanity.UnlimitedSwitchesTurn = true;
				}
			}
		}

		public class Ex_1 : Skill_Extended
		{
			public override bool CanSkillEnforce(Skill MainSkill)
			{
				return MainSkill.AP >= 1;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				foreach (var enemy in Utils.EnemyTeam.AliveChars)
				{
					foreach (var buff in enemy.Buffs)
					{
						if (buff.BuffData.Debuff && buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
						{
							int damage = buff.Tick() * buff.LifeTime * 2;
							BattleSystem.DelayInput(DealDamage(Utils.DummyChar, enemy, buff, damage));
						}
					}
				}
			}

			public IEnumerator DealDamage(BattleChar user, BattleChar target, Buff buff, int damage = 0)
			{
				if (!target.IsDead && target != null)
				{
					target.Damage(user, damage, false, true, false, 0, false, false, false);
				}
				buff.SelfDestroy();
				yield break;
			}
		}
	}
}
