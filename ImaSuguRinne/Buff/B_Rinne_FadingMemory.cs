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
	/// Fading Memory
	/// </summary>
    public class B_Rinne_FadingMemory : Buff
    {
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker.Info.KeyData != ModItemKeys.Character_Rinne && addedbuff == this)
            {
                SelfDestroy(BuffTaker);
            }
        }

        public override void BuffStat()
        {
            PlusStat.hit = 2 * StackNum;
            PlusPerStat.Damage = 2 * StackNum;
        }
    }
}