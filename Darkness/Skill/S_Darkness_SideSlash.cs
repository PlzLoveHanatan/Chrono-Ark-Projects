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
	/// Side Slash
	/// This skill always lands if you have 20 or more barrier remaining.
	/// </summary>
    public class S_Darkness_SideSlash : Skill_Extended
    {
        private bool DarknessAttackMisses;
        public override void Init()
        {
            base.Init();
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path; 
        }

        public override void FixedUpdate()
        {
            if (BChar.BarrierHP >= 15)
            {
                MySkill.MySkill.NODOD = true;
                SkillParticleOn();
            }
            else
            {
                SkillParticleOff();
            }            
        }

        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            DarknessAttackMisses = true;
        }

        private IEnumerator Miss()
        {
            if (DarknessAttackMisses) yield break;
            Utils.PlayDarknessBattleDialogue2(MySkill, BChar);

            yield return null;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            DarknessAttackMisses = false;

            if (Utils.DarknessVoiceSkills)
            {
                if (BChar.BarrierHP >= 15)
                {
                    Utils.PlayDarknessBattleDialogue(MySkill, BChar);
                }
                else if (DarknessAttackMisses)

                {
                    Utils.TryPlayDarknessSound(SkillD, BChar);
                }
                else
                {
                    BattleSystem.DelayInput(Miss());
                }
            }
        }
    }
}