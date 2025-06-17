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
    /// Shield of Faith
    /// While this skill is counting, reduce all incoming damage by 30%, and gain &a barrier when you cast your own skill.
    /// </summary>
    public class S_Darkness_ShieldofFaith : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.maxhp * 0.6f)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayDarknessSound(SkillD, BChar);

            BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.maxhp * 0.6f);

            foreach (var e in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                e.BuffAdd(ModItemKeys.Buff_B_Darkness_BustyTaunt, BChar, false, 999, false, -1, false);
            }
        }
    }
}