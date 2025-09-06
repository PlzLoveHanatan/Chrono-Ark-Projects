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
    /// <summary>
    /// Eternal Requiem
    /// When drawn, draw 1 skill, cast the highest-cost skill in your hand, then shuffle a copy of this skill into your deck.
    /// </summary>
    public class S_Rinne_Rare_EternalFate_1 : Skill_Extended
    {
        public override IEnumerator DrawAction()
        {
            if (Utils.Rinne)
            {
                Utils.GlitchEffect(MySkill, 1);
                BattleSystem.DelayInputAfter(BattleSystem.instance.SkillRandomUseIenum(Utils.Rinne, MySkill, false, false, true));
            }
            else
            {
                Utils.AllyTeam.Skills.Remove(MySkill);
            }
            return base.DrawAction();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var highManaSkill = Utils.AllyTeam.Skills.Where(x => x != null).OrderByDescending(x => x.AP).FirstOrDefault();
            BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(Utils.Rinne, highManaSkill, false, false, true));
            Utils.AllyTeam.Draw(1);
        }
    }
}