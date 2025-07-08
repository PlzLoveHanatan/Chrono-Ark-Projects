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
using System.Numerics;
namespace SuperHero
{
	/// <summary>
	/// Plot Armor
	/// </summary>
    public class B_SuperHero_PlotArmor : Buff, IP_DamageTakeChange, IP_PlayerTurn, IP_Awake, IP_BuffAddAfter
    {
        public override string DescExtended()
        {
            var complex = BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false);
            if (BattleSystem.instance != null && complex != null)
            {
                return base.DescExtended().Replace("&a", ((int)(complex.StackNum * 3)).ToString());
            }
            return base.DescExtended().Replace("&a", 0.ToString());
        }

        public override void Init()
        {
            PlusStat.Strength = true;
        }

        public void Awake()
        {
            if (BChar.Info.Passive is P_SuperHero superHero)
                superHero.PlotArmor = true;
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
            var complex = BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false);
            if (complex != null)
            {
                BChar.Heal(BattleSystem.instance.DummyChar, (int)(complex.StackNum * 3f), false, true, null);

                Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_DummyHeal, BChar, BChar.MyTeam);
                healingParticle.PlusHit = true;
                healingParticle.FreeUse = true;

                BChar.ParticleOut(healingParticle, BChar);
            }
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_PlotArmor;
            if (addedbuff.BuffData.Key == buff)
            {
                if (BuffTaker.Info.KeyData != ModItemKeys.Character_SuperHero)
                {
                    BuffTaker.BuffRemove(buff, true);
                }
            }
        }
    }
}