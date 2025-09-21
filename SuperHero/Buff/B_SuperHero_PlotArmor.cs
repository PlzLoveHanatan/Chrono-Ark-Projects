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
    public class B_SuperHero_PlotArmor : Buff, IP_PlayerTurn, IP_Awake, IP_BuffAddAfter
    {
        public override string DescExtended()
        {
            int hp = (int)(BChar.GetStat.maxhp * 0.2f);
            return base.DescExtended().Replace("&a", hp.ToString());
        }

        public override void Init()
        {
            PlusStat.Strength = true;
            PlusStat.DMGTaken = -20;
        }

        public void Awake()
        {
            if (BChar.Info.Passive is P_SuperHero superHero)
            {
                superHero.PlotArmor = true;
                superHero.BecomeJusticeHero();
            } 
        }

        public void Turn()
        {
            int barrierValue = (int)(BChar.GetStat.maxhp * 0.2f);
            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_EgoShield, BChar, false, 0, false, -1, false).BarrierHP += barrierValue;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff.BuffData.Key == ModItemKeys.Buff_B_SuperHero_PlotArmor && BuffTaker != Utils.SuperHero)
            {
                BuffTaker.BuffRemove(ModItemKeys.Buff_B_SuperHero_PlotArmor);
            }
        }
    }
}