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
    /// Endless Despair
    /// </summary>
    public class B_Rinne_EndlessDespair : Buff, IP_Draw
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", StackNum.ToString());
        }

        public override void BuffStat()
        {
            PlusPerStat.Damage = -5 * StackNum;
            PlusStat.def = -5 * StackNum;
            PlusStat.hit = -5 * StackNum;
            PlusStat.dod = -5 * StackNum;
        }

        public IEnumerator Draw(Skill Drawskill, bool NotDraw)
        {
            BChar.Damage(BChar, StackNum, false, false, false, 0, false, false, false);

            yield return null;
            yield break;
        }
    }
}