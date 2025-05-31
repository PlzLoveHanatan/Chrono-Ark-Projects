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
using System.Diagnostics.Eventing.Reader;
using static UnityEngine.Experimental.UIElements.EventDispatcher;
using Mono.Cecil.Cil;
using System.Reflection;
using System.Reflection.Emit;
using EItem;
namespace Aqua
{
    public class Aqua_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("Aqua");

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
                Debug.Log("Aqua: Patch Catch: " + e.ToString());
            }
        }

        private static readonly Dictionary<string, string> AquaVoiceLinesEnglish = new Dictionary<string, string>
        {
            { "BattleStart_0", "I am the Divine Embodiment of the Aqua Axis faith and the Goddess who governs water!" },
            { "BattleStart_1", "Pamper me more! Worship me! I'm a Goddess, after all!" },
            { "BattleStart_2", "Who am I ? I can't tell you my name, but I'm a passing water Goddess!" },
            { "IdlingB_0", "Leave healing and support to me, the Archpriest!" },
            { "IdlingB_1", "Kazuma-san, I'm bored! Pick me up high!" },
            { "DeathA_0", "As a Goddess, I refuse to play such a ridiculous role!" },
            { "DeathA_1", "Sometimes I just want to quietly drink sparkling drinks..." },
            { "DeathA_2", "Uugh... It stinks... It stiiinks..." },
            { "DeathAlly_0", "Hurry up and defeat the Demon Lord, and save this world somehow!" },
            { "DeathAlly_1", "Leave it to me! I'll give you a God Smash!" },
            { "Kill_0", "Freshly caught prey, Aqua is here!" },
            { "Kill_1", "I have to work hard and someday escape this stable life!" },
            { "Cri_0", "Today, I was praised by the Master again!" },
            { "Chest_0", "Why does Estia get believed to be a Goddess? I'm a Goddess too, you know!" },
            { "Potion_0", "Tonight's a festival! Let's party till morning with delicious sweets and sparkling drinks!" },
            { "IdlingF_0", "Hey Megumi! Come here, come here! There's a strange fish over here!" },
            { "IdlingF_1", "I'm not interested in mere humans. Don't worry! I'm a real Goddess!" },
            { "IdlingF_2", "Kazuma! I'm tired! Piggyback me! Piggyback!" },
            { "Heal_0", "Hey, is dinner ready yet ? I'm starving here..." },
            { "Heal_1", "A Goddess is delicate — without a quiet room and a warm bed, she can't sleep!" },
            { "Heal_2", "We should be grateful for our current life; there's no going back to straw beds anymore!" },
            { "Curse", "When the soul commands, the true art is what you perform yourself!" },
            { "Pharos", "Sorry for purifying the hot spring water, but hey, I'm a Goddess—what can I do ?" },
            { "Other_0", "Heheh, this is my special Goddess-made cake! How does it look? Delicious, right?" },
            { "Other_1", "Beauty of nature~! After a quest, fizzy drinks really are the best!" },
            { "Master", "Since you came to this world, I want you to make lots of fun memories!" }
        };

        private static readonly Dictionary<string, string> AquaVoiceLinesKorean = new Dictionary<string, string>
        {
            { "BattleStart_0", "나는 아쿠아, 아쿠시즈교의 신앙의 대상이자, 물을 관장하는 여신이야!" },
            { "BattleStart_1", "더 떠받들어달라고! 나를 드높이라니까! 나 여신님인데요!!" },
            { "BattleStart_2", "내가 누구냐고? 이름을 말 못해주지만, 지나가는 물의 여신님이야!" },
            { "IdlingB_0", "회복도 지원도, 아크프리스트인 나에게 맡겨줘!" },
            { "IdlingB_1", "카즈마 씨, 심심해~. 비행기 놀이 해줘~." },
            { "DeathA_0", "여신인 내가 이런 웃기지도 않는 역할이라니, 인정 못해!" },
            { "DeathA_1", "가끔은, 조용히 슈와슈와를 마시고 싶을 때도 있어." },
            { "DeathA_2", "우우… 비려… 비려어…" },
            { "DeathAlly_0", "빨리 마왕을 쓰러뜨리고, 이 세상 좀 어떻게 해봐!" },
            { "DeathAlly_1", "나한테 맡겨줘! 갓-스매시를 보여줄테니까~!" },
            { "Kill_0", "신성한 물의 무희, 아쿠아야!" },
            { "Kill_1", "열심히 일해서, 언젠가 마구간살이에서 벗어나야지!" },
            { "Cri_0", "오늘도 사장님한테 칭찬받았어!" },
            { "Chest_0", "어째서 헤스티아는 신님이라고 믿어주는데?! 나도 여신인데!!" },
            { "Potion_0", "오늘밤은 축제야! 맛있는 과자랑 슈와슈와로, 아침까지 달려보자!" },
            { "IdlingF_0", "잠깐, 메구밍! 와봐, 와봐! 여기에 특이한 물고기가 있어!" },
            { "IdlingF_1", "\"단순한 인간한테는 흥미없어요\"? 괜찮아, 나는 진짜 여신이니까!" },
            { "IdlingF_2", "카즈마! 힘들어! 어부바해줘, 어부바!" },
            { "Heal_0", "저기여~ 밥 아직~? 배고픈데~" },
            { "Heal_1", "여신은 섬세하다니까! 조용한 방이랑 따뜻한 이불이 없으면 못 잔단 말야!" },
            { "Heal_2", "지금의 삶에 감사해야지. 이제 밀짚에서 자는 건 상상도 하기 싫으니까." },
            { "Curse", "영혼이 명령할 때, 진정한 예술은 오직 스스로가 만들어내는 거야!" },
            { "Pharos", "온천수를 정화시킨 건 사과할게, 그치만 여신이니까 어쩔 수 없잖아~." },
            { "Other_0", "흐흥, 이게 여신님 특제 케이크야. 맛있어보이지!" },
            { "Other_1", "화조풍월~! 역시 퀘스트가 끝난 후에는 슈와슈와가 최고야!" },
            { "Master", "기껏 이쪽 세계에 왔는 걸~. 재밌는 기억을 잔뜩 만들어줬으면 해!" }
        };

        private static readonly Dictionary<string, string> AquaVoiceLinesChinese = new Dictionary<string, string>
        {
            { "BattleStart_0", "我可是阿库西斯教团尊崇的神，水之女神阿库娅喔！" },
            { "BattleStart_1", "再多崇拜我、宠爱我吧！毕竟我可是女神啊！" },
            { "BattleStart_2", "我是谁？这我可不能说，不过我是个路过的水之女神！" },
            { "IdlingB_0", "治疗和支援，就交给我这位大祭司吧！" },
            { "IdlingB_1", "和真，我好无聊！举高高！" },
            { "DeathA_0", "我可是女神耶，这么荒唐的事情我才不干！" },
            { "DeathA_1", "有的时候我只想喝点气泡饮……" },
            { "DeathA_2", "呜呃……好臭……好臭喔……" },
            { "DeathAlly_0", "快点打倒魔王，拯救这个世界吧！" },
            { "DeathAlly_1", "交给我吧！我要给它降下神的制裁！" },
            { "Kill_0", "猎物捕获，阿库娅来啦！" },
            { "Kill_1", "努力再努力，有一天迟早要大富大贵！" },
            { "Cri_0", "今天又受到师父的表扬了！" },
            { "Chest_0", "为什么赫斯提亚就没被质疑过，我也是女神啊，你懂的！" },
            { "Potion_0", "今晚得好好庆祝！来个点心与气泡饮的派对吧！" },
            { "IdlingF_0", "嘿，惠惠！快来快来，这里有一条奇怪的鱼！" },
            { "IdlingF_1", "我对凡人没兴趣。不必担心，我可是实实在在的女神喔！" },
            { "IdlingF_2", "和真！我累了！背我，快点！" },
            { "Heal_0", "我饿了……晚餐……好了吗……？" },
            { "Heal_1", "女神也是很脆弱的－－我想要安静的房间跟温暖的床呀！" },
            { "Heal_2", "我们应当对这一刻有所感激……不用再睡草床啦！" },
            { "Curse", "你的灵魂可在呐喊啊，真正的艺术可是要由你自己表现的！" },
            { "Pharos", "抱歉打扰了你的兴致，但我可是女神耶－－我该做什么？" },
            { "Other_0", "嘿嘿嘿，这可是女神特制蛋糕！它看起来非常美味，对吧？" },
            { "Other_1", "自然真美好！做完任务后的气泡饮最棒了！" },
            { "Master", "自打你来到这个世界，我希望你能留下许多快乐的回忆呢！" }
        };

        private static readonly Dictionary<string, string> AquaVoiceLinesChineseTW = new Dictionary<string, string>
        {
            { "BattleStart_0", "我可是阿庫西斯教團尊崇的神，水之女神阿克婭喔！" },
            { "BattleStart_1", "再多崇拜我、寵愛我吧！畢竟我可是女神啊！" },
            { "BattleStart_2", "我是誰？這我可不能說，不過我是個路過的水之女神！" },
            { "IdlingB_0", "治療和支援，就交給我這位大祭司吧！" },
            { "IdlingB_1", "和真，我好無聊！舉高高！" },
            { "DeathA_0", "我可是女神耶，這麼荒唐的事情我才不幹！" },
            { "DeathA_1", "有的時候我只想喝點氣泡飲……" },
            { "DeathA_2", "嗚呃……好臭……好臭喔……" },
            { "DeathAlly_0", "快點打倒魔王，拯救這個世界吧！" },
            { "DeathAlly_1", "交給我吧！我要給它降下神的制裁！" },
            { "Kill_0", "獵物捕獲，阿克婭來啦！" },
            { "Kill_1", "努力再努力，有一天遲早要大富大貴！" },
            { "Cri_0", "今天又受到師父的表揚了！" },
            { "Chest_0", "為什麼赫斯提亞就沒被質疑過，我也是女神啊，你懂的！" },
            { "Potion_0", "今晚得好好慶祝！來個點心與氣泡飲的派對吧！" },
            { "IdlingF_0", "嘿，惠惠！快來快來，這裡有一條奇怪的魚！" },
            { "IdlingF_1", "我對凡人沒興趣。不必擔心，我可是實實在在的女神喔！" },
            { "IdlingF_2", "和真！我累了！背我，快點！" },
            { "Heal_0", "我餓了……晚餐……好了嗎……？" },
            { "Heal_1", "女神也是很脆弱的－－我想要安靜的房間跟溫暖的床呀！" },
            { "Heal_2", "我們應當對這一刻有所感激……不用再睡草床啦！" },
            { "Curse", "你的靈魂可在吶喊啊，真正的藝術可是要由你自己表現的！" },
            { "Pharos", "抱歉打擾了你的興致，但我可是女神耶－－我該做什麼？" },
            { "Other_0", "嘿嘿嘿，這可是女神特製蛋糕！它看起來非常美味，對吧？" },
            { "Other_1", "自然真美好！做完任務後的氣泡飲最棒了！" },
            { "Master", "自打你來到這個世界，我希望你能留下許多快樂的回憶呢！" }
        };



        [HarmonyPatch(typeof(PrintText))]
        [HarmonyPatch("TextInput")]
        public class VoiceOn
        {
            [HarmonyPrefix]
            public static bool Prefix(PrintText __instance, string inText)
            {
                if (Utils.AquaVoice)
                {
                    string language = LocalizationManager.CurrentLanguage;

                    switch (language)
                    {
                        case "Korean":

                            foreach (var kvp in AquaVoiceLinesKorean)
                            {
                                if (inText.Contains(kvp.Value))
                                {
                                    MasterAudio.StopBus("SE");
                                    MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                                }
                            }
                            break;

                        case "English":

                            foreach (var kvp in AquaVoiceLinesEnglish)
                            {
                                if (inText.Contains(kvp.Value))
                                {
                                    MasterAudio.StopBus("SE");
                                    MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                                }
                            }
                            break;

                        //case "Japanese":
                        //    break;

                        case "Chinese":

                            foreach (var kvp in AquaVoiceLinesChinese)
                            {
                                if (inText.Contains(kvp.Value))
                                {
                                    MasterAudio.StopBus("SE");
                                    MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                                }
                            }
                            break;

                        case "Chinese-TW":

                            foreach (var kvp in AquaVoiceLinesChineseTW)
                            {
                                if (inText.Contains(kvp.Value))
                                {
                                    MasterAudio.StopBus("SE");
                                    MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                                }
                            }
                            break;

                        default:
                            foreach (var kvp in AquaVoiceLinesEnglish)
                            {
                                if (inText.Contains(kvp.Value))
                                {
                                    MasterAudio.StopBus("SE");
                                    MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                                }
                            }
                            break;
                    }
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(CharEquipInven))]
        [HarmonyPatch(nameof(CharEquipInven.AddNewItem))]
        public class Curse_Equip_Patch
        {
            [HarmonyPrefix]
            public static void Prefix(int ItemNum, ItemBase Item)
            {
                if (AquaInPlay())
                {
                    if (Item is Item_Equip equip && equip.IsCurse)
                    {
                        equip.Curse = new EquipCurse();
                    }

                    List<string> blockedEnchantKeys = new List<string>
                    {
                        "En_Broken",
                        "En_uncomfortable",
                        "En_heavy"
                    };

                    if (Item is Item_Equip equip2 && equip2.Enchant != null && blockedEnchantKeys.Contains(equip2.Enchant.Key))
                    {
                        equip2.Enchant = new ItemEnchant();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(EquipCurse))]
        [HarmonyPatch(nameof(EquipCurse.NewCurse))]
        public static class EquipCursePatch
        {
            [HarmonyPrefix]
            public static bool NewCurse_Prefix(Item_Equip MainItem, string CurseKey, ref EquipCurse __result)
            {
                if (AquaInPlay())
                {
                    __result = new EquipCurse();
                    __result.MyItem = MainItem;
                    __result.Name = ""; //Cleansed by Aqua-sama☆
                    return false;
                }

                return true;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(EquipCurse.RandomCurse))]
        public static bool RandomCurse_Prefix(Item_Equip MainItem, ref EquipCurse __result)
        {
            if (AquaInPlay())
            {
                __result = new EquipCurse();
                __result.MyItem = MainItem;
                __result.Name = "";

                return false;
            }

            return true;
        }

        [HarmonyPatch(typeof(Misc))]
        public static class Misc_IsFemale_Patch
        {
            [HarmonyPatch("IsFemale")]
            [HarmonyPostfix]
            public static void IsFemale_Postfix(ref bool __result, string Key)
            {
                __result = true;
            }
        }

        private static bool AquaInPlay()
        {
            if (!Utils.CleanseAllCurses) return false;

            foreach (var character in PlayData.TSavedata.Party)
            {
                if (character.KeyData == ModItemKeys.Character_Aqua) return true;
            }

            return false;
        }
    }
}