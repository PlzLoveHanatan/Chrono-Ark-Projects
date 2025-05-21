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
namespace EmotionalSystem
{
    public class B_Abnormality_TechnologicalLv2_TheSeventhBullet_0 : Buff, IP_SkillUse_User, IP_PlayerTurn
    {
        public override void Init()
        {
            base.Init();
            LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_EmotionalSystem_TheSeventhBullet);
        }
        public void Turn()
        {
            var buff = ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet;
            if (BChar.BuffReturn(buff, false) == null)
            {
                SelfDestroy();
            }
        }

        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.ExtendedFind<Ex_EmotionalSystem_TheSeventhBullet>() == null && AddedSkill.Master == BChar && AddedSkill.IsDamage && AddedSkill.MySkill.Target.Key != GDEItemKeys.s_targettype_all_enemy;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.PlusHit && SkillD.MySkill.Target.Key != GDEItemKeys.s_targettype_all_enemy)
            {
                SelfDestroy();
            }
        }
    }
}