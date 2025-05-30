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
namespace Raphi
{
	/// <summary>
	/// Refreshing Renewal
	/// Draw 1 additional skill next turn.
	/// </summary>
    public class RefreshingRenewal : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.instance.AllyTeam.Draw();

            if (BChar.Info.KeyData == ModItemKeys.Character_Raphi)
            {
                for (int i = 0; i < 2; i++)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection, BChar, false, 0, false, -1, false);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection_0, BChar, false, 0, false, -1, false);
                }
            }
        }
    }
}