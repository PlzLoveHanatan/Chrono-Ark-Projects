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
namespace Raphi
{
	/// <summary>
	/// Refreshing Renewal
	/// Draw 1 additional skill next turn.
	/// </summary>
    public class RefreshingRenewal : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_AngelsWhisper, this.BChar, false, 0, false, -1, false);
        }
    }
}