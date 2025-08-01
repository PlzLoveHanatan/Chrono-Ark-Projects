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
    /// Rutting Instinct
    /// </summary>
    public class Ex_Urunhilda_RuttingInstinct_1 : BuffSkillExHand, IP_ParticleOut_Before
    {
        public bool Switching;
        public bool Handinit;

        public override void Init()
        {
            base.Init();
            NoClone = true;
            IgnoreTaunt = true;
            HandInit();
        }

        public override void HandInit()
        {
            base.HandInit();
            SkillBasePlus.Target_BaseDMG = 0;
            PlusSkillPerStat.Heal = 0;
            SkillBasePlus.Target_BaseHeal = 0;
            PlusSkillPerStat.Damage = 0;
            IsHeal = true;
            SkillBasePlusPreview.Target_BaseHeal = MySkill.TargetDamage;
            Switching = false;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!Handinit)
            {
                if (MySkill.BasicSkill)
                {
                    Handinit = true;
                }
                if (MySkill.GetIsSkillinHand)
                {
                    Handinit = true;
                    if (MySkill.MySkill.Target.Key == GDEItemKeys.s_targettype_enemy)
                    {
                        MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_onetarget);
                    }
                    if (MySkill.MySkill.Target.Key == GDEItemKeys.s_targettype_all_other)
                    {
                        MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_onetarget);
                    }
                    OnePassive = true;
                    Switching = false;
                }
            }
        }

        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD == MySkill)
            {
                IsHeal = true;
                SkillBasePlus.Target_BaseDMG = 0;
                PlusSkillPerStat.Heal = 0;
                SkillBasePlus.Target_BaseHeal = 0;
                PlusSkillPerStat.Damage = 0;
                if (Targets[0].Info.Ally)
                {
                    SkillBasePlus.Target_BaseHeal = MySkill.TargetDamage;
                    PlusSkillPerStat.Damage = -99999;
                }
            }
        }
    }
}