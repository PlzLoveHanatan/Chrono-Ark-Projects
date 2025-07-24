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
namespace Urunhilda
{
	/// <summary>
	/// Succubus Squeeze Feet
	/// </summary>
    public class S_Urunhilda_SuccubusDrillingFeet_2 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BChar.Heal(BattleSystem.instance.DummyChar, 4, false, true, null);

            Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Urunhilda_DummyHeal, BChar, BChar.MyTeam);
            healingParticle.PlusHit = true;
            healingParticle.FreeUse = true;
            BChar.ParticleOut(healingParticle, BChar);
        }
    }
}