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
        private bool SuperHero;
        public override void Init()
        {
            OnePassive = true;
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
            Utils.PlaySong(MySkill.MySkill.KeyID);
            if (BChar.Info.Passive is P_SuperHero Hero)
            {
                if (Hero != null && Hero.SuperHeroPassive)
                {
                    SuperHero = true;
                }
            }
            var superHero = ModItemKeys.Character_SuperHero;
            var allies = BattleSystem.instance.AllyTeam.AliveChars.Where(x => x != null && x.Info.KeyData != superHero);
            var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish;
            foreach (var target in enemies)
            {
                target?.BuffAdd(ModItemKeys.Buff_B_SuperHero_HerosSpotlight, BChar, false, 999, false, -1, false);
            }

            if (!SuperHero)
            {
                foreach (var ally in allies)
                {
                    ally?.BuffAdd(ModItemKeys.Buff_B_SuperHero_HerosSpotlight, BChar, false, 0, false, -1, false);
                }
            }
        }

        public void SkillUseAfter(Skill SkillD)
        {
            var superHero = ModItemKeys.Character_SuperHero;
            var buff = ModItemKeys.Buff_B_SuperHero_HerosSpotlight;
            var allies = BattleSystem.instance.AllyTeam.AliveChars.Where(x => x != null && x.Info.KeyData != superHero);
            var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish;

            foreach (var target in enemies)
            {
                if (target?.BuffReturn(buff, false) == null)
                {
                    target?.BuffAdd(buff, BChar, false, 999, false, -1, false);
                }
            }

            if (!SuperHero)
            {
                foreach (var ally in allies)
                {
                    if (ally?.BuffReturn(buff, false) == null)
                    {
                        ally?.BuffAdd(buff, BChar, false, 999, false, -1, false);
                    }
                }
            }
        }
    }
}