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
namespace Mia
{
    public class Mia_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("Mia");

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
                Debug.Log("Mia: Patch Catch: " + e.ToString());
            }
        }

        [HarmonyPatch(typeof(BattleSystem))]
        [HarmonyPatch(nameof(BattleSystem.IlyaExtended))]
        public static class MiaDoubleSheathePlugin
        {
            [HarmonyPrefix]
            public static void MiaDoubleSheathe(SkillExtedned_IlyaP Ilyaex, BattleSystem __instance)
            {
                var buffKeys = new[]
                {
                ModItemKeys.Buff_B_Mia_BurstofFlavor_0,
                ModItemKeys.Buff_B_Mia_BurstofFlavor
                };

                foreach (var key in buffKeys)
                {
                    var target = BattleSystem.instance.AllyTeam.AliveChars
                        .Find(c => c.BuffReturn(key, false) != null);

                    if (target != null)
                    {
                        var buff = target.BuffReturn(key, false) as Buff;

                        if (buff != null)
                        {
                            Ilyaex.IlyaWasteFirst();
                            Ilyaex.IlyaWaste();

                            if (key == ModItemKeys.Buff_B_Mia_BurstofFlavor_0)
                            {
                                buff.SelfDestroy();
                            }
                            else
                            {
                                buff.SelfStackDestroy();
                            }

                            break;
                        }
                    }
                }
            }

            //private static readonly Dictionary<string, string> MiaVoiceLines = new Dictionary<string, string>
            //{
            //    { "MiaBattleStart_0", ModLocalization.MiaBattleStart_0 },
            //    { "MiaBattleStart_1", ModLocalization.MiaBattleStart_1 },
            //    { "MiaChest", ModLocalization.MiaChest },
            //    { "MiaCri", ModLocalization.MiaCri },
            //    { "MiaCurse", ModLocalization.MiaCurse },
            //    { "MiaHealed", ModLocalization.MiaHealed },
            //    { "MiaIdleB_0", ModLocalization.MiaIdleB_0 },
            //    { "MiaIdleB_1", ModLocalization.MiaIdleB_1 },
            //    { "MiaIdleF", ModLocalization.MiaIdleF },
            //    { "MiaKill", ModLocalization.MiaKill },
            //    { "MiaMaster", ModLocalization.MiaMaster },
            //    { "MiaOther_0", ModLocalization.MiaOther_0 },
            //    { "MiaOther_1", ModLocalization.MiaOther_1 },
            //    { "MiaOther_2", ModLocalization.MiaOther_2 },
            //    { "MiaPharos_0", ModLocalization.MiaPharos_0 },
            //    { "MiaPharos_1", ModLocalization.MiaPharos_1 },
            //    { "MiaPharos_2", ModLocalization.MiaPharos_2 },
            //    { "MiaPotion", ModLocalization.MiaPotion },
            //    { "MiaDD", ModLocalization.MiaDD },
            //    { "MiaDDAlly", ModLocalization.MiaDDAlly },
            //};

            private static readonly Dictionary<string, string> MiaVoiceLinesKorean = new Dictionary<string, string>
            {
                { "MiaBattleStart_0", "에이미, 미야, 그리고 스콸 경은 모두 악기를 잘 다뤄요! 미야는 춤추고 응원하는 담당이에요!" },
                { "MiaBattleStart_1", "고기는 최고지만, 에이미가 야채도 꼭 먹으라고 했어요." },
                { "MiaChest", "미야와 함께 보물 사냥을 떠나요! 낙타를 타고 출발~!" },
                { "MiaCri", "모든 음식을 맛있게 먹는 것이 예의예요!" },
                { "MiaCurse", "정리할 시간이에요! 케팔, 시작해요!" },
                { "MiaDD", "미야는 아직 안 졸려요. 오늘 밤은 아침까지 잠 못 자요~" },
                { "MiaDDAlly", "처음 보는 게임이 정말 많아요! 바깥 세상은 정말 재밌어요!" },
                { "MiaHealed", "맛있는 걸 먹는 것도 일종의 운동이에요!" },
                { "MiaIdleB_0", "미야는 랜서라구요!" },
                { "MiaIdleB_1", "미야는 진짜 강해요! 근데 힘 쓰면 배가 고파져요..." },
                { "MiaIdleF", "미야가 태어난 곳에 비하면, 악셀 시티는 신기한 걸로 가득해요!" },
                { "MiaKill", "순발력도 좋고, 몬스터도 쓰러뜨리고, 이 창은 정말 만능이에요!" },
                { "MiaMaster", "도시에는 맛있는 게 정말 많아요." },
                { "MiaOther_0", "바닷가에서 먹는 오징어 구이가 최고예요! 에이미도 먹어봤으면 좋겠어요!" },
                { "MiaOther_1", "하아~ 푸우~ 맛있는 게 너무 많아요! 크리스마스는 정말 멋진 날이에요!" },
                { "MiaOther_2", "에이미랑 할로윈 장식했어요! 어때요? 귀엽죠?" },
                { "MiaPharos_0", "슬립오버엔 역시 베개 싸움이죠! 미야의 스피드 베개 받아보세요!" },
                { "MiaPharos_1", "배가 부르면 졸려지죠... 달콤한 꿈 꾸세요!" },
                { "MiaPharos_2", "니쿠만이다~ 너무 행복해요! 전부 혼자 먹을 거예요!" },
                { "MiaPotion", "그 과일도 맛있어 보이지만... 미야는 고기를 더 좋아해요!" }
            };

            private static readonly Dictionary<string, string> MiaVoiceLinesEnglish = new Dictionary<string, string>
            {
                { "MiaBattleStart_0", "Amy, Miya, and Lord Squall are all amazing with instruments! Miya's in charge of dancing and cheering!" },
                { "MiaBattleStart_1", "Meat is great, but Amy said you've gotta eat your veggies too." },
                { "MiaChest", "Let's go treasure hunting with Miya! Mount the camel, and off we go!" },
                { "MiaCri", "It's good manners to enjoy any kind of food deliciously!" },
                { "MiaCurse", "Time to clean up! Let's do this, Keppal!" },
                { "MiaDD", "Miya's not sleepy yet. Tonight, no one's getting to sleep until morning..." },
                { "MiaDDAlly", "There are so many games I've never seen before! The outside world is so much fun!" },
                { "MiaHealed", "Eating delicious food is its own kind of exercise!" },
                { "MiaIdleB_0", "Miya is a lancer, you know!" },
                { "MiaIdleB_1", "Miya is really strong! But using all that strength sure makes me hungry..." },
                { "MiaIdleF", "Compared to where Mia was born, Axel city is full of wonders!" },
                { "MiaKill", "I can think on my feet, defeat monsters, and this spear is just perfect for everything!" },
                { "MiaMaster", "The city has all kinds of delicious food." },
                { "MiaOther_0", "Grilled squid by the sea is the best! I want Amy to try some too!" },
                { "MiaOther_1", "Haah! Phew! There's so much delicious food! Christmas really is an amazing day!" },
                { "MiaOther_2", "I decorated for Halloween with Amy! What do you think Cute, right ?" },
                { "MiaPharos_0", "Sleepovers mean pillow fights! Try to catch Miya's speedy pillow!" },
                { "MiaPharos_1", "When you're full, you get sleepy... Hope you have really sweet dreams!" },
                { "MiaPharos_2", "Nikuman, Nikuman, I'm so happy! I'm going to eat it all myself!" },
                { "MiaPotion", "That fruit looks delicious... but Miya prefers meat!" }
            };

            private static readonly Dictionary<string, string> MiaVoiceLinesJapanese = new Dictionary<string, string>
            {
                { "MiaBattleStart_0", "エイミー、ミヤ、そしてスコール様は楽器が得意！ミヤはダンスと応援担当だよ！" },
                { "MiaBattleStart_1", "お肉は最高だけど、エイミーが野菜も食べなきゃって言ってたよ。" },
                { "MiaChest", "ミヤと一緒に宝探しに行こう！ラクダに乗って、しゅっぱーつ！" },
                { "MiaCri", "どんな食べ物もおいしくいただくのがマナーだよ！" },
                { "MiaCurse", "お片づけの時間だよ！ケッパル、いくよー！" },
                { "MiaDD", "ミヤはまだ眠くないよ。今夜は朝まで寝かせないんだから～" },
                { "MiaDDAlly", "見たことないゲームがいっぱい！外の世界って楽しいね！" },
                { "MiaHealed", "おいしいものを食べるのも、ある意味運動だよね！" },
                { "MiaIdleB_0", "ミヤはランサーなんだよ！" },
                { "MiaIdleB_1", "ミヤはすっごく強いよ！でもいっぱい動くとお腹すいちゃう…" },
                { "MiaIdleF", "ミヤの故郷と比べて、アクセルシティは驚きがいっぱい！" },
                { "MiaKill", "とっさの判断もバッチリ、モンスターだって倒せるし、この槍はなんでもできるよ！" },
                { "MiaMaster", "街にはおいしいものがいっぱいあるんだよ。" },
                { "MiaOther_0", "海辺で食るイカ焼きは最高！エイミーにも食べさせたいな！" },
                { "MiaOther_1", "はぁ～ふぅ～おいしいものがいっぱい！クリスマスって本当に素敵！" },
                { "MiaOther_2", "エイミーと一緒にハロウィンの飾り付けしたの！どう？かわいいでしょ？" },
                { "MiaPharos_0", "お泊まり会といえば枕投げ！ミヤのスピード枕、受けてみなさいっ！" },
                { "MiaPharos_1", "お腹いっぱいになると、眠くなるよね…素敵な夢が見られますように！" },
                { "MiaPharos_2", "肉まんだ～うれし～い！全部ひとりで食べちゃうもんね！" },
                { "MiaPotion", "その果物、おいしそうだけど…ミヤはやっぱりお肉派！}" }
            };
            private static readonly Dictionary<string, string> MiaVoiceLinesChinese = new Dictionary<string, string>
            {
                { "MiaBattleStart_0", "艾米、米娅和斯奎尔大人都精通乐器！米娅负责跳舞和加油！" },
                { "MiaBattleStart_1", "肉很好吃，但艾米说也要吃蔬菜哦。" },
                { "MiaChest", "和米娅一起去寻宝吧！骑上骆驼，出发！" },
                { "MiaCri", "无论什么食物，好好享受才是礼貌哦！" },
                { "MiaCurse", "该打扫啦！来吧，凯帕尔！" },
                { "MiaDD", "米娅还不困呢。今晚大家都别想睡觉啦……" },
                { "MiaDDAlly", "有好多从没见过的游戏！外面的世界好有趣！" },
                { "MiaHealed", "吃美食也是一种锻炼哦！" },
                { "MiaIdleB_0", "你知道吗，米娅可是个枪兵！" },
                { "MiaIdleB_1", "米娅超级强！不过用力过头也会肚子饿……" },
                { "MiaIdleF", "和米娅的故乡比起来，艾克塞尔城真是神奇又热闹！" },
                { "MiaKill", "我反应很快，打怪兽也不在话下，这把长枪超好用的！" },
                { "MiaMaster", "城里有各种各样好吃的。" },
                { "MiaOther_0", "海边的烤鱿鱼最棒了！真想让艾米也尝一尝！" },
                { "MiaOther_1", "哈啊呼美食太多了！圣诞节真是太棒啦！" },
                { "MiaOther_2", "我和艾米一起装饰了万圣节！怎么样？可爱吧？" },
                { "MiaPharos_0", "过夜派对当然要打枕头战啦！来接住米娅的极速枕头！" },
                { "MiaPharos_1", "吃饱了就容易困……祝你做个甜甜的美梦！" },
                { "MiaPharos_2", "肉包、肉包，好开心啊！我要一个人全吃掉！" },
                { "MiaPotion", "那个水果看起来很好吃……但米娅更喜欢肉！" }
            };



            [HarmonyPatch(typeof(PrintText))]
            [HarmonyPatch("TextInput")]
            public class VoiceOn
            {
                [HarmonyPrefix]
                public static bool Prefix(PrintText __instance, string inText)
                {
                    if (Utils.MiaVoice)
                    {
                        string language = LocalizationManager.CurrentLanguage;

                        switch (language)
                        {
                            case "Korean":

                                foreach (var kvp in MiaVoiceLinesKorean)
                                {
                                    if (inText.Contains(kvp.Value))
                                    {
                                        MasterAudio.StopBus("SE");
                                        MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                                    }
                                }
                                break;

                            case "English":

                                foreach (var kvp in MiaVoiceLinesEnglish)
                                {
                                    if (inText.Contains(kvp.Value))
                                    {
                                        MasterAudio.StopBus("SE");
                                        MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                                    }
                                }
                                break;

                            case "Japanese":

                                foreach (var kvp in MiaVoiceLinesJapanese)
                                {
                                    if (inText.Contains(kvp.Value))
                                    {
                                        MasterAudio.StopBus("SE");
                                        MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                                    }
                                }
                                break;

                            case "Chinese":

                                foreach (var kvp in MiaVoiceLinesChinese)
                                {
                                    if (inText.Contains(kvp.Value))
                                    {
                                        MasterAudio.StopBus("SE");
                                        MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                                    }
                                }
                                break;

                            default:

                                foreach (var kvp in MiaVoiceLinesEnglish)
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
        }
    }
}