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
namespace Mikure
{
	/// <summary>
	/// Emergency Injection!
	/// </summary>
    public class E_Mikure_EmergencyInjection : EquipBase, IP_PlayerTurn
    {
        public override void Init()
        {
            PlusPerStat.Heal = 20;
            PlusStat.dod = 20;
        }

        public void Turn()
        {
            string injection = ModItemKeys.Buff_B_Mikure_E_EmergencyInjection;
            if (BChar.BuffReturn(injection, false) == null)
            {
                Utils.AddBuff(BChar, BattleSystem.instance.DummyChar, injection);
            }
        }
    }
}