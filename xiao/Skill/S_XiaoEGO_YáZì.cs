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
namespace Xiao
{
    /// <summary>
    /// Yá Zì
    /// </summary>
    public class S_XiaoEGO_YáZì : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Once = true;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 4)
            {
                base.SkillParticleOn();
                return;
            }
            base.SkillParticleOff();
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("EGOHit", 100f, null, 0f, null, null, false, false);

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 2);
                }
            }

            if (BChar.EmotionLevel() >= 4)
            {
                this.BChar.BuffAdd(ModItemKeys.Buff_B_XiaoEGO_LièYànZhīYì, this.BChar, false, 0, false, -1, false);
            }
        }
    }
}