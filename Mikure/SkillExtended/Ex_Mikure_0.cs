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
using System.ComponentModel;
using System.Security.Cryptography;
namespace Mikure
{
    public class Ex_Mikure_0 : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsDamage;
        }

        public override void Init()
        {
            CanUseStun = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            int damage = BChar.GetStat.maxhp / 2;
            SkillBaseFinal.Target_BaseDMG = damage;
        }
    }
}