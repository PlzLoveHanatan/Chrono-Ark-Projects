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

namespace XiaoLOR
{
    public class XiaoLOR_Plugin : ChronoArkPlugin
    {
        public const string modname = "XiaoLOR";

        public const string version = "1.0";

        public const string author = "Midana";
        
        Harmony harmony = new Harmony("XiaoLOR");

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

        [HarmonyPatch(typeof(BattleSystem))]
        [HarmonyPatch(nameof(BattleSystem.CustomBGM))]
        public class BGMPatch
        {
            [HarmonyPostfix]
            private static void Postfix()
            {
                if (PlayData.BattleQueue == GDEItemKeys.EnemyQueue_LastBoss_MasterBattle_1)
                {
                    MasterAudio.StopAllOfSound("ProgramMaster_Boss_Theme_Phase1_(Intro)");
                    MasterAudio.StopAllOfSound("ProgramMaster_Boss_Theme_Phase1_(Loop)");
                    MasterAudio.StopAllOfSound("Challenge_Loop");
                }
            }
        }

        [HarmonyPatch(typeof(P_ProgramMaster))]
        public class MoreBGMPatch
        {
            [HarmonyPatch(nameof(P_ProgramMaster.Phase2Start))]
            [HarmonyPrefix]
            private static bool Prefix(P_ProgramMaster __instance)
            {
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(
                        Skill.TempSkill(GDEItemKeys.Skill_S_ProgramMaster2_Main, __instance.BChar, null),
                        BattleSystem.instance.AllyTeam.AliveChars[0],
                        false, false, false, null));

                return false;
            }

            private static readonly FieldInfo selectTargetField = AccessTools.Field(typeof(P_ProgramMaster), "SelectTarget");

            [HarmonyPatch(nameof(P_ProgramMaster.Select))]
            [HarmonyPrefix]
            public static bool Prefix(P_ProgramMaster __instance, SkillButton Mybutton)
            {
                selectTargetField.SetValue(__instance, Mybutton.Myskill.Master);

                return false;
            }
        }

        [HarmonyPatch(typeof(P_ProgramMaster_2nd))]
        [HarmonyPatch(nameof(P_ProgramMaster_2nd.Init))]
        class MoreMoreBGMPatch
        {
            static bool Prefix(P_ProgramMaster_2nd __instance)
            {
                var baseInit = typeof(Buff).GetMethod("Init", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                baseInit?.Invoke(__instance, null);

                __instance.main = ((__instance.BChar as BattleEnemy).Ai as AI_ProgramMaster2);

                return false;
            }
        }
    }
}
