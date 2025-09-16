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
namespace Mikure
{
    /// <summary>
    /// Care Headdress
    /// </summary>
    public class E_Mikure_CareHeaddress : EquipBase, IP_SkillUse_User
    {
        public override void Init()
        {
            PlusPerStat.Heal = 30;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && SkillD.IsHeal)
            {
                foreach (BattleChar target in Targets)
                {
                    if (target.Info.Ally)
                    {
                        string buff = ModItemKeys.Buff_B_Mikure_E_CareHeaddress;
                        Utils.AddBuff(target, BChar, buff);
                        target.BuffReturn(buff, false).BarrierHP += (int)(BChar.GetStat.reg * 0.8f);
                    }
                }
            }
        }
    }
}