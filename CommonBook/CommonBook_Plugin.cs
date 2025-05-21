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
namespace CommonBook
{
    public class CommonBook_Plugin : ChronoArkPlugin
    {
        private Harmony harmony;

        public override void Initialize()
        {
            this.harmony = new Harmony(base.GetGuid());
            this.harmony.PatchAll();
        }
        public override void Dispose()
        {
            this.harmony.UnpatchSelf();
        }
    }

    [HarmonyPatch(typeof(P_Priest))]
    class PrefixExample
    {
        [HarmonyPatch(nameof(P_Priest._Oracle))]

        [HarmonyPrefix]
        static bool CommonBook(P_Priest __instance)

        {
            bool flag = false;

            foreach (BattleChar b in BattleSystem.instance.AllyTeam.AliveChars)
            {
                if (b.BuffReturn("B_Raphi_3", false) != null) {flag = true;}
            }

                if (flag == true)
                    return false;
                    else return true;
        }
    }
}