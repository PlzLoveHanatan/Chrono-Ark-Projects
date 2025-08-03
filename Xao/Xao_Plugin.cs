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
using Steamworks;
using HarmonyLib;
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

        [HarmonyPatch(typeof(CharacterSkinData), "SteamDLCCheck")]
        public static class DLC_Patch
        {
            [HarmonyPrefix]
            public static bool Prefix(ref bool __result, uint dlcKey)
            {
                __result = true;
                return false;
            }
        }
    }
}