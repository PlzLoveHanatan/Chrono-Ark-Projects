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
	/// The Fairies' Care
	/// </summary>
    public class B_Abnormality_HistoryLv1_TheFairiesCare : Buff, IP_TurnEnd
    {
		public override string DescExtended()
		{
			int heal = (int)(BChar.GetStat.maxhp * 0.2f);
			return base.DescExtended().Replace("&a", heal.ToString());
		}

		public override void Init()
        {
            PlusStat.RES_DOT = -20f;
            PlusStat.RES_CC = -20f;
            PlusStat.RES_DEBUFF = -20f;
        }

		public void TurnEnd()
		{
			Utils.PlaySound("Floor_History_FairiesCare");
			int heal = (int)(BChar.GetStat.maxhp * 0.2f);
			BattleSystem.DelayInputAfter(Utils.HealingParticle(BChar, BattleSystem.instance.DummyChar, heal, true));
		}
	}
}