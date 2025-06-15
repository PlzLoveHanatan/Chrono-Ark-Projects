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
	/// Iron Maiden Mode
	/// </summary>
    public class B_Darkness_IronMaidenMode : Buff, IP_DamageTakeChange, IP_PlayerTurn
    {
        public override void Init()
        {
            base.Init();
            PlusStat.AggroPer = 50;
        }

        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (!Preview && Dmg >= 1)
            {
                Dmg = (int)(Dmg * 0.5f);
            }
            if (Dmg <= 1)
            {
                Dmg = 1;
            }
            return Dmg;
        }

        public void Turn()
        {
            foreach (var e in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                e.BuffAdd(ModItemKeys.Buff_B_Darkness_BustyTaunt, BChar, false, 999, false, -1, false);
            }
        }
    }
}