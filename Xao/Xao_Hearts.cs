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

        public static int SavedStackNum;

        public static void HeartsCheck(BattleChar bchar, int stackChange = 0)
        {
            SavedStackNum += stackChange;

            SavedStackNum = Mathf.Clamp(SavedStackNum, 0, 3);
            UpdateHearts(bchar, SavedStackNum);
        }

        public static void UpdateHearts(BattleChar bchar, int stackNum)
        {
            if (stackNum > 3) return;

            GameObject[] normalHearts = { HeartNormal_0, HeartNormal_1, HeartNormal_2 };
            GameObject[] greyHearts = { HeartGrey_0, HeartGrey_1, HeartGrey_2 };
            string buffKey = ModItemKeys.Buff_B_Xao_Affection;
            string xao = ModItemKeys.Character_Xao;
            var aliveXao = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(c => c.Info.KeyData == xao);

            for (int i = 0; i < 3; i++)
            {
                if (i < stackNum) // Эти должны быть цветными
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
                else // Эти должны быть серыми
                {
                    if (normalHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref normalHearts[i]);
                    }
                    if (greyHearts[i] == null)
                    {
                        greyHearts[i] = Utils.CreateIcon(bchar, $"HeartGrey_{i}", Utils.HeartsPath[$"HeartGrey_{i}"], Utils.HeartsPosition[$"HeartGrey_{i}"], new Vector3(90f, 90f), false);
                        Utils.StartHeartsGreyPopOut(normalHearts[i]);
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
    }
}
