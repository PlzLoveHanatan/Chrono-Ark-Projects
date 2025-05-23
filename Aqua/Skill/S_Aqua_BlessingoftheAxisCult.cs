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
namespace Aqua
{
	/// <summary>
	/// Blessing of the Axis Cult
	/// Cost is reduced by 1 if this skill is a fixed ability.
	/// </summary>
    public class S_Aqua_BlessingoftheAxisCult : Skill_Extended
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
            }
        }
    }
}