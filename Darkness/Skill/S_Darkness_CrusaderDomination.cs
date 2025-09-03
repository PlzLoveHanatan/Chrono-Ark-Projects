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
using NLog.Targets;
using ChronoArkMod.ModEditor;
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
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_WitchBoss_Ex_0).Particle_Path;
        }

        public override bool TargetHit(BattleChar Target)
        {
            return Target.GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (BChar.BarrierHP >= 15)
            {
                SkillParticleOn();
            }
            else
            {
                SkillParticleOff();
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally)
                {
                    if (BChar.BarrierHP >= 15)
                    {
                        target.BuffAdd(ModItemKeys.Buff_B_Darkness_BustyTaunt, BChar, false, 0, false, -1, false);
                    }

                    if (BChar.BarrierHP >= 30)
                    {
                        target.BuffAdd(ModItemKeys.Buff_B_Darkness_TrialofWeakness, BChar, false, 0, false, -1, false);
                        target.BuffAdd(ModItemKeys.Buff_B_Darkness_BustyTaunt, BChar, false, 0, false, -1, false);
                    }
                }
            }
            Utils.TryPlayDarknessSound(SkillD, BChar);
        }
    }
}
