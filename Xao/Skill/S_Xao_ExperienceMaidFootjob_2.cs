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
using System.Diagnostics;
namespace Xao
{
	/// <summary>
	/// Experience Maid Footjob ♡♡♡
	/// </summary>
    public class S_Xao_ExperienceMaidFootjob_2 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            string debuff = ModItemKeys.Buff_B_Xao_MistressTouch;

            if (Xao_Combo.CurrentCombo >= 4)
            {
                foreach (var target in Targets)
                {
                    if (target != null)
                    {
                        Utils.AddDebuff(target, BChar, debuff, 1);
                    }
                }
            }
            Utils.PlayXaoVoiceMaid(BChar);
        }
    }
}