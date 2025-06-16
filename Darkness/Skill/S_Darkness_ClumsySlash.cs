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
	/// Clumsy Slash
	/// </summary>
    public class S_Darkness_ClumsySlash : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            OnePassive = true;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
            }
            if (BChar.BarrierHP >= 15)
            {
                MySkill.MySkill.NODOD = true;
                base.SkillParticleOn();
                
                return;
            }

            base.SkillParticleOff();
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Darkness_SideSlash, BChar, BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);
        }
    }
}