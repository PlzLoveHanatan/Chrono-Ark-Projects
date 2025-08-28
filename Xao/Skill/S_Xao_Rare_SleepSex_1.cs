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
	/// Sleep Sex
	/// </summary>
    public class S_Xao_Rare_SleepSex_1 : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            string heart = "♡";
            if (BattleSystem.instance != null)
            {
                heart = Utils.GetHeart(BChar, 2);
            }
            return base.DescExtended(desc).Replace("&a", heart);
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Xao_Combo.ComboChange(1);
            Utils.RareSleepSex(BChar, ModItemKeys.Skill_S_Xao_Rare_SleepSex_2, 0, 3);
        }
    }
}