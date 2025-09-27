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
	/// Wet Bliss
	/// Cannot gain Overload.
	/// </summary>
    public class B_Xao_WetBliss : Buff, IP_SkillUse_User
    {
        public override void Init()
        {
            PlusStat.dod = 10;
            PlusStat.cri = 10;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && SkillD.IsDamage && !SkillD.FreeUse && !SkillD.BasicSkill)
            {
                foreach (var target in Targets)
                {
                    if (target.Info.Ally && target.IsDead) continue;

                    Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Xao_B_WetBliss, BChar, BChar.MyTeam);
                    skill.FreeUse = true;
                    skill.PlusHit = true;
                    skill.NeverCri = true;
                    BChar.ParticleOut(skill, target);
                    //EffectView.TextOutSimple(target, ModLocalization.WetBliss);
                    //BattleSystem.instance.ForceAction(skill, target, false, false, true, null);
                } 
            }
        }
    }
}