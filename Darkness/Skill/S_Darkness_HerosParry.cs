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
    /// Hero's Parry
    /// </summary>
    public class S_Darkness_HerosParry : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.maxhp * 0.4f)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int barrierHP = (int)(BChar.GetStat.maxhp * 0.4f);
            BChar.BuffAdd(ModItemKeys.Buff_S_Darkness_StubbornKnight, BChar, false, 0, false, -1, false).BarrierHP += barrierHP;
            foreach (var a in BChar.MyTeam.AliveChars)
            {
                if (a != null && a != BChar)
                    a.BuffAdd(ModItemKeys.Buff_B_Darkness_DarknessProtection, BChar, false, 0, false, -1, false);
            }
        }
    }
}