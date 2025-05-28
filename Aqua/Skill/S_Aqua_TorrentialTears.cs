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
    /// Torrential Tears
    /// </summary>
    public class S_Aqua_TorrentialTears : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            foreach (var ally in BChar.MyTeam.AliveChars)
            {
                ally.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, BChar, false, 0, false, -1, false);
                ally.BuffAdd(ModItemKeys.Buff_B_Aqua_CryingShame, BChar, false, 0, false, -1, false);
            }

            bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 50);

            if (alwaysLucky)
            {
                BattleSystem.DelayInput(AdditionalCast(Targets[0]));
            }
        }

        public IEnumerator AdditionalCast(BattleChar Target)
        {

            yield return new WaitForSecondsRealtime(0.3f);

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_TorrentialTears, BChar, BChar.MyTeam);
            skill.PlusHit = true;
            skill.FreeUse = true;

            foreach (var ally in BChar.MyTeam.AliveChars)
            {
                ally.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, BChar, false, 0, false, -1, false);
                ally.BuffAdd(ModItemKeys.Buff_B_Aqua_CryingShame, BChar, false, 0, false, -1, false);
            }

            BChar.ParticleOut(MySkill, skill, Target);

            yield break;
        }
    }
}