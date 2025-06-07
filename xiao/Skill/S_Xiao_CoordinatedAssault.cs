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
namespace Xiao
{
	/// <summary>
	/// Coordinated Assault
	/// If facing 1 enemy, damage is increased by &a.
	/// While this skill is under countdown, apply "A Fighter that Never Retreats" to all allies.
	/// All allies' attacks inflict 1 <color=#f8181c>Burn</color>.
	/// When an ally is attacked, the attacker receives 1 <color=#f8181c>Burn</color>.
	/// At Emotional Level 3 or higher inflict 1 additional <color=#f8181c>Burn</color>. 
	/// </summary>
    public class S_Xiao_CoordinatedAssault : Skill_Extended, IP_SkillCastingStart, IP_SkillCastingQuit
    {
        //public override string DescExtended(string desc)
        //{
        //    return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.5f)).ToString());
        //}
        //public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        //{
        //    foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
        //    {
        //        battleChar.BuffAdd(ModItemKeys.Buff_B_Xiao_AFighterthatNeverRetreats, this.BChar, false, 0, false, -1, false);
        //    }
        //}
        //public override void FixedUpdate()
        //{
        //    if (this.BChar.BattleInfo.EnemyList.Count == 1)
        //    {
        //        base.SkillParticleOn();
        //        this.On();
        //        return;
        //    }
        //    base.SkillParticleOff();
        //    this.Off();
        //}
        //public void On()
        //{
        //    this.SkillBasePlus.Target_BaseDMG = (int)(this.BChar.GetStat.atk * 0.5f);
        //}
        //public void Off()
        //{
        //    this.SkillBasePlus.Target_BaseDMG = 0;
        //}
        //public override void Init()
        //{
        //    base.Init();
        //    this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        //}
        public void SkillCasting(CastingSkill ThisSkill)
        {
            foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
            {
                battleChar.BuffAdd(ModItemKeys.Buff_B_Xiao_AFighterthatNeverRetreats, this.BChar, false, 0, false, -1, false);
            }
        }
        public void SkillCastingQuit(CastingSkill ThisSkill)
        {
            foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
            {
                battleChar.BuffRemove(ModItemKeys.Buff_B_Xiao_AFighterthatNeverRetreats, true);
            }
        }
    }
}