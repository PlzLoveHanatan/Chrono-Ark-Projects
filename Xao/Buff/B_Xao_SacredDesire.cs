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
	/// Sacred Desire
	/// </summary>
    public class B_Xao_SacredDesire : Buff, IP_Awake
    {
        private bool FirstAwake;

        public void Awake()
        {
            if (!FirstAwake)
            {
                Xao_Combo.GainComboRewards(Xao_Combo.CurrentCombo, true);
                FirstAwake = true;  
            }
        }
    }
}