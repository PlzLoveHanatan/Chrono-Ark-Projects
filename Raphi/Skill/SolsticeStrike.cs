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
	/// Solstice Strike
	/// </summary>
    public class SolsticeStrike : SkillExtedned_IlyaP
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.atk * 0.5f)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Off();
            if (Targets.Count == 1)
            {
                Targets[0].BuffAdd(ModItemKeys.Buff_B_DivineDaze, BChar, false, 0, false, -1, false);
                On();
            }
            base.SkillUseSingle(SkillD, Targets);
        }
        public override void FixedUpdate()
        {
            if (BChar.BattleInfo.EnemyList.Count == 1)
            {
                base.SkillParticleOn();
                On();
                return;
            }
            base.SkillParticleOff();
            Off();
        }
        public void On()
        {
            SkillBasePlus.Target_BaseDMG = (int)(BChar.GetStat.atk * 0.5f);
        }
        public void Off()
        {
            SkillBasePlus.Target_BaseDMG = 0;
        }
        public override void Init()
        {
            base.Init();
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }
        public override void IlyaWaste()
        {
            foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
            {
                battleEnemy.BuffAdd(ModItemKeys.Buff_B_DivineDaze, BChar, false, 0, false, -1, false);
            }
        }
    }
}