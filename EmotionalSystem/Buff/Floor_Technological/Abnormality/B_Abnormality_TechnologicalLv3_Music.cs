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
	/// Pulse of the Machine
	/// </summary>
    public class B_Abnormality_TechnologicalLv3_Music : Buff, IP_DamageTakeChange, IP_DealDamage
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 35;
        }
        public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
        {
            if (Damage >= 1 && BChar == BChar.Info.Ally)
            {
                this.BChar.Heal(this.BChar, (int)Damage * 0.2f, false, false, null);
            }
        }
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (!Preview && Dmg >= 1)
            {
                Dmg = (int)(Dmg * 1.35f);
            }

            return Dmg;
        }
    }
}