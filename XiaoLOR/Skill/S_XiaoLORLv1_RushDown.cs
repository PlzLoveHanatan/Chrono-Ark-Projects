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
namespace XiaoLOR
{
	/// <summary>
	/// Rush Down
	/// This skill can be played 1 more time during this turn.
	/// </summary>
    public class S_XiaoLORLv1_RushDown : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("NormalHit", 100f, null, 0f, null, null, false, false);

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_XiaoLORLv1_RushDown_0, this.BChar, this.BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);
            skill.AutoDelete = 1;
            skill.isExcept = true;
        }
    }
}