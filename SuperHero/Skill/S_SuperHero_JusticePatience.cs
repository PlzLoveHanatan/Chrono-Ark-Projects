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
    /// Justice ☆ Patience
    /// </summary>
    public class S_SuperHero_JusticePatience : Skill_Extended
    {
        public override void Init()
        {
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_WitchBoss_Ex_0).Particle_Path;
        }

        public override void FixedUpdate()
        {
            CanUseStun = true;

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
            Utils.CreateSkill(BChar, ModItemKeys.Skill_S_SuperHero_JusticePatience, true, true, 1, 1, true);

            var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).Random(BChar.GetRandomClass().Main, 1);
            foreach (var debuff in debuffs)
            {
                debuff.SelfDestroy();
            }
        }
    }
}