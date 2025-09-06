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
	/// Eternal Blooming
	/// </summary>
    public class B_Rinne_EternalBlooming : Buff, IP_BuffAddAfter, IP_PlayerTurn
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
            PlusStat.cri = 10;
            PlusStat.PlusCriDmg = 10;
            PlusStat.hit = 10;
            PlusStat.PlusCriHeal = 10;
            PlusStat.HitMaximum = true;
        }

        public void Turn()
        {
            Utils.EternalRare(BChar);
        }
    }
}