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
            if (!Utils.SuperHeroMod(BChar))
            {
                SkillParticleOn();
            }
        }

        public override bool Terms()
        {
            return BChar.Info.KeyData == ModItemKeys.Character_SuperHero;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);
            Utils.JusticeKillAll();
        }
    }
}