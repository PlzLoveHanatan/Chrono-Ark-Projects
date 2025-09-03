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
    public class P_Darkness : Passive_Char, IP_PlayerTurn, IP_LevelUp
    {
        public override void Init()
        {
            OnePassive = true;
            base.Init();
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
                var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false).Random(BChar.GetRandomClass().Main, 1);
                //var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish;
                //int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, enemies.Count);
                //BattleChar randomTarget = enemies[randomIndex];
                foreach (var b in debuffs)
                {
                    //randomTarget.BuffAdd(b.BuffData.Key, BChar, false, 999, false, -1, false);
                    b.SelfDestroy();
                }
            }

            var hurtMeMore = ModItemKeys.Buff_B_Darkness_HurtMeMorePlease;
            var hurtMeMoreReturn = BChar.BuffReturn(hurtMeMore, false);

            if (MyChar.LV >= 6 && hurtMeMoreReturn == null)
            {
                BChar.BuffAdd(hurtMeMore, BChar, false, 0, false, -1, false);
            }
        }

        public void LevelUp()
        {
            int level = MyChar.LV;
            switch (level)
            {
                case 2:
                    IncreaseStats(MyChar, true, 10);
                    break;
                case 3:
                    IncreaseStats(MyChar);
                    break;
                case 4:
                    IncreaseStats(MyChar);
                    break;
                case 5:
                    IncreaseStats(MyChar, true, 15);
                    break;
                case 6:
                    IncreaseStats(MyChar);
                    break;
            }
        }

        public void IncreaseStats(Character character, bool isDamageTaken = false, int damageTaken = 0)
        {
            character.OriginStat.DeadImmune += 5;
            character.OriginStat.AggroPer += 10;

            if (isDamageTaken)
            {
                character.OriginStat.DMGTaken -= damageTaken;
            }
        }
    }
}