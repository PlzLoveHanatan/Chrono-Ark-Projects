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
    /// <color=#9400D3>Justice ☆ Darkest Hour</color>
    /// Only <color=#FFA500>Super Hero</color> can use this skill.
    /// <b>Kill all allies</b> except <color=#FFA500>Super Hero</color>.
    /// <color=#FFA500>Super Hero</color> gain Max <color=#FFD700>Hero Complex</color> and become a <color=#FF00FF>Super Villain</color>.
    /// <color=#919191><color=#FF00FF>Justice ☆</color> always win.</color>
    /// </summary>
    public class S_SuperHero_Rare_JusticeDarkestHour : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_WitchBoss_Ex_0).Particle_Path;
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
            Utils.PlaySong(MySkill.MySkill.KeyID);
            var superHero = ModItemKeys.Character_SuperHero;
            var heroComplex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var hero = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == superHero);
            var allies = BattleSystem.instance.AllyTeam.AliveChars.Where(x => x != null && x.Info.KeyData != superHero);
            if (superHero != null)
            {
                for (int i = 0; i < 25; i++)
                {
                    hero.BuffAdd(heroComplex, BChar, false, 0, false, -1, false);
                }
                foreach (var target in allies)
                {
                    Utils.ForceKill(target);
                }
            }
            else
            {
                BattleSystem.instance.AllyTeam.Draw();
                MySkill.isExcept = true;
            }
        }
    }
}