using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class Relic
	{
		public class History
		{
			public class Ember : PassiveItemBase, IP_PlayerTurn
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
						Utils.ApplyBurn(enemy, Utils.DummyChar, 3);
					}
				}
			}
		}
	}
}
