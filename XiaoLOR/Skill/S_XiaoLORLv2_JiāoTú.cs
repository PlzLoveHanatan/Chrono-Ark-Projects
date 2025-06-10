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
            if (BChar.EmotionLevel() >= 3 && this.MySkill.BasicSkill)
            {
                this.MySkill.APChange = -1;
                base.SkillParticleOn();
                return;
            }
            base.SkillParticleOff();
        }
        //public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        //{
        //    if (BChar.EmotionLevel() >= 3)
        //    {
        //        Utils.ApplyBurn(Targets[0], this.BChar, 2);
        //    }
        //}
    }
}