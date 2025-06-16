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
using Spine;
namespace Darkness
{
    /// <summary>
    /// Last Stand
    /// This skill always lands if you have barrier remaining. Deal additional &a damage based on 
    /// </summary>
    public class S_Darkness_LastStand : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;
        }

        public override void FixedUpdate()
        {
            OnePassive = true;
            if (BChar.BarrierHP >= 1)
            {
                base.SkillParticleOn();
                return;
            }

            base.SkillParticleOff();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int Barrierhp = 0;

            foreach (BattleAlly battleAlly in BChar.MyTeam.AliveChars)
            {
                Barrierhp += battleAlly.BarrierHP;
            }
            if (Barrierhp >= 15)
            {
                MySkill.MySkill.NODOD = true;
                MySkill.MySkill.IgnoreTaunt = true;
                SkillBaseFinal.Target_BaseDMG += (int)(Barrierhp * 0.2f);
                return;
            }
            else
            {
                SkillBaseFinal.Target_BaseDMG += (int)(Barrierhp * 0.2f);
            }
        }
    }
}