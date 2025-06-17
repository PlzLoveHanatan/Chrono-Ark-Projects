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
using System.Net;
namespace Darkness
{
    /// <summary>
    /// Darkness
    /// Passive:
    /// </summary>
    public class P_Darkness : Passive_Char, IP_PlayerTurn, IP_DamageTakeChange
    {
        public override void Init()
        {
            OnePassive = true;
            base.Init();
        }
        public override void FixedUpdate()
        {
            PlusStat.DeadImmune = MyChar.LV * 10 - 5;
        }
        public void Turn()
        {
            var buff = ModItemKeys.Buff_B_Darkness_Armorgasm;
            var buffReturn = BChar.BuffReturn(buff, false);
            if (buffReturn == null)
            {
                BChar.BuffAdd(buff, BChar, false, 0, false, -1, false);
            }

            if (MyChar.LV >= 3)
            {
                int barrierHP = (int)(BChar.GetStat.maxhp * 0.2f);
                BChar.BuffAdd(ModItemKeys.Buff_S_Darkness_StubbornKnight_0, BChar, false, 0, false, -1, false).BarrierHP += barrierHP;
            }

            if (MyChar.LV >= 4)
            {
                var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).Random(BChar.GetRandomClass().Main, 1);
                var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish;
                int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, enemies.Count);
                BattleChar randomTarget = enemies[randomIndex];
                foreach (var b in debuffs)
                {
                    randomTarget.BuffAdd(b.BuffData.Key, BChar, false, 999, false, -1, false);
                    b.SelfDestroy();
                }
            }
        }
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (MyChar.LV >= 2)
            {
                if (!Preview && Dmg >= 1)
                {
                    if (MyChar.LV >= 5)
                        Dmg = (int)(Dmg * 0.4f);

                    else
                        Dmg = (int)(Dmg * 0.85f);
                }
                if (Dmg <= 1)
                {
                    Dmg = 1;
                }
            }
            return Dmg;
        }
    }
}