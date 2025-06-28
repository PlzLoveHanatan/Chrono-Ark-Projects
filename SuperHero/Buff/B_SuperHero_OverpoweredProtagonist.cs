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
namespace SuperHero
{
	/// <summary>
	/// Overpowered Protagonist
	/// </summary>
    public class B_SuperHero_OverpoweredProtagonist : Buff, IP_HPChange, IP_Awake
    {
        public override void Init()
        {
            PlusStat.hit = 50;
            PlusStat.cri = 50;
            PlusStat.PlusCriDmg = 50;
            PlusStat.HitMaximum = true;
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
    }
}