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
    public class S_SuperHero_Rare_JusticeHero : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;
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

            foreach (var ally in Utils.AllyTeam.AliveChars)
            {
                foreach (var buff in Utils.SuperHeroDebuff)
                {
                    if (ally.BuffReturn(buff, false) != null)
                    {
                        ally.BuffRemove(buff, true);
                    }
                }
            }
            Utils.PlaySong(MySkill.MySkill.KeyID);
            BattleSystem.DelayInput(Utils.SuperHeroModCheck(BChar, true, false));
        }
    }
}