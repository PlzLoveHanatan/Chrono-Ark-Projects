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
using System.Diagnostics.Contracts;
namespace SuperHero
{
	/// <summary>
	/// Plot Armor
	/// </summary>
    public class B_SuperHero_PlotArmor : Buff, IP_DamageTakeChange, IP_PlayerTurn
    {
        public override void Init()
        {
            PlusStat.Strength = true;
        }

        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (!Preview && Dmg >= 1)
            {
                Dmg = (int)(Dmg * 0.85f);
            }
            if (Dmg <= 1)
            {
                Dmg = 1;
            }
            return Dmg;
        }

        public void Turn()
        {
            var complex = BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false) as B_SuperHero_HeroComplex;
            BChar.Heal(BattleSystem.instance.DummyChar, (int)(complex.HeroComplex * 5f), false, true, null);

            Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_DummyHeal, BChar, BChar.MyTeam);
            healingParticle.PlusHit = true;
            healingParticle.FreeUse = true;

            BChar.ParticleOut(healingParticle, BChar);
        }
    }
}