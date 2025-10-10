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
	/// Predation
	/// </summary>
    public class B_Abnormality_HistoryLv2_Predation : Buff, IP_TurnEnd, IP_Awake
    {
		public void Awake()
		{
			Utils.PlaySound("Floor_History_Predation");
		}

		public override void BuffStat()
        {
			PlusPerStat.Damage = 40;
			PlusStat.PlusCriDmg = 40;
        }

		public void TurnEnd()
		{
			Utils.PlaySound("Floor_History_Predation");
			foreach (var ally in Utils.AllyTeam.AliveChars)
			{
				int damage = (int)(ally.GetStat.maxhp * 0.2f);
				ally.Damage(BChar, damage, false, true, false, 0, false, false, false);
			}
		}
	}
}