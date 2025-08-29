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
	/// Activate <color=#87CEFA>Combo</color> <color=#d78fe9>Rewards II</color> for this battle.
	/// Gain all current <color=#87CEFA>Combo</color> rewards upon gaining this buff (Once per battle).
	/// </summary>
    public class B_Xao_Miko_1 : Buff, IP_Awake
    {
        private bool FirstAwake;

        public void Awake()
        {
            if (!FirstAwake)
            {
                Xao_Combo.AdditionalComboRewards_1 = true;
                Xao_Combo.GainComboRewards(Xao_Combo.CurrentCombo, Xao_Combo.AdditionalComboRewards_0, Xao_Combo.AdditionalComboRewards_1);
                FirstAwake = true;
            }
        }
    }
}