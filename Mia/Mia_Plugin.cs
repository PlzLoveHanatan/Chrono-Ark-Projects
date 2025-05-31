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
        }
    }
}