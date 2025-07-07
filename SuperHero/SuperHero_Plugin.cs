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
namespace SuperHero
{
    public class SuperHero_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("SuperHero");

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
                Debug.Log("SuperHero: Patch Catch: " + e.ToString());
            }
        }
        public static bool SuperHeroInParty()
        {
            foreach (var character in PlayData.TSavedata.Party)
            {
                if (character.KeyData == ModItemKeys.Character_SuperHero)
                {
                    return true;
                }
            }
            return false;
        }

        [HarmonyPatch(typeof(B_ProgramMaster_LucyMain))]
        [HarmonyPatch(nameof(B_ProgramMaster_LucyMain.BuffOneAwake))]
        public static class PainSharingPatch
        {
            static bool Prefix(B_ProgramMaster_LucyMain __instance)
            {
                if (SuperHeroInParty())
                {
                    return false;
                }
                return true;
            }
        }
    }
}