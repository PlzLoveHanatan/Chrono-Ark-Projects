using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;
using EmotionSystem;
using GameDataEditor;
using System.Collections;
using static CharacterDocument;
using UnityEngine.PostProcessing;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QoH
{
	public class Relics
	{
		public class Star : PassiveItemBase, IP_PlayerTurn
		{
			private bool oncePerFight;

			public override void Init()
			{
				OnePassive = true;
			}

			public void Turn()
			{
				if (BattleSystem.instance.TurnNum >= 6 && !oncePerFight)
				{
					if (Utils.AllyTeam.AliveChars.All(a => a != null && a.EmotionLevel() >= 5))
					{
						oncePerFight = true;
						ShinyEffect();
						EmotionManager.AbnormalitySelection(5);
					}
				}
			}
		}

		public class SunMoon : PassiveItemBase, PassiveItem_EnableItem, IP_EnemyAwake, IP_BattleStart_UIOnBefore
		{
			public override void Init()
			{
				//NoBattleRelic = true;
			}

			public void BattleStartUIOnBefore(BattleSystem Ins)
			{
				EnemyEmotionsCheck();
			}

			public void EnemyAwake(BattleChar Enemy)
			{
				BattleSystem.DelayInputAfter(ApplySunMoon(Enemy));
			}

			private IEnumerator ApplySunMoon(BattleChar Enemy)
			{
				yield return null;
				ShinyEffect();
				Utils.AddBuff(Enemy, ModItemKeys.Buff_B_R_QoH_SunMoon);
			}

			public void EnableItem()
			{
				IncreaseArkPassiveNum(2);

				InventoryManager.Reward(new List<ItemBase>
				{
					ItemBase.GetItem(GDEItemKeys.Item_Consume_EquipPouch),
					ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy_Rare),
					ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenBread),
				});
			}

			private void EnemyEmotionsCheck()
			{
				if (!Utils.EnemyEmotions)
				{
					var mod = ModManager.getModInfo("EmotionSystem");
					mod.GetSetting<ToggleSetting>("Enemy Emotions").Value = true;
					mod.SaveSetting();
				}
			}
		}
	}
}
