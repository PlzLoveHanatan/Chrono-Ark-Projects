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
using System.Security.Cryptography;
namespace SuperHero
{
    /// <summary>
    /// Bloodstained Dress
    /// Apply 'Scarlet Remnant' to all allies.
    /// </summary>
    public class S_SuperHero_BloodstainedDress : Skill_Extended
    {
        public override void FixedUpdate()
        {
            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
            }
        }

        public override string DescExtended(string desc)
        {
            string text = Utils.SuperHeroMod(BChar) ? ModLocalization.BloodStained_1 : ModLocalization.BloodStained_0;
            return base.DescExtended(desc).Replace("Description", text);
        }

        public override bool Terms()
        {
            return BChar.Info.KeyData == ModItemKeys.Character_SuperHero;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (!Utils.SuperHeroMod(BChar))
            {
                foreach (var ally in Utils.AllyTeam.AliveChars)
                {
                    if (ally == Utils.SuperHero) continue;

                    Utils.AddDebuff(ally, BChar, ModItemKeys.Buff_B_SuperHero_ScarletRemnant, 1);
                }
            }  
        }
    }
}