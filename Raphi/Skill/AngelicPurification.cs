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
namespace Raphi
{
	/// <summary>
	/// Remove 1 random debuff.
	/// Sheathe : Cast this skill on the ally with the lowest health.
	/// </summary>
    public class AngelicPurification : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            var debuffs = Targets[0].GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false);
            if (debuffs.Any())
            {
                Targets[0].BuffRemove(debuffs.Random(BChar.GetRandomClass().Main).BuffData.Key, false);
            }
        }

        public override void IlyaWaste()
        {
            var allyWithLowestHP = BChar.MyTeam.AliveChars.OrderBy(x => x.HP).FirstOrDefault();

            if (allyWithLowestHP != null)
            {
                Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_AngelicPurification, BChar, BChar.MyTeam);
                newSkill.PlusHit = true;
                newSkill.FreeUse = true;
                BChar.ParticleOut(newSkill, allyWithLowestHP);

                var debuffs = allyWithLowestHP.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false);
                if (debuffs.Any())
                {
                    allyWithLowestHP.BuffRemove(debuffs.Random(BChar.GetRandomClass().Main).BuffData.Key, false);
                }
            }
        }
    }
}