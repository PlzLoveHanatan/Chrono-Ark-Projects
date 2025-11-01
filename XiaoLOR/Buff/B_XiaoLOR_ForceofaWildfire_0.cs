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
namespace XiaoLOR
{
	/// <summary>
	/// Force of a Wildfire
	/// At 5 stack apply Stun (<sprite=2>200%).
	/// </summary>
    public class B_XiaoLOR_ForceofaWildfire_0 : Buff
    {
        public override void Init()
        {
            if (BattleSystem.instance != null && base.StackNum == 4)
            {
                this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_Rest, Usestate_L, false, 125, false, -1, false);
                SelfDestroy();
            }
        }
    }
}