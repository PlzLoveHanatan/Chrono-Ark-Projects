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
namespace Raphi
{
    public class B_ElysianEdge : Buff
    {
        public override void Init()
        {
            if (StackNum >= 2)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection_0, BChar, false, 0, false, -1, false);
                SelfDestroy();
            }
        }
    }
}