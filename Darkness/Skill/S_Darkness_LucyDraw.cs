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
namespace Darkness
{
	/// <summary>
	/// Battle Prep
	/// Draw 3 skills and gain &a barrier
	/// </summary>
    public class S_Darkness_LucyDraw : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveDarkness = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Darkness);
            return base.DescExtended(desc).Replace("&a", (aliveDarkness.GetStat.maxhp * 0.5f).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveDarkness = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Darkness);

            BattleSystem.instance.AllyTeam.Draw(3);

            if (aliveDarkness != null)
            {
                BChar.MyTeam.partybarrier.BarrierHP += (int)(aliveDarkness.GetStat.maxhp * 0.5f);
            }
        }
    }
}