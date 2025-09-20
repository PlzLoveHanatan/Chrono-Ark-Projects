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
using System.Runtime.InteropServices.WindowsRuntime;
namespace SuperHero
{
	/// <summary>
	/// Barrier of Light ☆
	/// </summary>
    public class B_Ex_SuperHero_BarrierofLight : Buff, IP_DamageTake, IP_BuffAddAfter
    {
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var complex = User.BuffReturn(buff, false) as B_SuperHero_HeroComplex;
            if (User.Info.KeyData == ModItemKeys.Character_SuperHero)
            {
                int damage = 0;
                damage = Dmg;
                resist = true;

                if (BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Count > 0)
                {
                    BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Random(BChar.GetRandomClass().Main).Damage(BChar, damage / 2, false, false, false, 0, false, false, false);
                }
                SelfStackDestroy();
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_MarkofJustice;
            if (addedbuff.BuffData.Key == buff)
            {
                if (BuffTaker == BChar)
                {
                    addedbuff.SelfDestroy();
                }
            }
        }
    }
}