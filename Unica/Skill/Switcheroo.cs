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
namespace Unica
{
	/// <summary>
	/// Switcheroo
	/// Sheathe : Draw 1 skill.
	/// </summary>
    public class Switcheroo : SkillExtedned_IlyaP
    {       
        public override void IlyaWaste()
        {
            BattleSystem.instance.AllyTeam.Draw();
            this.BChar.BuffAdd(ModItemKeys.Buff_DoubleDown, this.BChar, false, 0, false, -1, false);
        }
    }
}