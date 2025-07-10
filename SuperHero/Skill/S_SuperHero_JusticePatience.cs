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

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_JusticePatience, this.BChar, this.BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);
            skill.AutoDelete = 1;
            skill.isExcept = true;

            var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).Random(BChar.GetRandomClass().Main, 1);
            foreach (var debuff in debuffs)
            {
                debuff.SelfDestroy();
            }

            var target = Targets[0];
            var markOfJustice = ModItemKeys.Buff_B_SuperHero_MarkofJustice;
            var buffs = target.GetBuffs(BattleChar.GETBUFFTYPE.ALL, false, false);

            if (target != null)
            {
                foreach (var buff in buffs)
                {
                    if (buff != null && buff.BuffData.Key != markOfJustice)
                    {
                        buff.SelfDestroy();
                    }
                }
            }
        }
    }
}