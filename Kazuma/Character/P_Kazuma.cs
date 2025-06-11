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
using TileTypes;
using System.Security.Principal;
namespace Kazuma
{
    /// <summary>
    /// Kazuma
    /// Passive:
    /// </summary>
    public class P_Kazuma : Passive_Char, IP_LevelUp
    {
        public bool Identify;
        public bool MapIdentify;
        public override void Init()
        {
            base.Init();
            OnePassive = true;
        }

        public void LevelUp()
        {
            if (MyChar.LV >= 2 && Identify)
            {
                Identify = false;
                FieldSystem.DelayInput(EnableItem());
            }
            if (MyChar.LV >= 1 && MapIdentify)
            {
                MapIdentify = false;
                KazumaDeleteFog();
            }
        }

        public IEnumerator EnableItem()
        {
            List<string> collection = new List<string>();
            GDEDataManager.GetAllDataKeysBySchema(GDESchemaKeys.Item_Scroll, out collection);
            PlayData.TSavedata.IdentifyItems.AddRange(collection);

            yield return null;
        }

        public void KazumaDeleteFog()
        {
            if (StageSystem.instance.gameObject.activeInHierarchy && StageSystem.instance != null && StageSystem.instance.Map != null)
            {
                StageSystem.instance.Fogout(false);
            }
            for (int i = 0; i < StageSystem.instance.Map.EventTileList.Count; i++)
            {
                if (StageSystem.instance.Map.EventTileList[i].Info.Type is HiddenWall)
                {
                    StageSystem.instance.Map.EventTileList[i].HexTileComponent.HiddenWallOpen();
                }
            }
            StageSystem.instance.SightView(StageSystem.instance.PlayerPos);
        }
    }
}