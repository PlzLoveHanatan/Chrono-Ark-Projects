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

        private static readonly Dictionary<string, string> XaoVoiceLineEN = new Dictionary<string, string>
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
                            selectedDict = XaoVoiceLineEN;
                            break;
                        case "English":
                            selectedDict = XaoVoiceLineEN;
                            break;
                        case "Japanese":
                            selectedDict = XaoVoiceLineEN;
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