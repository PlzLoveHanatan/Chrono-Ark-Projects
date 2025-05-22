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
	/// Malicious Mark
	/// All incoming damage is increased based on the missing HP of the character with "Malice".
	/// Removed when the character with Malice is killed.
	/// </summary>
    public class B_Abnormality_HistoryLv3_Malice_0 : Buff, IP_PlayerTurn
    {
        public void Turn()
        {
            SelfDestroy();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            var allyWithMalice = BattleSystem.instance.AllyList.FirstOrDefault(i => i.BuffFind(ModItemKeys.Buff_B_Abnormality_HistoryLv3_Malice, false));

            if (allyWithMalice == null)
            {
                this.SelfDestroy();
                return;
            }

            float allyMissingHP = (float)allyWithMalice.HP / allyWithMalice.GetStat.maxhp;

            this.PlusStat.DMGTaken = 10f + (1f - allyMissingHP) * 20; // min 10 & 30% max
        }
    }
}