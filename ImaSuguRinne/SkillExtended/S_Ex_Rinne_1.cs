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
namespace ImaSuguRinne
{
    public class S_Ex_Rinne_1 : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 1;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            bool swift = MySkill.NotCount;
            int ap = MySkill.MySkill.UseAp;

            Skill skill = Utils.CreateSkill(BChar, MySkill.MySkill.KeyID, true, true, 1, ap, swift, true);
            skill?.ExtendedAdd(this);
        }
    }
}