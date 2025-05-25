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
    }
}