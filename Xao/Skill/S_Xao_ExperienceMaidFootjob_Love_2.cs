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
	/// Experience Maid Footjob Pleasure ♥♥♥
	/// </summary>
    public class S_Xao_ExperienceMaidFootjob_Love_2 : Skill_Extended
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

            if (Xao_Combo.CurrentCombo >= 6)
            {
                foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
                {
                    if (enemy != null)
                    {
                        Utils.AddDebuff(enemy, BChar, debuff, 2);
                    }
                }
            }
            Utils.PlayXaoVoiceMaid(BChar);
        }
    }
}