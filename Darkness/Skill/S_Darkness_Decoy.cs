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
namespace Darkness
{
	/// <summary>
	/// Decoy
	/// <color=#50C878>Barrier</color> <color=#1E90FF>15</color>: Gain <color=#FF1493>Hurt Me Please ♡</color>.
	/// </summary>
    public class S_Darkness_Decoy : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayDarknessSound(SkillD, BChar);

            if (BChar.BarrierHP >= 20)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_Darkness_HurtMeMorePlease, BChar, false, 0, false, -1, false);
            }
        }
    }
}