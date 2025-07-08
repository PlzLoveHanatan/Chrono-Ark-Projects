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
	/// Ego Shield
	/// </summary>
    public class B_SuperHero_EgoShield : Buff, IP_BuffAddAfter
    {
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_SecondAct;
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