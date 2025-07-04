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
using ChronoArkMod.ModData.Settings;
namespace EmotionalSystem
{
    public class EmotionalSystem_Plugin : ChronoArkPlugin
    {
        public const string modname = "EmotionalSystem";

        public const string version = "1.0";

        public const string author = "Midana && surprise4u";

        public static ModInfo ThisMod => ModManager.getModInfo(modname);

        [HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.EnemyTurn))]
        public class EndTurnPatch
        {
            // This patch will switch the hand back if EGO is active when turn ends
            public static IEnumerator Postfix(IEnumerator result, bool EndButton)
            {
                if (EGO_System.instance != null && EGO_System.instance.egoActive)
                {
                    try
                    {
                        EGO_System.instance.SwitchToNormal();
                    }
                    catch { }
                    yield return new WaitForSecondsRealtime(0.2f);
                }
                while (result.MoveNext())
                {
                    yield return result.Current;
                }
            }
        }

        Harmony harmony = new Harmony("EmotionalSystem");

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
                Debug.Log("EmotionalSystem: Patch Catch: " + e.ToString());
            }
        }

        [HarmonyPatch(typeof(ResultUI))]
        [HarmonyPatch(nameof(ResultUI.Init))]
        class ResultScreenPatch
        {
            [HarmonyPostfix]
            static void Postfix(ResultUI __instance)
            {
                __instance.DifficultyObj.SetActive(true);
                __instance.BloodyMistObj.SetActive(true);
                Sprite sprite = AddressableLoadManager.LoadAsyncCompletion<Sprite>(new GDEImageDatasData(GDEItemKeys.ImageDatas_Image_BloodyMist).Sprites_Path[3], AddressableLoadManager.ManageType.Stage);
                __instance.BloodyMistImage.sprite = sprite;
                string newText = "Emotional System";
                if (PlayData.TSavedata.bMist != null)
                {
                    newText = ScriptLocalization.System_Mode.BloodyMist + " " + PlayData.TSavedata.bMist.Level.ToString() + "\n" + newText;
                }
                if (EmotionalSystem.B_EnemyEmotionalLevel.EnemyEmotionOn)
                {
                    newText += "\n+Enemy Emotions";
                }
                if (EmotionalSystem.AllLibraryFloors.Floor == FloorCode.Technological)
                {
                    newText += "\n+Floor of Technological Sciences";
                }
                if (EmotionalSystem.AllLibraryFloors.Floor == FloorCode.History)
                {
                    newText += "\n+Floor of History";
                }

                __instance.BloodyMistText.text = newText;
            }
        }

        [HarmonyPatch(typeof(BuffObject))]
        public class BuffObjectUpdatePlugin
        {
            [HarmonyPatch("Update")]
            [HarmonyPostfix]
            public static void Update_Patch(BuffObject __instance)
            {
                if (__instance.MyBuff != null)
                {
                    IP_BuffObject_Updata ip_BuffObject_Updata = __instance.MyBuff as IP_BuffObject_Updata;
                    if (ip_BuffObject_Updata != null)
                    {
                        ip_BuffObject_Updata.BuffObject_Updata(__instance);
                    }
                }
            }
        }
    }
}