using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;

namespace EmotionSystem
{
	public partial class Relic
	{
		public class Technological
		{
			public class Rest : PassiveItemBase, IP_TurnEndButtonEnemy
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public void TurnEndButtonEnemy()
				{
					if (BattleSystem.instance.AllyTeam.AP >= 1)
					{
						ShinyEffect();

						foreach (var enemy in Utils.EnemyTeam.AliveChars)
						{
							Utils.AddDebuff(enemy, Utils.DummyChar, GDEItemKeys.Buff_B_Common_Rest, 1, 100);
						}
					}
				}
			}
		}
	}
}
