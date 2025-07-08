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
	/// Light ☆ Armor
	/// </summary>
    public class B_E_SuperHero_LightArmor : Buff, IP_BuffAddAfter
    {
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_MarkofJustice;
            if (addedbuff.BuffData.Key == buff)
            {
                if (BuffTaker == BChar)
                {
                    BuffTaker.BuffRemove(buff, true);
                }
            }
        }
    }
}