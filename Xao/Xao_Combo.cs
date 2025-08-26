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
        public static bool InfinityBookOnce = false;
        public static bool ManaPotionOnce = false;
        public static bool AdditionalComboRewards = false;
        public static bool XaoEquipMagicWand;
        public static bool EnchantedRing;

        public static void ComboChange(int comboChange = 1, bool resetCombo = false, bool isNewTurn = false)
        {
            if (resetCombo)
            {
                DestroyAndNullifyAll(isNewTurn);
            }

            else
            {
                CurrentCombo += comboChange;
                //int[] comboMilestones = { 2, 4, 6, 8, 10, 15, 20, 30, 50 };

                //if (comboMilestones.Contains(CurrentCombo))
                //{
                //    ApplyComboRewards(CurrentCombo);
                //}
                switch (CurrentCombo)
                {
                    case 2:
                        Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Affection);
                        break;

                    case 4:
                        Utils.DestroyAndNullify(ref Combo_0);
                        Utils.CreateNewCombo(Combo_1, "Combo_1", Utils.SpritePaths[Utils.SpriteType.Combo_1], true);
                        Utils.AllyTeam.Draw();
                        break;

                    case 6:
                        Utils.AllyTeam.AP += 1;
                        break;

                    case 8:
                        Utils.DestroyAndNullify(ref Combo_1);
                        Utils.CreateNewCombo(Combo_2, "Combo_2", Utils.SpritePaths[Utils.SpriteType.Combo_2], true);
                        break;

                    case 10:
                        IncreaseXaoAP(Utils.Xao.Info);
                        break;

                    case 12:
                        if (AdditionalComboRewards && !ManaPotionOnce)
                        {
                            string manaPotion = GDEItemKeys.Item_Potions_Potion_Mana;
                            GainReward(manaPotion);
                            ManaPotionOnce = true;
                        }
                        break;

                    case 14:
                        if (AdditionalComboRewards && !KeyOnce)
                        {
                            GainReward(GDEItemKeys.Item_Misc_Item_Key);
                            KeyOnce = true;
                        }
                        break;

                    case 16:
                        if (AdditionalComboRewards)
                        {
                            Utils.RemoveFogFromStage = true;
                        }
                        break;

                    case 18:
                        if (AdditionalComboRewards && !InfinityBookOnce)
                        {
                            GainReward(GDEItemKeys.Item_Consume_SkillBookInfinity);
                            InfinityBookOnce = true;
                        }
                        break;

                    case 20:
                        if (AdditionalComboRewards && !EnchantedRing)
                        {
                            string enchantedRing = GDEItemKeys.Item_Equip_EnchantedRing;
                            GainReward(enchantedRing);
                            EnchantedRing = true;
                        }
                        break;

                    case 25:
                        if (AdditionalComboRewards && !XaoEquipMagicWand)
                        {
                            GainReward(ModItemKeys.Item_Equip_Equip_Xao_MagicWand);
                            XaoEquipMagicWand = true;
                        }
                        break;

                    case 50:
                        if (AdditionalComboRewards && !LegendaryOnce)
                        {
                            GainReward(PlayData.GetEquipRandom(4, false, new List<string> { ModItemKeys.Item_Equip_Equip_Xao_MagicWand } ));
                            LegendaryOnce = true;
                        }
                        break;
                }
            }
        }

        public static void IncreaseXaoAP(Character character)
        {
            if (Utils.XaoHornyMod)
            {
                character.OriginStat.atk++;
            } 
        }

        public static void ApplyComboRewards(int currentCombo)
        {
            if (currentCombo >= 2)
            {
                Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Affection);
            }

            if (currentCombo >= 4)
            {
                Utils.AllyTeam.Draw();
            }

            if (currentCombo >= 6)
            {
                Utils.AllyTeam.AP += 1;
            }

            if (currentCombo >= 8)
            {
                RemoveOverload();
            }

            if (currentCombo >= 10)
            {
                IncreaseXaoAP(Utils.Xao.Info);
            }

            if (!AdditionalComboRewards) return;

            if (currentCombo >= 12 && !ManaPotionOnce)
            {
                string manaPotion = GDEItemKeys.Item_Potions_Potion_Mana;
                GainReward(manaPotion);
                ManaPotionOnce = true;
            }

            if (currentCombo >= 14 && !KeyOnce)
            {
                string key = GDEItemKeys.Item_Misc_Item_Key;
                GainReward(key);
                KeyOnce = true;
            }

            if (currentCombo >= 16)
            {
                Utils.RemoveFogFromStage = true;
            }

            if (currentCombo >= 18 && !InfinityBookOnce)
            {
                string infinityBook = GDEItemKeys.Item_Consume_SkillBookInfinity;
                GainReward(infinityBook);
                InfinityBookOnce = true;
            }

            if (currentCombo >= 20 && !EnchantedRing)
            {
                string enchantedRing = GDEItemKeys.Item_Equip_EnchantedRing;
                GainReward(enchantedRing);
                EnchantedRing = true;
            }

            if (currentCombo >= 25 && !XaoEquipMagicWand)
            {
                string xaoUniqueEquip = ModItemKeys.Item_Equip_Equip_Xao_MagicWand;
                GainReward(xaoUniqueEquip);
                XaoEquipMagicWand = true;
            }

            if (currentCombo >= 50 && !LegendaryOnce)
            {
                string equipKey = PlayData.GetEquipRandom(4, false, new List<string> { ModItemKeys.Item_Equip_Equip_Xao_MagicWand });
                GainReward(equipKey);
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
                Utils.CreateNewCombo(Combo_0, "Combo_0", Utils.SpritePaths[Utils.SpriteType.Combo_0]);
            }
        }

        public static void GainReward(string key)
        {
            InventoryManager.Reward(ItemBase.GetItem(key, 1));
        }

        public static void RemoveOverload()
        {
            Utils.AllyTeam.LucyChar.Overload = 0;
            Utils.AllyTeam.AP += 1;

            foreach (var ally in Utils.AllyTeam.AliveChars)
            {
                if (ally != null)
                {
                    ally.Overload = 0;
                }
            }
        }
    }
}
