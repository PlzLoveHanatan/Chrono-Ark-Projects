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
    public class B_Aqua_DubiousBlessing : Buff, IP_Awake
    {
        public int AttackPower = 0;
        public int HealingPower = 0;
        public int Defense = 0;
        public int Evade = 0;
        public int Critical = 0;
        private bool FirstAwake;

        public void Awake()
        {
            if (FirstAwake)
            {
                AttackPower = 0;
                HealingPower = 0;
                Defense = 0;
                Critical = 0;
                Evade = 0;
                FirstAwake = false;
            }
        }

        public override void Init()
        {
            List<Action> AquaBuffs = new List<Action>();

            if (AttackPower != 20) AquaBuffs.Add(() => AttackPower = 20);
            if (HealingPower != 20) AquaBuffs.Add(() => HealingPower = 20);
            if (Defense != 20) AquaBuffs.Add(() => Defense = 20);
            if (Evade != 20) AquaBuffs.Add(() => Evade = 20);
            if (Critical != 20) AquaBuffs.Add(() => Critical = 20);

            if (AquaBuffs.Count == 0) return;

            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, AquaBuffs.Count);
            AquaBuffs[index].Invoke();
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