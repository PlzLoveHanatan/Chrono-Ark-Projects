using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Xao
{
    public class Xao_Combo : MonoBehaviour
    {
        public static GameObject Combo_0, Combo_1, Combo_2, Combo_3;
        public static int CurrentCombo;

        public static bool SaveComboBetweenTurns = false;

        public static void ComboChange(int comboChange = 1, bool resetCombo = false, bool isNewTurn = false)
        {
            if (resetCombo)
            {
                CurrentCombo = 0;
                DestroyCombo(isNewTurn);
            }
            else
            {
                int oldCombo = CurrentCombo;
                int newCombo = oldCombo + comboChange;

                CurrentCombo = newCombo;

                TriggerComboVisual(CurrentCombo);

                if (comboChange > 0)
                {
                    for (int i = oldCombo + 1; i <= newCombo; i++)
                    {
                        if (Xao_Combo_Rewards.СomboRewards.TryGetValue(i, out var rewardAction))
                        {
                            rewardAction?.Invoke();
                        } 
                    }
                }
            }
        }

        private static void TriggerComboVisual(int currentCombo)
        {
            if (currentCombo >= 4 && currentCombo < 8 && Combo_1 == null)
            {
                Utils.DestroyAndNullify(ref Combo_0);
                Combo_1 = Utils.CreateNewCombo(Combo_1, "Combo_1", Utils.SpritePaths[Utils.SpriteType.Combo_1], true);
            }
            else if (currentCombo >= 8 && Combo_2 == null)
            {
                Utils.DestroyAndNullify(ref Combo_1);
                Combo_2 = Utils.CreateNewCombo(Combo_2, "Combo_2", Utils.SpritePaths[Utils.SpriteType.Combo_2], true);
            }
        }

        public static void DestroyCombo(bool isNewTurn = false)
        {
            if (isNewTurn)
            {
                Utils.DestroyAndNullify(ref Combo_0);
                Utils.DestroyAndNullify(ref Combo_1);
                Utils.DestroyAndNullify(ref Combo_2);

                Combo_0 = Utils.CreateNewCombo(Combo_0, "Combo_0", Utils.SpritePaths[Utils.SpriteType.Combo_0]);
            }
        }
    }
}
