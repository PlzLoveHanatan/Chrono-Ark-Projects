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
using Spine;
namespace Mikure
{
    public class Mikure_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("Mikure");

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
                Debug.Log("worky worky");
                harmony.PatchAll();
            }
            catch (Exception e)
            {
                Debug.Log("Mikure: Patch Catch: " + e.ToString());
            }
        }

        [HarmonyPatch(typeof(SKillCollection), nameof(SKillCollection.SkillTarget))]
        public static class DescriptionPatch
        {
            [HarmonyPostfix]
            public static void Postfix(string TargetType, ref string __result)
            {
                if (__result == ModLocalization.DeadAlly) return;

                if (!string.IsNullOrEmpty(TargetType) && TargetType == GDEItemKeys.s_targettype_deathally)
                {
                    __result = ModLocalization.DeadAlly;
                }
            }
        }

        [HarmonyPatch(typeof(SkillToolTip), nameof(SkillToolTip.SkillTarget))]
        public static class DescriptionPatch2
        {
            [HarmonyPostfix]
            public static void Postfix(string TargetType, ref string __result)
            {
                if (__result == ModLocalization.DeadAlly) return;

                if (!string.IsNullOrEmpty(TargetType) && TargetType == GDEItemKeys.s_targettype_deathally)
                {
                    __result = ModLocalization.DeadAlly;
                }
            }
        }


        [HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.SkillTargetReturn), new Type[] { typeof(Skill), typeof(BattleChar), typeof(BattleChar) })]
        public static class TargetingPatch
        {
            [HarmonyPostfix]
            public static void Postfix(Skill sk, BattleChar Target, BattleChar PlusTarget, ref List<BattleChar> __result, BattleSystem __instance)
            {
                bool myCheck = sk.Master == Utils.Mikure;

                if (sk?.MySkill.Target.Key == GDEItemKeys.s_targettype_all_ally && myCheck)
                {
                    var targets = new List<BattleChar>();

                    if (sk.Master.Info.Ally)
                    {
                        foreach (var ally in __instance.AllyTeam.Chars)
                        {
                            targets.Add(ally);
                        }
                    }

                    if (targets.Count > 0)
                    {
                        __result = targets;
                    }
                }

                else if (sk?.MySkill.Target.Key == GDEItemKeys.s_targettype_ally && myCheck)
                {
                    var targets = __result = new List<BattleChar> { Target };

                    if (targets.Count > 0)
                    {
                        __result = targets;
                    }
                }

                else if (sk?.MySkill.Target.Key == GDEItemKeys.s_targettype_otherally && myCheck)
                {
                    var targets = __result = new List<BattleChar> { Target };

                    if (targets.Count > 0)
                    {
                        __result = targets;
                    }
                }

                else if (sk?.MySkill.Target.Key == GDEItemKeys.s_targettype_deathally && myCheck)
                {
                    var targets = __result = new List<BattleChar> { Target };

                    if (targets.Count > 0)
                    {
                        __result = targets;
                    }
                }

                else if (sk?.MySkill.Target.Key == GDEItemKeys.s_targettype_all_onetarget && myCheck)
                {
                    var targets = __result = new List<BattleChar> { Target };

                    if (targets.Count > 0)
                    {
                        __result = targets;
                    }
                }
            }

            [HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.IsSelect))]
            public static class SelectTerms
            {
                [HarmonyPostfix]
                public static void Postfix(BattleChar Target, Skill skill, ref bool __result)
                {
                    string targetTypeKey = skill.TargetTypeKey;
                    bool ally = skill.Master.Info.Ally;
                    bool myCheck = skill.Master == Utils.Mikure && skill.IsHeal && Target.Info.Incapacitated;

                    if (targetTypeKey == GDEItemKeys.s_targettype_all_ally && ally == Target.Info.Ally && myCheck)
                    {
                        __result = true;
                    }

                    if (targetTypeKey == GDEItemKeys.s_targettype_ally && ally == Target.Info.Ally && myCheck)
                    {
                        __result = true;
                    }

                    if (targetTypeKey == GDEItemKeys.s_targettype_otherally && Target != skill.Master && ally == Target.Info.Ally && myCheck)
                    {
                        __result = true;
                    }

                    if (targetTypeKey == GDEItemKeys.s_targettype_all_onetarget && ally == Target.Info.Ally && myCheck)
                    {
                        __result = true;
                    }
                }
            }
        }
    }
}