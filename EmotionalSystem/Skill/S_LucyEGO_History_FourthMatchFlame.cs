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
namespace EmotionalSystem
{
	/// <summary>
	/// Fourth Match Flame
	/// Inflict 5 <color=#f8181c>Burn</color>.
	/// If facing 1 enemy, inflict 3 additional <color=#f8181c>Burn</color>. 
	/// </summary>
    public class S_LucyEGO_History_FourthMatchFlame : Ex_EmotionalSystem_EGO
    {
        private bool OneTarget;
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
            Countdown = 3;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Cry", 100f, null, 0f, null, null, false, false);

            this.Off();
            if (Targets.Count == 1)
            {
                this.On();
            }
            base.SkillUseSingle(SkillD, Targets);


            if (OneTarget)
            {
                Utils.ApplyBurn(Targets[0], this.BChar, 15);
            }
            else
            {
                foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
                {
                    Utils.ApplyBurn(battleEnemy, this.BChar, 10);
                }
            }
        }
        public override void FixedUpdate()
        {
            if (this.BChar.BattleInfo.EnemyList.Count == 1)
            {
                base.SkillParticleOn();
                this.On();
                return;
            }
            base.SkillParticleOff();
            this.Off();
        }
        public void On()
        {
            OneTarget = true;
        }
        public void Off()
        {
            OneTarget = false;
        }
    }
}