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
using System.Threading;
namespace Darkness
{
    /// <summary>
    /// Last Stand
    /// This skill always lands if you have barrier remaining. Deal additional &a damage based on 
    /// </summary>
    public class S_Darkness_LastStand : Skill_Extended
    {
        private int Barrier;
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", Barrier.ToString());
        }

        public override void Init()
        {
            OnePassive = true;
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;
        }

        public override void FixedUpdate()
        {
            OnePassive = true;
            int Barrierhp = 0;

            foreach (BattleAlly battleAlly in BChar.MyTeam.AliveChars)
            {
                Barrierhp += battleAlly.BarrierHP;
            }

            if (Barrierhp >= 1)
            {
                Barrier = (int)(Barrierhp * 0.4f);
            }
            else
            {
                Barrier = 0;
            }


            if (BChar.BarrierHP >= 15)
            {
                base.SkillParticleOn();
                return;
            }

            base.SkillParticleOff();
        }


        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayDarknessSound(SkillD, BChar);

            int Barrierhp = 0;

            foreach (BattleAlly battleAlly in BChar.MyTeam.AliveChars)
            {
                Barrierhp += battleAlly.BarrierHP;
            }

            if (Barrierhp >= 1)
            {
                MySkill.MySkill.NODOD = true;
                SkillBasePlus.Target_BaseDMG += (int)(Barrierhp * 0.4f);
            }

            else
            {
                SkillBasePlus.Target_BaseDMG = 0;
            }
        }
    }
}