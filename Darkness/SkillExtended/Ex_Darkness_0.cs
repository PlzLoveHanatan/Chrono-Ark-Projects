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
    public class Ex_Darkness_0 : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            OnePassive = true;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (BChar.BarrierHP >= 15)
            {
                MySkill.APChange = -1;
                base.SkillParticleOn();

                return;
            }

            base.SkillParticleOff();
        }
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 2;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.maxhp * 0.5f);
        }
    }
}