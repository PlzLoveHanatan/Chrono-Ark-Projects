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

        public static bool AP_OncePerFight;
        public static bool ManaPotion_OncePerFight;
        public static bool Key_OncePerFight;
        public static bool InfinityBook_OncePerFight;
        public static bool Legendary_OncePerFight;

        public static bool EnchantedRing;
        public static bool XaoEquipMagicWand;

        public static bool AdditionalComboRewards_0;
        public static bool AdditionalComboRewards_1;

        private static Dictionary<int, Action> СomboRewards = new Dictionary<int, Action>
        {
            { 2, () => { if (AdditionalComboRewards_0) Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Affection, 1); } },
            { 4, () => { if (AdditionalComboRewards_0) Utils.AllyTeam.Draw(); } },
            { 6, () => { if (AdditionalComboRewards_0) Utils.AllyTeam.AP += 1; } },
            { 8, () => { if (AdditionalComboRewards_0) RemoveOverloadAndGainMana(); } },
            { 10, () => { if (!AP_OncePerFight) IncreaseXaoAttackPower(Utils.Xao.Info, Utils.XaoHornyMod, AP_OncePerFight); } },
            { 12, () => { if (AdditionalComboRewards_1 && !ManaPotion_OncePerFight) { GainReward(GDEItemKeys.Item_Potions_Potion_Mana); ManaPotion_OncePerFight = true; } } },
            { 14, () => { if (AdditionalComboRewards_1 && !Key_OncePerFight) { GainReward(GDEItemKeys.Item_Misc_Item_Key); Key_OncePerFight = true; } } },
            { 16, () => { if (AdditionalComboRewards_1) Utils.RemoveFogFromStage = true; } },
            { 18, () => { if (AdditionalComboRewards_1 && !InfinityBook_OncePerFight) { GainReward(GDEItemKeys.Item_Consume_SkillBookInfinity); InfinityBook_OncePerFight = true; } } },
            { 20, () => { if (AdditionalComboRewards_1 && !EnchantedRing) { GainReward(GDEItemKeys.Item_Equip_EnchantedRing); EnchantedRing = true; } } },
            { 25, () => { if (AdditionalComboRewards_1 && !XaoEquipMagicWand) { GainReward(ModItemKeys.Item_Equip_Equip_Xao_MagicWand); XaoEquipMagicWand = true; } } },
            { 50, () => { if (AdditionalComboRewards_1 && !Legendary_OncePerFight) { GainReward(PlayData.GetEquipRandom(4, false, new List<string>())); Legendary_OncePerFight = true; } } },
        };

        public static void ComboChange(int comboChange = 1, bool resetCombo = false, bool isNewTurn = false)
        {
            if (resetCombo)
            {
                CurrentCombo = 0;
                DestroyCombo(isNewTurn);
                return;
            }

            CurrentCombo += comboChange;

            TriggerComboVisual(CurrentCombo);

            if (СomboRewards.TryGetValue(CurrentCombo, out var rewardAction))
            {
                rewardAction?.Invoke();
            }
        }

        private static void TriggerComboVisual(int currentCombo)
        {
            switch (currentCombo)
            {
                case 4:
                    Utils.DestroyAndNullify(ref Combo_0);
                    Utils.CreateNewCombo(Combo_1, "Combo_1", Utils.SpritePaths[Utils.SpriteType.Combo_1], true);
                    break;
                case 8:
                    Utils.DestroyAndNullify(ref Combo_1);
                    Utils.CreateNewCombo(Combo_2, "Combo_2", Utils.SpritePaths[Utils.SpriteType.Combo_2], true);
                    break;
            }
        }

        public static void IncreaseXaoAttackPower(Character character, bool isHorny = false, bool oncePerFight = false)
        {
            if (isHorny && !oncePerFight)
            {
                character.OriginStat.atk++;
                AP_OncePerFight = true;
            }
        }
        public static void IncreaseAttackPower(Character character)
        {
            character.OriginStat.atk++;
        }

        public static void GainComboRewards(int currentCombo, bool isSmallReward = false, bool isBigReward = false, bool isAdditionalReward = false)
        {
            if (currentCombo >= 10)
            {
                IncreaseXaoAttackPower(Utils.Xao.Info, Utils.XaoHornyMod, AP_OncePerFight);

                if (isAdditionalReward)
                {
                    IncreaseAttackPower(Utils.Xao.Info);
                }
            }

            if (isSmallReward)
            {
                if (currentCombo >= 2)
                {
                    Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Affection, 1);
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
                    RemoveOverloadAndGainMana();
                }
            }

            if (isBigReward)
            {
                if (currentCombo >= 12 && (!ManaPotion_OncePerFight || isAdditionalReward))
                {
                    GainReward(GDEItemKeys.Item_Potions_Potion_Mana);
                    ManaPotion_OncePerFight = true;
                }

                if (currentCombo >= 14 && (!Key_OncePerFight || isAdditionalReward))
                {
                    GainReward(GDEItemKeys.Item_Misc_Item_Key);
                    Key_OncePerFight = true;
                }

                if (currentCombo >= 16)
                {
                    Utils.RemoveFogFromStage = true;
                }

                if (currentCombo >= 18 && (!InfinityBook_OncePerFight || isAdditionalReward))
                {
                    GainReward(GDEItemKeys.Item_Consume_SkillBookInfinity);
                    InfinityBook_OncePerFight = true;
                }

                if (currentCombo >= 20 && !EnchantedRing)
                {
                    GainReward(GDEItemKeys.Item_Equip_EnchantedRing);
                    EnchantedRing = true;
                }

                if (currentCombo >= 25 && !XaoEquipMagicWand)
                {
                    GainReward(ModItemKeys.Item_Equip_Equip_Xao_MagicWand);
                    XaoEquipMagicWand = true;
                }

                if (currentCombo >= 50 && (!Legendary_OncePerFight || isAdditionalReward))
                {
                    string equipKey = PlayData.GetEquipRandom(4, false, new List<string>());
                    GainReward(equipKey);
                    Legendary_OncePerFight = true;
                }
            }
        }   

        public static void GainReward(string key)
        {
            InventoryManager.Reward(ItemBase.GetItem(key, 1));
        }

        public static void RemoveOverloadAndGainMana()
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
        public static void DestroyCombo(bool isNewTurn = false)
        {
            Utils.DestroyAndNullify(ref Combo_0);
            Utils.DestroyAndNullify(ref Combo_1);
            Utils.DestroyAndNullify(ref Combo_2);

            if (isNewTurn)
            {
                Utils.CreateNewCombo(Combo_0, "Combo_0", Utils.SpritePaths[Utils.SpriteType.Combo_0]);
            }
        }
    }
}
