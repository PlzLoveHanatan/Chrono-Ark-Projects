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
namespace Kazuma
{
    public class Kazuma_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("Kazuma");

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
                Debug.Log("Kazuma: Patch Catch: " + e.ToString());
            }
        }

        public static bool KazumaInParty(out int level)
        {
            foreach (var character in PlayData.TSavedata.Party)
            {
                if (character.KeyData == ModItemKeys.Character_Kazuma)
                {
                    level = character.LV;
                    return true;
                }
            }
            level = 0;
            return false;
        }


        [HarmonyPatch(typeof(HexGenerator))]
        [HarmonyPatch(nameof(HexGenerator.GeneratorMap))]
        public static class AdventurePatch
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
                    cm.Insert(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(AdventurePatch), nameof(KazumaAddChest))));
                });


                return codeMatch.InstructionEnumeration();
            }

            public static int KazumaAddChest(int chestNum)
            {
                bool hasKazuma = KazumaInParty(out int kazumaIndex);

                if (hasKazuma)
                {
                    return chestNum + 1;
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
                bool hasKazuma = KazumaInParty(out int kazumaIndex);
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

        [HarmonyPatch(typeof(FieldSystem), "StageStart")]
        public static class StagePatch
        {
            [HarmonyPostfix]
            public static void StageStartPostfix()
            {
                int level;
                if (KazumaInParty(out level))
                {
                    if (!IsCamp && level >= 1 && PlayData.TSavedata.StageNum >= 0)
                    {
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Misc_Item_Key, 1));
                        //InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Item_Key, 1));
                    }

                    if (level >= 1)
                    {
                        if (StageSystem.instance.gameObject.activeInHierarchy && StageSystem.instance != null && StageSystem.instance.Map != null)
                        {
                            StageSystem.instance.Fogout(false);
                        }
                        for (int i = 0; i < StageSystem.instance.Map.EventTileList.Count; i++)
                        {
                            if (StageSystem.instance.Map.EventTileList[i].Info.Type is HiddenWall)
                            {
                                StageSystem.instance.Map.EventTileList[i].HexTileComponent.HiddenWallOpen();
                            }
                        }
                        StageSystem.instance.SightView(StageSystem.instance.PlayerPos);
                    }
                }
            }

            public static bool IsCamp
            {
                get
                {
                    StageSystem instance = StageSystem.instance;
                    string key = instance?.StageData?.Key;
                    return IsCampKey(key);
                }
            }

            public static bool IsCampKey(string key)
            {
                return key == GDEItemKeys.Stage_Stage_Camp ||
                       key == GDEItemKeys.Stage_Stage2_Camp ||
                       key == GDEItemKeys.Stage_Stage3_Camp;
            }

            [HarmonyPatch(typeof(Item_Equip))]
            [HarmonyPatch("IsIdentify", MethodType.Getter)]
            public static class MiaIdentifyPatch_Equip
            {
                [HarmonyPostfix]
                public static void ApplyKazumaIdentify(Item_Equip __instance, ref bool __result)
                {
                    if (__result)
                        return;

                    int level;
                    if (KazumaInParty(out level) && level >= 3)
                    {
                        __instance._Isidentify = true;
                        __result = true;
                    }
                }
            }


            [HarmonyPatch(typeof(FieldSystem))]
            [HarmonyPatch(nameof(FieldSystem.NextStage))]
            public static class KazumaBlacksmithPatch
            {
                [HarmonyPostfix]
                public static void Postfix()
                {
                    if (PlayData.TSavedata == null || PlayData.TSavedata.Party == null)
                        return;

                    int level;
                    if (KazumaInParty(out level) && level >= 2)
                    {
                        if (PlayData.TSavedata.CampAnvilPlus)
                            PlayData.TSavedata.AnvilCount = 6;
                        else
                            PlayData.TSavedata.AnvilCount = 3;
                    }
                }
            }

            [HarmonyPatch(typeof(CampAnvilEvent))]
            [HarmonyPatch(nameof(CampAnvilEvent.B1Fuc))]
            public class Patch_CampAnvil_B1Fuc
            {
                static bool Prefix(CampAnvilEvent __instance)
                {
                    int level;
                    if (KazumaInParty(out level) && level >= 2)
                        return true;

                    if (!__instance.CombineBtn.interactable)
                        return false;

                    if (__instance.InventoryItems[0] == null || __instance.InventoryItems[1] == null)
                        return false;

                    int rewardCount = 2;
                    if (PlayData.TSavedata.CampAnvilPlus && KazumaInParty(out level) && level >= 2)
                        rewardCount = 5;
                    else if (KazumaInParty(out level) && level >= 2)
                        rewardCount = 3;
                    else if (PlayData.TSavedata.CampAnvilPlus)
                        rewardCount = 4;

                    if (__instance.InventoryItems.Any(i => i.itemkey == GDEItemKeys.Item_Consume_GoldenApple))
                    {
                        Item_Equip item = __instance.InventoryItems.OfType<Item_Equip>().FirstOrDefault();
                        if (item == null)
                            return false;

                        int classNum = Mathf.Min(item.ItemClassNum + 1, 4);
                        List<ItemBase> results = new List<ItemBase>();
                        int attempts = 0;

                        for (int i = 0; i < rewardCount; i++)
                        {
                            while (true)
                            {
                                string key = PlayData.GetEquipRandom(classNum);
                                if (key != __instance.InventoryItems[0].itemkey &&
                                    key != __instance.InventoryItems[1].itemkey &&
                                    !results.Any(x => x.itemkey == key))
                                {
                                    MasterAudio.PlaySound("Anvil");
                                    results.Add(ItemBase.GetItem(key));
                                    break;
                                }

                                attempts++;
                                if (attempts >= 50)
                                {
                                    MasterAudio.PlaySound("Anvil");
                                    results.Add(ItemBase.GetItem(key));
                                    break;
                                }
                            }
                        }

                        GiveReward(results, rewardCount);
                    }
                    else if (__instance.InventoryItems[0] is Item_Passive && __instance.InventoryItems[1] is Item_Passive)
                    {
                        MasterAudio.PlaySound("Anvil");
                        UIManager.InstantiateActive(UIManager.inst.SelectItemUI)
                            .GetComponent<SelectItemUI>()
                            .Init(new List<ItemBase>
                            {
                    PlayData.GetPassiveRandom(),
                    PlayData.GetPassiveRandom(),
                    PlayData.GetPassiveRandom(),
                    PlayData.GetPassiveRandom()
                            }, null, false);
                    }
                    else if (!(__instance.InventoryItems[0] is Item_Passive) && !(__instance.InventoryItems[1] is Item_Passive))
                    {
                        int classNum = Mathf.Min(
                            Mathf.CeilToInt((__instance.InventoryItems[0].ItemClassNum + __instance.InventoryItems[1].ItemClassNum + 1f) / 2f),
                            4
                        );

                        List<ItemBase> results = new List<ItemBase>();
                        int attempts = 0;

                        for (int i = 0; i < rewardCount; i++)
                        {
                            while (true)
                            {
                                string key = PlayData.GetEquipRandom(classNum);
                                if (key != __instance.InventoryItems[0].itemkey &&
                                    key != __instance.InventoryItems[1].itemkey &&
                                    !results.Any(x => x.itemkey == key))
                                {
                                    MasterAudio.PlaySound("Anvil");
                                    results.Add(ItemBase.GetItem(key));
                                    break;
                                }

                                attempts++;
                                if (attempts >= 50)
                                {
                                    MasterAudio.PlaySound("Anvil");
                                    results.Add(ItemBase.GetItem(key));
                                    break;
                                }
                            }
                        }

                        GiveReward(results, rewardCount);
                    }

                    __instance.DelItem(0);
                    __instance.DelItem(1);

                    PlayData.TSavedata.AnvilCount--;
                    if (PlayData.TSavedata.AnvilCount <= 0)
                    {
                        for (int i = 0; i < __instance.Align.transform.childCount; i++)
                        {
                            UnityEngine.Object.Destroy(__instance.Align.transform.GetChild(i).gameObject);
                        }
                    }

                    return false;
                }

                static void GiveReward(List<ItemBase> items, int count)
                {
                    if (count == 1)
                        InventoryManager.Reward(items);
                    else
                        UIManager.InstantiateActive(UIManager.inst.SelectItemUI)
                            .GetComponent<SelectItemUI>()
                            .Init(items, null, false);
                }
            }
        }

		// Adding custom keywords to skills with base Description (damage/heal/accuracy/critical chance or if skill already have Keyword)
		[HarmonyPatch(typeof(SkillToolTip), "Input")]
		public static class SkillToolTip_Input_Plugin
		{
            [HarmonyPostfix]
            public static void Postfix(SkillToolTip __instance, Skill Skill)
            {
                //if (__instance?.Desc == null || Skill?.MySkill == null) return;

                var kw = Skill.MySkill.PlusKeyWords.FirstOrDefault(a => a.Key == ModItemKeys.SkillKeyword_KeyWord_Panties || a.Key == ModItemKeys.SkillKeyword_KeyWord_Contract);
				if (kw == null) return;

				string myWord = SkillToolTip.ColorChange("FF7C34", kw.Name);
				var lines = (__instance.Desc.text ?? string.Empty).Split('\n').ToList();
				int idx = lines.FindIndex(l => l.Contains("<b>") && l.Contains("</b>"));

				if (idx >= 0)
				{
					lines[idx] += ". " + myWord;
				}
				else
				{
					lines.Insert(0, $"<b>{myWord}</b>");
				}

				__instance.Desc.text = string.Join("\n", lines);
			}
		}
	}
}