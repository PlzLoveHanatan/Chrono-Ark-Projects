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
using NLog.Targets;
namespace EmotionalSystem
{
	/// <summary>
	/// The Fairies' Care
	/// The target gains Healing Gauge equal to their max HP, then restores up to 20 HP.
	/// <color=#919191>The fairies protect our employees. Everything will be peaceful while you are under the fairies' care.</color>
	/// </summary>
    public class S_Abnormality_HistoryLv1_TheFairiesCare_Neg : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("TheFairiesCare", 100f, null, 0f, null, null, false, false);

            this.BChar.Recovery = this.BChar.GetStat.maxhp;
            int missingHP = this.BChar.GetStat.maxhp - this.BChar.HP;
            int healAmount = Mathf.Min(10, missingHP);
            this.BChar.Heal(this.BChar, healAmount, false, false, null);
        }
    }
}