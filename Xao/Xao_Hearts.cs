using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.U2D;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Xao
{
    public class Xao_Hearts : MonoBehaviour
    {
        public static GameObject HeartGrey_0, HeartGrey_1, HeartGrey_2;
        public static GameObject HeartNormal_0, HeartNormal_1, HeartNormal_2;

        // Для союзников отдельные ссылки
        public static GameObject HeartGreyAlly_0, HeartGreyAlly_1, HeartGreyAlly_2;
        public static GameObject HeartNormalAlly_0, HeartNormalAlly_1, HeartNormalAlly_2;

        // Для синергии
        public static GameObject HeartGreyAllySynergy;
        public static GameObject HeartNormalAllySynergy;

        public static int SavedStackNum;
        public static int SavedStackAlly;
        public static int SavedStackAllySynergy;

        public static void HeartsCheck(BattleChar bchar, int stackChange = 0)
        {
            SavedStackNum += stackChange;
            SavedStackNum = Mathf.Clamp(SavedStackNum, 0, 3);
            UpdateHearts(bchar, SavedStackNum);
        }

        public static void HeartsCheckAlly(BattleChar bchar, int stackChange = 0)
        {
            SavedStackAllySynergy += stackChange;
            SavedStackAllySynergy = Mathf.Clamp(SavedStackAllySynergy, 0, 3);
            UpdateHeartsAlly(bchar, SavedStackAllySynergy);
        }

        public static void HeartsCheckAllySynergy(BattleChar bchar, int stackChange = 0)
        {
            SavedStackAllySynergy += stackChange;
            SavedStackAllySynergy = Mathf.Clamp(SavedStackAllySynergy, 0, 1);
            UpdateHeartsAllySynergy(bchar, SavedStackAllySynergy);
        }

        public static void UpdateHearts(BattleChar bchar, int stackNum)
        {
            if (stackNum > 3) return;

            GameObject[] normalHearts = { HeartNormal_0, HeartNormal_1, HeartNormal_2 };
            GameObject[] greyHearts = { HeartGrey_0, HeartGrey_1, HeartGrey_2 };

            for (int i = 0; i < 3; i++)
            {
                if (i < stackNum) // цветные
                {
                    if (greyHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref greyHearts[i]);
                    }
                    if (normalHearts[i] == null)
                    {
                        normalHearts[i] = Utils.CreateIcon(bchar, $"HeartNormal_{i}", Utils.HeartsPath[$"HeartNormal_{i}"], Utils.HeartsPosition[$"HeartNormal_{i}"], new Vector3(90f, 90f), false, true);
                        Utils.StartHeartsPopOut(normalHearts[i]);
                        Utils.AddComponent<Xao_Hearts_Script>(normalHearts[i]);
                        Utils.AddComponent<Xao_Hearts_Tooltip>(normalHearts[i]);
                    }
                }
                else // серые
                {
                    if (normalHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref normalHearts[i]);
                    }
                    if (greyHearts[i] == null)
                    {
                        greyHearts[i] = Utils.CreateIcon(bchar, $"HeartGrey_{i}", Utils.HeartsPath[$"HeartGrey_{i}"], Utils.HeartsPosition[$"HeartGrey_{i}"], new Vector3(90f, 90f), false);
                        Utils.StartHeartsGreyPopOut(greyHearts[i]);
                    }
                }
            }

            HeartNormal_0 = normalHearts[0];
            HeartNormal_1 = normalHearts[1];
            HeartNormal_2 = normalHearts[2];

            HeartGrey_0 = greyHearts[0];
            HeartGrey_1 = greyHearts[1];
            HeartGrey_2 = greyHearts[2];
        }

        public static void UpdateHeartsAlly(BattleChar bchar, int stackNum)
        {
            if (stackNum > 3) return;

            GameObject[] normalHearts = { HeartNormalAlly_0, HeartNormalAlly_1, HeartNormalAlly_2 };
            GameObject[] greyHearts = { HeartGreyAlly_0, HeartGreyAlly_1, HeartGreyAlly_2 };

            for (int i = 0; i < 3; i++)
            {
                if (i < stackNum) // цветные
                {
                    if (greyHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref greyHearts[i]);
                    }
                    if (normalHearts[i] == null)
                    {
                        normalHearts[i] = Utils.CreateIcon(bchar, $"HeartNormalAlly_{i}", Utils.HeartsPath[$"HeartNormal_{i}"], Utils.HeartsPosition[$"HeartNormal_{i}"], new Vector3(90f, 90f), false, true);
                        Utils.StartHeartsPopOut(normalHearts[i]);
                        Utils.AddComponent<Xao_Hearts_Script_Ally>(normalHearts[i]);
                        Utils.AddComponent<Xao_Hearts_Tooltip>(normalHearts[i]);
                    }
                }
                else // серые
                {
                    if (normalHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref normalHearts[i]);
                    }
                    if (greyHearts[i] == null)
                    {
                        greyHearts[i] = Utils.CreateIcon(bchar, $"HeartGreyAlly_{i}", Utils.HeartsPath[$"HeartGrey_{i}"], Utils.HeartsPosition[$"HeartGrey_{i}"], new Vector3(90f, 90f), false);
                        Utils.StartHeartsGreyPopOut(greyHearts[i]);
                    }
                }
            }

            HeartNormalAlly_0 = normalHearts[0];
            HeartNormalAlly_1 = normalHearts[1];
            HeartNormalAlly_2 = normalHearts[2];

            HeartGreyAlly_0 = greyHearts[0];
            HeartGreyAlly_1 = greyHearts[1];
            HeartGreyAlly_2 = greyHearts[2];
        }
        public static void UpdateHeartsAllySynergy(BattleChar bchar, int stackNum)
        {
            if (stackNum > 1) return;

            GameObject[] normalHearts = { HeartNormalAllySynergy };
            GameObject[] greyHearts = { HeartGreyAllySynergy };

            for (int i = 0; i < 1; i++)
            {
                if (i < stackNum) // цветные
                {
                    if (greyHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref greyHearts[i]);
                    }
                    if (normalHearts[i] == null)
                    {
                        normalHearts[i] = Utils.CreateIcon(bchar, $"HeartNormalAlly", Utils.HeartsPath[$"HeartNormal_0"], Utils.HeartsPosition[$"HeartNormal_0"], new Vector3(90f, 90f), false, true);
                        Utils.StartHeartsPopOut(normalHearts[i]);
                        Utils.AddComponent<Xao_Hearts_Script_Ally_Synergy>(normalHearts[i]);
                        Utils.AddComponent<Xao_Hearts_Tooltip>(normalHearts[i]);
                    }
                }
                else // серые
                {
                    if (normalHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref normalHearts[i]);
                    }
                    if (greyHearts[i] == null)
                    {
                        greyHearts[i] = Utils.CreateIcon(bchar, $"HeartGreyAlly_0", Utils.HeartsPath[$"HeartGrey_0"], Utils.HeartsPosition[$"HeartGrey_0"], new Vector3(90f, 90f), false);
                        Utils.StartHeartsGreyPopOut(greyHearts[i]);
                    }
                }
            }

            HeartNormalAllySynergy = normalHearts[0];

            HeartGreyAllySynergy = greyHearts[0];
        }

        public static void CreateHearts(BattleChar bchar)
        {
            if (HeartGrey_0 == null)
            {
                HeartGrey_0 = Utils.CreateIcon(bchar, "HeartGrey_0", Utils.HeartsPath["HeartGrey_0"], Utils.HeartsPosition["HeartGrey_0"], new Vector3(90f, 90f), false);
            }

            if (HeartGrey_1 == null)
            {
                HeartGrey_1 = Utils.CreateIcon(bchar, "HeartGrey_1", Utils.HeartsPath["HeartGrey_1"], Utils.HeartsPosition["HeartGrey_1"], new Vector3(90f, 90f), false);
            }

            if (HeartGrey_2 == null)
            {
                HeartGrey_2 = Utils.CreateIcon(bchar, "HeartGrey_2", Utils.HeartsPath["HeartGrey_2"], Utils.HeartsPosition["HeartGrey_2"], new Vector3(90f, 90f), false);
            }
        }

        public static void DestroyAndNullifyAll()
        {
            Utils.DestroyAndNullify(ref HeartGrey_0);
            Utils.DestroyAndNullify(ref HeartGrey_1);
            Utils.DestroyAndNullify(ref HeartGrey_2);
            Utils.DestroyAndNullify(ref HeartNormal_0);
            Utils.DestroyAndNullify(ref HeartNormal_1);
            Utils.DestroyAndNullify(ref HeartNormal_2);
        }

        public static void CreateHeartsAlly(BattleChar bchar)
        {
            if (HeartGreyAlly_0 == null)
            {
                HeartGreyAlly_0 = Utils.CreateIcon(bchar, "HeartGreyAlly_0", Utils.HeartsPath["HeartGrey_0"], Utils.HeartsPosition["HeartGrey_0"], new Vector3(90f, 90f), false);
            }

            if (HeartGreyAlly_1 == null)
            {
                HeartGreyAlly_1 = Utils.CreateIcon(bchar, "HeartGreyAlly_1", Utils.HeartsPath["HeartGrey_1"], Utils.HeartsPosition["HeartGrey_1"], new Vector3(90f, 90f), false);
            }

            if (HeartGreyAlly_2 == null)
            {
                HeartGreyAlly_2 = Utils.CreateIcon(bchar, "HeartGreyAlly_2", Utils.HeartsPath["HeartGrey_2"], Utils.HeartsPosition["HeartGrey_2"], new Vector3(90f, 90f), false);
            }
        }

        public static void DestroyAndNullifyAllAlly()
        {
            Utils.DestroyAndNullify(ref HeartGreyAlly_0);
            Utils.DestroyAndNullify(ref HeartGreyAlly_1);
            Utils.DestroyAndNullify(ref HeartGreyAlly_2);
            Utils.DestroyAndNullify(ref HeartNormalAlly_0);
            Utils.DestroyAndNullify(ref HeartNormalAlly_1);
            Utils.DestroyAndNullify(ref HeartNormalAlly_2);
        }

        // Создать 1 серое сердце союзника (Synergy)
        public static void CreateHeartAllySynergy(BattleChar bchar)
        {
            if (HeartGreyAllySynergy == null)
            {
                HeartGreyAllySynergy = Utils.CreateIcon(bchar, "HeartGreyAllySynergy", Utils.HeartsPath["HeartGrey_0"], Utils.HeartsPosition["HeartGrey_0"], new Vector3(90f, 90f), false);
            }
        }

        // Уничтожить 1 сердце союзника (Synergy)
        public static void DestroyHeartAllySynergy()
        {
            Utils.DestroyAndNullify(ref HeartNormalAllySynergy);
            Utils.DestroyAndNullify(ref HeartGreyAllySynergy);
        }
    }
}
