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
using EmotionalSystem;
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
            int barrierGain = Math.Min(barrierValue, 20);

            return base.DescExtended(desc).Replace("&a", barrierGain.ToString());
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

                Utils.GiveEmotionsToAllies(3, SkillD.GetPosUI());
            }

            else
            {
                target.BuffAdd(GDEItemKeys.Buff_B_EnemyTaunt, this.BChar, false, 0, false, -1, false);
                Utils.GiveEmotionsToChar(this.BChar, 3, SkillD.GetPosUI());
            }

            if (BChar.EmotionLevel() >= 3)
            {
                int barrierValue = (int)(BChar.GetStat.def * 0.5f);
                int barrierGain = Math.Min(barrierValue, 20);

                BChar.MyTeam.partybarrier.BarrierHP += barrierGain;

                //foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
                //{
                //    int barrierValue = (int)(BChar.GetStat.def * 0.5f);
                //    int barrierGain = Math.Min(barrierValue, 20);

                //    ally.BuffAdd(GDEItemKeys.Buff_B_Control_12_0_T, this.BChar, false, 0, false, -1, false).BarrierHP += barrierGain;
                //}
            }
        }
    }
}
