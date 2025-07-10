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
                if (SuperHeroInParty())
                {
                    if (PlayData.TSavedata.StageNum == 0 && !Utils.ItemTake)
                    {
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_SuperHero_LightArmor, 1));
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_SuperHero_JusticeSword, 1));
                        Utils.ItemTake = true;
                    }
                    if (PlayData.TSavedata.StageNum >= 1 && !IsCamp)
                    {
                        Utils.JusticeSword++;
                    }
                }
            }
        }


        public static bool IsCamp
        {
            get
            {
                StageSystem instance = StageSystem.instance;
                string key = instance?.StageData?.Key;
                return IsCampKey(key);
            }
        }

        public static bool IsCampKey(string key)
        {
            return key == GDEItemKeys.Stage_Stage_Camp ||
                   key == GDEItemKeys.Stage_Stage2_Camp ||
                   key == GDEItemKeys.Stage_Stage3_Camp;
        }

        [HarmonyPatch(typeof(CharEquipInven))]
        [HarmonyPatch(nameof(CharEquipInven.AddNewItem))]
        public class Curse_Equip_Patch
        {
            [HarmonyPrefix]
            public static void Prefix(int ItemNum, ItemBase Item)
            {
                if (SuperHeroInParty())
                {
                    var heroSword = ModItemKeys.Item_Equip_E_SuperHero_JusticeSword;
                    var heroArmor = ModItemKeys.Item_Equip_E_SuperHero_LightArmor;

                    List<string> blockedEnchantKeys = new List<string>
                    {
                        "En_Broken",
                        "En_uncomfortable",
                        "En_heavy"
                    };

                    if (Item is Item_Equip equip && equip.Enchant != null)
                    {
                        if ((equip.MyData.Key == heroSword || equip.MyData.Key == heroArmor) &&
                            blockedEnchantKeys.Contains(equip.Enchant.Key))
                        {
                            equip.Enchant = new ItemEnchant();
                        }
                    }
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

                if (inputitem.itemkey == ModItemKeys.Item_Equip_E_SuperHero_JusticeSword && bc != ModItemKeys.Character_SuperHero
                    || (inputitem.itemkey == ModItemKeys.Item_Equip_E_SuperHero_LightArmor && bc == ModItemKeys.Character_SuperHero))
                {
                    __result = true;
                    return false;
                }

                return true;
            }
        }
    }
}