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
	/// Mia's Dreamland
	/// Draw 4 skills.
	/// Apply 'Discarded after 1 turn' to these skills.
	/// </summary>
    public class S_Mia_LucyDraw_1 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.instance.AllyTeam.Draw(4, new BattleTeam.DrawInput(Drawinput));
        }
        public void Drawinput(Skill skill)
        {
            skill.AutoDelete = 1;
        }
    }
}