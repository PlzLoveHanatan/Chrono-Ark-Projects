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
using ChronoArkMod.ModData;
using Steamworks;
using HarmonyLib;
using TileTypes;
namespace Xao
{
    public class Xao_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("Xao");

        public override void Dispose()
        {
            if (harmony != null)
            {
                harmony.UnpatchSelf();
            }
        }

        public override void Initialize()
        {
            try
            {
                harmony.PatchAll();
            }
            catch (Exception e)
            {
                Debug.Log("Xao: Patch Catch: " + e.ToString());
            }
        }

        public static bool XaoInParty()
        {
            foreach (var character in PlayData.TSavedata.Party)
            {
                if (character.KeyData == ModItemKeys.Character_Xao)
                {
                    return true;
                }
            }
            return false;
        }

        [HarmonyPatch(typeof(FieldSystem), "StageStart")]
        public static class StagePatch
        {
            [HarmonyPostfix]
            public static void StageStartPostfix()
            {
                if (XaoInParty())
                {
                    if (PlayData.TSavedata.StageNum >= 1 && !Utils.ItemTake)
                    {
                        Utils.ItemTake = true;
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_Equip_Xao_LoveEgg, 1));
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_Equip_Xao_MagicWand, 1));
                    }
                    else if (PlayData.TSavedata.StageNum == 0)
                    {
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_Equip_Xao_LoveEgg, 1));
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_Equip_Xao_MagicWand, 1));
                        Utils.ItemTake = true;
                    }
                }
            }

            //[HarmonyPatch(typeof(CharacterSkinData), "SteamDLCCheck")]
            //public static class DLC_Patch
            //{
            //    [HarmonyPrefix]
            //    public static bool Prefix(ref bool __result, uint dlcKey)
            //    {
            //        __result = true;
            //        return false;
            //    }
            //}
        }
    }
}