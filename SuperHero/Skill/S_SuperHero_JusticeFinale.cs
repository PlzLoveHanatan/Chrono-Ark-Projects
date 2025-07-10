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
using MonoMod.ModInterop;
namespace SuperHero
{
    /// <summary>
    /// <color=#8B00FF>Justice ☆ Finale</color>
    /// </summary>
    public class S_SuperHero_JusticeFinale : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
            CanUseStun = true;
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
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);

            var superHero = ModItemKeys.Character_SuperHero;
            var allies = BattleSystem.instance.AllyTeam.AliveChars.Where(x => x != null && x.Info.KeyData != superHero);
            var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Concat(allies);
            foreach (var target in enemies)
            {
                target.HPToZero();
                if (!target.IsDead)
                {
                    target.Dead(false, false);
                }
            }
        }
    }
}