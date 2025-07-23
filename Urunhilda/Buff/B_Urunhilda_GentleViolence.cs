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
	/// <summary>
	/// Gentle Violence
	/// </summary>
    public class B_Urunhilda_GentleViolence : Buff, IP_Awake
    {
        public override void Init()
        {
            OnePassive = true;
            LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Urunhilda_RuttingInstinct_1);
        }

        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.ExtendedFind<Ex_Urunhilda_RuttingInstinct_1>() == null && AddedSkill.Master == BChar && AddedSkill.IsDamage && AddedSkill.ExtendedFind<Ex_Urunhilda_RuttingInstinct_0>() == null;
        }
        public void Awake()
        {
            if (BChar.Info.Passive is P_Urunhilda passive)
            {
                passive.GentleViolence = true;
            }
        }
    }
}