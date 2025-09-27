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

        public static bool AttackPower_OncePerFight;
        public static bool Legendary_OncePerFight;

        private static Dictionary<int, Action> СomboRewards = new Dictionary<int, Action>
        {
            { 4, () => { Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Affection, 1); } },
            { 6, () => { Utils.AllyTeam.Draw(); } },
            { 8, () => { Utils.AllyTeam.AP += 1; } },
            { 10, () => { if (!AttackPower_OncePerFight) IncreaseXaoAttackPower(Utils.Xao.Info, Utils.XaoHornyMod(), AttackPower_OncePerFight); } },
            { 50, () => {{ GainReward(PlayData.GetEquipRandom(4, false, new List<string>())); Legendary_OncePerFight = true; } } },
        };

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
                    // прошли вперёд: триггерим все пороги между oldCombo+1 и newCombo (включительно)
                    for (int i = oldCombo + 1; i <= newCombo; i++)
                    {
                        if (СomboRewards.TryGetValue(i, out var rewardAction))
                        {
                            rewardAction?.Invoke();
                        } 
                    }
                }
                else
                {
                    // поведение как раньше: проверка только текущего значения
                    if (СomboRewards.TryGetValue(CurrentCombo, out var rewardAction))
                    {
                        rewardAction?.Invoke();
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

        public static void IncreaseXaoAttackPower(Character character, bool isHorny = false, bool oncePerFight = false, bool isAdditionalReward = false)
        {
            if (isHorny && !oncePerFight)
            {
                character.OriginStat.atk++;
                AttackPower_OncePerFight = true;
            }
            else if (isAdditionalReward)
            {
                character.OriginStat.atk++;
            }
        }

        public static void GainComboRewards(int currentCombo, bool isAdditionalReward = false)
        {
            if (currentCombo >= 4)
            {
                Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Affection, 1);
            }

            if (currentCombo >= 6)
            {
                Utils.AllyTeam.AP += 1;
            }

            if (currentCombo >= 8)
            {
                Utils.AllyTeam.Draw();

            }

            //if (currentCombo >= 8)
            //{
            //    RemoveOverloadAndGainMana();
            //}

            if (currentCombo >= 10)
            {
                IncreaseXaoAttackPower(Utils.Xao.Info, Utils.XaoHornyMod(), AttackPower_OncePerFight);

                if (isAdditionalReward)
                {
                    IncreaseXaoAttackPower(Utils.Xao.Info, isAdditionalReward);
                }
            }
        }   

        public static void GainReward(string key)
        {
            InventoryManager.Reward(ItemBase.GetItem(key, 1));
        }

        //public static void RemoveOverloadAndGainMana()
        //{
        //    Utils.AllyTeam.LucyChar.Overload = 0;
        //    Utils.AllyTeam.AP += 1;

        //    foreach (var ally in Utils.AllyTeam.AliveChars)
        //    {
        //        if (ally != null)
        //        {
        //            ally.Overload = 0;
        //        }
        //    }
        //}

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
