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
using System.Threading;
namespace Akari
{
	/// <summary>
	/// Ferrous Guard (Melee)
	/// </summary>
    public class FerrousGuard : Skill_Extended
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Melee_Normal", 100f, null, 0f, null, null, false, false);
        }
    }
}