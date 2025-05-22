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
namespace EmotionalSystem
{
	/// <summary>
	/// Predation
	/// Deal non-lethal <color=purple>15 Pain damage</color> to all living allies and heal the user for half of the total damage dealt.
	/// <color=#919191>His stomach and face were ripped off, and his eyeballs and organs were damaged as if they were eaten by something. Meanwhile, the fairies had someone's blood and flesh smeared all over their mouths.</color>
	/// </summary>
    public class S_Abnormality_HistoryLv2_Predation_Neg : Skill_Extended
    {
        private int TotalHeal;
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            TotalHeal = 0;

            MasterAudio.PlaySound("Predation", 100f, null, 0f, null, null, false, false);

            foreach (BattleChar battleChar in this.BChar.MyTeam.AliveChars)
            {
                if (battleChar != this.BChar)
                {
                    var nonLethalDamage = GDEItemKeys.Buff_B_Momori_P_NoDead;

                    battleChar.BuffAdd(nonLethalDamage, this.BChar, false, 0, false, -1, false);
                    battleChar.Damage(this.BChar, 15, false, true, false, 0, false, false, false);
                    battleChar.BuffRemove(nonLethalDamage, false);
                    TotalHeal++;
                }
            }
            this.BChar.Heal(this.BChar, (15 * TotalHeal) * 0.6f, false, true, null);
        }
    }
}