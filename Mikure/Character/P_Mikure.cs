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
    /// Mikure
    /// Passive:
    /// </summary>
    public class P_Mikure : Passive_Char, IP_Healed, IP_SkillUse_User
    {
        public override void Init()
        {

        }

        public override void FixedUpdate()
        {
            if (MyChar.BasicSkill.SkillInfo.HealSkill)
            {
                PlusStat.PlusMPUse.PlusMP_Fixed = -1;
            }
        }

        public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
        {
            if (Healer == BChar && OverHeal >= 1 && (HealedChar.Info.KeyData != GDEItemKeys.Character_Phoenix || !HealedChar.Info.Ally) && !HealedChar.IsDead)
            {
                string areYouOK = ModItemKeys.Buff_B_Mikure_AreYouOK;
                Utils.AddBuff(HealedChar, BChar, areYouOK);
                BChar.StartCoroutine(AreYouOK(HealedChar, areYouOK, OverHeal));
            }
        }

        public IEnumerator AreYouOK(BattleChar HealedChar, string areYouOK, int OverHeal)
        {
            yield return null;

            if (HealedChar.BuffReturn(areYouOK, false) is B_Mikure_AreYouOK ok)
            {
                ok.OverHealed += (int)Misc.PerToNum(OverHeal, 50);
                ok.BuffStat();
            }
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && SkillD.IsHeal)
            {
                Utils.ReviveAllies(Targets);
            }
        }
    }
}