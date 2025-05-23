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
            var target = Targets[0];

            bool neverLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 20);

            if (neverLucky)
            {
                foreach (var ally in BChar.MyTeam.AliveChars)
                {
                    ally.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, BChar, false, 0, false, -1, false);
                    ally.BuffAdd(ModItemKeys.Buff_B_Aqua_AquaVeil, BChar, false, 0, false, -1, false);
                }
            }

            bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 50);

            if (alwaysLucky)
            {
                BattleSystem.DelayInput(AdditionalCast(target));
            }
        }

        public IEnumerator AdditionalCast(BattleChar Target)
        {

            yield return new WaitForSecondsRealtime(0.2f);

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_TorrentialTears, BChar, BChar.MyTeam);
            skill.PlusHit = true;
            skill.FreeUse = true;

            foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars)
            {
                enemy.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, BChar, false, 0, false, -1, false);
                enemy.BuffAdd(ModItemKeys.Buff_B_Aqua_AquaVeil, BChar, false, 0, false, -1, false);
            }

            bool neverLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 20);

            if (neverLucky)
            {
                foreach (var ally in BChar.MyTeam.AliveChars)
                {
                    ally.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, BChar, false, 0, false, -1, false);
                    ally.BuffAdd(ModItemKeys.Buff_B_Aqua_AquaVeil, BChar, false, 0, false, -1, false);
                }
            }

            BChar.ParticleOut(MySkill, skill, Target);

            yield break;
        }
    }
}