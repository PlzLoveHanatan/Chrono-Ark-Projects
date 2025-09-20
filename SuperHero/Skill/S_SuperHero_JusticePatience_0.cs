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
	/// <color=#FF4500>Justice ☆ Patience</color>
	/// This skill can be cast even if you are stunned.
	/// Only <color=#FFA500>Super Hero</color> can use this skill.
	/// This skill can be played repeatedly during this turn.
	/// Remove 1 random debuff from <color=#FFA500>Self</color>.
	/// </summary>
    public class S_SuperHero_JusticePatience_0 : Skill_Extended
    {
        public override void Init()
        {
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            CanUseStun = true;
            SkillParticleOn();
        }

        public override bool Terms()
        {
            return BChar.Info.KeyData == ModItemKeys.Character_SuperHero;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.UnlockSkillPreview(MySkill.MySkill.KeyID);

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_JusticePatience_0, this.BChar, this.BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);
            skill.AutoDelete = 1;
            skill.isExcept = true;

            var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).Random(BChar.GetRandomClass().Main, 1);
            foreach (var debuff in debuffs)
            {
                debuff.SelfDestroy();
            }
        }

    }
}