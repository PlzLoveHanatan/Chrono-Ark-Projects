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
namespace ImaSuguRinne
{
    /// <summary>
    /// Endless Sorrow
    /// This skill can be played repeatedly during this turn.
    /// </summary>
    public class S_Rinne_EndlessSorrow : Skill_Extended
    {
        public override void Init()
        {
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_WitchBoss_Ex_0).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (BChar.BattleInfo.EnemyList.Count == 1)
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
            Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_EndlessSorrow, true, true, 1, 1, true);

            if (BChar.BattleInfo.EnemyList.Count == 1)
            {
                Utils.AddDebuff(Targets[0], BChar, ModItemKeys.Buff_B_Rinne_SorrowEmbrace, 1);
                Utils.AddDebuff(Targets[0], BChar, ModItemKeys.Buff_B_Rinne_SorrowResonance, 1);
            }
        }
    }
}