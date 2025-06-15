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
namespace Darkness
{
    /// <summary>
    /// Unbreakable Will
    /// </summary>
    public class S_Darkness_Rare_UnbreakableWill : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var lucyAlly = BattleSystem.instance.AllyTeam.LucyAlly;

            IEnumerable<BattleChar> allAllies = allies;
            if (lucyAlly != null)
                allAllies = allAllies.Concat(new[] { lucyAlly });

            foreach (BattleChar battleChar in allAllies)
            {
                foreach (Buff buff in battleChar.Buffs)
                {
                    if (buff.BuffData.Hide)
                        continue;

                    if (buff.BuffData.Debuff)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            buff.TurnUpdate();
                        }
                    }
                    else if (buff.BuffData.LifeTime != 0f)
                    {
                        foreach (StackBuff stackBuff in buff.StackInfo)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                stackBuff.RemainTime++;
                            }
                        }
                    }
                }
            }

            foreach (var e in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                e.BuffAdd(ModItemKeys.Buff_B_Darkness_BustyTaunt, BChar, false, 999, false, -1, false);
            }
        }
    }
}