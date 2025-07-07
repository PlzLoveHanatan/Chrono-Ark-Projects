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
namespace SuperHero
{
    /// <summary>
    /// Limit Break
    /// Only Super Hero can use this skill.
    /// </summary>
    public class S_SuperHero_LimitBreak : Skill_Extended
    {
        public override void Init()
        {
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            base.SkillParticleOn();
        }

        public override bool Terms()
        {
            if (BChar.Info.KeyData == ModItemKeys.Character_SuperHero)
                return true;

            return false;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).ToList();
            foreach (var debuff in debuffs)
            {
                debuff.SelfDestroy();
            }

            foreach (var buff in BChar.Buffs)
            {
                if (buff.BuffData.LifeTime != 0f)
                {
                    foreach (StackBuff stackBuff in buff.StackInfo)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            stackBuff.RemainTime++;
                        }
                    }
                }
            }
        }
    }
}