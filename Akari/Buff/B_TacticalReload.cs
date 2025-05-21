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
    /// Tactical Reload
    /// </summary>
    public class B_TacticalReload : Buff
    {
        public override void Init()
        {
            base.Init();

            if (BattleSystem.instance != null && StackNum >= 5)
            {
                MasterAudio.PlaySound("Gun_Reload1", 100f, null, 0f, null, null, false, false);
                BattleSystem.instance.AllyTeam.Draw();
                SelfDestroy();
            }
        }
    }
}