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
namespace Aqua
{
	/// <summary>
	/// Dubious Blessing
	/// </summary>
    public class B_Aqua_DubiousBlessing : Buff
    {
        private int AttackPower;
        private int HealingPower;
        private int Defense;
        private int Evade;
        private int Critical;

        public override void Init()
        {
            AttackPower = 0;
            HealingPower = 0;
            Defense = 0;
            Critical = 0;
            Evade = 0;

            int randomNum = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 6);
            switch (randomNum)
            {
                case 1:
                    AttackPower = 20;
                    break;
                case 2:
                    HealingPower = 20;
                    break;
                case 3:
                    Defense = 20;
                    break;
                case 4:
                    Evade = 20;
                    break;
                case 5:
                    Critical = 20;
                    break;
            }
        }

        public override void BuffStat()
        {
            PlusPerStat.Damage = AttackPower;
            PlusPerStat.Heal = HealingPower;
            PlusStat.def = Defense;
            PlusStat.cri = Critical;
            PlusStat.dod = Evade;
        }
    }
}