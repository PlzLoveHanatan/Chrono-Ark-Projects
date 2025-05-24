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
                { "Who am I I can't tell you my name, but I'm a passing water Goddess!", "BattleStart_2" },
                { "Kazuma-san, I'm bored! Pick me up high!", "IdlingB_0" },
                { "Leave healing and support to me, the Archpriest!", "IdlingB_1" },
                { "As a Goddess, I refuse to play such a ridiculous role!", "DeathA_0" },
                { "Sometimes I just want to quietly drink sparkling drinks...", "DeathA_1" },
                { "Hurry up and defeat the Demon Lord, and save this world somehow!", "DeathAlly_0" },
                { "Leave it to me! I'll give you a God Smash!", "DeathAlly_1" },
                { "Freshly caught prey, Aqua is here!", "Kill_0" },
                { "I have to work hard and someday escape this stable life!", "Kill_1" },
                { "Today, I was praised by the Master again!", "Cri_0" },
                { "Why does Estia get believed to be a God I'm a Goddess too, you know!", "Chest_0" },
                { "Tonight's a festival! Let's party till morning with delicious sweets and sparkling drinks!", "Potion_0" },
            };

        [HarmonyPatch(typeof(PrintText))]
        [HarmonyPatch("TextInput")]
        public class VoiceOn
        {
            [HarmonyPrefix]
            public static bool Prefix(PrintText __instance, string inText)
            {
                if (BattleSystem.instance != null && Utils.AquaVoice)
                {
                    foreach (var kvp in voiceLines)
                    {
                        if (inText.Contains(kvp.Key))
                        {
                            MasterAudio.PlaySound(kvp.Value, 100f, null, 0f, null, null, false, false);
                        }
                    }
                }

                return true;
            }
        }
    }
}