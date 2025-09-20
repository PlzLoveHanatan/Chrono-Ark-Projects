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
	/// Overpowered Protagonist
	/// </summary>
    public class B_SuperHero_OverpoweredProtagonist : Buff, IP_HPChange, IP_Awake, IP_BuffAddAfter, IP_SkillUse_User
    {
        public override void Init()
        {
            PlusStat.atk = 10;
            //PlusStat.PlusCriDmg = 50;
        }

        public override string DescExtended()
        {
            string text = ModLocalization.OverPowered;
            if (!Utils.SuperHeroMod(BChar))
            {
                return base.DescExtended() + "\n" + text;
            }
            return base.DescExtended();
        }

        public void HPChange(BattleChar Char, bool Healed)
        {
            if (BChar.HP <= 0)
            {
                BChar.HP = 1;
            }
        }

        public void Awake()
        {
            if (BChar.Info.Passive is P_SuperHero superHero)
            {
                superHero.OverPowered = true;
            }

            if (BChar.HP <= 0)
            {
                BChar.HP = 1;
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff.BuffData.Key == ModItemKeys.Buff_B_SuperHero_OverpoweredProtagonist && BuffTaker != Utils.SuperHero)
            {
                BuffTaker.BuffRemove(ModItemKeys.Buff_B_SuperHero_OverpoweredProtagonist);
            }
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            bool neverLucky = RandomManager.RandomPer(BChar.GetRandomClass().Main, 100, 50);

            if (neverLucky && !Utils.SuperHeroMod(BChar))
            {
                Utils.AttackRedirect(BChar, SkillD, Targets);
            }
        }
    }
}