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
    /// <summary>
    /// Draw
    /// </summary>
    public class S_Xao_LucyDraw_0 : Skill_Extended
    {
        private readonly List<Skill> strings = new List<Skill>
        {
            Skill.TempSkill(ModItemKeys.Skill_S_Xao_LucyDraw_1, Utils.AllyTeam.LucyAlly, Utils.AllyTeam),
            Skill.TempSkill(ModItemKeys.Skill_S_Xao_LucyDraw_2, Utils.AllyTeam.LucyAlly, Utils.AllyTeam),
        };

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int drawNum = 0;
            drawNum = Utils.Xao ? 2 : 1;
            MySkill.isExcept = Utils.Xao ? false : true;

            Utils.AllyTeam.Draw(drawNum);

            if (Utils.Xao)
            {
                BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(strings, new SkillButton.SkillClickDel(Selection), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
            }
        }

        private void Selection(SkillButton Mybutton)
        {
            string key = Mybutton.Myskill.MySkill.KeyID;

            if (key == ModItemKeys.Skill_S_Xao_LucyDraw_1)
            {
                //Xao_Hearts.HeartsCheck(Utils.Xao, 1);
                Xao_Combo.ComboChange(2);
                Utils.AddBuff(Utils.Xao, ModItemKeys.Buff_B_Xao_Affection);
            }
            else
            {
                Utils.AllyTeam.AP += 1;
                Xao_Combo.SaveComboBetweenTurns = true;
                Utils.PopHentaiText(Utils.Xao);
            }
        }
    }
}