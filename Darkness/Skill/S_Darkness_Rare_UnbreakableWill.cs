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
using NLog.Targets;
namespace Darkness
{
    /// <summary>
    /// Unbreakable Will
    /// </summary>
    public class S_Darkness_Rare_UnbreakableWill : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.BarrierHP >= 10)
            {
                foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars)
                {
                    enemy?.BuffAdd(ModItemKeys.Buff_B_Darkness_BustyTaunt, BChar, false, 0, false, -1, false);
                }

                if (BChar.BarrierHP >= 20)
                {
                    BattleSystem.DelayInputAfter(ExtendBuffs());
                }
            }
            Utils.TryPlayDarknessSound(SkillD, BChar);
        }

        public IEnumerator ExtendBuffs()
        {
            yield return null;

            var buffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.BUFF, true, false).ToList();

            foreach (Buff buff in buffs)
            {
                foreach (StackBuff stackBuff in buff.StackInfo)
                {
                    if (!buff.BuffExtended.Any((Buff_Ex p) => p is B_Darkness_Ex_UnbreakableWill))
                    {
                        if (stackBuff.RemainTime != 0)
                        {
                            stackBuff.RemainTime++;
                        }
                        buff.AddBuffEx(new B_Darkness_Ex_UnbreakableWill());
                    }
                }
            }
        }
    }
}