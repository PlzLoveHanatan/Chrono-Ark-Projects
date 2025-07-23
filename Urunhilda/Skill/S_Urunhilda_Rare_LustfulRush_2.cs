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
	/// Gentle Violence
	/// OveHeal all allies by 6.
	/// </summary>
    public class S_Urunhilda_Rare_LustfulRush_2 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);

            foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
            {
                if (ally != null)
                {
                    ally.Heal(BattleSystem.instance.DummyChar, 6f, false, true, null);

                    Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Urunhilda_DummyHeal, this.BChar, this.BChar.MyTeam);
                    healingParticle.PlusHit = true;
                    healingParticle.FreeUse = true;

                    BChar.ParticleOut(healingParticle, ally);
                }
            }
        }
    }
}