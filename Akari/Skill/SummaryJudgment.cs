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
namespace Akari
{
	/// <summary>
	/// Summary Judgment (range)
	/// Discard a page with the lowest Cost
	/// If an Ammunition was discarded
	/// </summary>
    public class SummaryJudgment : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Gun_Normal", 100f, null, 0f, null, null, false, false);

            int discardedAmo = Utils.DiscardAndApplyAmmunition(BChar, 4, Targets[0]);

            if (discardedAmo > 0)
            {
                PlusSkillPerFinal.Damage = 40 * discardedAmo;
            }
        }
    }
}