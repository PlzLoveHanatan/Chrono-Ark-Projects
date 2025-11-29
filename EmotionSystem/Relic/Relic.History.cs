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

			public class Torch : PassiveItemBase, IP_BuffAddAfter, IP_PlayerTurn, IP_BattleStart_Ones, IP_BattleEnd
			{
				private readonly HashSet<string> Exception = new HashSet<string>()
				{
					ModItemKeys.Buff_B_EmotionSystem_Bleed,
					ModItemKeys.Buff_B_EmotionSystem_Burn,
				};

				public override void Init()
				{
					OnePassive = true;
				}

				public void BattleStart(BattleSystem Ins)
				{
					Utils.Data.TorchActive = true;
				}

				public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
				{
					if (BuffTaker.Info.Ally) return;

					if (addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT && !addedbuff.TimeUseless)
					{
						addedbuff.TimeUseless = true;
					}
				}

				public void Turn()
				{
					foreach (var enemy in Utils.EnemyTeam.AliveChars)
					{
						foreach (var buff in enemy.Buffs)
						{
							if (buff.BuffData.BuffTag.Key != GDEItemKeys.BuffTag_DOT || Exception.Contains(buff.BuffData.Key))
							{
								continue;
							}
							buff.SelfStackDestroy();
						}
					}
				}

				public void BattleEnd()
				{
					Utils.Data.TorchActive = true;
				}
			}
		}
	}
}
