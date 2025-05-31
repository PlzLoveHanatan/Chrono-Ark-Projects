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

            private static readonly Dictionary<string, string> MiaVoiceLines = new Dictionary<string, string>
            {
                { "MiaBattleStart_0", ModLocalization.MiaBattleStart_0 },
                { "MiaBattleStart_1", ModLocalization.MiaBattleStart_1 },
                { "MiaChest", ModLocalization.MiaChest },
                { "MiaCri", ModLocalization.MiaCri },
                { "MiaCurse", ModLocalization.MiaCurse },
                { "MiaHealed", ModLocalization.MiaHealed },
                { "MiaIdleB_0", ModLocalization.MiaIdleB_0 },
                { "MiaIdleB_1", ModLocalization.MiaIdleB_1 },
                { "MiaIdleF", ModLocalization.MiaIdleF },
                { "MiaKill", ModLocalization.MiaKill },
                { "MiaMaster", ModLocalization.MiaMaster },
                { "MiaOther_0", ModLocalization.MiaOther_0 },
                { "MiaOther_1", ModLocalization.MiaOther_1 },
                { "MiaOther_2", ModLocalization.MiaOther_2 },
                { "MiaPharos_0", ModLocalization.MiaPharos_0 },
                { "MiaPharos_1", ModLocalization.MiaPharos_1 },
                { "MiaPharos_2", ModLocalization.MiaPharos_2 },
                { "MiaPotion", ModLocalization.MiaPotion },
                { "MiaDD", ModLocalization.MiaDD },
                { "MiaDDAlly", ModLocalization.MiaDDAlly },
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
                        foreach (var kvp in MiaVoiceLines)
                        {
                            if (inText.Contains(kvp.Value))
                            {
                                MasterAudio.StopBus("SE");
                                MasterAudio.PlaySound(kvp.Key, 100f, null, 0f, null, null, false, false);
                            }
                        }
                    }

                    return true;
                }
            }
        }
    }
}