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
using UnityEngine.Analytics;
namespace Xao
{
    /// <summary>
    /// Love Egg
    /// </summary>
    public class Equip_Xao_LoveEgg : EquipBase, IP_PlayerTurn, IP_SomeOneDead, IP_SkillUse_User, IP_BattleStart_UIOnBefore
    {
        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            if (BChar != Utils.Xao)
            {
                Xao_Hearts.CreateHeartsAlly(BChar);
                Xao_Hearts.SavedStackAlly = 0;
            }
        }

        public override void Init()
        {
            OnePassive = true;
            PlusStat.atk = 2;
            PlusStat.reg = 2;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar)
            {
                Utils.AllyHentaiText(BChar);
            }
        }

        public void SomeOneDead(BattleChar DeadChar)
        {
            if (DeadChar == BChar)
            {
                Xao_Hearts.DestroyAndNullifyAllAlly();
            }
        }

        public void Turn()
        {
            string buff = BChar == Utils.Xao ? ModItemKeys.Buff_B_Xao_Affection : ModItemKeys.Buff_B_Xao_Affection_Ally;
            Utils.AddBuff(BChar, buff, 1);
            Utils.AllyHentaiText(BChar);
        }
    }
}