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
namespace ImaSuguRinne
{
    public class S_Ex_E_BloomingDress : Skill_Extended
    {
        public string BloomingDressDesc => ModLocalization.Blooming_Dress ?? "";

        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc) + "\n" + BloomingDressDesc;
        }
    }
}