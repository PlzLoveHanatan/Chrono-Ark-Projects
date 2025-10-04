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
    /// Spend 1 <sprite name="Xao_Heart"> to remove Overload.
    /// <color=#919191>You can activate this buff by left-clicking. Cannot be activated if user is stunned.</color>
    /// </summary>
    public class B_Xao_Affection_Ally : Buff, IP_BuffAddAfter, IP_SkillUse_User
    {
        public override void BuffOneAwake()
        {
            BuffIcon.AddComponent<Button>().onClick.AddListener(AllyCall);
        }

        public void AllyCall()
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
                Xao_Hearts_Ally.HeartsCheckAlly(BChar, 1);
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