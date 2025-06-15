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
	/// <summary>
	/// Side Slash
	/// This skill always lands if you have 20 or more barrier remaining.
	/// </summary>
    public class S_Darkness_SideSlash : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            OnePassive = true;
            if (MySkill.MySkill.Basic)
            {
                MySkill.APChange = -1;
            }
            if (BChar.BarrierHP >= 20)
            {
                MySkill.MySkill.NODOD = true;
                base.SkillParticleOn();

                return;
            }

            base.SkillParticleOff();
        }
    }
}