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
using NLog.Targets;
namespace XiaoLOR
{
	/// <summary>
	/// High Kick
	/// </summary>
    public class S_XiaoLORLv1_HighKick : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Kick", 100f, null, 0f, null, null, false, false);

            var burnStacks = Targets[0].BuffReturn(EmotionalSystem.ModItemKeys.Buff_B_Xiao_Burn, false) as B_Xiao_Burn;

            if (burnStacks?.Burn >= 4)
            {
                BattleSystem.DelayInput(this.PlusAttack(Targets[0]));
            }

        }
        public IEnumerator PlusAttack(BattleChar Target)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_XiaoLORLv1_HighKick_0, this.BChar, this.BChar.MyTeam);
            skill.FreeUse = true;
            skill.Counting = 1;
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