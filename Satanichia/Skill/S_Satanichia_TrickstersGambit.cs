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
namespace Satanichia
{
    /// <summary>
    /// Trickster's Gambit
    /// </summary>
    public class S_Satanichia_TrickstersGambit : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);

            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(s => s != MySkill).OrderByDescending(s => s.AP).ToList();

            if (skillsInHand.Count <= 0) return;

            Skill highestManaSkillInHand = skillsInHand.FirstOrDefault();

            PlusSkillPerFinal.Damage = highestManaSkillInHand.AP * 15;

            highestManaSkillInHand.Delete(false);
        }

        public override void IlyaWaste()
        {
            int drawNum = Math.Min(2, MySkill.AP);
            BattleSystem.instance.AllyTeam.Draw(drawNum);
        }
    }
}