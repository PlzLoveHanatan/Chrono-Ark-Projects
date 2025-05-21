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
	/// Bleed
	/// </summary>
    public class B_EmotionalSystem_Bleed : Buff, IP_PlayerTurn
    {
        public int Bleed;

        public void Turn()
        {
            if (Bleed <= 2)
            {
                SelfDestroy();
            }
        }
        public override void Init()
        {
            this.OnePassive = true;
            this.isStackDestroy = true;
            //base.PlusDamageTick = Bleed * 2;
        }
        public override void TurnUpdate()
        {
            this.BChar.Damage(BattleSystem.instance.DummyChar, (int)(Bleed * 2), false, true, true, 0, false, false, false);

            if (Bleed >= 3)
            {
                Bleed = Bleed - Bleed / 3;
            }
            else if (Bleed == 2)
            {
                Bleed = 1;
            }
        }

        public override string DescExtended()
        {
            return base.DescExtended()
                .Replace("&a", ((int)Bleed).ToString())
                .Replace("&b", ((int)Bleed * 2).ToString());
        }
    }
}