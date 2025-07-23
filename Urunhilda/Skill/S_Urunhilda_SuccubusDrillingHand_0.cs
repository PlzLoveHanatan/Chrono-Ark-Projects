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
namespace Urunhilda
{
	/// <summary>
	/// Succubus Drilling Hand
	/// Create a <color=#d78fe9>Succubus Caress Hand</color> in hand.
	/// </summary>
    public class S_Urunhilda_SuccubusDrillingHand_0 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);
            Utils.CreateSkill(ModItemKeys.Skill_S_Urunhilda_SuccubusDrillingHand_1, BChar);
        }
    }
}