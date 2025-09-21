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
using Steamworks;
using static UnityEngine.ParticleSystem;
namespace SuperHero
{
    /// <summary>
    /// The Applause Never Ends
    /// </summary>
    public class S_SuperHero_TheApplauseNeverEnds : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.maxhp * 0.2f)).ToString());
        }

        public override void Init()
        {
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Ex_LucyD_18_SwimDLC).Particle_Path;
        }

        public override void FixedUpdate()
        {
            SkillParticleOn();
        }

        public override bool Terms()
        {
            return BChar.Info.KeyData == ModItemKeys.Character_SuperHero;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int barrierValue = (int)(BChar.GetStat.maxhp * 0.2f);
            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_EgoShield, BChar, false, 0, false, -1, false).BarrierHP += barrierValue;
        }
    }
}