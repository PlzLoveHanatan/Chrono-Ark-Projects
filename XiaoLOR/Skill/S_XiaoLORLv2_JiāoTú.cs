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
using EmotionSystem;
namespace XiaoLOR
{
	/// <summary>
	/// Jiāo Tú
	/// </summary>
    public class S_XiaoLORLv2_JiāoTú : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 3)
            {
                this.MySkill.APChange = -1;
                base.SkillParticleOn();
                return;
            }
            else
            {
				base.SkillParticleOff();
			}
            
        }
    }
}