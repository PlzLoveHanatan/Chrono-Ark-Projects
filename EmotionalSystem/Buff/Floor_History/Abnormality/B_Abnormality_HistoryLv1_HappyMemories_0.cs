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
    public class B_Abnormality_HistoryLv1_HappyMemories_0 : Buff, IP_SkillUseHand_Team
    {
        public override void Init()
        {
            base.Init();
            LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_EmotionalSystem_HappyMemories);
        }

        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.ExtendedFind<Ex_EmotionalSystem_HappyMemories>() == null && AddedSkill.Master == BChar;
        }

        public void SKillUseHand_Team(Skill skill)
        {
            if (skill.Master == BChar && !skill.FreeUse)
            {
                Utils.PlaySound("Floor_History_HappyMemories");
                SelfDestroy();
            }
        }
    }
}