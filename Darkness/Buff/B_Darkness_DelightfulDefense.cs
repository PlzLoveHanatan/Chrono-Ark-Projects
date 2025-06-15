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
    public class B_Darkness_DelightfulDefense : Buff, IP_PlayerTurn
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", (BChar.GetStat.maxhp * 0.7f).ToString());
        }

        public void Turn()
        {
            if (StackNum >= 1)
            {
                for (int i = 0; i < StackNum; i++)
                {
                    BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.maxhp * 0.7f);
                }
            } 
        }
    }
}