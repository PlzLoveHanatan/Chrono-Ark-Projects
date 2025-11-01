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
	/// Bì Àn
	/// </summary>
    public class S_XiaoLORLv2_BìÀn : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (BChar.EmotionLevel() >= 4)
            {
                this.MySkill.APChange = -1;
                base.SkillParticleOn();
                return;
            }
            else if (BChar.EmotionLevel() >= 3)
            {
                base.SkillParticleOn();
                return;
            }
            base.SkillParticleOff();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("Bi");
            Utils.ApplyBurn(Targets[0], this.BChar, 2);

            if (BChar.EmotionLevel() >= 3)
            {
                BattleSystem.instance.AllyTeam.CharacterDraw(Targets[0], null);
            }
        }
    }
}