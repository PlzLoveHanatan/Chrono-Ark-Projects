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
namespace Kazuma
{
    public class Item_Kazuma_Panties_01_Standart : UseitemBase
    {
        public override bool Use(Character CharInfo)
        {
            if (CharInfo?.KeyData == ModItemKeys.Character_Kazuma)
            {
                Utils.Luck++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}