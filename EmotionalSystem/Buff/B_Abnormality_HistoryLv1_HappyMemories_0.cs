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
    public class B_Abnormality_HistoryLv1_HappyMemories_0 : Buff, IP_SkillUseHand_Team, IP_PlayerTurn
    {
        public override void Init()
        {
            base.Init();
            LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_EmotionalSystem_HappyMemories);
        }
        public void Turn()
        {
            var buff = ModItemKeys.Buff_B_Abnormality_HistoryLv1_HappyMemories;
            if (BChar.BuffReturn(buff, false) == null)
            {
                SelfDestroy();
            }
        }

        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.ExtendedFind<Ex_EmotionalSystem_HappyMemories>() == null && AddedSkill.Master == BChar;
        }

        public void SKillUseHand_Team(Skill skill)
        {
            if (skill.Master == this.BChar && !skill.FreeUse)
            {
                MasterAudio.PlaySound("HappyMemories", 100f, null, 0f, null, null, false, false);
                SelfDestroy();
            }
        }
    }
}