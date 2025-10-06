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
using System.Runtime.InteropServices.WindowsRuntime;
using System.Reflection;
using System.Reflection.Emit;
using Newtonsoft.Json.Linq;
namespace Xao
{
    public class Xao_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("Xao");

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
                Debug.Log("Xao: Patch Catch: " + e.ToString());
            }
        }

        public static bool XaoInParty()
        {
            return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Xao);
        }

        [HarmonyPatch(typeof(FieldSystem), "StageStart")]
        public static class StagePatch
        {
            [HarmonyPostfix]
            public static void StageStartPostfix()
            {
                if (XaoInParty())
                {
                    EnsureXaoEquip();

                    if (PlayData.TSavedata.StageNum >= 0)
                    {
                        if (Utils.KaijuEquip && !HasXaoEquip())
                        {
                            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_Equip_Xao_LoveEgg, 1));
                            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_Equip_Xao_MagicWand, 1));
                            SetXaoEquipFlag(true);
                        }
                    }
                }
            }
        }

        public static bool HasXaoEquip() => PlayData.TSavedata.GetCustomValue<Xao_Equip>()?.GainXaoEquip ?? false;

        public static void EnsureXaoEquip()
        {
            var equip = PlayData.TSavedata.GetCustomValue<Xao_Equip>();
            if (equip == null)
            {
                equip = new Xao_Equip { GainXaoEquip = false };
                PlayData.TSavedata.AddCustomValue(equip);
            }
        }

        public static void SetXaoEquipFlag(bool value)
        {
            var equip = PlayData.TSavedata.GetCustomValue<Xao_Equip>();
            if (equip == null)
            {
                equip = new Xao_Equip();
                PlayData.TSavedata.AddCustomValue(equip);
            }
            equip.GainXaoEquip = value;
        }

		//[HarmonyPatch(typeof(PlayData), "GameEndInit")]
		//public static class MemoryReset
		//{
		//    [HarmonyPostfix]
		//    public static void Postfix()
		//    {
		//        Utils.Equip = false;
		//    }
		//}
		public static readonly Dictionary<string, string> XaoVoiceLineKR = new Dictionary<string, string>
        {
	        { "Xao_BattleStart_0", "처음 뵙겠습니다  잘 부탁드려요." },
	        { "Xao_BattleStart_1", "최선을 다해 주세요." },
	        { "Xao_BattleStart_2", "짜증나..." },
	        { "Xao_Chest", "최근 결과가 좋아서 예상보다 더 많은 휴식을 받았어요." },
	        { "Xao_Cri", "여기요." },
	        { "Xao_Curse_0", "간지러워요." },
	        { "Xao_Curse_1", "깜짝 놀랐어요." },
	        { "Xao_Healed", "기분이 좋아요..." },
	        { "Xao_Idling_in_Battle_0", "피곤하면 침대에서 쉬세요." },
	        { "Xao_Idling_in_Battle_1", "잠깐 쉴래?" },
	        { "Xao_Idling_in_Battle_2", "너무 무리하고 있어요." },
	        { "Xao_Idling_in_Field_0", "하고 싶은 일이 있거나 이루고 싶은 목표가 있다면, 해보세요. 그렇죠?" },
	        { "Xao_Idling_in_Field_1", "당신은 나를 멀리 데려가서 새로운 경험을 하게 하고, 이전과는 다른 풍경을 보여주겠다고 했죠." },
	        { "Xao_Idling_in_Field_2", "이번에도 열심히 했네요, 수고했어요." },
	        { "Xao_Killing_Enemy_0", "끝났나요?" },
	        { "Xao_Killing_Enemy_1", "자, 이걸 받으세요." },
	        { "Xao_Master", "실패하더라도, 혹은 중간에 게을러지고 싶더라도 괜찮아요." },
	        { "Xao_Pharos_0", "이상한 느낌이에요." },
	        { "Xao_Pharos_1", "섹스할래요?" },
	        { "Xao_Pharos_2", "지쳤어요..." },
	        { "Xao_Potion", "아침식사?" },
	        { "Xao_DeathDoor_0", "움직일 수 없어요." },
	        { "Xao_DeathDoor_1", "피곤해요..." },
	        { "Xao_DeathDoorAlly_0", "괜찮아요?" },
	        { "Xao_DeathDoorAlly_1", "여기서 쓰러지면 지금까지의 노력이 모두 헛수고가 됩니다." },
        };

		public static readonly Dictionary<string, string> XaoVoiceLineEN = new Dictionary<string, string>
        {
            { "Xao_BattleStart_0",  "Nice to meet you  Please take care of me."},
            { "Xao_BattleStart_1", "Please do your best." },
            { "Xao_BattleStart_2", "So annoying..." },
            { "Xao_Chest", "Since the recent results were good, we got more time off than expected" },
            { "Xao_Cri", "Here you go." },
            { "Xao_Curse_0", "That tickles." },
            { "Xao_Curse_1", "That surprised me." },
            { "Xao_Healed", "It feels good..." },
            { "Xao_Idling_in_Battle_0", "If you're tired, please rest in bed." },
            { "Xao_Idling_in_Battle_1", "Want to take a break?" },
            { "Xao_Idling_in_Battle_2", "You're pushing yourself too hard." },
            { "Xao_Idling_in_Field_0", "If you've got something you want to do, some goal to reach—then do it, won't you?" },
            { "Xao_Idling_in_Field_1", "You said you'd take me somewhere far, let me experience something new, and show me different scenery than before." },
            { "Xao_Idling_in_Field_2", "You worked hard again this time, good job." },
            { "Xao_Killing_Enemy_0", "Is it over?" },
            { "Xao_Killing_Enemy_1", "Here, take this." },
            { "Xao_Master", "Even if you fail, or feel like slacking off along the way, it's fine." },
            { "Xao_Pharos_0", "Feels strange." },
            { "Xao_Pharos_1", "Do you want to have sex?" },
            { "Xao_Pharos_2", "I'm worn out..." },
            { "Xao_Potion", "Breakfast?" },
            { "Xao_DeathDoor_0", "I can't move." },
            { "Xao_DeathDoor_1", "I'm tired..." },
            { "Xao_DeathDoorAlly_0", "Are you okay?" },
            { "Xao_DeathDoorAlly_1", "If you collapse here, all your effort until now will go to waste." },
        };

		public static readonly Dictionary<string, string> XaoVoiceLineJP = new Dictionary<string, string>
        {
	        { "Xao_BattleStart_0", "はじめまして  よろしくお願いします。" },
	        { "Xao_BattleStart_1", "頑張ってください。" },
	        { "Xao_BattleStart_2", "うるさいな…" },
	        { "Xao_Chest", "最近の結果が良かったので、思ったより多くの休暇をもらえました。" },
	        { "Xao_Cri", "どうぞ。" },
	        { "Xao_Curse_0", "くすぐったい。" },
	        { "Xao_Curse_1", "びっくりした。" },
	        { "Xao_Healed", "気持ちいい…" },
	        { "Xao_Idling_in_Battle_0", "疲れたらベッドで休んでください。" },
	        { "Xao_Idling_in_Battle_1", "少し休憩する？" },
	        { "Xao_Idling_in_Battle_2", "無理しすぎです。" },
	        { "Xao_Idling_in_Field_0", "やりたいこと、達成したい目標があるなら、やってみたらどう？" },
	        { "Xao_Idling_in_Field_1", "遠くに連れて行って、新しい体験をさせて、今までと違う景色を見せてくれるって言ってたよね。" },
	        { "Xao_Idling_in_Field_2", "今回もよく頑張りましたね、お疲れさま。" },
	        { "Xao_Killing_Enemy_0", "終わりましたか？" },
	        { "Xao_Killing_Enemy_1", "はい、これをどうぞ。" },
	        { "Xao_Master", "途中で失敗しても、怠けたくなっても大丈夫。" },
	        { "Xao_Pharos_0", "変な感じ。" },
	        { "Xao_Pharos_1", "セックスしたい？" },
	        { "Xao_Pharos_2", "疲れた…" },
	        { "Xao_Potion", "朝ごはん？" },
	        { "Xao_DeathDoor_0", "動けません。" },
	        { "Xao_DeathDoor_1", "疲れました…" },
	        { "Xao_DeathDoorAlly_0", "大丈夫ですか？" },
	        { "Xao_DeathDoorAlly_1", "ここで倒れると今までの努力が無駄になります。" },
        };

		public static readonly Dictionary<string, string> XaoVoiceLineCN = new Dictionary<string, string>
        {
	        { "Xao_BattleStart_0", "初次见面  请多关照。" },
	        { "Xao_BattleStart_1", "请加油。" },
	        { "Xao_BattleStart_2", "好烦啊……" },
	        { "Xao_Chest", "因为最近表现不错，所以得到了比预期更多的休息时间。" },
	        { "Xao_Cri", "给你。" },
	        { "Xao_Curse_0", "好痒。" },
	        { "Xao_Curse_1", "吓到我了。" },
	        { "Xao_Healed", "感觉真好……" },
	        { "Xao_Idling_in_Battle_0", "如果累了，请在床上休息。" },
	        { "Xao_Idling_in_Battle_1", "要不要休息一下？" },
	        { "Xao_Idling_in_Battle_2", "你太勉强自己了。" },
	        { "Xao_Idling_in_Field_0", "如果有想做的事，有想达到的目标——那就去做吧，好吗？" },
	        { "Xao_Idling_in_Field_1", "你说过要带我去远一点的地方，让我体验新的事物，看看不同的风景。" },
	        { "Xao_Idling_in_Field_2", "这次你也辛苦了，干得好。" },
	        { "Xao_Killing_Enemy_0", "结束了吗？" },
	        { "Xao_Killing_Enemy_1", "来，拿这个。" },
	        { "Xao_Master", "即使失败了，或者中途想偷懒也没关系。" },
	        { "Xao_Pharos_0", "感觉有点奇怪。" },
	        { "Xao_Pharos_1", "想做爱吗？" },
	        { "Xao_Pharos_2", "我累坏了……" },
	        { "Xao_Potion", "早餐？" },
	        { "Xao_DeathDoor_0", "我动不了了。" },
	        { "Xao_DeathDoor_1", "我累了……" },
	        { "Xao_DeathDoorAlly_0", "你还好吗？" },
	        { "Xao_DeathDoorAlly_1", "如果在这里倒下，你之前的努力就白费了。" },
        };

		public static readonly Dictionary<string, string> XaoVoiceLineTW = new Dictionary<string, string>
        {
	        { "Xao_BattleStart_0", "初次見面  請多關照。" },
	        { "Xao_BattleStart_1", "請加油。" },
	        { "Xao_BattleStart_2", "好煩啊……" },
	        { "Xao_Chest", "因為最近表現不錯，所以得到了比預期更多的休息時間。" },
	        { "Xao_Cri", "給你。" },
	        { "Xao_Curse_0", "好癢。" },
	        { "Xao_Curse_1", "嚇到我了。" },
	        { "Xao_Healed", "感覺真好……" },
	        { "Xao_Idling_in_Battle_0", "如果累了，請在床上休息。" },
	        { "Xao_Idling_in_Battle_1", "要不要休息一下？" },
	        { "Xao_Idling_in_Battle_2", "你太勉強自己了。" },
	        { "Xao_Idling_in_Field_0", "如果有想做的事，有想達到的目標——那就去做吧，好嗎？" },
	        { "Xao_Idling_in_Field_1", "你說過要帶我去遠一點的地方，讓我體驗新的事物，看看不同的風景。" },
	        { "Xao_Idling_in_Field_2", "這次你也辛苦了，做得好。" },
	        { "Xao_Killing_Enemy_0", "結束了嗎？" },
	        { "Xao_Killing_Enemy_1", "來，拿這個。" },
	        { "Xao_Master", "即使失敗了，或者中途想偷懶也沒關係。" },
	        { "Xao_Pharos_0", "感覺有點奇怪。" },
	        { "Xao_Pharos_1", "想做愛嗎？" },
	        { "Xao_Pharos_2", "我累壞了……" },
	        { "Xao_Potion", "早餐？" },
	        { "Xao_DeathDoor_0", "我動不了了。" },
	        { "Xao_DeathDoor_1", "我累了……" },
	        { "Xao_DeathDoorAlly_0", "你還好嗎？" },
	        { "Xao_DeathDoorAlly_1", "如果在這裡倒下，你之前的努力就白費了。" },
        };

		[HarmonyPatch(typeof(PrintText))]
        [HarmonyPatch(nameof(PrintText.TextInput))]
        public class VoiceOn
        {
            [HarmonyPrefix]
            public static bool Prefix(PrintText __instance, string inText)
            {
                if (Utils.XaoVoice)
                {
                    string language = LocalizationManager.CurrentLanguage;
                    Dictionary<string, string> selectedDict;

                    switch (language)
                    {
                        case "Korean":
                            selectedDict = XaoVoiceLineKR;
                            break;
                        case "English":
                            selectedDict = XaoVoiceLineEN;
                            break;
                        case "Japanese":
                            selectedDict = XaoVoiceLineJP;
                            break;
                        case "Chinese":
                            selectedDict = XaoVoiceLineEN;
                            break;
                        default:
                            selectedDict = XaoVoiceLineEN;
                            break;
                    }

                    foreach (var kvp in selectedDict)
                    {
                        if (inText.Contains(kvp.Value))
                        {
                            MasterAudio.StopBus("SE");
                            var result = MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);

                            if (result.ActingVariation == null)
                            {
                                Debug.LogWarning($"Sound '{kvp.Key}' failed to play.");
                            }
                        }
                    }
                }
                return true;
            }
        }
    }
}