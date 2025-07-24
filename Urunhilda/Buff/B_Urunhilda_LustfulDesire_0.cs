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
using JetBrains.Annotations;
namespace Urunhilda
{
	/// <summary>
	/// Only Male characters can have this buff.
	/// </summary>
    public class B_Urunhilda_LustfulDesire_0 : Buff, IP_BuffAddAfter, IP_CriPerChange
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 5 * StackNum;
            PlusStat.hit = 5 * StackNum;
            PlusStat.HitMaximum = true;
        }
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            if (Target.NullCheck())
            {
                return;
            }
            int num = Target.HitPerNum(skill.Master, skill, false);
            int num2 = 0;
            if (num > 100)
            {
                num2 = num - 100;
            }
            if (num2 > 0)
            {
                CriPer += (float)num2;
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            string buff = ModItemKeys.Buff_B_Urunhilda_LustfulDesire_0;
            string debuff = ModItemKeys.Buff_B_Urunhilda_LustfulDesire_1;

            Utils.ReverseBuffs(BuffUser, BuffTaker, buff, debuff);
        }
    }
}