using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using UnityEngine;

namespace QoH
{
	public class Equips
	{
		public class FormingHate : EquipBase, IP_BuffAdd
		{
			public override void Init()
			{
				OnePassive = true;
				PlusStat.atk = 2;
				PlusStat.dod = 5;
				PlusStat.HIT_DOT = 40;
				PlusStat.RES_DOT = 40;
			}

			public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
			{
				if (BuffUser != BChar && !BuffTaker.Info.Ally) return;

				if (addedbuff.BuffData.Debuff && addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT && !addedbuff.TimeUseless)
				{
					if (RandomManager.RandomPer(RandomClassKey.Active, 100, 40))
					{
						int damage = addedbuff.Tick() * addedbuff.LifeTime * 2;
						//Debug.Log($"Added buff is {addedbuff.BuffData.Name}");
						//Debug.Log($"Total damage is {addedbuff.Tick()}");
						//Debug.Log($"Total damage with multiplier {addedbuff.Tick() * addedbuff.LifeTime * 2}");
						BattleSystem.DelayInput(DealDamage(BuffUser, BuffTaker, addedbuff, damage));
					}
				}
			}

			public IEnumerator DealDamage(BattleChar BuffUser, BattleChar BuffTaker, Buff addedBuff, int damage = 0)
			{
				if (BuffTaker != null && !BuffTaker.IsDead && !BuffTaker.Info.Ally)
				{
					BuffTaker.Damage(BuffUser, damage, false, true, false, 0, false, false, false);
				}
				addedBuff.SelfDestroy();
				yield break;
			}
		}

		public class LovelyGift : EquipBase, IP_PlayerTurn
		{
			public override void Init()
			{
				OnePassive = true;
				PlusStat.reg = 3;
				PlusStat.maxhp = 5;
				PlusStat.dod = 10;
				PlusStat.RES_DOT = 20;
			}

			public void Turn()
			{
				foreach (var ally in BChar.MyTeam.AliveChars)
				{
					Utils.AddBuff(BChar, ally, ModItemKeys.Buff_B_QoH_LoveHate_0, 1);
				}
			}
		}
	}
}
