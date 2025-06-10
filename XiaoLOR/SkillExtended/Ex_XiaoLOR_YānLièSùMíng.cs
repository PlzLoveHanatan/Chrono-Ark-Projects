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
using EmotionalSystem;
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
            this.PlusSkillPerFinal.Damage = 15;

            if (SkillD.Master == BChar)
            {
                foreach (var target in Targets)
                {
                    if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                    {
                        Utils.ApplyBurn(target, this.BChar, 3);

                        if (BChar.EmotionLevel() >= 3)
                        {
                            Utils.ApplyBurn(target, this.BChar, 3);
                        }
                    }
                }

                Utils.GiveEmotionsToChar(this.BChar, 2, SkillD.GetPosUI());

                if (BChar.EmotionLevel() >= 3)
                {
                    Utils.GiveEmotionsToChar(this.BChar, 2, SkillD.GetPosUI());
                }
            }
        }
    }
}