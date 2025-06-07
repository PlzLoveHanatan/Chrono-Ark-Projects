using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.CompilerServices;
namespace Kazuma
{
    /// <summary>
    /// Kazuma
    /// Passive:
    /// </summary>
    public class P_Kazuma : Passive_Char, IP_LevelUp
    {
        private int KazumaLevel;
        public override void Init()
        {
            base.Init();
            OnePassive = true;
        }

        public void LevelUp()
        {
            KazumaLevel++;
            Debug.Log($"[LevelUp] KazumaLevel increased to {KazumaLevel}");
            Debug.Log($"[LevelUp] MyChar.LV is {MyChar.LV}");

            if (KazumaLevel == 4 || MyChar.LV >= 2)
            {
                Debug.Log("[LevelUp] Condition met — calling EnableItem");
                FieldSystem.DelayInput(EnableItem());
            }
            else
            {
                Debug.Log("[LevelUp] Condition not met — EnableItem not called");
            }
        }

        public IEnumerator EnableItem()
        {
            Debug.Log("[EnableItem] Started");

            List<string> collection = new List<string>();

            GDEDataManager.GetAllDataKeysBySchema(GDESchemaKeys.Item_Scroll, out collection);
            Debug.Log($"[EnableItem] Found {collection.Count} scroll items");

            PlayData.TSavedata.IdentifyItems.AddRange(collection);
            Debug.Log("[EnableItem] Added scroll items to IdentifyItems");

            yield return null;
            Debug.Log("[EnableItem] Finished");
        }
    }
}