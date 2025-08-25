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
namespace Xao
{
    /// <summary>
    /// Horny Mod
    /// At the start of each turn obtain <sprite name="Xao_Heart">.
    /// </summary>
    public class B_Xao_Mod_1 : Buff, IP_PlayerTurn
    {
        public void Turn()
        {
            //string affection = ModItemKeys.Buff_B_Xao_Affection;
            //Utils.AddBuff(BChar, affection);
        }
    }
}