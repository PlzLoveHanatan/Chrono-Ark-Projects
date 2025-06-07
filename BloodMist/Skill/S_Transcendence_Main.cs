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
namespace BloodMist
{
    public class S_Transcendence_Main : Skill_Extended
    {
        public override IEnumerator DrawAction()
        {
            List<string> list = new List<string>();
            list.Add(GDEItemKeys.Skill_S_Transcendence_Virtue);
            BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill>
            {
                Skill.TempSkill(list.Random(this.BChar.GetRandomClass().Main), BattleSystem.instance.AllyTeam.LucyAlly, null)
            }, new SkillButton.SkillClickDel(this.SelectSkill), ScriptLocalization.System_SkillSelect.BloodMyst, false, false, true, false, false));
            yield return null;
        }

        private void SelectSkill(SkillButton Mybutton)
        {
            if (Mybutton.Myskill.MySkill.KeyID == GDEItemKeys.Skill_S_Transcendence_Virtue)
            {
                BattleSystem.instance.AllyTeam.Draw();
                BattleTeam allyTeam2 = BattleSystem.instance.AllyTeam;
                allyTeam2.AP++;

                if (this.MySkill != null && this.MySkill.MyButton != null)
                {
                    this.MySkill.Except();
                }
            }
        }
    }
}