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
	/// Malice
	/// At the start of each turn, apply "Malicious Mark" to all enemies who do not already have it.
	/// </summary>
    public class B_Abnormality_HistoryLv3_Malice : Buff, IP_PlayerTurn_1
    {
        public void Turn1()
        {
            foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
            {
                if (!battleEnemy.BuffFind(ModItemKeys.Buff_B_Abnormality_HistoryLv3_Malice_0, false))
                {
                    battleEnemy.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv3_Malice_0, this.BChar, false, 0, false, -1, false);
                }
            }
        }
    }
}