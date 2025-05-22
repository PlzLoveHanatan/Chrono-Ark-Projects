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
    /// Le Regole
    /// All allies deal +2 damage with their Offensive dice next Scene
    /// </summary>
    public class LeRegole : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.8f)).ToString());
        }

        public override void FixedUpdate()
        {
            if (this.BChar.BattleInfo.EnemyList.Count == 1)
            {
                base.SkillParticleOn();
                this.On();
                return;
            }
            base.SkillParticleOff();
            this.Off();
        }
        public void On()
        {
            this.SkillBasePlus.Target_BaseDMG = (int)(this.BChar.GetStat.atk * 0.8f);
        }
        public void Off()
        {
            this.SkillBasePlus.Target_BaseDMG = 0;
        }
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.Off();
            if (Targets.Count == 1)
            {
                this.On();
            }

            base.SkillUseSingle(SkillD, Targets);
            BChar.BuffAdd(ModItemKeys.Buff_B_LeRegole_NextTurn, this.BChar, false, 0, false, -1, false);

            MasterAudio.PlaySound("Melee_Normal2", 100f, null, 0f, null, null, false, false);
            MasterAudio.PlaySound("Gun_Reload1", 100f, null, 0f, null, null, false, false);

            Utils.CreateRandomAmmunition(BChar);
        }        
    }
}