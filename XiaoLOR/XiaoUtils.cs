using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;
using UnityEngine.UI;
using HarmonyLib;
using ChronoArkMod.ModData;
using System.Runtime.InteropServices.WindowsRuntime;
using DarkTonic.MasterAudio;
using System.Collections;
using UnityEngine;

namespace XiaoLOR
{
    public static class XiaoUtils
    {
        private static ModInfo modInfo = ModManager.getModInfo("XiaoLOR");
        public static ToggleSetting IronLotusSongSetting => modInfo.GetSetting<ToggleSetting>("Iron Lotus");
        public static ToggleSetting IronLotusSongKeyIngredientSetting => modInfo.GetSetting<ToggleSetting>("Iron Lotus Key");

        public static bool IronLotusSong
        {
            get => IronLotusSongSetting.Value;
            set
            {
                if (value)
                {
                    IronLotusSongKeyIngredientSetting.Value = false;
                }
                IronLotusSongSetting.Value = value;
            }
        }

        public static bool IronLotusSongKeyIngredient
        {
            get => IronLotusSongKeyIngredientSetting.Value;
            set
            {
                if (value)
                {
                    IronLotusSongSetting.Value = false;
                }
                IronLotusSongKeyIngredientSetting.Value = value;
            }
        }

        public static void XiaoSongStart()
        {
            string song = "";
            if (XiaoUtils.IronLotusSong)
               song = "IronLotus";

            else if (XiaoUtils.IronLotusSongKeyIngredient)
              song = "IronLotusKey";

            if (string.IsNullOrEmpty(song))
                return;

            MasterAudio.StopBus("BGM");
            //MasterAudio.StopBus("BattleBGM");
            MasterAudio.FadeBusToVolume("BGM", 1f, 1f, null, false, false);
            MasterAudio.FadeBusToVolume("BattleBGM", 0f, 0.5f, null, false, false);
            MasterAudio.PlaySound(song, 100f, null, 0f, null, null, false, false);
        }

        public static IEnumerator XiaoSongEnd()
        {
            yield return new WaitForFixedUpdate();
            MasterAudio.StopBus("BGM");
            MasterAudio.FadeBusToVolume("BGM", 1f, 1f, null, false, false);

            yield return null;

            //Action completionCallback = delegate ()
            //{
            //    MasterAudio.StopBus("BattleBGM");
            //};
            //MasterAudio.FadeBusToVolume("BattleBGM", 0f, 0.5f, completionCallback, true, true);
        }
    }
}
