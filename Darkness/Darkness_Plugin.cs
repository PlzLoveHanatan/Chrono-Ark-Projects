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
using System.Reflection.Emit;
using Dialogical;
using static CharacterDocument;
using System.Reflection;
namespace Darkness
{
    public class Darkness_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("Darkness");

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
                Debug.Log("Darkness: Patch Catch: " + e.ToString());
            }
        }
        public static bool DarknessInParty()
        {
            foreach (var character in PlayData.TSavedata.Party)
            {
                if (character.KeyData == ModItemKeys.Character_Darkness)
                {
                    return true;
                }
            }
            return false;
        }

        [HarmonyPatch(typeof(GDEStageData), "get_EnemyNum")]
        public static class DarknessEnemyPatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref int __result)
            {
                int enemies = (int)(Utils.DarknessMoreEnemies);

                if (PlayData.TSavedata == null || PlayData.TSavedata.Party == null || enemies == 0)
                    return;

                if (DarknessInParty())
                {
                    __result += enemies;
                }
            }
        }

        private static readonly Dictionary<string, string> DarknessVoiceLinesKR = new Dictionary<string, string>
        {
            { "DarknessAttackLands_0", "내 공격이 적중하고 있어! 진짜로 맞고 있다고!"},
            { "DarknessAttackLands_1", "아이즈의 검술 훈련은 정말 대단해! 적중의 쾌감에 중독되기 시작했어!"},
            { "DarknessAttackMisses", "힘과 체력에는 자신이 있는데, 너무 서툴러서 공격이 전혀 맞질 않아..."},
            { "DarknessBattleStart_0", "성전사로서 나는 끝까지 나의 의무를 다해야 해."},
            { "DarknessBattleStart_1", "머뭇거리지 마! 필요하다면 적과 함께 나도 공격해!"},
            { "DarknessBattleStart_2", "대역이라도 망설이지 말고 마음껏 명령해!"},
            { "DarknessChest", "갑옷 없이 이런 강한 일격을 맞는다면... 뭐, 그래도 모험은 계속해야지!"},
            { "DarknessCrit", "그, 그게 부대 이름이라도 상관없으니까… 라라티나라고 부르지 마!"},
            { "DarknessCurse", "성전사는 추위에 굴복하지 않아. 오히려 나는 그것을 반긴다!"},
            { "DarknessHealed", "자, 잠깐만! 그거 당기지 마! 지금 당장 멈춰!"},
            { "DarknessIdleBattle_0", "타워 실드라... 믿음직하긴 한데, 왜 이렇게 짜증나는 거지!"},
            { "DarknessIdleBattle_1", "파티의 방패가 되어서 마왕군에게 잡히다니… 으윽, 상상만 해도!"},
            { "DarknessIdleBattle_2", "관중들이 전부 날 보고 있어… 윽!"},
            { "DarknessIdleField_0", "나, 나, 나, 나도 여자라고요! 딱딱하다거나 괴력녀라고 부르면... 상처받는다구요!"},
            { "DarknessIdleField_1", "헤헤헤헤헤... 뭘 하냐고요? 뻔하잖아요. 매일 하는 뜨개질 루틴이죠."},
            { "DarknessKill", "상대가 누구든, 내 임무는 변하지 않아. 덤벼라—나는 벽이다!"},
            { "DarknessMaster", "이 엄청난 조임이라니! 역시 크라켄, 기대를 뛰어넘는군!"},
            { "DarknessPharos_0", "지금은 경장비를 입었지만, 나는 여전히 십자군이다. 자, 나에게 네 최고의 일격을 날려라!"},
            { "DarknessPharos_1", "나는 마지막 순간까지 저항할 것이다, 내 길을 막지 마라!"},
            { "DarknessPharos_2", "옛날부터 여성 기사들이 마왕에게 치욕을 당하는 것이 운명이었다."},
            { "DarknessPotion", "조용히 마시는 이런 술도… 가끔은 나쁘지 않아."},
            { "DarknessDeathDoor", "이 몸이 부서진다 해도, 나는 절대 악에 굴복하지 않을 것이다!"},
            { "DarknessDeathDoorAlly", "고통이 무서워? 나는 아니야 — 고통은 내가 가장 좋아하는 것 중 하나라고!"},
        };

        private static readonly Dictionary<string, string> DarknessVoiceLinesEN = new Dictionary<string, string>
        {
            { "DarknessAttackLands_0", "My attacks are landing! They're actually landing!"},
            { "DarknessAttackLands_1", "Aiz's sword training is amazing! I'm starting to get addicted to the thrill of landing hits!"},
            { "DarknessAttackMisses", "I'm confident in my strength and endurance, but I'm so clumsy my attacks never land..."},
            { "DarknessBattleStart_0", "As a crusader, I must fulfill my duty to the very end."},
            { "DarknessBattleStart_1", "Don't hold back! Attack me along with the enemy if you must!"},
            { "DarknessBattleStart_2", "Feel free to boss me around as a stand-in without holding back!"},
            { "DarknessChest", "If I take a heavy blow like this without armor... Well! Let's go on a quest anyway!"},
            { "DarknessCrit", "I-I don't care if it's the unit name—don't call me Lalatina!"},
            { "DarknessCurse", "A crusader never yields to the cold. In fact, I welcome it!"},
            { "DarknessHealed", "H-Hey, don't pull that! S-Stop it right now!"},
            { "DarknessIdleBattle_0", "A tower shield, huh... It's dependable, sure... but why is it so frustrating!"},
            { "DarknessIdleBattle_1", "Being the party's shield and getting caught by the Demon Lord's army... Ngh, just imagining it!"},
            { "DarknessIdleBattle_2", "So many eyes watching me from the audience... Nghh!"},
            { "DarknessIdleField_0", "E-E-E-Even I'm a woman! Calling me stiff or brute-strong... that hurts, you know!"},
            { "DarknessIdleField_1", "Hehehehehehe... Wondering what I'm doing Isn't it obvious My daily knitting routine."},
            { "DarknessKill", "No matter the opponent, my mission stays the same. Come at me—I am the wall!"},
            { "DarknessMaster", "What incredible tightness! As expected of a Kraken—this exceeds all expectations!"},
            { "DarknessPharos_0", "I may be lightly armored now, but I'm still a crusader. Go on—hit me with your strongest blow!"},
            { "DarknessPharos_1", "I'll resist until the very last moment, so don't get in my way!"},
            { "DarknessPharos_2", "Since ancient times, it's always been the fate of female knights to suffer indecencies at the Demon Lord's hands."},
            { "DarknessPotion", "Quiet drinks like this... not bad once in a while."},
            { "DarknessDeathDoor", "Even if this body shatters, I will never bow to evil!"},
            { "DarknessDeathDoorAlly", "Afraid of pain? Me? No — pain's one of my favorite things!"},
        };

        private static readonly Dictionary<string, string> DarknessVoiceLinesJP = new Dictionary<string, string>
        {
            { "DarknessAttackLands_0", "私の攻撃が当たってる！本当に当たってる！" },
            { "DarknessAttackLands_1", "アイズの剣の訓練はすごいよ！攻撃が当たるスリルにハマりそう！" },
            { "DarknessAttackMisses", "体力と持久力には自信があるけど、不器用すぎて攻撃が全然当たらない…"},
            { "DarknessBattleStart_0", "聖騎士として、最後まで使命を果たさねばならない。"},
            { "DarknessBattleStart_1", "ためらうな！必要なら敵と一緒に俺を攻撃しろ！"},
            { "DarknessBattleStart_2", "パーティーの足を引っ張るようなことがあれば強めに罵ってくれ!"},
            { "DarknessChest", "鎧がないこの状態で、もし重い一撃をくらったら…よし!クエストに行こう!"},
            { "DarknessCrit", "い、いくらユニット名でもラルティーナと呼ばないでくれ!"},
            { "DarknessCurse", "クルセイダーは寒さになんて屈しない。むしろ歓迎するわ！"},
            { "DarknessHealed", "ちょ、引っ張るな!や、やめ、やめろー!"},
            { "DarknessIdleBattle_0", "大盾か…確かに頼もしいが…なんだこのもどかしさは…"},
            { "DarknessIdleBattle_1", "皆の盾となり魔王軍に捕まった私は、くっ、想像しただけで!"},
            { "DarknessIdleBattle_2", "大勢の観客の視線が私に注がれて…くぅ!"},
            { "DarknessIdleField_0", "わ、わ、わ、わたしだって女だ!堅いだの、怪力だの言われたら、傷つくのだぞ!"},
            { "DarknessIdleField_1", "ふっふっふっふっふっふっふっふっ…何をしているかって?見ればわかるだろう…日課の編み物だ…"},
            { "DarknessKill", "誰が相手でも私の使命に変わりはない さあ来い私が壁だ!"},
            { "DarknessMaster", "なんという締め付け! さすがはクラーケン! 期待以上だ!"},
            { "DarknessPharos_0", "今は軽装だが私は来るせいだ さあ遠慮なく重い一撃よ"},
            { "DarknessPharos_1", "ギリギリギリまで抵抗してみるから邪魔はしないでくれ!"},
            { "DarknessPharos_2", "むかしから魔王にエロい目にあわされるのは、女騎士の役目と相場はきまっている。"},
            { "DarknessPotion", "たまにはこんな風に静かに飲むのも悪くないな。"},
            { "DarknessDeathDoor", "たとえこの身が砕けようとも、決して悪に屈指はしない!"},
            { "DarknessDeathDoorAlly", "痛いのは嫌か? だと?むしろ大好物だ!"},
        };

        private static readonly Dictionary<string, string> DarknessVoiceLinesCN = new Dictionary<string, string>
        {
            { "DarknessAttackLands_0", "我的攻击打中了！真的打中了！"},
            { "DarknessAttackLands_1", "艾丝的剑术训练太厉害了！我已经开始迷上击中敌人的快感了！" },
            { "DarknessAttackMisses", "我对自己的力量和耐力很有自信，但太笨拙了，攻击总是打不中……"},
            { "DarknessBattleStart_0", "作为一名十字军，我必须贯彻我的职责直到最后。"},
            { "DarknessBattleStart_1", "别犹豫！必要时就连我一起和敌人一同攻击吧！"},
            { "DarknessBattleStart_2", "即使我是替身，也请毫不犹豫地指挥我吧！"},
            { "DarknessChest", "要是没有护甲就吃下这种重击……唉，算了！我们还是去冒险吧！"},
            { "DarknessCrit", "就、就算那是部队的名字也无所谓……别叫我拉拉蒂娜！"},
            { "DarknessCurse", "十字军战士从不向寒冷低头。事实上，我欢迎它！"},
            { "DarknessHealed", "嘶、喂！别拉那个！快住手！"},
            { "DarknessIdleBattle_0", "塔盾啊……虽然很可靠，但为什么这么让人烦躁！"},
            { "DarknessIdleBattle_1", "成为队伍的盾牌却被魔王军抓住……呃啊，光是想象就……"},
            { "DarknessIdleBattle_2", "台下那么多眼睛盯着我看……呃啊！"},
            { "DarknessIdleField_0", "我、我、我、我也是女人啊！说我僵硬、蛮力女什么的……会受伤的啊！"},
            { "DarknessIdleField_1", "嘿嘿嘿嘿嘿……你问我在做什么？很明显嘛，是我每天的编织日常啊。"},
            { "DarknessKill", "无论对手是谁，我的使命都不变。放马过来吧——我就是那堵墙！"},
            { "DarknessMaster", "这股惊人的紧致感！不愧是克拉肯，完全超出预期！"},
            { "DarknessPharos_0", "我现在装备轻甲，但我依然是十字军。来吧——用你最强的一击攻击我！"},
            { "DarknessPharos_1", "我会坚持到最后一刻，不要挡我的路！"},
            { "DarknessPharos_2", "自古以来，女骑士们注定要遭受魔王的羞辱。"},
            { "DarknessPotion", "像这样安静地喝一杯……偶尔也不错。"},
            { "DarknessDeathDoor", "即使身体粉碎，我也绝不向邪恶低头！"},
            { "DarknessDeathDoorAlly", "害怕痛苦？我？不——痛苦可是我最喜欢的东西之一！"},
        };


        [HarmonyPatch(typeof(PrintText))]
        [HarmonyPatch(nameof(PrintText.TextInput))]
        public class VoiceOn
        {
            [HarmonyPrefix]
            public static bool Prefix(PrintText __instance, string inText)
            {
                if (!Utils.DarknessVoice)
                {
                    return true;
                }

                string language = LocalizationManager.CurrentLanguage;
                Dictionary<string, string> selectedDict;

                switch (language)
                {
                    case "Korean":
                        selectedDict = DarknessVoiceLinesKR;
                        break;
                    case "English":
                        selectedDict = DarknessVoiceLinesEN;
                        break;
                    case "Japanese":
                        selectedDict = DarknessVoiceLinesJP;
                        break;
                    case "Chinese":
                        selectedDict = DarknessVoiceLinesCN;
                        break;
                    default:
                        selectedDict = DarknessVoiceLinesEN;
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