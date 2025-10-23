using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using ChronoArkMod.Template;
using GameDataEditor;
using UnityEngine;
using static MonoMod.Cil.RuntimeILReferenceBag.FastDelegateInvokers;

namespace Meeyu
{
	public class Buffs
	{
		public class EliseDuty : Buff
		{
			public override void Init()
			{
				PlusStat.DMGTaken = -10;
				PlusStat.AggroPer = 40;
			}
		}

		public class LustfulDesire : Buff, IP_DamageTake
		{
			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				if (Dmg >= 1)
				{
					resist = true;
					SelfStackDestroy();
				}
			}
		}

		public class EliseResolve : Buff
		{
			public override void Init()
			{
				PlusStat.DMGTaken = 20;
			}
		}

		public class SlipperyPleasure : Buff
		{
			public override void Init()
			{
				PlusStat.dod = 30;
			}
		}

		public class BlissfulPleasure : Buff
		{
			public override void Init()
			{
				PlusStat.DMGTaken = -5;
			}
		}

		public class SlipperyEcstasy : Buff
		{
			public override void Init()
			{
				PlusStat.DMGTaken = 10;
				PlusStat.dod = -10;
			}
		}

		public class PrincessArousal : B_Taunt
		{
			public override void Init()
			{
				PlusStat.DMGTaken = 5 * StackNum;
			}

			public override void SkillUse(Skill SkillD, List<BattleChar> Targets)
			{
				if (SkillD.IsDamage)
				{
					SelfStackDestroy();
				}
			}
		}

		public class ElisesProtection : Buff, IP_Hit, IP_Dodge
		{
			public override string DescExtended()
			{
				int damage = (int)(BChar.GetStat.atk * 0.4f);
				return base.DescExtended().Replace("&a", damage.ToString());
			}

			public override void Init()
			{
				PlusStat.AggroPer = 80;
				PlusStat.DMGTaken = -5;
			}

			public void Hit(SkillParticle SP, int Dmg, bool Cri)
			{
				if (Dmg > 0)
				{
					CounterAttack(SP.SkillData.Master);
				}
			}

			public void Dodge(BattleChar Char, SkillParticle SP)
			{
				if (Char == BChar)
				{
					CounterAttack(SP.SkillData.Master);
				}
			}

			private void CounterAttack(BattleChar target)
			{
				if (target.Info.Ally) return;

				Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Meeyu_EliseHelpMe_1, BChar, BChar.MyTeam);
				skill.FreeUse = true;
				skill.PlusHit = true;
				BattleSystem.DelayInput(Wait());
				BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, target, false, false, true, null));
			}

			private IEnumerator Wait()
			{
				yield return new WaitForSeconds(0.2f);
				yield break;
			}
		}

		public class PussyLock : Common_Buff_Rest
		{
			
		}

		public class Overmilking : Common_Buff_Rest
		{
			
		}
	}
}
