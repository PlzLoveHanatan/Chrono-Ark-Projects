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
using NLog.Targets;
namespace Akari
{
    /// <summary>
    /// Shock Round (Range)
    /// Discard 2 pages with the lowest Cost from hand, and draw a page
    /// </summary>
    public class ShockRound : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(100f + BChar.GetStat.HIT_CC)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Gun_Normal", 100f, null, 0f, null, null, false, false);

            int discardedAmo = Utils.DiscardAndApplyAmmunition(BChar, 2, Targets[0]);

            if (discardedAmo > 0)
            {
                PlusSkillPerFinal.Damage = 20 * discardedAmo;

                if (discardedAmo >= 2)
                {
                    foreach (BattleChar target in Targets)
                    {
                        target.BuffAdd(GDEItemKeys.Buff_B_Common_Rest, BChar, false, (int)(100f + BChar.GetStat.HIT_CC), false, -1, false);
                    }
                }
            }
        }
    }
}
