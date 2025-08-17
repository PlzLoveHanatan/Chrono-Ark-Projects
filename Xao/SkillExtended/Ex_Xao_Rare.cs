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
namespace Xao
{
    public class Ex_Xao_Rare : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return RareDescription + "" + base.DescExtended(desc);
        }

        public static string RareDescription
        {
            get
            {
                var text = ModLocalization.RareDescription ?? "";
                return text;
            }
        }
    }
}