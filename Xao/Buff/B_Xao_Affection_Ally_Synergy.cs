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
namespace Xao
{
    /// <summary>
    /// Affection
    /// </summary>
    public class B_Xao_Affection_Ally_Synergy : Buff, IP_BuffAddAfter, IP_SkillUse_User
    {
        public override void BuffOneAwake()
        {
            BuffIcon.AddComponent<Button>().onClick.AddListener(AllyCallSynergy);
        }

        public void AllyCallSynergy()
        {
            Utils.AffectionSelection(BChar);
        }

        public override void BuffStat()
        {
            PlusStat.dod = 3 * StackNum;
            PlusStat.cri = 3 * StackNum;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker == BChar && addedbuff == this)
            {
                Xao_Hearts_Ally_Synergy.HeartsCheck(BChar, 1);
                Utils.AllyHentaiText(BChar);
            }
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar)
            {
                Utils.AllyHentaiText(BChar);
            }
        }
    }
}