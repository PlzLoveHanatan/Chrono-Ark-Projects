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
using NLog.Targets;
namespace XiaoLOR
{
    /// <summary>
    /// Fiery Waltz
    /// </summary>
    public class S_XiaoLORLv1_FieryWaltz : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("Fiery");

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 2);

                    var burnStacks = target.BuffReturn(EmotionSystem.ModItemKeys.Buff_B_EmotionSystem_Burn, false) as Debuffs.Burn;

                    if (burnStacks?.CurrentBurn < 4 || burnStacks?.CurrentBurn == 0)
                    {
                        Utils.ApplyBurn(target, this.BChar, 2);
                    }
                }
            }
        }
    }
}