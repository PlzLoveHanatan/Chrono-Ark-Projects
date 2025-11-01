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
    /// Iron Wall
    /// </summary>
    public class S_XiaoLORLv2_IronWall : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 4)
            {
                this.MySkill.APChange = -1;
                base.SkillParticleOn();
            }
            else
            {
				base.SkillParticleOff();
			}
        }

        public override string DescExtended(string desc)
        {
            int barrierValue = (int)(BChar.GetStat.def * 0.5f);
            return base.DescExtended(desc).Replace("&a", barrierValue.ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("Guard");

            var target = Targets[0];
            target.BuffAdd(GDEItemKeys.Buff_B_EnemyTaunt, this.BChar, false, 0, false, -1, false);

            if (BChar.EmotionLevel() >= 3)
            {
                int barrierValue = (int)BChar.GetStat.def;
                BChar.BuffAdd(GDEItemKeys.Buff_B_Control_12_0_T, this.BChar, false, 0, false, -1, false).BarrierHP += barrierValue;
            }
        }
    }
}