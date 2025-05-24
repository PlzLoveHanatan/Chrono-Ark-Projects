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
	/// God's Blow
	/// </summary>
    public class S_Aqua_Rare_GodsBlow : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 50);

            if (alwaysLucky && SkillD.MySkill.KeyID == ModItemKeys.Skill_S_Aqua_Rare_GodsBlow)
            {
                PlusSkillPerFinal.Damage = 400;
            }
        }
    }
}