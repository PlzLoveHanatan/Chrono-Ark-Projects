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
namespace Aqua
{
    public class Ex_Aqua_DivineLottery : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 1;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Skill divineLottery = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_DivineLottery, this.BChar, this.BChar.MyTeam);
            divineLottery.FreeUse = true;

            BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(BChar, divineLottery, false, false, false));
        }
    }
}