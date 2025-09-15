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
using System.Runtime.InteropServices.WindowsRuntime;
namespace Mikure
{
	/// <summary>
	/// Stay With Me!
	/// </summary>
    public class B_Mikure_StayHere : Buff, IP_BuffAdd, IP_Awake
    {
        public bool DebuffBlocked;

        public override string DescExtended()
        {
            string text = !DebuffBlocked ? ModLocalization.DebuffBlocked : "";
            return text;
        }

        public void Awake()
        {
            DebuffBlocked = false;
        }

        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if (BuffTaker == BChar && addedbuff.BuffData.Debuff)
            {
                if (!DebuffBlocked)
                {
                    DebuffBlocked = true;

                    addedbuff.SelfDestroy();
                    BuffTaker.SimpleTextOut(ScriptLocalization.UI_Battle.DebuffGuard);
                }
            }
        }

        public override void Init()
        {
            PlusStat.RES_DOT = 15;
            PlusStat.RES_DEBUFF = 15;
            PlusStat.RES_CC = 15;
        }
    }
}