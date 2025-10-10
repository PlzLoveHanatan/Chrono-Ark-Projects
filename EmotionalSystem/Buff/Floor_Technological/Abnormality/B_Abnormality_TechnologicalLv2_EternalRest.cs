using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
namespace EmotionalSystem
{
	/// <summary>
	/// Eternal Rest
	/// </summary>
    public class B_Abnormality_TechnologicalLv2_EternalRest : Buff, IP_DealDamage
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 20;
        }

		public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
		{
			if (Take is BattleEnemy enemy)
			{
				int threshold = (int)(enemy.GetStat.maxhp * 0.2f);

				if (Damage >= threshold)
				{
					Utils.PlaySound("Floor_Technological_EternalRest");
                    Utils.AddDebuff(enemy, BChar, GDEItemKeys.Buff_B_Common_Rest, 1, (int)(BChar.GetStat.HIT_CC + 100));
				}
			}
		}
	}
}