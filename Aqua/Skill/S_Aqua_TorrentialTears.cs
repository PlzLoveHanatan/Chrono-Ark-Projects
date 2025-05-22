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
namespace Aqua
{
	/// <summary>
	/// Torrential Tears
	/// </summary>
    public class S_Aqua_TorrentialTears : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            bool neverLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 20);

            if (neverLucky)
            {
                foreach (var ally in BChar.MyTeam.AliveChars)
                {
                    ally.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, this.BChar, false, 0, false, -1, false);
                }
            }
        }
    }
}