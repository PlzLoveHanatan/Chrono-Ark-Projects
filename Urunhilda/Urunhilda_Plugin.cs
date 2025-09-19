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
            return Utils.BonusesAlwaysOn || UrunhildaAlwaysInParty();
        }


        public static bool UrunhildaAlwaysInParty()
        {
            return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Urunhilda);
        }

        [HarmonyPatch(typeof(FieldSystem), "StageStart")]
        public static class StagePatch
        {
            [HarmonyPostfix]
            public static void StageStartPostfix()
            {
                if (UrunhildaAlwaysInParty())
                {
                    if (PlayData.TSavedata.StageNum >= 0)
                    {
                        if (!Utils.RewardTake)
                        {
                            Utils.UrunhildaFirstReward();
                            Utils.IncreaseArkPassiveNum();
                            Utils.RewardTake = true;
                        }

                        if (Utils.BeastkinEquip && !Utils.Equip)
                        {
                            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_Urunhilda_BeastkinBrush, 1));
                            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_Urunhilda_GoldenOathRing, 1));
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
                Utils.RewardTake = false;
                Utils.Equip = false;
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
            private static readonly Dictionary<string, string> UrunhildaVoiceLinesKR = new Dictionary<string, string>
            {
                { "Urunhilda_BS_0", "날 보러 온 거야? 외로웠어?" },
                { "Urunhilda_BS_1", "좋은 아침. 오늘은 무슨 계획이 있어?" },
                { "Urunhilda_BS_2", "이 시간에 무슨 일로 온 거야?" },
                { "Urunhilda_Chest_0", "내 광기의 결실을 보고 싶어? 정말, 넌 변태야." },
                { "Urunhilda_Chest_1", "뭐? 내가 너 위해 일부러 이렇게 하는데, 그게 네 대답이야?" },
                { "Urunhilda_Cri_0", "네가 지금 누구한테 말하고 있다고 생각해?" },
                { "Urunhilda_Cri_1", "내가 이렇게까지 만져도 된다고 한 적 없잖아!" },
                { "Urunhilda_Curse_0", "하-하아, 잠깐만!" },
                { "Urunhilda_Curse_1", "칫, 알았어. 이 정도는 봐줄 테니까, 빨리 끝내, 알았지?" },
                { "Urunhilda_Healed_0", "으으... 기분 좋아... 나... 벌써..." },
                { "Urunhilda_Healed_1", "읏... 하-하아, 좀 더 부드럽게 만져줄래!" },
                { "Urunhilda_BI_0", "네가 날 만질 때마다 점점 기분이 좋아져." },
                { "Urunhilda_BI_1", "더 만져도 돼." },
                { "Urunhilda_BI_2", "넌 정말... 짓궂어..." },
                { "Urunhilda_BI_3", "아, 아-안 돼, 그렇게 내 젖꼭지를 문지르지 마! 하, 하, 하, 하..." },
                { "Urunhilda_FI_0", "네가 가장 좋아하는 파트너가 될 테니까, 여러 가지 가르쳐줘." },
                { "Urunhilda_FI_1", "오늘은 나한테 뭘 해줄 거야?" },
                { "Urunhilda_FI_2", "칫, 좋아. 그냥 하자, 그렇지?" },
                { "Urunhilda_Kill_0", "나-나-나-나-나... 안 돼... 나, 나 가버려!" },
                { "Urunhilda_Kill_1", "왜... 왜 나를 이렇게 부끄럽게 만들어...?" },
                { "Urunhilda_Master", "너무 시끄러워, 그만 좀 물어봐, 바보야, 이제 좀 꺼져!" },
                { "Urunhilda_Pharos_0", "또 시험이야, 뭐야. 이번엔 나한테 뭘 시키려는 거야?" },
                { "Urunhilda_Pharos_1", "네가 말 안 해도 알아! 날 그냥 내버려둬, 이 변태 괴짜야!" },
                { "Urunhilda_Pharos_2", "으... 나... 정말... 이걸 해야 해...?" },
                { "Urunhilda_Potion", "더..." },
                { "Urunhilda_DeathDoor_0", "너... 나한테 뭘 할 생각이야..." },
                { "Urunhilda_DeathDoor_1", "왜 날 만지는 거야?" },
                { "Urunhilda_DeathDoorAlly", "네 상황을 정말 싫어하는 건 아닐지도 몰라." },
            };

            private static readonly Dictionary<string, string> UrunhildaVoiceLinesJP = new Dictionary<string, string>
            {
                { "Urunhilda_BS_0", "私に会いに来たの？寂しかったの？" },
                { "Urunhilda_BS_1", "おはよう。さて、今日は何をするつもり？" },
                { "Urunhilda_BS_2", "こんな時間にどうして来たの？" },
                { "Urunhilda_Chest_0", "私の狂気の果実を見たいの？まったく、あなたって変態ね。" },
                { "Urunhilda_Chest_1", "え？わざわざあなたのためにしてあげてるのに、その言い方？" },
                { "Urunhilda_Cri_0", "あなたは今誰に話していると思ってるの？" },
                { "Urunhilda_Cri_1", "こんなに触っていいなんて言ってないでしょ！" },
                { "Urunhilda_Curse_0", "ちょ、ちょっと待って！" },
                { "Urunhilda_Curse_1", "ちっ、わかったわ。このくらいは許してあげるから、早く終わらせてよね。" },
                { "Urunhilda_Healed_0", "あぁ…気持ちいい…私…もう…" },
                { "Urunhilda_Healed_1", "んっ…ちょ、ちょっと…もっと優しく触ってよ！" },
                { "Urunhilda_BI_0", "あなたに触れられると、少しずつ気持ちよくなる。" },
                { "Urunhilda_BI_1", "もっと触っていいよ。" },
                { "Urunhilda_BI_2", "あなたって本当に…いじわる…" },
                { "Urunhilda_BI_3", "あ、だ、だめ、そんな風に乳首をこすらないで！はぁ、はぁ、はぁ、はぁ…" },
                { "Urunhilda_FI_0", "あなたのお気に入りのパートナーになってあげるから、いろいろ教えてね。" },
                { "Urunhilda_FI_1", "今日は私に何をしてくれるの？" },
                { "Urunhilda_FI_2", "ちっ、いいわ。やればいいんでしょ？" },
                { "Urunhilda_Kill_0", "わ、わ、わ、わ、わ…だめ…私、イっちゃう！" },
                { "Urunhilda_Kill_1", "ど、どうしてこんなに恥ずかしいことをさせるの…？" },
                { "Urunhilda_Master", "うるさいな、もう質問やめろよ、バカ、さっさと消えて！" },
                { "Urunhilda_Pharos_0", "また試練かよ、ったく。今度は私に何をさせるつもりなの？" },
                { "Urunhilda_Pharos_1", "言われなくても分かってる！放っておいてよ、この変態！" },
                { "Urunhilda_Pharos_2", "んっ…私…本当に…これをやらなきゃいけないの…？" },
                { "Urunhilda_Potion", "もっと…" },
                { "Urunhilda_DeathDoor_0", "あなたは私に何をするつもりなの…" },
                { "Urunhilda_DeathDoor_1", "なんで私に触ってるの？" },
                { "Urunhilda_DeathDoorAlly", "君の状況を本当に嫌っているわけではないかもしれない。" },
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
                        case "Korean":
                            selectedDict = UrunhildaVoiceLinesKR;
                            break;
                        case "English":
                            selectedDict = UrunhildaVoiceLinesEN;
                            break;
                        case "Japanese":
                            selectedDict = UrunhildaVoiceLinesJP;
                            break;
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

            [HarmonyPatch(typeof(CharStatV4), "UIEnableSetting")]
            public static class ManaArraysEnsurePatch
            {
                // максимальное количество иконок для отображения
                public const int MaxAllowedIcons = 10;

                [HarmonyPrefix]
                public static void PrefixEnsureArrays()
                {
                    if (UrunhildaInParty())
                    { 
                        // вычисляем сколько реально нужно
                        int required = Math.Max(PlayData.AP, PlayData.TSavedata.SoulUpgrade.AP + 1);
                        required = Math.Max(required, PlayData.TSavedata.SoulUpgrade.SkillDraw + 1);

                        int ensureLength = Math.Min(required, MaxAllowedIcons);

                        EnsureNumericCollectionLength("MPUpgradeNum", ensureLength);
                        EnsureNumericCollectionLength("DrawUpgradeNum", ensureLength);
                    }
                }

                private static void EnsureNumericCollectionLength(string fieldName, int requiredLength)
                {
                    if (UrunhildaInParty())
                    {
                        FieldInfo fi = typeof(PlayData).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
                        if (fi == null) return;

                        object val = fi.GetValue(null);
                        if (val == null) return;

                        if (val is int[] arr)
                        {
                            if (arr.Length < requiredLength)
                            {
                                int oldLen = arr.Length;
                                int[] newArr = new int[requiredLength];
                                Array.Copy(arr, newArr, oldLen);
                                int fillValue = (oldLen > 0) ? arr[oldLen - 1] : 1;
                                for (int i = oldLen; i < requiredLength; i++) newArr[i] = fillValue;
                                fi.SetValue(null, newArr);
                            }
                        }
                        else if (val is List<int> list)
                        {
                            if (list.Count < requiredLength)
                            {
                                int oldCount = list.Count;
                                int fillValue = (oldCount > 0) ? list[oldCount - 1] : 1;
                                while (list.Count < requiredLength) list.Add(fillValue);
                            }
                        }
                    }
                }
            }

            [HarmonyPatch(typeof(CharStatV4), "UIEnableSetting")]
            public static class ManaUIPatch
            {
                [HarmonyPostfix]    
                public static void ManaPatchPostfix(CharStatV4 __instance)
                {
                    if (UrunhildaInParty())
                    { 
                        if (__instance == null) return;
                        Transform manaAlign = __instance.ManaAlign?.transform;
                        if (manaAlign == null) return;

                        int ap = Math.Min(PlayData.AP, ManaArraysEnsurePatch.MaxAllowedIcons);

                        // Создаём недостающие иконки
                        while (manaAlign.childCount < ap)
                        {
                            GameObject prototype = (manaAlign.childCount > 0) ? manaAlign.GetChild(0).gameObject : null;
                            if (prototype == null) break;

                            GameObject newIcon = GameObject.Instantiate(prototype, manaAlign);
                            Image img = newIcon.GetComponent<Image>();
                            if (img != null) img.sprite = __instance.ManaSprite;
                            newIcon.SetActive(true);
                        }

                        // Включаем/выключаем иконки по AP
                        for (int i = 0; i < manaAlign.childCount; i++)
                        {
                            manaAlign.GetChild(i).gameObject.SetActive(i < ap);
                        }
                    }
                }
            }
        }
    }
}