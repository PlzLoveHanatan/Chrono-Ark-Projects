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
using TileTypes;
namespace EmotionalSystem
{
    /// <summary>
    /// Energy Conversion
    /// </summary>
    public class B_EnemyAbnormality_EnergyConversion : Buff, IP_DamageTake, IP_BuffObject_Updata
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", Threshold.ToString());
        }

        private int Threshold;

        public override void Init()
        {
            base.Init();
            this.OnePassive = true;

            int step;

            if (BChar is BattleEnemy enemy && enemy.Boss)
            {
                step = BChar.GetStat.maxhp / 4;
            }
            else
            {
                step = BChar.GetStat.maxhp / 2;
            }

            int damageTaken = BChar.GetStat.maxhp - BChar.HP;

            if (damageTaken < step)
            {
                Threshold = step - damageTaken;
            }
            if (damageTaken >= step)
            {
                Threshold = 1;
            }
        }

        public void BuffObject_Updata(BuffObject obj)
        {
            string num = Threshold.ToString();
            obj.StackText.text = num;
        }


        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (Dmg > BChar.HP)
            {
                Threshold -= BChar.HP;
            }
            else
            {
                Threshold -= Dmg;
            }

            while (Threshold < 1)
            {
                if (BChar is BattleEnemy enemy && enemy.Boss)
                {
                    Threshold += BChar.GetStat.maxhp / 4;
                }
                else
                {
                    Threshold += BChar.GetStat.maxhp / 2;
                }

                BattleSystem.instance.AllyTeam.AP -= 1;
            }
        }
    }
}