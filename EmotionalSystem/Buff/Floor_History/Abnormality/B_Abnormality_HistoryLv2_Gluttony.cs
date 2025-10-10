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
    /// Gluttony
    /// Applies "Glutton's Mark" on attack.
    /// Against targets with "Glutton's Mark", all user attacks deal 15% more damage and heal the user for 15% of the damage dealt.
    /// </summary>
    public class B_Abnormality_HistoryLv2_Gluttony : Buff, IP_DealDamage
    {
        public override void BuffStat()
        {
            PlusStat.HitMaximum = true;
            PlusStat.cri = 20f;
            PlusStat.PlusCriDmg = 20;
        }

        public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
		{
			if (Damage >= 1)
			{
                Utils.PlaySound("Floor_History_Gluttony");
				BChar.Heal(BChar, (int)(Damage * 0.2), false, false, null);
			}
		}
    }
}