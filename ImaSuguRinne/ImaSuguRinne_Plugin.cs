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
using VFX_Explosion_Pack;
namespace ImaSuguRinne
{
    public class ImaSuguRinne_Plugin : ChronoArkPlugin
    {
        Harmony harmony = new Harmony("ImaSuguRinne");

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
                Debug.Log("ImaSuguRinne: Patch Catch: " + e.ToString());
            }
        }

        public static bool RinneInParty()
        {
            return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Rinne);
        }

        [HarmonyPatch(typeof(FieldSystem), "StageStart")]
        public static class StagePatch
        {
            [HarmonyPostfix]
            public static void StageStartPostfix()
            {
                if (PlayData.TSavedata.StageNum >= 0 && RinneInParty())
                {
                    if (Utils.MagicalEquip && !Utils.Equip)
                    {
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_Rinne_BloomingPetal, 1));
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_Rinne_BloomingDress, 1));
                        Utils.Equip = true;
                    }

                    if (!Utils.GettingMemory)
                    {
                        Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Rinne_FragmentofMemory);

                        for (int i = 0; i < 1; i++)
                        {
                            Utils.RinneChar.UseSoulStone(skill);
                        }

                        Utils.GettingMemory = true;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PlayData), "GameEndInit")]
        public static class MemoryReset
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                Utils.GettingMemory = false;
                Utils.Equip = false;
            }
        }
    }
}