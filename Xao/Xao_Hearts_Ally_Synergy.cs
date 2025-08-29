using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Xao
{
    public class Xao_Hearts_Ally_Synergy : MonoBehaviour
    {
        // для каждого союзника храним его сердца и stack
        public static Dictionary<BattleChar, GameObject> heartNormals = new Dictionary<BattleChar, GameObject>();
        public static Dictionary<BattleChar, GameObject> heartGreys = new Dictionary<BattleChar, GameObject>();
        public static Dictionary<BattleChar, int> savedStacks = new Dictionary<BattleChar, int>();

        public static void HeartsCheck(BattleChar bchar, int stackChange = 0)
        {
            if (!savedStacks.ContainsKey(bchar))
            {
                savedStacks[bchar] = 0;
            }

            savedStacks[bchar] += stackChange;
            savedStacks[bchar] = Mathf.Clamp(savedStacks[bchar], 0, 1);

            UpdateHearts(bchar, savedStacks[bchar]);
        }

        private static void UpdateHearts(BattleChar bchar, int stackNum)
        {
            if (stackNum > 1 || !bchar.Info.Ally) return;

            // Цветное сердце
            if (stackNum > 0)
            {
                // удалить серое, если было
                if (heartGreys.TryGetValue(bchar, out var grey) && grey != null)
                {
                    Utils.DestroyAndNullify(ref grey);
                    heartGreys[bchar] = null;
                }

                // создать цветное, если нет
                if (!heartNormals.ContainsKey(bchar) || heartNormals[bchar] == null)
                {
                    var heart = Utils.CreateIcon(
                        bchar,
                        $"HeartNormalAlly_{bchar.Info.KeyData}",
                        Utils.HeartsPath[$"HeartNormal_0"],
                        new Vector3(1.25f, 0.5f),
                        new Vector3(90f, 90f),
                        false,
                        true
                    );

                    Utils.StartHeartsPopOut(heart);
                    Utils.AddComponent<Xao_Hearts_Script_Ally_Synergy>(heart);
                    Utils.AddComponent<Xao_Hearts_Tooltip>(heart);

                    Xao_Hearts_Script_Ally_Synergy script = heart.GetComponent<Xao_Hearts_Script_Ally_Synergy>();
                    script.SetOwner(bchar);

                    heartNormals[bchar] = heart;
                }
            }
            else // Серое сердце
            {
                // удалить цветное
                if (heartNormals.TryGetValue(bchar, out var normal) && normal != null)
                {
                    Utils.DestroyAndNullify(ref normal);
                    heartNormals[bchar] = null;
                }

                // создать серое, если нет
                //if (!heartGreys.ContainsKey(bchar) || heartGreys[bchar] == null)
                //{
                //    var grey = Utils.CreateIcon(
                //        bchar,
                //        $"HeartGreyAlly_{bchar.Info.KeyData}",
                //        Utils.HeartsPath[$"HeartGrey_0"],
                //        new Vector3(1.25f, 0.5f),
                //        new Vector3(90f, 90f),
                //        false
                //    );

                //    Utils.StartHeartsGreyPopOut(grey);
                //    heartGreys[bchar] = grey;
                //}
            }
        }

        public static void DestroyHearts(BattleChar bchar)
        {
            if (heartNormals.TryGetValue(bchar, out var normal) && normal != null)
            {
                Utils.DestroyAndNullify(ref normal);
                heartNormals[bchar] = null;
            }

            if (heartGreys.TryGetValue(bchar, out var grey) && grey != null)
            {
                Utils.DestroyAndNullify(ref grey);
                heartGreys[bchar] = null;
            }

            savedStacks[bchar] = 0;
        }

        // если нужно подчистить всё вообще
        public static void DestroyAll()
        {
            foreach (var kv in heartNormals)
            {
                var obj = kv.Value;
                if (obj != null) Utils.DestroyAndNullify(ref obj);
            }
            heartNormals.Clear();

            foreach (var kv in heartGreys)
            {
                var obj = kv.Value;
                if (obj != null) Utils.DestroyAndNullify(ref obj);
            }
            heartGreys.Clear();
            savedStacks.Clear();
        }
    }
}
