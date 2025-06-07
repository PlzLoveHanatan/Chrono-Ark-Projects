//using UnityEngine;
//using UnityEngine.UI;
//using System;
//using System.Linq;
//using System.Collections;
//using System.Collections.Generic;
//using GameDataEditor;
//using I2.Loc;
//using DarkTonic.MasterAudio;
//using ChronoArkMod;
//using ChronoArkMod.Plugin;
//using ChronoArkMod.Template;
//using Debug = UnityEngine.Debug;
//using ChronoArkMod.ModData;
//using HarmonyLib;
//using EmotionalSystem;
//using System.Diagnostics.Eventing.Reader;
//using static System.Net.Mime.MediaTypeNames;

//namespace Xiao
//{
//    public class Xiao_Plugin : ChronoArkPlugin
//    {
//        Harmony harmony = new Harmony("XiaoMod");

//        public override void Dispose()
//        {
//            if (harmony != null)
//            {
//                harmony.UnpatchSelf();
//            }
//        }

//        public override void Initialize()
//        {
//            try
//            {
//                harmony.PatchAll();
//            }
//            catch (Exception e)
//            {
//                Debug.Log("XiaoMod: Patch Catch: " + e.ToString());
//            }
//        }
//    }
//}
