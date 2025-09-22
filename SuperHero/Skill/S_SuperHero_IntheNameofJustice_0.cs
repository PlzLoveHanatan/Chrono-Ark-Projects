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
    /// <color=#FF00FF>In the Name of Justice ☆</color>
    /// Draw 1 skill if this skill Exchanged or Discarded.
    /// </summary>
    public class S_SuperHero_IntheNameofJustice_0 : Skill_Extended
    {
        public override void Init()
        {
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_WitchBoss_Ex_0).Particle_Path;
        }

        public override string DescExtended(string desc)
        {
            string text = ModLocalization.InTheNameOfJustice_4;
            return base.DescExtended(desc).Replace("Description", text);
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
            Utils.ApplyJusticeMark(BChar, true);
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);
        }
    }
}