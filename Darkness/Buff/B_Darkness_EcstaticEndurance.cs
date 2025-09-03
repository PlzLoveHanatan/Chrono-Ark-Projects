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
	/// Ecstatic Endurance
	/// </summary>
    public class B_Darkness_EcstaticEndurance : Buff, IP_HPChange, IP_Awake, IP_BuffAddAfter
    {
        public override void Init()
        {
            PlusStat.DeadImmune = 100;
            PlusStat.AggroPer = 100;
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
            if (BChar.HP <= 0)
            {
                BChar.HP = 1;
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