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
namespace Darkness
{
	/// <summary>
	/// Darkness Ecstasy
	/// </summary>
    public class B_Darkness_DarknessEcstasy : Buff, IP_BuffAddAfter
    {
        public override void Init()
        {
            base.Init();
            PlusStat.AggroPer = 70;
            PlusStat.DMGTaken = -15;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker.Info.KeyData != ModItemKeys.Character_Darkness && addedbuff == this)
            {
                SelfDestroy();
            }
        }
    }
}