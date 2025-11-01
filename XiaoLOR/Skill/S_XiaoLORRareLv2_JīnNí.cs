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
	/// Jīn Ní
	/// </summary>
    public class S_XiaoLORRareLv2_JīnNí : Skill_Extended
    {
        private bool OneTarget;
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.Off();
            if (Targets.Count == 1)
            {
                this.On();
            }
            base.SkillUseSingle(SkillD, Targets);

            XiaoUtils.PlaySound("Fervid");

            if (OneTarget)
            {
                foreach (var target in Targets)
                {
                    if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                    {
                        Utils.ApplyBurn(target, this.BChar, 4);
                    }
                }
            }
            else
            {
                foreach (var target in Targets)
                {
                    if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                    {
                        Utils.ApplyBurn(target, this.BChar, 8);
                    }
                }
            }
        }
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 3)
            {
                this.MySkill.APChange = -1;
            }

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
            OneTarget = true;
        }
        public void Off()
        {
            OneTarget = false;
        }
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
    }
}