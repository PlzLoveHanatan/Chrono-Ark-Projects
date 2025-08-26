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
using TileTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Reflection;
using System.Reflection.Emit;
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


        //public static bool XaoInParty()
        //{
        //    foreach (var character in PlayData.TSavedata.Party)
        //    {
        //        if (character.KeyData == ModItemKeys.Character_Xao)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public static bool XaoInParty()
        {
            return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Xao);
        }

        public static bool AdditionalSecretTile()
        {
            return XaoInParty() && Utils.AdditionalSecretTile;
        }

        [HarmonyPatch(typeof(HexGenerator))]
        [HarmonyPatch(nameof(HexGenerator.GeneratorMap))]
        public static class KaijuChestPatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codeMatch = new CodeMatcher(instructions);

                codeMatch.MatchEndForward(
                    new CodeMatch(instr => instr.opcode == OpCodes.Ldc_I4_1 || instr.opcode == OpCodes.Ldc_I4_2 || instr.opcode == OpCodes.Ldarg_1),
                    new CodeMatch(instr => instr.opcode == OpCodes.Stloc_S)
                )
                .Repeat(matchAction: cm =>
                {
                    cm.Insert(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(KaijuChestPatch), nameof(XaoAddChest))));
                });


                return codeMatch.InstructionEnumeration();
            }

            public static int XaoAddChest(int chestNum)
            {
                bool hasXao = XaoInParty();

                if (hasXao)
                {
                    return chestNum + (int)(Utils.MoreChests);
                }

                return chestNum;
            }
        }

        [HarmonyPatch(typeof(HexGenerator))]

        public static class GeneratorMapPlugin
        {
            [HarmonyPatch("GeneratorMap")]
            [HarmonyTranspiler]
            private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                //this line is 
                var methodInfo = AccessTools.Method(typeof(MapTest), nameof(MapTest.AddHideWall), new System.Type[] { typeof(HexMap).MakeByRefType() });
                bool hasKazuma = XaoInParty();
                var codes = instructions.ToList();
                for (int i = 0; i < codes.Count; i++)
                {
                    if (i > 5 && hasKazuma)
                    {
                        //this part is to find the position you want to insert new code
                        //by compare opcode and operand.Most of the time, comparing with opcode is enough.
                        bool f1 = true;
                        f1 = f1 && codes[i - 5].opcode == OpCodes.Ldloc_S;
                        try
                        {
                            f1 = f1 && (codes[i - 5].operand is LocalBuilder && ((LocalBuilder)(codes[i - 5].operand)).LocalIndex == 13);
                        }
                        catch { f1 = false; }

                        f1 = f1 && codes[i - 4].opcode == OpCodes.Ldloca_S;
                        f1 = f1 && codes[i - 3].opcode == OpCodes.Ldloc_S;
                        try
                        {
                            f1 = f1 && (codes[i - 3].operand is LocalBuilder && ((LocalBuilder)(codes[i - 3].operand)).LocalIndex == 13);
                        }
                        catch { f1 = false; }

                        f1 = f1 && codes[i - 2].opcode == OpCodes.Ldloc_0;

                        f1 = f1 && codes[i - 1].opcode == OpCodes.Callvirt;
                        f1 = f1 && codes[i - 0].opcode == OpCodes.Callvirt;
                        //---------------------------
                        //this part is to insert new code
                        if (f1)
                        {
                            yield return codes[i]; //The original code
                            yield return new CodeInstruction(OpCodes.Ldloca_S, 0); //new code
                            yield return new CodeInstruction(OpCodes.Call, methodInfo); //new code
                            continue;
                        }
                        //----------------------------
                    }
                    yield return codes[i];//The original code
                }
            }
        }

        public static class MapTest
        {
            public static void AddHideWall(ref HexMap map) //this is the function I insert to the original code
            {
                bool flag = true; //Change this paragraph to the conditions that need to be met
                {
                    HiddenWall wall = new HiddenWall();
                    if (PlayData.TSavedata.StageNum != 0)
                    {
                        wall.Add(ref map, wall.PosSet(map));
                    }
                }
            }
        }


        [HarmonyPatch(typeof(BattleSystem), "BattleEnd")]
        public static class BattleSystem_FogRevealPatch
        {
            public static void Postfix(BattleSystem __instance, bool NoSaveAfterEnd, bool isDefeat)
            {
                if (Utils.RemoveFogFromStage)
                {
                    Utils.RemoveFog();
                }
            }
        }
    }
}