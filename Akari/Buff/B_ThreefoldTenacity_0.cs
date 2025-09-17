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
    /// Increase all skill's damage by 15%.
    /// </summary>
    public class B_ThreefoldTenacity_0 : Buff
    {
        public override void Init()
        {
            base.Init();
            LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_ThreefoldTenacity);
        }

        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.ExtendedFind<Ex_ThreefoldTenacity>() == null && AddedSkill.Master == BChar && AddedSkill.IsDamage && !Utils.Ammunition.Contains(AddedSkill.MySkill.KeyID);
        }
    }
}