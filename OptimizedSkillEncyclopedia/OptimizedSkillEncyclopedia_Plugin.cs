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
using System.Reflection.Emit;
using EItem;
using System.Runtime.InteropServices.WindowsRuntime;
namespace OptimizedSkillEncyclopedia
{
    public class OptimizedSkillEncyclopedia_Plugin: ChronoArkPlugin
    {
        Harmony harmony = new Harmony("OptimizedSkillEncyclopedia");

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
                Debug.Log("OptimizedSkillEncyclopedia: Patch Catch: " + e.ToString());
            }
        }

        [HarmonyPatch]
        public static class SkillCollectionPatch
        {
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(SKillCollection), "SkillAdd")]
            public static IEnumerable<CodeInstruction> SkillAddTranspiler(IEnumerable<CodeInstruction> instructions)
            {
                var codeMatcher = new CodeMatcher(instructions);
                codeMatcher.End();
                codeMatcher.MatchStartBackwards(
                    new CodeMatch(code => code.opcode == OpCodes.Ldloc_S || code.opcode == OpCodes.Ldloc),
                    new CodeMatch(code => code.opcode == OpCodes.Ldloc_S || code.opcode == OpCodes.Ldloc),
                    new CodeMatch(OpCodes.Stfld, AccessTools.Field(typeof(SkillPrefab), nameof(SkillPrefab.Data))),
                    new CodeMatch(code => code.opcode == OpCodes.Ldloc_S || code.opcode == OpCodes.Ldloc),
                    new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(GDESkillData), nameof(GDESkillData.Image_0_Path))),
                    new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(string), nameof(string.IsNullOrEmpty))),
                    new CodeMatch(code => code.opcode == OpCodes.Brtrue || code.opcode == OpCodes.Brtrue_S)
                );
                var loadPrefab = codeMatcher.Instruction;
                codeMatcher.Advance(4);
                var startInd = codeMatcher.Pos;
                var label = (Label)codeMatcher.InstructionAt(2).operand;
                codeMatcher.SearchForward(code => code.labels.Contains(label));
                var endInd = codeMatcher.Pos;
                codeMatcher.Advance(startInd - endInd);
                codeMatcher.RemoveInstructionsInRange(startInd, endInd - 1);
                codeMatcher.Insert(
                    loadPrefab,
                    CodeInstruction.Call(typeof(SkillCollectionPatch), nameof(LoadSkinnedImage))
                );
                return codeMatcher.InstructionEnumeration();
            }


            public static void LoadSkinnedImage(GDESkillData skill, SkillPrefab prefab)
            {
                GDEVFXSkillData vfxskillData = CharacterSkinData.GetVFXSkillData(skill.User, skill.KeyID, out _);
                string text = skill.Image_0_Path;
                if (!Misc.NullCheck(vfxskillData) && vfxskillData.SkillImage_Path.Count > 0)
                {
                    text = vfxskillData.SkillImage_Path[0];
                }
                if (!string.IsNullOrEmpty(text))
                {
                    AddressableLoadManager.LoadAsyncAction(text, AddressableLoadManager.ManageType.Collection, prefab.SkillImage);
                }
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(CharacterSkinData), nameof(CharacterSkinData.GetIllustChangePath))]
            [HarmonyPatch(typeof(CharacterSkinData), nameof(CharacterSkinData.GetVFXSkillData))]
            [HarmonyPatch(typeof(CharacterSkinData), nameof(CharacterSkinData.GetVFXChangePath))]
            static void IllustChangePrefix(ref string charKey, string skillKey)
            {
                var skillData = new GDESkillData(skillKey);
                if (skillData != null && !string.IsNullOrEmpty(skillData.LucyPartyDraw))
                {
                    charKey = skillData.LucyPartyDraw;
                }
            }

            [HarmonyTranspiler]
            [HarmonyPatch(typeof(FieldScriptOut), nameof(FieldScriptOut.Text_GetItem))]
            static IEnumerable<CodeInstruction> GetItemTranspiler(IEnumerable<CodeInstruction> instructions)
            {
                return Transpilers.Manipulator(instructions,
                    item => item.opcode == OpCodes.Ldc_I4_3,
                    item => item.opcode = OpCodes.Ldc_I4_1
                );
            }
        }
    }
}