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
namespace EmotionalSystem
{
	/// <summary>
	/// Strengthen
	/// </summary>
    public class B_EnemyAbnormality_Strengthen : Buff, IP_DamageTakeChange
    {
        public override void BuffStat()
        {
            base.BuffStat();
            PlusStat.dod = 15f;
            PlusStat.def = 15;
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
    }
}