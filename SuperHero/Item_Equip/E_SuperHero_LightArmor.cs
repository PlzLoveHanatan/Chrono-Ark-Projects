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
    public class E_SuperHero_LightArmor : EquipBase, IP_PlayerTurn, IP_BuffAddAfter, IP_DamageTakeChange
    {
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker == BChar && Utils.SuperHeroDebuff.Contains(addedbuff.BuffData.Key))
            {
                BuffTaker.BuffRemove(addedbuff.BuffData.Key, true);
            }
        }

        public void Turn()
        {
            var lightArmor = ModItemKeys.Buff_B_E_SuperHero_LightArmor;
            if (BChar.BuffReturn(lightArmor, false) == null)
            {
                Utils.AddBuff(BChar, BattleSystem.instance.DummyChar, lightArmor);
            }
        }

        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (Hit == BChar && User == Utils.SuperHero && !Utils.SuperVillainMod(User))
            {
                Dmg = Dmg / 2;
            }
            return Dmg;
        }
    }
}