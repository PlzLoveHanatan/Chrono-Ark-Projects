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
    /// Shield of Faith
    /// While this skill is counting, reduce all incoming damage by 30%, and gain &a barrier when you cast your own skill.
    /// </summary>
    public class S_Darkness_ShieldofFaith : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.maxhp * 0.5f)).ToString());
        }

        public override void Init()
        {
            base.Init();
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (BChar.BarrierHP >= 10)
            {
                MySkill.MySkill.NODOD = true;
                SkillParticleOn();
            }
            else
            {
                SkillParticleOff();
            }
        }


        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.BarrierHP >= 20)
            {
                BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.maxhp * 0.5f);
            }
            Utils.TryPlayDarknessSound(SkillD, BChar);
        }
    }
}