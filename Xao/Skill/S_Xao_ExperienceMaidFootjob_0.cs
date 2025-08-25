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
namespace Xao
{
	/// <summary>
	/// Experience Maid Footjob
	/// </summary>
    public class S_Xao_ExperienceMaidFootjob_0 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.CreateSkill(ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_1, BChar);
            Utils.PlayXaoVoiceMaid(BChar);
        }
    }
}