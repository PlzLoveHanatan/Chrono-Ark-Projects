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
    /// Got You Now!
    /// </summary>
    public class S_Mikure_GotYouNow : Skill_Extended
    {
        public override void Init()
        {
            IsDamage = false;
            SkillBasePlus.Target_BaseDMG = 0;
            PlusSkillPerStat.Heal = 0;
        }
        public override void HandInit()
        {
            base.HandInit();
            IsDamage = true;
            SkillBasePlus.Target_BaseDMG = 0;
            PlusSkillPerStat.Heal = 0;
            SkillBasePlusPreview.Target_BaseDMG = (int)(MySkill.TargetHeal * 2.1f);
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            IsDamage = false;
            SkillBasePlus.Target_BaseDMG = 0;
            PlusSkillPerStat.Heal = 0;
            if (!Targets[0].Info.Ally)
            {
                IsDamage = true;
                SkillBasePlus.Target_BaseDMG = (int)(SkillD.TargetHeal);
                PlusSkillPerStat.Heal = -99999;
            }
        }
	}
}