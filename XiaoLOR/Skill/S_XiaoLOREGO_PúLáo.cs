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
using NLog.Targets;
namespace XiaoLOR
{
    /// <summary>
    /// PúLáo
    /// </summary>
    public class S_XiaoLOREGO_PúLáo : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Countdown = 3;
            Once = true;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 4)
            {
                base.SkillParticleOn();
                return;
            }
            base.SkillParticleOff();
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            XiaoUtils.PlaySound("EGOHit");

            var target = Targets[0];
            Utils.ApplyBurn(target, this.BChar, 2);

            if (BChar.EmotionLevel() >= 4)
            {
                BattleSystem.DelayInput(this.AdditionalAttack(Targets[0]));
            }
        }

        public IEnumerator AdditionalAttack(BattleChar Target)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_XiaoLOREGO_PúLáo, this.BChar, this.BChar.MyTeam);
            skill.PlusHit = true;
            skill.FreeUse = true;

            Utils.ApplyBurn(Target, this.BChar, 2);

            if (this.BChar != null && !this.BChar.Dummy && !this.BChar.IsDead)
            {
                if (Target.IsDead)
                {
                    this.BChar.ParticleOut(this.MySkill, skill, this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main));
                }
                else
                {
                    this.BChar.ParticleOut(this.MySkill, skill, Target);
                }
            }
            yield break;
        }
    }
}