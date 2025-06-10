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
    public class Ex_XiaoLOR_SouloftheLotus : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 2;
        }
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 3)
            {
                this.MySkill.APChange = -1;
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar)
            {
                Utils.GiveEmotionsToAllies(3, SkillD.GetPosUI());

                if (BChar.EmotionLevel() >= 3)
                {
                    Utils.GiveEmotionsToAllies(3, SkillD.GetPosUI());
                }
            }
        }
    }
}