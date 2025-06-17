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
    /// Knight's Resolve
    /// Cost reduced by 1 if this skill is a fixed ability.
    /// </summary>
    public class S_Darkness_KnightsResolve : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            OnePassive = true;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
                base.SkillParticleOn();

                return;
            }

            base.SkillParticleOff();
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayDarknessSound(SkillD, BChar);
        }
    }
}