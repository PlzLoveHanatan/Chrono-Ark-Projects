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
namespace Unica
{
	/// <summary>
	/// Low at Night, High at Day
	/// Sheathe : Restore 1 Mana.
	/// </summary>
    public class LowatNight_HighatDay : SkillExtedned_IlyaP
    {
        public override void IlyaWaste()
        {
            BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_DrawNextTurn, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(ModItemKeys.Buff_FavorableHand, this.BChar, false, 0, false, -1, false);
        }
    }
}