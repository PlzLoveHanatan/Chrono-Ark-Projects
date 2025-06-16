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
    /// Iron Maiden's Embrace
    /// When used create a party barrier equal &a <color=#FF7C34>(Max HP * 1.5)</color>.
    /// </summary>
    public class S_Darkness_Rare_IronMaidensEmbrace : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.maxhp * 0.5f)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.maxhp * 0.5f);
        }
    }
}