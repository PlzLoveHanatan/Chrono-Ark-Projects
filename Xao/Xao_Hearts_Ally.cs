using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Xao
{
    public class Xao_Hearts_Ally : MonoBehaviour
    {
        // для каждого союзника храним его сердца и stack
        private static Dictionary<BattleChar, GameObject[]> heartNormals = new Dictionary<BattleChar, GameObject[]>();
        private static Dictionary<BattleChar, GameObject[]> heartGreys = new Dictionary<BattleChar, GameObject[]>();
        private static Dictionary<BattleChar, int> savedStacks = new Dictionary<BattleChar, int>();

        private const int MaxHearts = 3;

        public static void HeartsCheckAlly(BattleChar bchar, int stackChange = 0)
        {
            if (!savedStacks.ContainsKey(bchar))
            {
                savedStacks[bchar] = 0;
            }

            savedStacks[bchar] += stackChange;
            savedStacks[bchar] = Mathf.Clamp(savedStacks[bchar], 0, MaxHearts);

            UpdateHeartsAlly(bchar, savedStacks[bchar]);
        }

        private static void UpdateHeartsAlly(BattleChar bchar, int stackNum)
        {
            if (!bchar.Info.Ally) return;

            // создаём массивы серых и нормальных сердец для этого союзника, если ещё нет
            if (!heartNormals.ContainsKey(bchar))
            {
                heartNormals[bchar] = new GameObject[MaxHearts];
            }

            if (!heartGreys.ContainsKey(bchar))
            {
                heartGreys[bchar] = new GameObject[MaxHearts];
            }

            var normalHearts = heartNormals[bchar];
            var greyHearts = heartGreys[bchar];

            for (int i = 0; i < MaxHearts; i++)
            {
                if (i < stackNum) // цветные сердца
                {
                    if (greyHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref greyHearts[i]);
                    }

                    if (normalHearts[i] == null)
                    {
                        normalHearts[i] = Utils.CreateIcon(
                            bchar,
                            $"HeartNormalAlly_{i}",
                            Utils.HeartsPath[$"HeartNormal_{i}"],
                            Utils.HeartsPosition[$"HeartNormal_{i}"],
                            new Vector3(90f, 90f),
                            false,
                            true
                        );

                        Utils.StartHeartsPopOut(normalHearts[i]);
                        Utils.AddComponent<Xao_Hearts_Script_Ally>(normalHearts[i]);
                        Utils.AddComponent<Xao_Hearts_Tooltip>(normalHearts[i]);

                        // Привязка владельца
                        var script = normalHearts[i].GetComponent<Xao_Hearts_Script_Ally>();
                        script.SetOwner(bchar);
                    }
                }
                else // серые сердца
                {
                    if (normalHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref normalHearts[i]);
                    }

                    //if (greyHearts[i] == null)
                    //{
                    //    greyHearts[i] = Utils.CreateIcon(
                    //        bchar,
                    //        $"HeartGreyAlly_{i}",
                    //        Utils.HeartsPath[$"HeartGrey_{i}"],
                    //        Utils.HeartsPosition[$"HeartGrey_{i}"],
                    //        new Vector3(90f, 90f),
                    //        false
                    //    );
                    //    Utils.StartHeartsGreyPopOut(greyHearts[i]);
                    //}
                }
            }
        }

        public static void DestroyHearts(BattleChar bchar)
        {
            if (heartNormals.TryGetValue(bchar, out var normalHearts))
            {
                for (int i = 0; i < normalHearts.Length; i++)
                {
                    if (normalHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref normalHearts[i]);
                    }   
                }
                heartNormals[bchar] = new GameObject[MaxHearts];
            }

            if (heartGreys.TryGetValue(bchar, out var greyHearts))
            {
                for (int i = 0; i < greyHearts.Length; i++)
                {
                    if (greyHearts[i] != null)
                    {
                        Utils.DestroyAndNullify(ref greyHearts[i]);
                    }   
                }
                heartGreys[bchar] = new GameObject[MaxHearts];
            }

            savedStacks[bchar] = 0;
        }

        public static void DestroyAll()
        {
            foreach (var kv in heartNormals)
            {
                var arr = kv.Value;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != null) Utils.DestroyAndNullify(ref arr[i]);
                } 
            }
            heartNormals.Clear();

            foreach (var kv in heartGreys)
            {
                var arr = kv.Value;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != null) Utils.DestroyAndNullify(ref arr[i]);
                }
            }
            heartGreys.Clear();

            savedStacks.Clear();
        }
    }
}
