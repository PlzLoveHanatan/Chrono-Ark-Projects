using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

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
						result = (sanity.MagicalGirlMode && MySkill.IsHeal) || (!sanity.MagicalGirlMode && MySkill.IsDamage);
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
				int heal = (int)(queen.GetStat.reg * 0.5);
				BattleSystem.instance.StartCoroutine(Utils.HealingParticle(null, Utils.DummyChar, heal, true, false, true, false, false, false));

				if (Utils.ReturnBuff(queen, ModItemKeys.Buff_B_QoH_Sanity) is Buffs.QoHSanity sanity)
				{
					sanity.UnlimitedSwitchesTurn = true;
				}
			}
		}
	}
}
