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
        private static Vector2 size = new Vector2(110f, 110f);
        public static bool KeyOnce = false;
        public static bool LegendaryOnce = false;
        public static bool AdditionalComboRewards = false;
        public static bool XaoEquipMagicWand;

        public static void ComboChange(int comboChange = 1, bool resetCombo = false, bool isNewTurn = false)
        {
            if (resetCombo)
            {
                DestroyAndNullifyAll(isNewTurn);
            }

            else
            {
                CurrentCombo += comboChange;
                //if (CurrentCombo < 1 || CurrentCombo > 5) return;

                switch (CurrentCombo)
                {
                    case 2:
                    case 4:
                    case 6:
                    case 8:
                    case 15:
                    case 20:
                    case 30:
                    case 50:
                        ApplyComboRewards(CurrentCombo);
                        break;
                }
            }
        }

        public static void IncreaseXaoAP(Character character)
        {
            character.OriginStat.atk++;
        }

        public static void ApplyComboRewards(int currentCombo)
        {
            // Базовые награды
            if (currentCombo >= 2)
            {
                Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Affection);
            }

            if (currentCombo >= 4)
            {
                // Обновление UI комбо
                Utils.DestroyAndNullify(ref Combo_0);
                Combo_1 = Utils.CreateComboButton("Combo_1", BattleSystem.instance.ActWindow.transform, Utils.SpritePaths[Utils.SpriteType.Combo_1], size, new Vector2(-749.7802f, -438.8362f));
                Utils.StartComboPopOut(Combo_1);
                Utils.AddComponent<Xao_Combo_Tooltip>(Combo_1);

                // Доп. действие
                Utils.AllyTeam.Draw();
            }

            if (currentCombo >= 6)
            {
                Utils.AllyTeam.AP += 1;
            }

            if (currentCombo >= 8)
            {
                Utils.DestroyAndNullify(ref Combo_1);
                Combo_2 = Utils.CreateComboButton("Combo_2", BattleSystem.instance.ActWindow.transform, Utils.SpritePaths[Utils.SpriteType.Combo_2], size, new Vector2(-749.7802f, -438.8362f));
                Utils.StartComboPopOut(Combo_2);
                Utils.AddComponent<Xao_Combo_Tooltip>(Combo_2);

                IncreaseXaoAP(Utils.Xao.Info);
            }

            // Дополнительные награды
            if (!AdditionalComboRewards) return;

            if (currentCombo >= 15 && !KeyOnce)
            {
                InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Item_Key, 1));
                KeyOnce = true;
            }

            if (currentCombo >= 20)
            {
                Utils.RemoveFogFromStage = true;
            }

            if (currentCombo >= 30 && !XaoEquipMagicWand)
            {
                string xaoUniqueEquip = ModItemKeys.Item_Equip_Equip_Xao_MagicWand;
                InventoryManager.Reward(ItemBase.GetItem(xaoUniqueEquip, 1));
                XaoEquipMagicWand = true;
            }

            if (currentCombo >= 50 && !LegendaryOnce)
            {
                string equipKey = PlayData.GetEquipRandom(4, false, new List<string> { ModItemKeys.Item_Equip_Equip_Xao_MagicWand } );
                InventoryManager.Reward(ItemBase.GetItem(equipKey, 1));
                LegendaryOnce = true;
            }
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
