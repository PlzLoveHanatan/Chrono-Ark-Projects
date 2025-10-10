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
using NLog.Targets;
namespace EmotionalSystem
{
    /// <summary>
    /// Unity
    /// </summary>
    public class B_EnemyAbnormality_Unity : Buff, IP_TurnEnd
    {
		public override void Init()
		{
            PlusStat.dod = 10;
		}

		public void TurnEnd()
		{
            int heal = 30;
            Utils.EnemyTeam.AliveChars.ForEach(e => BattleSystem.DelayInput(Utils.HealingParticle(e, BChar, heal, true, true)));
		}
    }
}