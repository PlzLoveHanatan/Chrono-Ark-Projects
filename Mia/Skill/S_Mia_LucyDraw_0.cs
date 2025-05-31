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
using System.Reflection;
namespace Mia
{
    /// <summary>
    /// Messy Notes
    /// Discard the top skill in your hand and draw 3 skills.
    /// </summary>
    public class S_Mia_LucyDraw_0 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveMia = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Mia);

            if (aliveMia != null)
            {
                Utils.TryPlayMiaSound(MySkill, BChar);
            }

            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(s => s != MySkill).ToList();

            Skill skill = skillsInHand[0];

            skill.Delete(false);

            BattleSystem.instance.AllyTeam.Draw(3);
        }
    }
}