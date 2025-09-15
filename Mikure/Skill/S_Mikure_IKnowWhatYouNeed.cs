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
namespace Mikure
{
    /// <summary>
    /// I Know What You Need!
    /// </summary>
    public class S_Mikure_IKnowWhatYouNeed : Skill_Extended
    {
        private BattleChar Ally;

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Ally = Targets[0];

            List<Skill> strings = new List<Skill>
            {
                Skill.TempSkill(ModItemKeys.Skill_S_Mikure_IKnowWhatYouNeed_0, BChar, BChar.MyTeam),
                Skill.TempSkill(ModItemKeys.Skill_S_Mikure_IKnowWhatYouNeed_1, BChar, BChar.MyTeam),
            };

            BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(strings, new SkillButton.SkillClickDel(Selection), ModLocalization.WhatYouNeed, false, false, true, false, false));
        }
        private void Selection(SkillButton Mybutton)
        {
            string key = Mybutton.Myskill.MySkill.KeyID;

            if (key == ModItemKeys.Skill_S_Mikure_IKnowWhatYouNeed_0)
            {
                Utils.AddBuff(Ally, BChar, ModItemKeys.Buff_B_Mikure_IKnowWhatYouNeed_0);
            }
            else
            {
                Utils.AddBuff(Ally, BChar, ModItemKeys.Buff_B_Mikure_IKnowWhatYouNeed_1);
            }
        }
    }
}