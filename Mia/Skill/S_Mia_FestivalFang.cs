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
	/// Festival Fang
	/// Sheathe : Permamently increase this skill's damage 2 for this run.
	/// </summary>
    public class S_Mia_FestivalFang : SkillExtedned_IlyaP
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", (Utils.FestivalFang * 2).ToString()).Replace("&b", "2");
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            SkillBasePlus.Target_BaseDMG = Utils.FestivalFang * 2;
        }

        public override void IlyaWaste()
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            Utils.FestivalFang++;
        }
    }
}