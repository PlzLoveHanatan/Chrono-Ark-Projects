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
            SkillParticleOn();
        }

        public override bool Terms()
        {
            return BChar.Info.KeyData == ModItemKeys.Character_SuperHero;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //Utils.SuperStats = true;
            Utils.PlaySong(MySkill.MySkill.KeyID);
            Utils.AddBuff(BChar, BattleSystem.instance.DummyChar, ModItemKeys.Buff_B_SuperHero_HeroComplex, 25);
            BattleSystem.DelayInput(Utils.SuperHeroModCheck(BChar, ModItemKeys.Buff_B_SuperHero_JusticeHero, false, true));
        }
    }
}