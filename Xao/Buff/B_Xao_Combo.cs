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
            string combo;

            if (Utils.XaoHornyMod)
            {
                if (Xao_Combo.AdditionalComboRewards_0 && Xao_Combo.AdditionalComboRewards_1)
                {
                    combo = ModLocalization.Combo_Description_Horny_3; // All Combo
                }
                else if (Xao_Combo.AdditionalComboRewards_0)
                {
                    combo = ModLocalization.Combo_Description_Horny_1; // Only Combo I
                }
                else if (Xao_Combo.AdditionalComboRewards_1)
                {
                    combo = ModLocalization.Combo_Description_Horny_2; // Only Combo II
                }
                else
                {
                    combo = ModLocalization.Combo_Description_Horny_0; // Only 10th Combo
                }
            }
            else if (Xao_Combo.AdditionalComboRewards_0 && Xao_Combo.AdditionalComboRewards_1)
            {
                combo = ModLocalization.Combo_Description_3; // All combo, no 10th
            }
            else if (Xao_Combo.AdditionalComboRewards_0)
            {
                combo = ModLocalization.Combo_Description_1; // Only Combo I
            }
            else if (Xao_Combo.AdditionalComboRewards_1)
            {
                combo = ModLocalization.Combo_Description_2; // Only Combo II
            }
            else
            {
                combo = ModLocalization.Combo_Description_0; // No rewards
            }
            return combo.Replace("&a", Xao_Combo.CurrentCombo.ToString());
        }

        public void Turn()
        {
            if (!Xao_Combo.SaveComboBetweenTurns)
            {
                Xao_Combo.CurrentCombo = 0;
                Xao_Combo.ComboChange(0, true, true);
            }
            else
            {
                Xao_Combo.SaveComboBetweenTurns = false;

                if (Xao_Combo.AttackPowerOncePerFight)
                {
                    Xao_Combo.AttackPowerOncePerFight = false;
                }

                Xao_Combo.ApplyComboRewards(Xao_Combo.CurrentCombo);
            }
        }
    }
}