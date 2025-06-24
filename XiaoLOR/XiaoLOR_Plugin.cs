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
using EmotionalSystem;
using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;

namespace XiaoLOR
{
    public class XiaoLOR_Plugin : ChronoArkPlugin
    {
        public const string modname = "XiaoLOR";

        public const string version = "1.0";

        public const string author = "Midana";

        Harmony harmony = new Harmony("XiaoLOR");

        public static ModInfo ThisMod => ModManager.getModInfo(modname);

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
                Debug.Log("XiaoLOR: Patch Catch: " + e.ToString());
            }
        }
        public static bool XiaoInParty()
        {
            foreach (var character in PlayData.TSavedata.Party)
            {
                if (character.KeyData == ModItemKeys.Character_XiaoLOR)
                {
                    return true;
                }
            }
            return false;
        }

        [HarmonyPatch(typeof(BattleSystem))]
        [HarmonyPatch(nameof(BattleSystem.CustomBGM))]
        public class BGMPatch
        {
            [HarmonyPostfix]
            private static void Postfix()
            {
                if (!XiaoInParty() && !XiaoUtils.IronLotusSong) return;

                if (PlayData.BattleQueue == GDEItemKeys.EnemyQueue_LastBoss_MasterBattle_1)
                {
                    Debug.Log("[BGMPatch] Stopping default boss music due to IronLotusSong override.");
                    MasterAudio.StopAllOfSound("ProgramMaster_Boss_Theme_Phase1_(Intro)");
                    MasterAudio.StopAllOfSound("ProgramMaster_Boss_Theme_Phase1_(Loop)");
                    MasterAudio.StopAllOfSound("Challenge_Loop");
                }
            }
        }        
    }
}