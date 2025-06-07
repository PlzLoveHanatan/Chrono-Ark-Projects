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
namespace Xiao
{
	/// <summary>
	/// If this Attack lands restore 1 Mana.
	/// </summary>
    public class S_XiaoLv1_FrontalAssault : Skill_Extended
    {
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            BattleSystem.instance.AllyTeam.AP += 1;
        }
    }
}