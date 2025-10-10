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
        public override void Init()
        {
            base.Init();
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
            Countdown = 3;
        }

		public override void FixedUpdate()
		{
			if (BattleSystem.instance.EnemyList.Count == 1)
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
			Utils.PlaySound("Floor_History_Matchlight");
			if (Targets.Count == 1)
            {
                Utils.ApplyBurn(Targets[0], BChar, 20);
            }
            else
            {
				Utils.ApplyBurn(Targets[0], BChar, 10);
			}
        }
    }
}