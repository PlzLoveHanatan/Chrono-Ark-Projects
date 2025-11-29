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
						Utils.ApplyBurn(enemy, Utils.DummyChar, 4);
					}
				}
			}

			public class Torch : PassiveItemBase, IP_BuffAddAfter, IP_PlayerTurn
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
				{
					if (!BuffTaker.Info.Ally && addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT && addedbuff is Buff buff)
					{
						buff.TimeUseless = true;
					}
				}

				public void Turn()
				{
					foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
					{
						foreach (var buff in enemy.Buffs)
						{
							if (Exception.Contains(buff.BuffData.Key)) continue;

							buff.SelfStackDestroy();
						}
					}
				}

				private readonly List<string> Exception = new List<string>()
				{
					ModItemKeys.Buff_B_EmotionSystem_Bleed,
					ModItemKeys.Buff_B_EmotionSystem_Burn,
				};
			}
		}
	}
}
