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
namespace Urunhilda
{
    /// <summary>
    /// Red Recoverin
    /// Increase Maximum Mana by 1 for <b>this run</b>.
    /// </summary>
    public class C_Urunhilda_RecoverinRed : UseitemBase
    {
        public override bool Use(Character CharInfo)
        {
            Utils.ForceMPUpgrade();
            MasterAudio.PlaySound("Potion", 1f);
            CharInfo.OriginStat.atk += 1f;

            return true;
        }
    }
}