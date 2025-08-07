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
    public class B_Xao_Combo : Buff, IP_PlayerTurn
    {
        public override string DescExtended()
        {
            if (BattleSystem.instance != null)
            {
                return base.DescExtended().Replace("&a", Xao_Combo.CurrentCombo.ToString());
            }
            return base.DescExtended().Replace("&a", 0.ToString());
        }

        public void Turn()
        {
            Xao_Combo.CurrentCombo = 0;
            Xao_Combo.ComboChange(0, true, true);
        }
    }
}