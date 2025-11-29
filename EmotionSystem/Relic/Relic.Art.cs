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
		public class Art
		{
			public class Sheet : PassiveItemBase, IP_PlayerTurn
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public void Turn()
				{
					if (BattleSystem.instance.TurnNum % 2 == 0)
					{
						ShinyEffect();
						Utils.AllyTeam.AP += 1;
					}
				}
			}

			public class Flower : PassiveItemBase, IP_BattleStart_Ones
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
					foreach (var ally in Utils.AllyTeam.AliveChars)
					{
						EmotionManager.GainOnlyPositivePoints(ally);
					}
					yield break;
				}
			}
		}
	}
}
