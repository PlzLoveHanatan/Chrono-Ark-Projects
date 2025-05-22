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
    public class B_EmotionalSystem_Draw : Buff, IP_SkillUseHand_Team
    {
        public int skillUse = 0;

        public override void Init()
        {
            base.Init();
            skillUse = 0;
            LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_EmotionalSystem_Draw);
        }

        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.ExtendedFind<Ex_EmotionalSystem_Draw>() == null && AddedSkill.Master == BChar;
        }

        public void SKillUseHand_Team(Skill skill)
        {
            if (skill.Master == this.BChar)
            {
                skillUse++;

                if (skillUse >= 2)
                {
                    BattleSystem.instance.AllyTeam.Draw(1);
                    SelfDestroy();
                }
            }
        }
    }
}