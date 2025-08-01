﻿using UnityEngine;
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
using System.Reflection;
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
            return Utils.BonusesAlwaysOn || UrunhildaAlwysInParty();
        }


        public static bool UrunhildaAlwysInParty()
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
                if (UrunhildaAlwysInParty())
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
                int events = AdditionalEvents();

                if (PlayData.TSavedata == null || PlayData.TSavedata.Party == null || events == 0)
                    return;

                if (UrunhildaInParty())
                {
                    __result += events;
                }
            }
        }

        public static int AdditionalEvents()
        {
            return (int)(Utils.MoreEvents);
        }

        [HarmonyPatch(typeof(FieldEventSelect), "Reroll")]
        public static class FieldEventSelect_Reroll_Patch
        {
            public static bool Prefix(FieldEventSelect __instance)
            {
                try
                {
                    if (UrunhildaInParty())
                    {
                        int eventNum = 2;

                        if (AdditionalEvents() >= 1)
                        {
                            eventNum = 3;

                            if (Compas())
                            {
                                eventNum = 4;
                            }
                        }

                        var variable = FieldEventSelect.GetEventList(true, new List<string>());
                        List<string> customList = variable.Random(RandomClassKey.Event, eventNum);

                        var buttonsField = typeof(FieldEventSelect).GetField("Buttons", BindingFlags.NonPublic | BindingFlags.Static);
                        if (buttonsField == null)
                        {
                            Debug.LogError("Не удалось найти поле Buttons в FieldEventSelect");
                            return false;
                        }

                        var buttons = (List<FieldEventSelectButton>)buttonsField.GetValue(null);

                        foreach (var oldBtn in buttons)
                        {
                            UnityEngine.Object.Destroy(oldBtn.gameObject);
                        }
                        buttons.Clear();

                        foreach (var key in customList)
                        {
                            var btn = UnityEngine.Object.Instantiate(
                                __instance.FieldEventSelectButtonPrefab,
                                __instance.Align
                            ).GetComponent<FieldEventSelectButton>();

                            btn.Main = __instance;
                            btn.Init(key, __instance.EVObj, __instance.MainObj);
                            buttons.Add(btn);
                        }

                        buttonsField.SetValue(null, buttons);

                        PlayData.TSavedata.ReRollCount--;
                        __instance.RemainText.text = string.Format(
                            ScriptLocalization.UI_SmallUI_CampAnvil.RemainNum,
                            PlayData.TSavedata.ReRollCount.ToString()
                        );

                        if (!PlayData.TSavedata.CanUseReRoll)
                            __instance.RerollBtn.SetActive(false);

                        return false;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"Additional Reroll Choices" + e.ToString());
                    return true;
                }

                return true;
            }
        }



        [HarmonyPatch(typeof(RandomEventObject), "Event")]
        public static class RandomEventObject_Patch
        {
            public static bool Prefix(RandomEventObject __instance)
            {
                try
                {
                    if (UrunhildaInParty() && __instance.EventList == null)
                    {
                        __instance.EventList = new List<string>();
                    }

                    __instance.EventList = FieldEventSelect.GetEventList(false, null);

                    int eventNum = 2;

                    if (AdditionalEvents() >= 1)
                    {
                        eventNum = 3;

                        if (Compas())
                        {
                            eventNum = 4;
                        }
                    }

                    eventNum = Mathf.Clamp(eventNum, 1, __instance.EventList.Count);

                    FieldEventSelect.FieldEventSelectOpen(
                        __instance.EventList.Random(RandomClassKey.Event, eventNum),
                        __instance.MyEventObj,
                        __instance.gameObject,
                        false
                    );

                    __instance.MyEventObj.Useless();
                    __instance.MyEventObj.Tile.Info.Type.Eventend = false;

                    return false;
                }

                catch (Exception e)
                {
                    Debug.Log($"Additional Event Choices" + e.ToString());
                    return true;
                }
            }
        }
        public static bool Compas()
        {
            return PlayData.TSavedata.Passive_Itembase.Exists(
                item => item != null && item.itemkey == GDEItemKeys.Item_Passive_Sign
            );
        }

        public static int Stage => PlayData.TSavedata.StageNum;

        [HarmonyPatch(typeof(FieldStore))]
        public static class NewShopItems
        {
            [HarmonyPatch("Init")]
            [HarmonyPostfix]
            public static void NewItems(FieldStore __instance)
            {
                if (UrunhildaInParty() && UrunhildaNewShops())
                {
                    switch (Stage)
                    {
                        case 1:
                            AddEquipment(__instance, Stage, 2);
                            AddItems(__instance,
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_Book_2),
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_RecoverinD),
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenApple),
                                PlayData.GetPassiveRandom()
                            );
                            break;

                        case 2:
                            AddEquipment(__instance, Stage, 2);
                            AddItems(__instance,
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_RecoverinRed),
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_Book_0),
                                ItemBase.GetItem(ModItemKeys.Item_Consume_C_Urunhilda_Book_1),
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenApple),
                                PlayData.GetPassiveRandom()
                            );
                            break;
                        case 3:
                            AddEquipment(__instance, Stage, 2);
                            AddItems(__instance,
                                ItemBase.GetItem(GDEItemKeys.Item_Misc_BlackironMoru),
                                ItemBase.GetItem(GDEItemKeys.Item_Misc_ArtifactPlusInven),
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenApple),
                                PlayData.GetPassiveRandom()
                            );
                            break;

                        default:
                            AddEquipment(__instance, Stage, 2);
                            AddItems(__instance,
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                                ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenApple),
                                PlayData.GetPassiveRandom()
                            );
                            break;
                    }
                }
            }

            [HarmonyPatch(typeof(SpecialStore))]
            public static class NewSpecialShopItems
            {
                [HarmonyPatch("Start")]
                [HarmonyPostfix]
                public static void NewItems(SpecialStore __instance)
                {
                    if (UrunhildaInParty() && UrunhildaNewShops())
                    {
                        AddEquipment(__instance, Stage, 2);

                        __instance.StoreItems.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial));
                        __instance.StoreItems.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenApple));
                        PlayData.GetPassiveRandom();
                    }
                }
            }


            private static void AddItems(FieldStore store, params object[] items)
            {
                foreach (var item in items)
                {
                    if (item is ItemBase itemBase)
                    {
                        store.StoreItems.Add(itemBase);
                    }
                    else
                    {
                        Debug.LogWarning($"[UrunhildaShop] Skipped invalid item: {item}");
                    }
                }
            }

            private static void AddEquipment(FieldStore store, int rarity, int count)
            {
                rarity--;
                for (int i = 0; i < count; i++)
                {
                    if (rarity > 4)
                    {
                        rarity = 4;
                    }
                    string equipKey = PlayData.GetEquipRandom(rarity, false, new List<string>());
                    ItemBase equipItem = ItemBase.GetItem(equipKey);
                    if (equipItem != null)
                    {
                        store.StoreItems.Add(equipItem);
                    }
                }
            }

            private static void AddEquipment(SpecialStore store, int rarity, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    if (rarity > 4)
                    {
                        rarity = 4;
                    }
                    string equipKey = PlayData.GetEquipRandom(rarity, false, new List<string>());
                    ItemBase equipItem = ItemBase.GetItem(equipKey);
                    if (equipItem != null)
                    {
                        store.StoreItems.Add(equipItem);
                    }
                }
            }

            public static bool UrunhildaNewShops()
            {
                try
                {
                    return Utils.MoreItemsInShop;
                }
                catch (Exception e)
                {
                    Debug.Log($"Items" + e.ToString());
                    return false;
                }
            }

            private static readonly Dictionary<string, string> UrunhildaVoiceLinesEN = new Dictionary<string, string>
        {
            { "Urunhilda_BS_0", "Did you come to see me? Feeling lonely, were you?"},
            { "Urunhilda_BS_1", "Good morning. So, what's your plan for today?"},
            { "Urunhilda_BS_2", "What brings you here at this hour?"},
            { "Urunhilda_Chest_0", "Do you want to see the fruits of my madness? Geez, you're such a perv."},
            { "Urunhilda_Chest_1", "Huh? I'm going out of my way to do this for you, and that's how you talk to me?"},
            { "Urunhilda_Cri_0", "Who exactly do you think you're talking to?"},
            { "Urunhilda_Cri_1", "I never said you could touch me this much, you know!"},
            { "Urunhilda_Curse_0", "H-Hey, wait a second!"},
            { "Urunhilda_Curse_1", "Ugh, fine. I'll let you get away with this much, so just finish it quickly, okay?"},
            { "Urunhilda_Healed_0", "Ugh... it feels good... I... already..."},
            { "Urunhilda_Healed_1", "Nngh... H-Hey, touch me more gently, will you!"},
            { "Urunhilda_BI_0", "When you touch me, I gradually start to feel good."},
            { "Urunhilda_BI_1", "You can touch me more."},
            { "Urunhilda_BI_2", "You're so... naughty..."},
            { "Urunhilda_BI_3", "Ah, n-no, don't rub my nipples like that! Hah, hah, hah, hah..."},
            { "Urunhilda_FI_0", "I'll become your favorite partner, so teach me various things."},
            { "Urunhilda_FI_1", "What will you do for me today?"},
            { "Urunhilda_FI_2", "Tch, fine. I guess we'll just do it, won't we?"},
            { "Urunhilda_Kill_0", "I-I-I-I-I-I-I... I can't... I-I'm c-coming!"},
            { "Urunhilda_Kill_1", "W-why are you making me do something so embarrassing...?"},
            { "Urunhilda_Master", "You're so noisy, just stop asking, idiot, go away already!"},
            { "Urunhilda_Pharos_0", "Another trial, huh? Geez. So what are you trying to make me do this time?"},
            { "Urunhilda_Pharos_1", "I don't need you to tell me! Leave me alone, you perverted freak!"},
            { "Urunhilda_Pharos_2", "Nngh... Do I... really... have to do this...?"},
            { "Urunhilda_Potion", "More..."},
            { "Urunhilda_DeathDoor_0", "What are you planning to do to me..."},
            { "Urunhilda_DeathDoor_1", "Why are you touching me?"},
            { "Urunhilda_DeathDoorAlly", "I might not really dislike your situation."},
        };

            private static readonly Dictionary<string, string> UrunhildaVoiceLinesCN = new Dictionary<string, string>
        {
            { "Urunhilda_BS_0", "你是来看我的吗？感到寂寞了吗？"},
            { "Urunhilda_BS_1", "早上好，今天有什么计划？"},
            { "Urunhilda_BS_2", "来找我做什么呢？"},
            { "Urunhilda_Chest_0", "你想看我疯狂的成果吗？天哪，真是个变态。"},
            { "Urunhilda_Chest_1", "哈？我特地为你做这个，你就这样跟我说话？"},
            { "Urunhilda_Cri_0", "你以为你在跟谁说话？"},
            { "Urunhilda_Cri_1", "我可没说过你可以再碰我！"},
            { "Urunhilda_Curse_0", "等-等等！"},
            { "Urunhilda_Curse_1", "好吧。我会让你做到这样的，所以快点结束，好吗？"},
            { "Urunhilda_Healed_0", "呃...感觉好舒服...我...已经..."},
            { "Urunhilda_Healed_1", "唔...嘿-嘿，再温柔地摸摸我，好吗。"},
            { "Urunhilda_BI_0", "每当你触碰我，我都会更加愉悦。"},
            { "Urunhilda_BI_1", "再多摸摸我......"},
            { "Urunhilda_BI_2", "你真是...太调皮了..."},
            { "Urunhilda_BI_3", "啊，不-不要，不要那樣揉我的乳頭！哈，哈，哈，哈...."},
            { "Urunhilda_FI_0", "我会成为你最喜欢的搭档，所以教我各种事情吧。"},
            { "Urunhilda_FI_1", "今天你会为我做什么？"},
            { "Urunhilda_FI_2", "啧，好吧。我想我们直接做吧！"},
            { "Urunhilda_Kill_0", "我-我-我...我不能...我-我去了！"},
            { "Urunhilda_Kill_1", "为-为什么你要让我做这么尴尬的事情...？"},
            { "Urunhilda_Master", "你好吵，别问了，白痴，快滚开！"},
            { "Urunhilda_Pharos_0", "又是试炼，嗯...那么这次你想让我做什么？"},
            { "Urunhilda_Pharos_1", "我不需要你告诉我！离我远点，你这个变态！"},
            { "Urunhilda_Pharos_2", "唔...我真的...必须做这个...？"},
            { "Urunhilda_Potion", "我还要更多..."},
            { "Urunhilda_DeathDoor_0", "你打算对我做什么..."},
            { "Urunhilda_DeathDoor_1", "为什么摸我？"},
            { "Urunhilda_DeathDoorAlly", "我可能并不是真的讨厌你。"},
        };

            [HarmonyPatch(typeof(PrintText))]
            [HarmonyPatch(nameof(PrintText.TextInput))]
            public class VoiceOn
            {
                [HarmonyPrefix]
                public static bool Prefix(PrintText __instance, string inText)
                {
                    if (!Utils.UrunhildaVoice)
                    {
                        return true;
                    }

                    string language = LocalizationManager.CurrentLanguage;
                    Dictionary<string, string> selectedDict;

                    switch (language)
                    {
                        //case "Korean":
                        //    selectedDict = UrunhildaVoiceLinesKR;
                        //    break;
                        case "English":
                            selectedDict = UrunhildaVoiceLinesEN;
                            break;
                        //case "Japanese":
                        //    selectedDict = UrunhildaVoiceLinesJP;
                        //    break;
                        case "Chinese":
                            selectedDict = UrunhildaVoiceLinesCN;
                            break;
                        default:
                            selectedDict = UrunhildaVoiceLinesEN;
                            break;
                    }

                    foreach (var kvp in selectedDict)
                    {
                        if (inText.Contains(kvp.Value))
                        {
                            MasterAudio.StopBus("SE");
                            var result = MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);

                            if (result.ActingVariation == null)
                                Debug.LogWarning($"Sound '{kvp.Key}' failed to play.");
                        }
                    }

                    return true;
                }
            }
        }
    }
}