using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class Relic
	{
		public class Literature
		{
			public class Shoes : PassiveItemBase, IP_PlayerTurn
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public void Turn()
				{
					ShinyEffect();

					foreach (var enemy in Utils.EnemyTeam.AliveChars)
					{
						Utils.ApplyBleed(enemy, Utils.DummyChar, 2);
					}
				}
			}

			public class Expression : PassiveItemBase, IP_BattleStart_Ones
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public void BattleStart(BattleSystem Ins)
				{
					BattleSystem.DelayInput(SetPoints());
				}

				private IEnumerator SetPoints()
				{
					foreach (var ally in Utils.AllyTeam.AliveChars_Vanish)
					{
						EmotionManager.GainOnlyNegativePoints(ally);
					}
					yield break;
				}
			}
		}
	}
}
