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
                    if (PlayData.TSavedata.StageNum >= 0 && !IsCamp)
                    {
                        IncreaseSwordDamage();
                    }
                    if (PlayData.TSavedata.StageNum >= 0 && !Utils.Equip)
                    {
                        Utils.Equip = true;
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_SuperHero_LightArmor, 1));
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_SuperHero_JusticeSword, 1));
                    }
                    else if (PlayData.TSavedata.StageNum == 0)
                    {
                        //if (Utils.Timer && !Utils.FirstTimer)
                        //{
                        //    GlobalTimerManager.Instance?.DestroySelfAndUI();
                        //    var TimerObj = new GameObject("GlobalTimerManager");
                        //    TimerObj.AddComponent<GlobalTimerManager>();
                        //    GlobalTimerManager.Instance?.ResetTimer();
                        //    GlobalTimerManager.Instance?.StartTimer();
                        //    Utils.FirstTimer = true;
                        //}
                    }
                }
            }
        }

        public static void IncreaseSwordDamage(int damage = 1)
        {
            var justiceDamage = PlayData.TSavedata.GetCustomValue<JusticeSword>();

            if (justiceDamage == null)
            {
                justiceDamage = new JusticeSword();
                PlayData.TSavedata.AddCustomValue(justiceDamage);
                justiceDamage.JusticeDamage = 0;
            }

            justiceDamage.JusticeDamage += damage;
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

        [HarmonyPatch(typeof(PlayData), "GameEndInit")]
        public static class MemoryReset
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                Utils.Equip = false;
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


        [HarmonyPatch(typeof(Character), nameof(Character.StatC))]
        class FaintResistPatch
        {
            [HarmonyPrefix]
            static bool Prefix(Stat inputstat, ref Stat __result)
            {
                Stat stat = inputstat;

                if (stat.atk < 0f)
                {
                    stat.atk = 0f;
                }

                if (stat.def < 0f)
                {
                    stat.def = 0f;
                }

                if (stat.PerfectShield)
                {
                    stat.def = 100f;
                }
                else if (stat.def > 80f)
                {
                    if (!Utils.SuperStats)
                    {
                        stat.def = 80f;
                    }      
                }

                if (stat.dod < 0f)
                {
                    stat.dod = 0f;
                }

                if (stat.PerfectDodge)
                {
                    stat.dod = 500f;
                }
                else if (stat.dod > 80f)
                {
                    if (!Utils.SuperStats)
                    {
                        stat.dod = 80f;
                    }
                }

                if (stat.Penetration < 0f)
                {
                    stat.Penetration = 0f;
                }

                if (stat.AggroPer < -100)
                {
                    stat.AggroPer = -100;
                }
                else if (stat.AggroPer > 100)
                {
                    if (!Utils.SuperStats)
                    {
                        stat.AggroPer = 100;
                    }
                }

                if (stat.maxhp < 0)
                {
                    stat.maxhp = 0;
                }

                if (stat.reg < 0f)
                {
                    stat.reg = 0f;
                }

                if (stat.DeadImmune < 0)
                {
                    stat.DeadImmune = 0;
                }
                else if (stat.DeadImmune > 80)
                {
                    if (!Utils.SuperStats)
                    {
                        stat.DeadImmune = 80;
                    }
                }

                __result = stat;
                return false;
            }
        }
    }
}