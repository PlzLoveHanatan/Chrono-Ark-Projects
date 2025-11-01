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
using EmotionSystem;
using NLog.Targets;
namespace XiaoLOR
{
	/// <summary>
	/// Inflict 3 <color=#f8181c>Burn</color> and gain 2 random Emotional Coins.Inflict 3 <color=#f8181c>Burn</color> and gain 2 random Emotional Coins.
	/// At Emotion Level 3 or higher, inflict 3 additional <color=#f8181c>Burn</color> and gain 2 more random Emotional Coins.
	/// At Emotion Level 3 or higher, inflict 3 additional <color=#f8181c>Burn</color> and gain 2 more random Emotional Coins.
	/// </summary>
    public class Ex_XiaoLOR_YānLièSùMíng : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsDamage && MainSkill.AP >= 1;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            PlusSkillPerFinal.Damage = 20;

            if (SkillD.Master == BChar)
            {
				Utils.ApplyBurn(Targets, BChar, 5);

				if (BChar.EmotionLevel() >= 3)
				{
					Utils.ApplyBurn(Targets, BChar, 5);
				}

				EmotionalManager.GetNegEmotion(BChar, SkillD.GetPosUI(), 3);
            }
        }
    }
}