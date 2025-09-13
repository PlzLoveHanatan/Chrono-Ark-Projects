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
namespace ImaSuguRinne
{
    /// <summary>
    /// Rinne
    /// Passive:
    /// </summary>
    public class P_Rinne : Passive_Char, IP_CriPerChange
    {
        public override void Init()
        {
            OnePassive = true;
        }

        public override void FixedUpdate()
        {
            PlusStat.DeadImmune = 100;
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
    }
}