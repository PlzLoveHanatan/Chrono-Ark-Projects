using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using GameDataEditor;
using HarmonyLib;
using UnityEngine;

namespace EmotionSystem
{
	//public partial class EmotionlSystem_Plugin
	//{
	//	[HarmonyPatch(typeof(HexGenerator))]
	//	[HarmonyPatch(nameof(HexGenerator.GeneratorMap))]
	//	public static class DistortionChests
	//	{
	//		public static bool rolled = false;
	//		public static List<int> activeResults = new List<int>(); // 0 = chests, 1 = large events, 2 = small events, 3 = enemies
	//		public static bool negativeEffect = false;

	//		[HarmonyPrefix]
	//		public static void Prefix()
	//		{
	//			if (!Utils.Distortions || IsCamp) return;
	//			if (rolled) return;

	//			int baseRoll = RandomManager.RandomInt(RandomClassKey.Map, 0, 101);
	//			if (baseRoll >= 50)
	//			{
	//				Debug.Log($"[Distortions] No distortion (roll {baseRoll})");
	//				rolled = true;
	//				return;
	//			}

	//			int secondRoll = RandomManager.RandomInt(RandomClassKey.Map, 0, 101);

	//			if (secondRoll <= 50)
	//			{
	//				// Positive outcome: add 2 random unique effects (+1 each)
	//				List<int> possible = new List<int> { 0, 1, 2 };
	//				int first = possible[RandomManager.RandomInt(RandomClassKey.Map, 0, possible.Count)];
	//				possible.Remove(first);
	//				int second = possible[RandomManager.RandomInt(RandomClassKey.Map, 0, possible.Count)];

	//				activeResults.Add(first);
	//				activeResults.Add(second);
	//				Debug.Log($"[Distortions] Positive outcome! +1 for {first} and {second}");
	//			}
	//			else
	//			{
	//				// Negative outcome: remove 1 random effect (-1)
	//				int chosen = RandomManager.RandomInt(RandomClassKey.Map, 0, 3);
	//				activeResults.Add(chosen);
	//				negativeEffect = true;
	//				Debug.Log($"[Distortions] Negative outcome! -1 for {chosen}");
	//			}

	//			rolled = true;
	//		}

	//		static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
	//		{
	//			var codeMatch = new CodeMatcher(instructions);

	//			codeMatch.MatchEndForward(
	//				new CodeMatch(instr => instr.opcode == OpCodes.Ldc_I4_1 || instr.opcode == OpCodes.Ldc_I4_2 || instr.opcode == OpCodes.Ldarg_1),
	//				new CodeMatch(instr => instr.opcode == OpCodes.Stloc_S)
	//			)
	//			.Repeat(matchAction: cm =>
	//			{
	//				cm.Insert(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(DistortionChests), nameof(AddChest))));
	//			});

	//			return codeMatch.InstructionEnumeration();
	//		}

	//		public static int AddChest(int chestNum)
	//		{
	//			if (!Utils.Distortions) return chestNum;

	//			if (activeResults.Contains(0))
	//			{
	//				if (negativeEffect)
	//				{
	//					chestNum -= 1;
	//					Debug.Log("[Distortions] -1 Chest");
	//				}
	//				else
	//				{
	//					chestNum += 1;
	//					Debug.Log("[Distortions] +1 Chest");
	//				}
	//			}

	//			return chestNum;
	//		}

	//		[HarmonyPostfix]
	//		public static void Postfix()
	//		{
	//			if (!Utils.Distortions && IsCamp) return;

	//			rolled = false;
	//			activeResults.Clear();
	//			negativeEffect = false;
	//			Debug.Log("[Distortions] Roll reset after map generation");
	//		}
	//	}

	//	[HarmonyPatch(typeof(GDEStageData), "get_RareEvent_L")]
	//	public static class DistortionEventsLarge
	//	{
	//		[HarmonyPostfix]
	//		public static void Postfix(ref int __result)
	//		{
	//			if (!Utils.Distortions || PlayData.TSavedata == null || PlayData.TSavedata.Party == null) return;

	//			if (DistortionChests.activeResults.Contains(1))
	//			{
	//				if (DistortionChests.negativeEffect)
	//				{
	//					__result -= 1;
	//					Debug.Log("[Distortions] -1 Large Event");
	//				}
	//				else
	//				{
	//					__result += 1;
	//					Debug.Log("[Distortions] +1 Large Event");
	//				}
	//			}
	//		}
	//	}

	//	[HarmonyPatch(typeof(GDEStageData), "get_EnemyNum")]
	//	public static class DistortionEnemies
	//	{
	//		[HarmonyPostfix]
	//		public static void Postfix(ref int __result)
	//		{
	//			if (!Utils.Distortions || PlayData.TSavedata == null || PlayData.TSavedata.Party == null) return;

	//			if (DistortionChests.activeResults.Contains(2))
	//			{
	//				if (DistortionChests.negativeEffect)
	//				{
	//					__result -= 1;
	//					Debug.Log("[Distortions] -1 Enemy");
	//				}
	//				else
	//				{
	//					__result += 1;
	//					Debug.Log("[Distortions] +1 Enemy");
	//				}
	//			}
	//		}
	//	}
	//}
}