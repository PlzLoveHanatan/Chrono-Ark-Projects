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
namespace Satanichia
{
	/// <summary>
	/// Twilight Trick
	/// </summary>
    public class S_Satanichia_TwilightTrick : SkillExtedned_IlyaP
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", (Utils.TwilightTrickScale * 2).ToString())
                .Replace("&b", "2");
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            SkillBasePlus.Target_BaseDMG = Utils.TwilightTrickScale * 2;
        }

        public override void IlyaWaste()
        {
            Utils.TwilightTrickScale++;
        }
    }
}