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
using HarmonyLib;
namespace Akari
{
    public class Akari_Plugin: ChronoArkPlugin
    {
        Harmony harmony = new Harmony("Akari");

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
                Debug.Log("Akari: Patch Catch: " + e.ToString());
            }
        }

        public static bool AkariInParty()
        {
            return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Akari);
        }

        [HarmonyPatch(typeof(FieldSystem), "StageStart")]
        public static class StagePatch
        {
            [HarmonyPostfix]
            public static void StageStartPostfix()
            {
                if (AkariInParty())
                {
                    if (PlayData.TSavedata.StageNum >= 0)
                    {
                        if (Utils.ThumbEquip && !Utils.Equip)
                        {
                            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_BayonetSword, 1));
                            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_JudgmentsPistol, 1));
                            Utils.Equip = true;
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PlayData), "GameEndInit")]
        public static class MemoryReset
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                Utils.Equip = false;
            }
        }
    }
}