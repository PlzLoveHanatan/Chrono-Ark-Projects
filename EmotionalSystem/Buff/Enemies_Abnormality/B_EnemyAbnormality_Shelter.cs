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
	/// Shelter from the 27th of March
	/// </summary>
    public class B_EnemyAbnormality_Shelter : Buff, IP_HPChange, IP_Awake, IP_DamageTake, IP_PlayerTurn
    {
        private bool canBeRemoved;

		public void Awake()
		{
            Utils.AddBuff(BChar, GDEItemKeys.Buff_B_EnemyTaunt);
		}

		public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
		{
            if (Dmg >= 25 && canBeRemoved)
            {
                SelfDestroy();
                BChar.Dead();
            }
		}

		public void HPChange(BattleChar Char, bool Healed)
        {
            if (Char.HP <= 0)
            {
                Char.HP = 1;
                Char.IsDead = false;
                EffectView.SimpleTextout(Char.GetPos(), ScriptLocalization.UI_Battle.Endure, true, 1f, false, 1f);
				canBeRemoved = true;
				//Utils.EnemyTeam.AliveChars.ForEach(e => Utils.AddBuff(e, BChar, ModItemKeys.Buff_B_EnemyAbnormality_Shelter_0));
				//SelfDestroy(false);
			}
        }

		public void Turn()
		{
			if (canBeRemoved)
			{
				SelfDestroy();
			}
		}
	}
}