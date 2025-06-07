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

namespace Unica
{
    /// <summary>
    /// Fierce Onslaught
    /// Discard a page with the lowest Cost
    /// </summary>
    public class FierceOnslaught : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse) return;

            base.SkillUseSingle(SkillD, Targets);

            var HighMana = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill).OrderByDescending(skill => skill.AP).ToList();

            var ManaReduce = HighMana.FirstOrDefault();

            if (ManaReduce != null) 
            {
                skill add ex
            }
            var SkillsWithTheSameName = BattleSystem.instance.AllyTeam.Skills_UsedDeck
    .Concat(BattleSystem.instance.AllyTeam.Skills_Deck)
    .Where(skill => skill.CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
    .ToList();

            foreach (var skill in SkillsWithTheSameName)
            {
                skill.SkillExtended();
            }


        }
        public override void IlyaWaste()
        {
            BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(this.BChar, this.MySkill, false, true, false));
        }
    }
}
