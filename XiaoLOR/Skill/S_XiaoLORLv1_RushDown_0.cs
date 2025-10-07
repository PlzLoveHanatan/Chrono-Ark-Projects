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
using EmotionalSystem;
namespace XiaoLOR
{
    public class S_XiaoLORLv1_RushDown_0 : Skill_Extended
    {
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 2)
            {
                var rushDown = BattleSystem.instance.AllyTeam.Skills
                    .Where(skill => skill?.MySkill?.KeyID == ModItemKeys.Skill_S_XiaoLORLv1_RushDown_0)
                    .ToList();

                foreach (Skill skill in rushDown)
                {
                    if (rushDown != null)
                    {
                        var changeTo = Skill.TempSkill(ModItemKeys.Skill_S_XiaoLORLv2_FleetEdge, skill.Master, skill.Master.MyTeam);
                        skill.SkillChange(changeTo);
                        skill.AutoDelete = 1;
                        skill.isExcept = true;
                    }
                }
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("NormalHit");
        }
    }
}