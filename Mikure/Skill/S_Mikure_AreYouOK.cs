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
using NLog.Targets;
namespace Mikure
{
	/// <summary>
	/// Are You OK!?
	/// Increase healing amount by &a for each type of debuff on a target.
	/// </summary>
    public class S_Mikure_AreYouOK : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.reg * 0.4f)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var target = Targets[0];
            int debuffTypes = Utils.GetDebuffTypes(target);
            int additionalHeal = (int)(BChar.GetStat.reg * 0.4f * debuffTypes);

            if (debuffTypes > 0)
            {
                SkillBasePlus.Target_BaseHeal = additionalHeal;
            }
        }
    }
}