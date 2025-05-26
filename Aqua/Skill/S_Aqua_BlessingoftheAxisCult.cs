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
namespace Aqua
{
    /// <summary>
    /// Blessing of the Axis Cult
    /// Cost is reduced by 1 if this skill is a fixed ability.
    /// </summary>
    public class S_Aqua_BlessingoftheAxisCult : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
                MySkill.NotCount = true;
                base.SkillParticleOn();
                return;
            }

            base.SkillParticleOff();
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var aqua = ModItemKeys.Character_Aqua;
            var target = Targets[0];

            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == aqua && target.Info.Name != aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }
        }
    }
}