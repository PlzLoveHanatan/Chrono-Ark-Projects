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
    /// Knight's Resolve
    /// Cost reduced by 1 if this skill is a fixed ability.
    /// </summary>
    public class S_Darkness_KnightsResolve : Skill_Extended
    {
        public override void FixedUpdate()
        {
            if (BChar.BarrierHP >= 20)
            {
                MySkill.APChange = -1;
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayDarknessSound(SkillD, BChar);
        }
    }
}