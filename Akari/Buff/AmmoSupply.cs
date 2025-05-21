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
    public class AmmoSupply : Buff
    {
        public override void Init()
        {
            base.Init();

            if (BattleSystem.instance != null && StackNum >= 3)
            {
                Utils.CreateRandomAmmunition(BChar);

                SelfDestroy();
            }
        }
    }
}