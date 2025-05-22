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
	/// Tödlicher Akkord
	/// </summary>
    public class B_Abnormality_TechnologicalLv3_DarkFlame : Buff, IP_PlayerTurn
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 35;
        }
        public void Turn()
        {
            var enemy = BattleSystem.instance.EnemyTeam.AliveChars_Vanish;
            var buff = ModItemKeys.Buff_B_Abnormality_TechnologicalLv3_DarkFlame;
            if (enemy != null)
            {
                foreach (var character in enemy)
                {
                    if (character?.BuffReturn(buff, false) == null)
                    {
                        character.BuffAdd(buff, this.BChar, false, 0, false, -1, false);
                    }
                }
            }
        }
    }
}