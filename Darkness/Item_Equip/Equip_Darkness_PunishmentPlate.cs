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
	/// Punishment Plate ♡
	/// </summary>
    public class Equip_Darkness_PunishmentPlate : EquipBase
    {
        public override void Init()
        {
            PlusStat.dod = -100;
            PlusStat.AggroPer = 100;
            PlusPerStat.MaxHP = 100;
            PlusStat.def = 25;
            PlusStat.DeadImmune = 25;
            PlusStat.RES_DEBUFF = 25f;
            PlusStat.RES_CC = 25f;
            PlusStat.RES_DOT = 25f; 
        }
    }
}