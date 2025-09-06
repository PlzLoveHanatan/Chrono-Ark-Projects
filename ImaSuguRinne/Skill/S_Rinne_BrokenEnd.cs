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
namespace ImaSuguRinne
{
	/// <summary>
	/// Broken End
	/// </summary>
    public class S_Rinne_BrokenEnd : Skill_Extended, IP_DamageChange
    {
        public override void Init()
        {
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_WitchBoss_Ex_0).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (CheckUsedSkills())
            {
                SkillParticleOn();
            }
            else
            {
                SkillParticleOff();
            }
        }

        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            Utils.AllyTeam.Draw(2);
            Utils.AddBuff(BChar, ModItemKeys.Buff_B_Rinne_SufferingCycle, 2);
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            
        }

        private bool CheckUsedSkills()
        {
            return BattleSystem.instance.BattleLogs.getSkills((BattleLog log) => log.WhoUse.Info.KeyData == ModItemKeys.Character_Rinne, (Skill skill) => !skill.FreeUse, BattleSystem.instance.TurnNum).Count >= 2;
        }

        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (CheckUsedSkills())
            {
                Cri = true;
            }
            return Damage;
        }
    }
}