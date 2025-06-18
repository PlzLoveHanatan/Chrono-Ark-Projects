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
            return Target.GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayDarknessSound(SkillD, BChar);
            var target = Targets[0];

            if (!target.Info.Ally)
                if (BChar.BarrierHP >= 15)
                    target.BuffAdd(ModItemKeys.Buff_B_Darkness_TrialofWeakness, BChar, false, 999, false, -1, false);

            if (BChar.BarrierHP >= 25)
                target.BuffAdd(ModItemKeys.Buff_B_Darkness_BustyTaunt, BChar, false, 999, false, -1, false);

        }
    }
}
