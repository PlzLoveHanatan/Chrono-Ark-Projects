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
    /// Burn
    /// </summary>
    public class B_Xiao_Burn : Buff, IP_TurnEnd
    {
        public int Burn;
        public override void Init()
        {
            this.OnePassive = true;
            this.isStackDestroy = true;
        }
        public void TurnEnd()
        {
            this.BChar.Damage(BattleSystem.instance.DummyChar, (int)(Burn * 2), false, true, true, 0, false, false, false);

            if (Burn >= 3)
            {
                Burn = Burn - Burn / 3;
            }
        }

        public override string DescExtended()
        {
            return base.DescExtended()
                .Replace("&a", ((int)Burn).ToString())
                .Replace("&b", ((int)Burn * 2).ToString());
        }
    }
}