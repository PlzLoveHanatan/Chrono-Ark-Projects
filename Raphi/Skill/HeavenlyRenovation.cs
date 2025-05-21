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
    /// Heavenly Renovation
    /// When played from hand, discard all skills in hand and increase healing by &a.
    /// For every 2 skills discarded, draw 1 additional skill next turn (Max 2).  
    /// If at least 6 skills are discarded, draw 1 skill.  
    /// If 8 skills are discarded, gain <color=#7B68EE>Celestial Connection</color>.
    /// Sheathe : Heal all allies by &b.
    /// </summary>
    public class HeavenlyRenovation : SkillExtedned_IlyaP
    {
        public override string DescExtended(string desc)
        {
            if (BattleSystem.instance != null)
            {
                List<Skill> skillList = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

                if (skillList != null)
                {
                    return base.DescExtended(desc)
                        .Replace("&a", ((int)(BChar.GetStat.reg * 0.2f) * skillList.Count).ToString())
                        .Replace("&b", ((int)(BChar.GetStat.reg * 0.55f)).ToString());
                }
            }

            return base.DescExtended(desc)
            .Replace("&a", ((int)(BChar.GetStat.reg * 0.2f)).ToString())
            .Replace("&b", ((int)(BChar.GetStat.reg * 0.55f)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);

            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            List<Skill> skillList = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

            if (skillList.Count == 0) return;

            skillList.ForEach(skill => skill.Delete(false));

            SkillBasePlus.Target_BaseHeal = (int)(BChar.GetStat.reg * 0.2f) * skillList.Count;

            for (int i = 0; i < skillList.Count / 6; i++)
            {
                BattleSystem.instance.AllyTeam.Draw();
            }

            if (skillList.Count == 2 || skillList.Count == 3)
            {
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_AngelsWhisper, BChar, false, 0, false, -1, false);
                return;
            }
            else if (skillList.Count >= 4)
            {
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_AngelsWhisper, BChar, false, 0, false, -1, false);
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_AngelsWhisper, BChar, false, 0, false, -1, false);
            }

            if (skillList.Count >= 8)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection, BChar, false, 0, false, -1, false);
            }
        }
        public override void IlyaWaste()
        {
            Skill skill = Skill.TempSkill(ModItemKeys.Skill_HeavenlyRenovation_0, BChar, BChar.MyTeam);
            skill.PlusHit = true;
            skill.FreeUse = true;
            BChar.ParticleOut(skill, BattleSystem.instance.AllyTeam.AliveChars);
        }
    }
}