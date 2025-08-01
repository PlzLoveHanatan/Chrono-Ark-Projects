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
namespace Urunhilda
{
	/// <summary>
	/// Succubus Mark
	/// </summary>
    public class B_Urunhilda_SuccubusMark_0 : Buff, IP_BuffAddAfter
    {
        public override void BuffStat()
        {
            PlusStat.DMGTaken = -5 * StackNum;
            PlusStat.dod = 5 * StackNum;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            string buff = ModItemKeys.Buff_B_Urunhilda_SuccubusMark_0;
            string debuff = ModItemKeys.Buff_B_Urunhilda_SuccubusMark_1;

            Utils.ReverseBuffs(BChar, BuffTaker, buff, debuff);
        }
    }
}