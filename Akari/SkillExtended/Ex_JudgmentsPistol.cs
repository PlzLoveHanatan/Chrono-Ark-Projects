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
namespace Akari
{
    public class Ex_JudgmentsPistol : Skill_Extended
    {
        public override void Init()
        {
            PlusSkillPerFinal.Damage = 30;
            PlusSkillPerFinal.Heal = 30;
        }
    }
}