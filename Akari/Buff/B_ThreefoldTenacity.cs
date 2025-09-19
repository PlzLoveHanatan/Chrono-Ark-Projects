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
namespace Akari
{
    /// <summary>
    /// Threefold Tenacity
    /// </summary>
    public class B_ThreefoldTenacity : Buff, IP_BuffAddAfter
    {
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker == BChar && addedbuff == this && addedbuff.StackNum >= 2)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_ThreefoldTenacity_0, BChar, false, 0, false, -1, false);
                SelfDestroy();
            }
        }
    }
}