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
	/// Shattered Blooming
	/// </summary>
    public class B_Rinne_ShatteredBlooming : Buff, IP_BuffAddAfter
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
            PlusPerStat.Damage = 5 * StackNum;
            PlusStat.hit = 5 * StackNum;
        }
    }
}