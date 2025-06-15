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
using Spine;
namespace Darkness
{
    public class Ex_Darkness_1 : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 2;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.BarrierHP >= 20 && SkillD.Master == BChar)
            {
                BattleSystem.DelayInputAfter(BattleSystem.instance.SkillRandomUseIenum(SkillD.Master, SkillD.CloneSkill(true, SkillD.Master, null, false), false, false, false));
            }
        }
    }
}