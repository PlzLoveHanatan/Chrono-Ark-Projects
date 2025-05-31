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
    /// Chaotic Harvest
    /// </summary>
    public class S_Mia_Rare_ChaoticHarvest : Skill_Extended
    {
        private int Index;

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

            Index = skillsInHand.Count;

            foreach (Skill skill in skillsInHand)
            {
                BattleSystem.DelayInputAfter(BattleSystem.instance.SkillRandomUseIenum(skill.Master, skill, false, false, true));
            }

            BattleSystem.DelayInputAfter(DrawSkills());
        }

        private IEnumerator DrawSkills()
        {
            if (Index > 0)
            {
                BattleSystem.instance.AllyTeam.Draw(Index);
            }
            yield break;
        }
    }
}