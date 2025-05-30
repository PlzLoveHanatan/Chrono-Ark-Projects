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
using HarmonyLib;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using ChronoArkMod.ModData;
using System.Reflection.Emit;
namespace SheatheExtended
{
    public class SheatheExtended_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("SheatheExtended");

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
                Debug.Log("SheatheExtended: Patch Catch: " + e.ToString());
            }
        }
    }

    [HarmonyPatch]
    public class IlyaWastePatches
    {
        // Patch to trigger all Sheathe effects on the same skill
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.IlyaExtended))]
        [HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.Ilya7), MethodType.Enumerator)]
        static IEnumerable<CodeInstruction> SheatheTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var code in instructions)
            {
                if (code.Is(OpCodes.Callvirt, AccessTools.Method(typeof(SkillExtedned_IlyaP), nameof(SkillExtedned_IlyaP.IlyaWaste))))
                {
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return code;
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(IlyaWastePatches), nameof(TriggerOtherSheathe)));
                }
                else
                {
                    yield return code;
                }
            }
        }

        // Patch to make the Sheathe selection cleaner (remove same skill duplicates)
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(BattleSystem), "FixedUpdate")]
        static IEnumerable<CodeInstruction> FixedUpdateTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            LocalBuilder IlyaexList = generator.DeclareLocal(typeof(List<SkillExtedned_IlyaP>));
            // store the filtered list in a new local variable
            yield return new CodeInstruction(OpCodes.Ldarg_0);
            yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BattleSystem), nameof(BattleSystem.IlyaWasteList)));
            yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(IlyaWastePatches), nameof(FilterIlyaList)));
            yield return new CodeInstruction(OpCodes.Stloc, IlyaexList);
            // process the rest of the function, replace this.IlyaWasteList by the local variable
            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                if (i + 2 < codes.Count && codes[i].opcode == OpCodes.Ldarg_0 &&
                    codes[i + 1].Is(OpCodes.Ldfld, AccessTools.Field(typeof(BattleSystem), nameof(BattleSystem.IlyaWasteList))) &&
                    !codes[i + 2].Is(OpCodes.Callvirt, AccessTools.Method(typeof(List<SkillExtedned_IlyaP>), nameof(List<SkillExtedned_IlyaP>.Clear))))
                {
                    codes[i].opcode = OpCodes.Ldloc;
                    codes[i].operand = IlyaexList;
                    codes[i + 1].opcode = OpCodes.Nop;
                    codes[i + 1].operand = null;
                }
            }
            foreach (var code in codes)
            {
                yield return code;
            }
        }


        private static void TriggerOtherSheathe(SkillExtedned_IlyaP Ilyaex)
        {
            foreach (var ex in Ilyaex.MySkill.AllExtendeds)
            {
                if (ex != Ilyaex && ex is SkillExtedned_IlyaP Ilyaex1)
                {
                    Ilyaex1.IlyaWasteFirst();
                    Ilyaex1.IlyaWaste();
                }
            }
        }

        private static List<SkillExtedned_IlyaP> FilterIlyaList(List<SkillExtedned_IlyaP> IlyaexList)
        {
            var newList = new List<SkillExtedned_IlyaP>();
            foreach (var Ilyaex in IlyaexList)
            {
                if (!newList.Any(ex => ex.MySkill == Ilyaex.MySkill))
                {
                    newList.Add(Ilyaex);
                }
            }
            return newList;
        }
    }
}