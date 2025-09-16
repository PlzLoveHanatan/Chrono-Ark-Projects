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
	/// Emergency Injection!
	/// </summary>
    public class B_Mikure_E_EmergencyInjection : Buff, IP_SkillUse_User
    {
        public override void Init()
        {
            OnePassive = true;
            LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Mikure_EmergencyInjection);
        }

        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.ExtendedFind<Ex_Mikure_EmergencyInjection>() == null && AddedSkill.Master == BChar && AddedSkill.IsHeal;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && !SkillD.BasicSkill)
            {
                SelfDestroy();
            }
        }
    }
}