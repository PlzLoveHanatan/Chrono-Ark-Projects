using System;
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
						Utils.AllyTeam.AP += 2;
					}
				}
			}
		}
	}
}
