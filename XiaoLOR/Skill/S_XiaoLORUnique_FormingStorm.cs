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
    /// At the start of the next turn, create a 0-cost "Raging Storm Harm" in hand.
    /// </summary>
    public class S_XiaoLORUnique_FormingStorm : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Once = true;
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
            base.SkillParticleOff();
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.BChar.BuffAdd(ModItemKeys.Buff_B_XiaoLORUnique_ForminStorm, this.BChar, false, 0, false, -1, false);

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 3);
                }
            }
        }
    }
}