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
using EmotionSystem;
namespace XiaoLOR
{
    /// <summary>
    /// Fervid Emotions
    /// </summary>
    public class S_XiaoLORLv2_FervidEmotions : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("Fervid");

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 3);

                    var burnStacks = target.BuffReturn(EmotionSystem.ModItemKeys.Buff_B_EmotionSystem_Burn, false) as Debuffs.Burn;

                    if (burnStacks?.CurrentBurn < 6 || burnStacks?.CurrentBurn == 0)
                    {
                        Utils.ApplyBurn(target, BChar, 3);
                    }

                    if (BChar.EmotionLevel() >= 3)
                    {
                        Utils.ApplyBurn(target, BChar, 3);
                    }
                }
            }

			foreach (var ally in Utils.AllyTeam.AliveChars)
			{
				EmotionalManager.GetNegEmotion(ally, SkillD.GetPosUI(), 2);
			}
		}
    }
}