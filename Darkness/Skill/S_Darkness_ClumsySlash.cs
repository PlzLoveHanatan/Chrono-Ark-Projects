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
namespace Darkness
{
	/// <summary>
	/// Clumsy Slash
	/// </summary>
    public class S_Darkness_ClumsySlash : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Darkness_SideSlash, BChar, BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);
        }
    }
}