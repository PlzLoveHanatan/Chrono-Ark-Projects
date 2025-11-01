using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curse;
using GameDataEditor;
using UnityEngine;
using static EmotionSystem.Debuffs;
using static EmotionSystem.Guests;
using static MonoMod.Cil.RuntimeILReferenceBag.FastDelegateInvokers;

namespace EmotionSystem
{
	public partial class Guest
	{
		public class Distortions
		{
			public class Distorted : Dice, IP_Dead
			{
				public readonly List<ItemBase> RewardItems = new List<ItemBase>();

				public override void Init()
				{
					if (RewardItems.Count > 0)
					{
						foreach (var item in RewardItems)
						{
							Itemviews.Add(item);
						}
					}
				}

				public void AddRewards(int equipTier = 0, int soulStone = 0)
				{
					string randomEquipKey = PlayData.GetEquipRandom(equipTier, false, new List<string>());

					RewardItems.Add(ItemBase.GetItem(randomEquipKey));
					//RewardItems.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_ArtifactPouch));we
					RewardItems.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, soulStone));
					//RewardItems.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_RedHammer));
				}

				public void RegisterRewardsToView()
				{
					if (RewardItems.Count > 0)
					{
						foreach (var item in RewardItems)
						{
							Itemviews.Add(item);
						}
					}
				}

				public virtual void Dead()
				{
					if (RewardItems.Count > 0)
					{
						foreach (var itemKey in RewardItems)
						{
							BattleSystem.instance.Reward.Add(itemKey);
						}
					}
				}
			}

			public class Marauding : Distorted
			{
				public override void Init()
				{
					PlusPerStat.MaxHP += 60;
					PlusStat.RES_CC += 20f;
					PlusStat.RES_DEBUFF += 20f;
					PlusStat.RES_DOT += 20f;
					AddRewards(3, 1);
					RegisterRewardsToView();
				}
			}

			public class Colossal : Distorted
			{
				public override void FixedUpdate()
				{
					(BChar as BattleEnemy).CharObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
				}

				public override void Init()
				{
					PlusPerStat.MaxHP += 140;
					PlusStat.RES_CC += 20f;
					PlusStat.RES_DEBUFF += 20f;
					PlusStat.RES_DOT += 20f;
					AddRewards(3, 1);
					RegisterRewardsToView();
				}
			}

			public class Robust : Distorted
			{
				public override void Init()
				{
					PlusPerStat.MaxHP += 60;
					PlusStat.RES_CC += 20f;
					PlusStat.RES_DEBUFF += 20f;
					PlusStat.RES_DOT += 20f;
					PlusStat.DMGTaken -= 10;
					AddRewards(3, 1);
					RegisterRewardsToView();
				}
			}

			public class Horrifying : Distorted, IP_PlayerTurn
			{
				public override void Init()
				{
					PlusPerStat.MaxHP += 60;
					PlusStat.RES_CC += 20f;
					PlusStat.RES_DEBUFF += 20f;
					PlusStat.RES_DOT += 20f;
					PlusStat.dod += 10;
					AddRewards(3, 2);
					RegisterRewardsToView();
				}

				public void Turn()
				{
					BattleSystem.instance.AllyTeam.AP--;
				}

				public override void Dead()
				{
					BattleSystem.instance.AllyTeam.AP += 1;
					base.Dead();
				}
			}

			public class Executioner : Distorted, IP_TurnEnd
			{
				public override void Init()
				{
					PlusPerStat.MaxHP += 60;
					PlusStat.RES_CC += 20f;
					PlusStat.RES_DEBUFF += 20f;
					PlusStat.RES_DOT += 20f;
					PlusPerStat.Damage += 10;
					AddRewards(3, 1);
					RegisterRewardsToView();
				}

				public void TurnEnd()
				{
					foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
					{
						if (battleChar.HP <= 0)
						{
							BattleSystem.DelayInputAfter(Curse_loss_of_will.Dead(battleChar));
						}
					}
				}
			}

			public class Unstoppable : Distorted, IP_PlayerTurn
			{
				public override void Init()
				{
					PlusPerStat.MaxHP += 60;
					PlusStat.RES_CC += 30f;
					PlusStat.RES_DEBUFF += 30f;
					PlusStat.RES_DOT += 30f;
					AddRewards(3, 1);
					RegisterRewardsToView();
				}

				public void Turn()
				{
					BChar.BuffAdd(GDEItemKeys.Buff_B_Blockdebuff, BChar, false, 0, false, -1, false);
				}
			}
		}
	}
}
