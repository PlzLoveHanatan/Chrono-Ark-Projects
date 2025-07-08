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
using TileTypes;
namespace SuperHero
{
    public class SuperHero_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("SuperHero");

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
                Debug.Log("SuperHero: Patch Catch: " + e.ToString());
            }
        }
        public static bool SuperHeroInParty()
        {
            foreach (var character in PlayData.TSavedata.Party)
            {
                if (character.KeyData == ModItemKeys.Character_SuperHero)
                {
                    return true;
                }
            }
            return false;
        }

        [HarmonyPatch(typeof(B_ProgramMaster_LucyMain))]
        [HarmonyPatch(nameof(B_ProgramMaster_LucyMain.BuffOneAwake))]
        public static class PainSharingPatch
        {
            static bool Prefix(B_ProgramMaster_LucyMain __instance)
            {
                if (SuperHeroInParty())
                {
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(FieldSystem), "StageStart")]
        public static class StagePatch
        {
            [HarmonyPostfix]
            public static void StageStartPostfix()
            {
                if (SuperHeroInParty() && PlayData.TSavedata.StageNum == 0 && !Utils.ItemTake)
                {
                    PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_SuperHero_LightArmor, 1));
                    PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_SuperHero_JusticeSword, 1));
                    Utils.ItemTake = true;
                }
            }
        }

        [HarmonyPatch(typeof(CharEquipInven), nameof(CharEquipInven.OnDropSlot))]
        [HarmonyPatch(new Type[] { typeof(ItemBase) })]
        public static class EquipPatch
        {
            public static bool Prefix(CharEquipInven __instance, ItemBase inputitem, ref bool __result)
            {
                var bc = __instance.Info.KeyData;

                Debug.Log($"[EquipPatch] Checking drop: item={inputitem.itemkey}, char={bc}");

                if (inputitem.itemkey == ModItemKeys.Item_Equip_E_SuperHero_JusticeSword && bc != ModItemKeys.Character_SuperHero
                    || (inputitem.itemkey == ModItemKeys.Item_Equip_E_SuperHero_LightArmor && bc == ModItemKeys.Character_SuperHero))
                {
                    Debug.Log("[EquipPatch] Justice Sword blocked drop (not SuperHero).");
                    __result = true;
                    return false;
                }

                return true;
            }
        }
    }
}