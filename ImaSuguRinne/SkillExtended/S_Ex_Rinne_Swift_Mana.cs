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
    public class S_Ex_Rinne_Swift_Mana : Skill_Extended
    {
        public int AP = -1;

        public override void Init()
        {
            MySkill.APChange = AP;
            MySkill.NotCount = true;
        }
    }
}