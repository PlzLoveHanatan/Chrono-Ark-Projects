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
using Spine.Unity;
namespace Akari
{
    public class LDraw : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Skill> strings = new List<Skill>
            {
                Skill.TempSkill(ModItemKeys.Skill_LDraw_0, Utils.AllyTeam.LucyAlly, Utils.AllyTeam),
                Skill.TempSkill(ModItemKeys.Skill_LDraw_1, Utils.AllyTeam.LucyAlly, Utils.AllyTeam),
            };

            if (!Utils.Akari)
            {
                BattleSystem.instance.AllyTeam.Draw(1);
                MySkill.isExcept = true;
            }
            else
            {
                BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(strings, new SkillButton.SkillClickDel(Selection), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));

            }
        }

        private void Selection(SkillButton Mybutton)
        {
            string key = Mybutton.Myskill.MySkill.KeyID;

            if (key == ModItemKeys.Skill_LDraw_0)
            {
                BattleSystem.instance.AllyTeam.Draw(3);
            }
            else
            {
                BattleSystem.instance.AllyTeam.Draw(2);
                MasterAudio.PlaySound("Gun_Reload1", 100f, null, 0f, null, null, false, false);
                Utils.ChargeMag(Utils.Akari);
            }
        }
    }
}
                    