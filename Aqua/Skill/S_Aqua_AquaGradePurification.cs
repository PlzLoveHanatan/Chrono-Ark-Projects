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
using System.IO;
namespace Aqua
{
    /// <summary>
    /// Aqua-Grade Purification
    /// </summary>
    public class S_Aqua_AquaGradePurification : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.KeyData == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            var target = Targets[0];

            if (target != null && target.Info.Ally)
            {
                var buffs = target.GetBuffs(BattleChar.GETBUFFTYPE.DEBUFF, false, false);

                foreach (var buff in buffs)
                {
                    buff.SelfDestroy();
                }
            }
        }
    }
}