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
using EItem;
using PItem;
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
            return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_SuperHero);
        }

        [HarmonyPatch(typeof(FieldSystem), "StageStart")]
        public static class StagePatch
        {
            [HarmonyPostfix]
            public static void StageStartPostfix()
            {
                if (SuperHeroInParty())
                {
                    var justiceData = PlayData.TSavedata.GetCustomValue<JusticeSword>();
                    if (justiceData == null)
                    {
                        justiceData = new JusticeSword();
                        PlayData.TSavedata.AddCustomValue(justiceData);
                    }

                    if (!justiceData.ItemsGiven)
                    {
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_SuperHero_LightArmor, 1));
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_SuperHero_JusticeSword, 1));
                        justiceData.ItemsGiven = true;
                    }

                    int currentStage = PlayData.TSavedata.StageNum;
                    if (currentStage >= 0 && !IsCamp)
                    {
                        if (justiceData.LastBoostedStage < currentStage)
                        {
                            int stagesPassed = currentStage - Math.Max(justiceData.LastBoostedStage, 0);
                            justiceData.JusticeSwordDamage += stagesPassed;
                            justiceData.LastBoostedStage = currentStage;
                        }
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

        //[HarmonyPatch(typeof(Character), nameof(Character.StatC))]
        //public static class StatPatch
        //{
        //    [HarmonyPostfix]
        //    public static void Postfix(Character __instance, Stat inputstat, ref Stat __result)
        //    {
        //        if (__instance == null)
        //        {
        //            Debug.Log("[StatPatch] __instance is null, skipping patch.");
        //            return;
        //        }

        //        Stat stat = inputstat;

        //        stat.atk = Mathf.Max(stat.atk, 0f);
        //        stat.def = Mathf.Max(stat.def, 0f);
        //        stat.dod = Mathf.Max(stat.dod, 0f);
        //        stat.Penetration = Mathf.Max(stat.Penetration, 0f);
        //        stat.maxhp = Mathf.Max(stat.maxhp, 0);
        //        stat.reg = Mathf.Max(stat.reg, 0f);
        //        stat.DeadImmune = Mathf.Max(stat.DeadImmune, 0);
        //        stat.AggroPer = Mathf.Clamp(stat.AggroPer, -100, 100);

        //        if (stat.PerfectShield) stat.def = 100f;
        //        if (stat.PerfectDodge) stat.dod = 500f;

        //        if (__instance.KeyData == ModItemKeys.Character_SuperHero)
        //        {
        //            Debug.Log("[StatPatch] Hero detected.");
        //        }

        //        __result = stat;
        //    }
        //}
    }
}