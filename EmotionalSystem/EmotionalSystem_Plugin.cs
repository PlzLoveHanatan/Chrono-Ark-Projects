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
                if (EmotionalSystem_EGO_Button.instance != null && EmotionalSystem_EGO_Button.instance.egoActive)
                {
                    try
                    {
                        EmotionalSystem_EGO_Button.instance.SwitchToNormal();
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
                string text = "Emotional System";
                if (PlayData.TSavedata.bMist != null)
                {
                    text += ScriptLocalization.System_Mode.BloodyMist + " " + PlayData.TSavedata.bMist.Level.ToString() + "\n" + text;
                }
                if (Utils.EnemyEmotions)
                {
                    text += "\n+Enemy Emotions";
                }
                else if (!Utils.EnemyEmotions)
                {
                    text += "\n+Emotionless Enemies";
                }
                if (!Utils.AllyEmotions)
                {
                    text += "\n+Emotionless Allies";
                }
                //if (EmotionalSystem_Library.CurrentFloor == FloorCode.Technological)
                //{
                //    text += "\n+Floor of Technological Sciences";
                //}
                //else if (AllLibraryFloors.Floor == FloorCode.History)
                //{
                //    text += "\n+Floor of History";
                //}

                __instance.BloodyMistText.text += text;
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


        [HarmonyPatch(typeof(EventBattle_TrialofTime))]
        [HarmonyPatch(nameof(EventBattle_TrialofTime.BattleStartUIOnBefore))]
        class TimeTrialPatch
        {
            [HarmonyPostfix]
            static void Postfix()
            {
                if (Utils.EnemyEmotions)
                {
                    PlayData.TSavedata.Timer *= 1.3f;
                }
            }
        }
    }
}