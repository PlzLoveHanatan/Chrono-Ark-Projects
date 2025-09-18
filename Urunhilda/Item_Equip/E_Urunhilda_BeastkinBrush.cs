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
namespace Urunhilda
{
    public class E_Urunhilda_BeastkinBrush : EquipBase, IP_BuffUpdate, IP_BattleEnd
    {
        public void BattleEnd()
        {
            PlusPerStat.Damage = 0;
        }

        public void BuffUpdate(Buff MyBuff)
        {
            if (MyBuff != null && !MyBuff.BChar.NullCheck() && MyBuff.BChar == BChar)
            {
                List<Buff> buffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.BUFF, false, false);

                int totalStacks = 0;
                foreach (Buff buff in buffs)
                {
                    totalStacks += buff.StackNum;
                }

                int damageBonusPercent = 7 * totalStacks;
                if (damageBonusPercent > 70)
                {
                    damageBonusPercent = 70;
                }

                PlusPerStat.Damage = damageBonusPercent;
            }
        }

        public override void Init()
        {
            PlusPerStat.Damage = 20;
            PlusStat.PlusCriDmg = 20;
        }
    }
}