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
    /// Light ☆ Armor
    /// </summary>
    public class E_SuperHero_LightArmor : EquipBase, IP_DamageTake, IP_PlayerTurn, IP_BattleStart_Ones
    {
        public void BattleStart(BattleSystem Ins)
        {
            var buff = ModItemKeys.Buff_B_E_SuperHero_LightArmor;
            BChar.BuffAdd(buff, BChar, false, 0, false, -1, false);
        }

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var complex = User.BuffReturn(buff, false) as B_SuperHero_HeroComplex;
            if (User.Info.KeyData == ModItemKeys.Character_SuperHero && complex.StackNum < 25 && Dmg >= 1)
            {
                //int damage = 0;
                //damage = Dmg;
                resist = true;                

                //if (BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Count > 0)
                //{
                //    BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Random(BChar.GetRandomClass().Main).Damage(BChar, damage / 2, false, false, false, 0, false, false, false);
                //}
            }
        }

        public void Turn()
        {
            var buff = ModItemKeys.Buff_B_E_SuperHero_LightArmor;
            if (BChar.BuffReturn(buff, false) == null)
            {
                BChar.BuffAdd(buff, BChar, false, 0, false, -1, false);
            }
        }
    }
}