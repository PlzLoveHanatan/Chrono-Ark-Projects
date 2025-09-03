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
namespace Darkness
{
	/// <summary>
	/// Delightful Defense ♡
	/// </summary>
    public class B_Darkness_DelightfulDefense : Buff, IP_TurnEndButtonEnemy, IP_BuffAddAfter
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(BChar.GetStat.maxhp * 0.5f)).ToString());
        }

        public void TurnEndButtonEnemy()
        {
            if (StackNum >= 1)
            {
                for (int i = 0; i < StackNum; i++)
                {
                    BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.maxhp * 0.5f);
                }
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker.Info.KeyData != ModItemKeys.Character_Darkness && addedbuff == this)
            {
                SelfDestroy();
            }
        }
    }
}