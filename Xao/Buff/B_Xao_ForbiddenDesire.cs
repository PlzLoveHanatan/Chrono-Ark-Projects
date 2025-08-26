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
    /// Forbidden Desire
    ///  Increase <color=#87CEFA>Combo</color> rewards for this battle.
    /// </summary>
    public class B_Xao_ForbiddenDesire : Buff, IP_Awake, IP_BattleEnd
    {
        private bool FirstAwake;

        public void Awake()
        {
            if (!FirstAwake)
            {
                Xao_Combo.AdditionalComboRewards = true;
                Xao_Combo.ApplyComboRewards(Xao_Combo.CurrentCombo);
                FirstAwake = true;
            }
        }

        public void BattleEnd()
        {
            Xao_Combo.AdditionalComboRewards = false;
        }
    }
}