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
namespace ImaSuguRinne
{
	/// <summary>
	/// Cycle of Suffering
	/// </summary>
    public class S_Rinne_CycleofSuffering_0 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);
            bool mySkill = MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_CycleofSuffering_0;
            string skill = mySkill ? ModItemKeys.Skill_S_Rinne_CycleofSuffering_1 : ModItemKeys.Skill_S_Rinne_CycleofSuffering_0;
            Utils.CreateSkill(BChar, skill, false, false, 0, 1, true);
        }
    }
}