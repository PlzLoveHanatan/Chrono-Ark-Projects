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
    /// <color=#FFD700>
    /// Only <color=#FFA500>Super Hero</color> can use this skill.
    /// Apply <color=#50C878>Hero's Spotlight</color> to all enemies and allies.
    /// </summary>
    public class S_SuperHero_Rare_ApotheosisofJustice : Skill_Extended, IP_SkillUse_User_After
    {
        public override void Init()
        {
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }
        public override string DescExtended(string desc)
        {
            string text = Utils.SuperHeroMod(BChar) ? ModLocalization.Apotheosis_1 : ModLocalization.Apotheosis_0;
            return base.DescExtended(desc).Replace("Description", text);
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
            Utils.PlaySong(MySkill.MySkill.KeyID);
            ApplySpotLight();
        }

        public void SkillUseAfter(Skill SkillD)
        {
            ApplySpotLight();
        }

        public void ApplySpotLight()
        {
            List<BattleChar> targets = Utils.EnemyTeam.AliveChars_Vanish;

            if (!Utils.SuperHeroMod(BChar))
            {
                targets.AddRange(Utils.AllyTeam.AliveChars.Where(x => x != Utils.SuperHero));
            }

            foreach (var target in targets)
            {
                if (target != null)
                {
                    Utils.AddDebuff(target, BChar, ModItemKeys.Buff_B_SuperHero_HerosSpotlight);
                }
            }
        }
    }
}