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
	/// Pollen
	/// Removed at the start of the next turn.
	/// </summary>
    public class B_Abnormality_HistoryLv2_WorkerBee_0 : Buff, IP_PlayerTurn
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusStat.DMGTaken = 30f;
        }
        public void Turn()
        {
            SelfDestroy();
        }
    }
}