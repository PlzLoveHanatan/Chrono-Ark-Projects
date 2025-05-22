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
namespace Akari
{
    public class B_CombatReadiness : Buff, IP_DamageTake
    {
        public override void BuffStat()
        {
            PlusStat.Strength = true;
            PlusStat.DMGTaken = -10;
        }

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (Dmg >= 1)
            {
                MasterAudio.PlaySound("Gun_Reload1", 100f, null, 0f, null, null, false, false);

                Utils.CreateRandomAmmunition(BChar);

                SelfStackDestroy();
            }            
        }
    }
}
