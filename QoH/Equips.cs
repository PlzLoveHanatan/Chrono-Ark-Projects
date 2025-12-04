using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;

namespace QoH
{
	public class Equips
	{
		public class FormingHate : EquipBase, IP_BuffAdd
		{
			public override void Init()
			{
				OnePassive = true;
				PlusStat.atk = 2;
				PlusStat.dod = 5;
				PlusStat.HIT_DOT = 40;
				PlusStat.RES_DOT = 40;
			}

			public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
			{
				if (BuffUser == BChar && !BuffTaker.Info.Ally && addedbuff.BuffData.Debuff && addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
				{
					BattleSystem.DelayInput(DealDamage(BuffUser, BuffTaker));
				}
			}

			public IEnumerator DealDamage(BattleChar BuffUser, BattleChar BuffTaker)
			{
				if (BuffTaker != null && !BuffTaker.IsDead)
				{
					BuffTaker.Damage(BuffUser, 5, false, true, false, 0, false, false, false);
				}
				yield break;
			}
		}

		public class LovelyGift : EquipBase, IP_SkillUse_User
		{
			public override void Init()
			{
				OnePassive = true;
				PlusPerStat.Heal = 20;
				PlusStat.dod = 20;
				PlusStat.RES_DOT = 20;
			}

			public void SkillUse(Skill SkillD, List<BattleChar> Targets)
			{
				if (SkillD.Master == BChar && SkillD.IsHeal)
				{
					foreach (var ally in BChar.MyTeam.AliveChars)
					{
						Utils.AddBuff(BChar, ally, ModItemKeys.Buff_B_QoH_LoveHate_0, 1);
					}
				}
			}
		}
	}
}
