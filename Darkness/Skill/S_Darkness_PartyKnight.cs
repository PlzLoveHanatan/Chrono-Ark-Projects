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
    /// Party Knight
    /// </summary>
    public class S_Darkness_PartyKnight : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", (BChar.GetStat.maxhp / 3).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int barrierHP = (int)(BChar.GetStat.maxhp / 3);
            Targets[0].BuffAdd(ModItemKeys.Buff_S_Darkness_StubbornKnight, BChar, false, 0, false, -1, false).BarrierHP += barrierHP;

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Darkness_PartyKnight, this.BChar, this.BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);
            skill.AutoDelete = 1;
            skill.isExcept = true;
        }
    }
}