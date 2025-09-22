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
	/// Only <color=#FFA500>Super Hero</color> can use this skill.
	/// Draw 1 skill if this skill Exchanged or Discarded.
	/// </summary>
    public class S_SuperHero_IntheNameofJustice_1 : Skill_Extended
    {
        public override void Init()
        {
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override string DescExtended(string desc)
        {
            string text = Utils.SuperHeroMod(BChar) ? ModLocalization.InTheNameOfJustice_3 : ModLocalization.InTheNameOfJustice_2;
            return base.DescExtended(desc).Replace("Description", text);
        }

        public override void FixedUpdate()
        {
            if (!Utils.SuperVillainMod(BChar))
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
            Utils.ApplyJusticeMark(BChar);
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);
        }
    }
}