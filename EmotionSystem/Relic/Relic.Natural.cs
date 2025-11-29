using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Relic
	{
		public class Natural
		{
			public class Ashen : PassiveItemBase, IP_PlayerTurn
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public void Turn()
				{
					ShinyEffect();

					foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
					{
						Utils.AddDebuff(enemy, Utils.DummyChar, ModItemKeys.Buff_B_EmotionSystem_Fragile, 5, 100);
					}
				}
			}

			public class Wealth : PassiveItemBase, IP_DamageTakeChange_sumoperation
			{
				public void DamageTakeChange_sumoperation(BattleChar Hit, BattleChar User, int Dmg, bool Cri, ref int PlusDmg, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
				{
					if (Hit.Info.Ally && Dmg >= 1)
					{
						float gold = PlayData.TSavedata._Gold;
						float reduction = (gold / 500) * 0.01f;
						float multiplier = 1f - reduction;
						multiplier = Mathf.Clamp(multiplier, 0, 1f);
						int newDamage = (int)(Dmg * multiplier);
						newDamage = Mathf.Max(1, newDamage);
						PlusDmg = newDamage - Dmg;
					}
				}
			}
		}
	}
}
