using System;
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

					foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
					{
						Utils.ApplyBleed(enemy, Utils.DummyChar, 2);
					}
				}
			}
		}
	}
}
