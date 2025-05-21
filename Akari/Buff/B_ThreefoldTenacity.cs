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
namespace Akari
{
    /// <summary>
    /// Threefold Tenacity
    /// </summary>
    public class B_ThreefoldTenacity : Buff, IP_SkillUse_User
    {
        public override void Init()
        {
            base.Init();
            LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_ThreefoldTenacity);
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.Info.Passive is P_Akari akariPassive)
            {
                if (akariPassive?.currentTurn >= 4) return;
            }

            if (SkillD.Master == BChar && SkillD.IsDamage && !SkillD.FreeUse && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                SelfDestroy();
            }
        }

        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.ExtendedFind<Ex_ThreefoldTenacity>() == null && AddedSkill.Master == BChar && AddedSkill.IsDamage && !Utils.Ammunition.Contains(AddedSkill.MySkill.KeyID);
        }
    }
}