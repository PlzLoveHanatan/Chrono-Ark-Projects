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
	/// Judgement Ascension
	/// Discard the top skill in hand and draw skills per cost of the discarded skill (Max 2).
	/// </summary>
    public class JudgementAscension : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Skill> list = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill).ToList();

            int num = Math.Min(list[0].AP, 2);

            list[0].Delete(false);
            BattleSystem.instance.AllyTeam.Draw(num);
        }
    }
}