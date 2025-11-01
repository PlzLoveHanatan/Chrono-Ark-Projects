using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using EmotionSystem;
using NLog.Targets;
using DarkTonic.MasterAudio;

namespace XiaoLOR
{
    /// <summary>
    /// All-out War
    /// </summary>
    public class S_XiaoLORLv2_AlloutWar : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            int barrierValue = (int)(BChar.GetStat.def * 0.5f);
            return base.DescExtended(desc).Replace("&a", barrierValue.ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("NormalHit");

            var target = Targets[0];

            if (target is BattleEnemy enemy && enemy.istaunt)
            {
                foreach (Buff buff in target.Buffs)
                {
                    if (buff.BuffData.TauntStat)
                    {
                        buff.SelfDestroy(false);
                    }
                }

                foreach (var ally in Utils.AllyTeam.AliveChars)
                {
					EmotionalManager.GetNegEmotion(ally, SkillD.GetPosUI(), 3);
				}
            }
            else
            {
                target.BuffAdd(GDEItemKeys.Buff_B_EnemyTaunt, this.BChar, false, 0, false, -1, false);
				EmotionalManager.GetNegEmotion(BChar, SkillD.GetPosUI(), 3);
            }

            if (BChar.EmotionLevel() >= 3)
            {
                int barrierValue = (int)(BChar.GetStat.def * 0.5f);
                BChar.MyTeam.partybarrier.BarrierHP += barrierValue;
            }
        }
    }
}
