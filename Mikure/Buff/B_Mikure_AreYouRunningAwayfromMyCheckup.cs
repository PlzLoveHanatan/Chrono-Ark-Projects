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
namespace Mikure
{
	/// <summary>
	/// Are You Running Away from My Checkup?
	/// </summary>
    public class B_Mikure_AreYouRunningAwayfromMyCheckup : Buff, IP_BuffAdd, IP_Awake, IP_PlayerTurn
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
            PlusStat.RES_DOT = 20;
            PlusStat.RES_DEBUFF = 20;
            PlusStat.RES_CC = 20;
        }

        public void Turn()
        {
            DebuffBlocked = false;
        }
    }
}