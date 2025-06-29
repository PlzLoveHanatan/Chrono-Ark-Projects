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
using EmotionalSystem;
namespace XiaoLOR
{
    /// <summary>
    /// Coordinated Assault
    /// If facing 1 enemy, damage is increased by &a.
    /// While this skill is under countdown, apply "A Fighter that Never Retreats" to all allies.
    /// All allies' attacks inflict 1 <color=#f8181c>Burn</color>.
    /// When an ally is attacked, the attacker receives 1 <color=#f8181c>Burn</color>.
    /// At Emotional Level 3 or higher inflict 1 additional <color=#f8181c>Burn</color>. 
    /// </summary>
    public class S_XiaoLOR_CoordinatedAssault : Skill_Extended, IP_SkillCastingStart, IP_SkillCastingQuit
    {
        public void SkillCasting(CastingSkill ThisSkill)
        {
            foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
            {
                battleChar.BuffAdd(ModItemKeys.Buff_B_XiaoLOR_AFighterthatNeverRetreats, this.BChar, false, 0, false, -1, false);
            }
        }
        public void SkillCastingQuit(CastingSkill ThisSkill)
        {
            foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
            {
                battleChar.BuffRemove(ModItemKeys.Buff_B_XiaoLOR_AFighterthatNeverRetreats, true);
            }
        }
    }
}