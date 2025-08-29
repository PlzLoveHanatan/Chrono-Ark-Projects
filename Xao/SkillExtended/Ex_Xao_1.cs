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
    public class Ex_Xao_1 : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD == null || BChar == null) return;

            Xao_Combo.SaveComboBetweenTurns = true;
            Xao_Combo.ComboChange(2);

            var buff = Utils.GetAffectionBuff(MyChar);
            Utils.AddBuff(BChar, buff, 1);
        }
    }
}