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
                    new CodeMatch(instr => instr.opcode == OpCodes.Ldc_I4_1 || instr.opcode == OpCodes.Ldc_I4_2),
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


        [HarmonyPatch(typeof(FieldSystem), "StageStart")]
        public static class StagePatch
        {
            [HarmonyPostfix]
            public static void StageStartPostfix()
            {
                int level;
                if (KazumaInParty(out level) && level >= 1)
                {
                    if (!IsCamp && PlayData.TSavedata.StageNum >= 0)
                    {
                        InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Item_Key, 1));
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
    }
}