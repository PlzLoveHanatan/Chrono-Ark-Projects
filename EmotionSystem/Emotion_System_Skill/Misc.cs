using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Miscellaneous
	{
		public class Relic
		{
			public class GoldenSound : PassiveItemBase, IP_PlayerTurn
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public void Turn()
				{
					//Utils.AllyTeam.Draw();

					foreach (var ally in Utils.AllyTeam.AliveChars)
					{
						EmotionalManager.GetPosEmotion(ally, null, 3);
					}
				}
			}
		}
	}
}
