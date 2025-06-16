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
    /// Crusader Domination
    /// If you have barrier remaining, apply an additional Destroy Weapon debuff, apply 'Hit Me Harder' to all target's.
    /// </summary>
    public class S_Darkness_CrusaderDomination : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_WitchBoss_Ex_0).Particle_Path;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (BChar.BarrierHP >= 15)
            {
                base.SkillParticleOn();
                return;
            }
            base.SkillParticleOff();
        }
        public override bool TargetHit(BattleChar Target)
        {
            return Target.GetBuffs(BattleChar.GETBUFFTYPE.DEBUFF, false, false).Count != 0;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.BarrierHP >= 15)
            {
                foreach (var b in Targets)
                {
                    if (!b.Info.Ally)
                    {
                        b.BuffAdd(ModItemKeys.Buff_B_Darkness_HitMeHarder, BChar, false, 0, false, -1, false);
                        b.BuffAdd(ModItemKeys.Buff_B_Darkness_TrialofWeakness, BChar, false, 0, false, -1, false);
                    }
                }
            }
        }
    }
}