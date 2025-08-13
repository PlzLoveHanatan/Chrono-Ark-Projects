using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Xao
{
    public class Xao_Combo : MonoBehaviour
    {
        public static GameObject Combo_0, Combo_1, Combo_2, Combo_3;
        public static int CurrentCombo = 0;
        public static bool SaveComboBetweenTurns = false;

        public static void ComboChange(int comboChange = 1, bool resetCombo = false, bool isNewTurn = false)
        {
            if (resetCombo)
            {
                DestroyAndNullifyAll(isNewTurn);
            }

            else
            {
                var xao = ModItemKeys.Character_Xao;
                var aliveXao = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == xao)?.Info;

                CurrentCombo += comboChange;
                Vector2 size = new Vector2(110f, 110f);
                if (CurrentCombo < 1 || CurrentCombo > 5)
                    return;

                switch (CurrentCombo)
                {
                    //case 1:
                    //    Utils.DestroyAndNullify(ref Combo_0);
                    //    Combo_0 = Utils.CreateComboButton("Combo_0", BattleSystem.instance.ActWindow.transform, Utils.SpritePaths[Utils.SpriteType.Combo_0], size, new Vector2(441.7031f, 303.54240f));
                    //    Utils.StartComboPopOut(Combo_0);
                    //    Utils.AddComponent<Xao_Combo_Tooltip>(Combo_0);
                    //    break;

                    case 2:
                        Utils.DestroyAndNullify(ref Combo_0);
                        Combo_1 = Utils.CreateComboButton("Combo_1", BattleSystem.instance.ActWindow.transform, Utils.SpritePaths[Utils.SpriteType.Combo_1], size, new Vector2(-749.7802f, -438.8362f));
                        Utils.StartComboPopOut(Combo_1);
                        Utils.AddComponent<Xao_Combo_Tooltip>(Combo_1);
                        break;

                    case 4:
                        Utils.DestroyAndNullify(ref Combo_1);
                        Utils.DestroyAndNullify(ref Combo_2);
                        Combo_2 = Utils.CreateComboButton("Combo_2", BattleSystem.instance.ActWindow.transform, Utils.SpritePaths[Utils.SpriteType.Combo_2], size, new Vector2(-749.7802f, -438.8362f));
                        Utils.StartComboPopOut(Combo_2);
                        Utils.AddComponent<Xao_Combo_Tooltip>(Combo_2);
                        IncreaseXaoAP(aliveXao);
                        break;

                    //case 5:
                    //    Utils.DestroyAndNullify(ref Combo_2);
                    //    Utils.DestroyAndNullify(ref Combo_3);
                    //    Combo_3 = Utils.CreateComboButton("Combo_3", BattleSystem.instance.ActWindow.transform, Utils.SpritePaths[Utils.SpriteType.Combo_3], size, new Vector2(441.7031f, 303.54240f));
                    //    Utils.StartComboPopOut(Combo_3);
                    //    break;

                    default:
                        break;
                }
            }
        }

        public static void IncreaseXaoAP(Character character)
        {
            character.OriginStat.atk++;
        }

        public static void DestroyAndNullifyAll(bool isNewTurn = false)
        {
            Utils.DestroyAndNullify(ref Combo_0);
            Utils.DestroyAndNullify(ref Combo_1);
            Utils.DestroyAndNullify(ref Combo_2);

            if (isNewTurn)
            {
                Combo_0 = Utils.CreateComboButton("Combo_0", BattleSystem.instance.ActWindow.transform, Utils.SpritePaths[Utils.SpriteType.Combo_0], new Vector2(110f, 110f), new Vector2(-749.7802f, -438.8362f));
                Utils.AddComponent<Xao_Combo_Tooltip>(Combo_0);
            }
        }
    }
}
