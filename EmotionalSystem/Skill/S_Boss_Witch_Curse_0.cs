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
namespace EmotionalSystem
{
	/// <summary>
	/// Weakening Curse
	/// </summary>
    public class S_Boss_Witch_Curse_0 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Skill skill = Skill.TempSkill(GDEItemKeys.Skill_S_Witch_2, BattleSystem.instance.AllyTeam.LucyAlly, BattleSystem.instance.AllyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);
        }
    }
}