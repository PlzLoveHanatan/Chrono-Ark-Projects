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

        private static readonly Dictionary<string, string> voiceLines = new Dictionary<string, string>
        {
                { "I am the Divine Embodiment of the Aqua Axis faith and the Goddess who governs water!", "BattleStart_0" },
                { "Pamper me more! Worship me! I'm a Goddess, after all!", "BattleStart_1" },
                { "Who am I ? I can't tell you my name, but I'm a passing water Goddess!", "BattleStart_2" },
                { "Leave healing and support to me, the Archpriest!", "IdlingB_0" },
                { "Kazuma-san, I'm bored! Pick me up high!", "IdlingB_1" },
                { "As a Goddess, I refuse to play such a ridiculous role!", "DeathA_0" },
                { "Sometimes I just want to quietly drink sparkling drinks...", "DeathA_1" },
                { "Uugh... It stinks... It stiiinks...", "DeathA_2" },
                { "Hurry up and defeat the Demon Lord, and save this world somehow!", "DeathAlly_0" },
                { "Leave it to me! I'll give you a God Smash!", "DeathAlly_1" },
                { "Freshly caught prey, Aqua is here!", "Kill_0" },
                { "I have to work hard and someday escape this stable life!", "Kill_1" },
                { "Today, I was praised by the Master again!", "Cri_0" },
                { "Why does Estia get believed to be a God I'm a Goddess too, you know!", "Chest_0" },
                { "Tonight's a festival! Let's party till morning with delicious sweets and sparkling drinks!", "Potion_0" },
                { "Hey Megumi! Come here, come here! There's a strange fish over here!", "IdlingF_0" },
                { "I'm not interested in mere humans. Don't worry! I'm a real Goddess!", "IdlingF_1" },
                { "Kazuma! I'm tired! Piggyback me! Piggyback!", "IdlingF_2" },
                { "Hey, is dinner ready yet I'm starving here...", "Heal_0" },
                { "A Goddess is delicate — without a quiet room and a warm bed, she can't sleep!", "Heal_1" },
                { "We should be grateful for our current life; there's no going back to straw beds anymore!", "Heal_2" },
                { "When the soul commands, the true art is what you perform yourself!", "Curse" },
                { "Sorry for purifying the hot spring water, but hey, I'm a Goddess—what can I do ?", "Pharos" },
                { "Heheh, this is my special Goddess-made cake! How does it look Delicious, right ?", "Other_0" },
                { "Beauty of nature~! After a quest, fizzy drinks really are the best!", "Other_1" },
                { "Since you came to this world, I want you to make lots of fun memories!", "Master" },
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
                    foreach (var kvp in voiceLines)
                    {
                        if (inText.Contains(kvp.Key))
                        {
                            MasterAudio.StopBus("SE");
                            MasterAudio.PlaySound(kvp.Value, 100f, null, 0f, null, null, false, false);
                        }
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
                    __result.Name = "Cleansed by Aqua-sama☆";
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
                __result.Name = "Cleansed by Aqua-Sama☆";

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
                if (character.Name == ModItemKeys.Character_Aqua) return true;
            }

            return false;
        }
    }
}