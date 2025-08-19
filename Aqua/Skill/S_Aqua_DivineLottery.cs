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
	/// Divine Lottery
	/// </summary>
    public class S_Aqua_DivineLottery : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.KeyData == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            Utils.DivineLottery();

            BattleChar battleChar = BChar; /*BattleSystem.instance.AllyList.Random(BattleRandom.PassiveItem);*/
            Skill skill = Skill.TempSkill(Utils.CachedSkills.Random(BattleRandom.PassiveItem).Key, battleChar, battleChar.MyTeam);
            skill.FreeUse = true;
            BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(battleChar, skill, false, false, false));
        }
    }
}