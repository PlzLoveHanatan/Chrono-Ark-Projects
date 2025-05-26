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
namespace Aqua
{
    /// <summary>
    /// Fog of Blessings (and Mistakes)
    /// Apply "Aqua Veil" to all enemies, gain 20% chance to apply "Aqua Veil" to allies.
    /// </summary>
    public class S_Aqua_FogofBlessings : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            var targets = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Concat(BattleSystem.instance.AllyTeam.AliveChars);

            foreach (var target in targets)
            {
                target.BuffAdd(ModItemKeys.Buff_B_Aqua_AquaVeil, this.BChar, false, 0, false, -1, false);
            }
        }
    }
}