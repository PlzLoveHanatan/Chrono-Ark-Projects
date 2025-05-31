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
namespace Mia
{
	/// <summary>
	/// Scrollfang: Mia's Cut
	/// Discard highest Mana cost skill in hand. Increase this skill's damage by discarded skill Mana cost * 15%.
	/// Sheathe : Draw skills per cost of this skill (Max 2).
	/// </summary>
    public class S_Mia_Scrollfang : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(s => s != MySkill).OrderByDescending(s => s.AP).ToList();

            if (skillsInHand.Count <= 0) return;

            Skill highestManaSkillInHand = skillsInHand.FirstOrDefault();

            PlusSkillPerFinal.Damage = highestManaSkillInHand.AP * 15;

            highestManaSkillInHand.Delete(false);
        }

        public override void IlyaWaste()
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            int drawNum = Math.Min(2, MySkill.AP);
            BattleSystem.instance.AllyTeam.Draw(drawNum);
        }
    }
}