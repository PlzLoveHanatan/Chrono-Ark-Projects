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
namespace Urunhilda
{
    public class Urunhilda_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("Urunhilda");

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
                Debug.Log("Urunhilda: Patch Catch: " + e.ToString());
            }
        }
        public static bool UrunhildaInParty()
        {
            foreach (var character in PlayData.TSavedata.Party)
            {
                if (character.KeyData == ModItemKeys.Character_Urunhilda)
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
                if (UrunhildaInParty())
                {
                    if (PlayData.TSavedata.StageNum >= 1 && !Utils.RewardTake)
                    {

                        Utils.UrunhildaFirstReward();
                        Utils.IncreaseArkPassiveNum();
                        Utils.RewardTake = true;
                    }
                    else if (PlayData.TSavedata.StageNum == 0)
                    {
                        Utils.UrunhildaFirstReward();
                        Utils.IncreaseArkPassiveNum(1);
                        Utils.RewardTake = true;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(GDEStageData), "get_Event_L")]
        public static class EventPatch // Increase Large Events
        {
            [HarmonyPostfix]
            public static void Postfix(ref int __result)
            {
                if (UrunhildaInParty())
                {
                    int events = 1;

                    if (PlayData.TSavedata == null || PlayData.TSavedata.Party == null || events == 0)
                        return;

                    __result += events;
                }
            }
        }

        public static int Stage => PlayData.TSavedata.StageNum;

        [HarmonyPatch(typeof(FieldStore))]
        public static class NewShopItems
        {
            [HarmonyPatch("Init")]
            [HarmonyPostfix]
            public static void NewItems(FieldStore __instance)
            {
                if (UrunhildaInParty())
                {
                    switch (Stage)
                    {
                        case 1:
                            AddItems(__instance,
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_Book_2),
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_RecoverinD),
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                                PlayData.GetPassiveRandom()
                            );
                            break;

                        case 2:
                            AddItems(__instance,
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_RecoverinRed),
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_Book_0),
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_Book_1),
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                                PlayData.GetPassiveRandom()
                            );
                            break;


                        default:
                            AddItems(__instance,
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial), PlayData.GetPassiveRandom());
                            break;
                    }
                }
            }

            private static void AddItems(FieldStore store, params object[] items)
            {
                foreach (var item in items)
                {
                    if (item is ItemBase itemBase)
                        store.StoreItems.Add(itemBase);
                }
            }
        }
    }
}