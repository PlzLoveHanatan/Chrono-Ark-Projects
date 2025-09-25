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
            string combo = Utils.XaoHornyMod() ? ModLocalization.Combo_Description_Horny_0 : ModLocalization.Combo_Description_0;
            return combo.Replace("&a", Xao_Combo.CurrentCombo.ToString());
        }

        public void Turn()
        {
            if (!Xao_Combo.SaveComboBetweenTurns)
            {
                Xao_Combo.ComboChange(0, true, true);
            }
            else
            {
                Xao_Combo.SaveComboBetweenTurns = false;
                Xao_Combo.GainComboRewards(Xao_Combo.CurrentCombo, false);
            }
        }
    }
}