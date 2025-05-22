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
    /// Flooding Bullets
    /// </summary>
    public class S_Synchronize_Technological_FloodingBullets : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.DelayInput(this.PlusAttack(Targets[0]));
        }
        public IEnumerator PlusAttack(BattleChar Target)
        {
            yield return new WaitForSecondsRealtime(0.2f);

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Synchronize_Technological_FloodingBullets_0, this.BChar, this.BChar.MyTeam);
            skill.FreeUse = true;
            skill.PlusHit = true;

            if (this.BChar != null && !this.BChar.Dummy && !this.BChar.IsDead)
            {
                if (!Target.IsDead)
                {
                    BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, Target, false, false, true, null));

                }
                else if (BattleSystem.instance.EnemyList.Count > 0)
                {
                    BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main), false, false, true, null));
                }
            }

            yield break;
        }
    }
}