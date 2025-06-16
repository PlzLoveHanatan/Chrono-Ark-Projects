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
using Spine;
namespace Darkness
{
    public class Ex_Darkness_1 : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            OnePassive = true;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (BChar.BarrierHP >= 15)
            {
                base.SkillParticleOn();
                return;
            }
            base.SkillParticleOff();
        }

        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 2;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.BarrierHP >= 15 && SkillD.Master == BChar)
            {
                Skill cloneSkill = MySkill.CloneSkill(true, null, null, true);
                BattleSystem.DelayInputAfter(AdditionalAttack(cloneSkill, Targets[0]));
            }
        }

        public IEnumerator AdditionalAttack(Skill AttackSkill, BattleChar Target)
        {
            yield return new WaitForSeconds(0.2f);
            bool AdditionalHit = true;
            if (Target.IsDead || !AdditionalHit) yield break;

            BChar.ParticleOut(AttackSkill, Target);

            AdditionalHit = false;

            yield break;
        }
    }
}