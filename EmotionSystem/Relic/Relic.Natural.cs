using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		}
	}
}
