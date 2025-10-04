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
            bool hasComboKeyword = MySkill.MySkill.PlusKeyWords.Any(k => k.Key == ModItemKeys.SkillKeyword_KeyWord_Combo);

            if (!hasComboKeyword)
            {
                MySkill.MySkill.PlusKeyWords.Add(new GDESkillKeywordData(ModItemKeys.SkillKeyword_KeyWord_Combo));
            }
        }

        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 1;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Xao_Combo.SaveComboBetweenTurns = true;
            Xao_Combo.ComboChange(2);

            var buff = Utils.GetAffectionBuff(MyChar);
            Utils.AddBuff(BChar, buff, 1);
        }
    }
}