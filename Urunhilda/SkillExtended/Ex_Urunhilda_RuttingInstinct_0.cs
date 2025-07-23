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
    public class Ex_Urunhilda_RuttingInstinct_0 : BuffSkillExHand
    {
        public override void Init()
        {
            base.Init();
            NoClone = true;
            if (MySkill.MySkill.Target.Key == GDEItemKeys.s_targettype_ally)
            {
                MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_onetarget);
            }
            if (MySkill.MySkill.Target.Key == GDEItemKeys.s_targettype_all_other)
            {
                MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_onetarget);
            }
            HandInit();
        }

        public override void HandInit()
        {
            base.HandInit();
            SkillBasePlus.Target_BaseDMG = 0;
            PlusSkillPerStat.Heal = 0;
            IsDamage = true;
            SkillBasePlusPreview.Target_BaseDMG = MySkill.TargetHeal;
        }

        public override void SkillUseSingleAfter(Skill SkillD, List<BattleChar> Targets)
        {
            IsDamage = false;
            SkillBasePlus.Target_BaseDMG = 0;
            PlusSkillPerFinal.Heal = 0;
            if (!Targets[0].Info.Ally)
            {
                IsDamage = true;
                SkillBasePlus.Target_BaseDMG = SkillD.TargetHeal;
                PlusSkillPerFinal.Heal = -100;
            }
        }
    }
}