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
    /// <summary>
    /// <sprite name="비용1"><sprite name="이상">
    /// </summary>
    public class Ex_Aqua_PartyTrick : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 1;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Skill partyTrick = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_PartyTrick, this.BChar, this.BChar.MyTeam);
            partyTrick.FreeUse = true;

            BattleSystem.DelayInputAfter(BattleSystem.instance.SkillRandomUseIenum(BChar, partyTrick, false, false, false));
        }
    }
}