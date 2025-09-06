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
namespace ImaSuguRinne
{
	/// <summary>
	/// Echoes Cycle
	/// </summary>
    public class B_Rinne_EchoesCycle : Buff, IP_BuffAddAfter
    {
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker.Info.KeyData != ModItemKeys.Character_Rinne && addedbuff == this)
            {
                SelfDestroy(BuffTaker);
            }
        }

        public override void Init()
        {
            PlusStat.hit = 2 * StackNum;

            if (BattleSystem.instance != null && StackNum >= 2)
            {
                Utils.AllyTeam.Draw();
                SelfDestroy();
            }
        }
    }
}