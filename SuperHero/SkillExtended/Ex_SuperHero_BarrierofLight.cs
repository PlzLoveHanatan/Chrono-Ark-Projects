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
namespace SuperHero
{
    public class Ex_SuperHero_BarrierofLight : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.SuperHero)
            {
                BattleSystem.DelayInput(Utils.HealingParticle(Utils.SuperHero, BChar));
                Utils.AddBuff(Utils.SuperHero, BChar, ModItemKeys.Buff_B_SuperHero_HeroComplex, 4);
                Utils.AddBuff(BChar, BattleSystem.instance.DummyChar, ModItemKeys.Buff_B_Ex_SuperHero_BarrierofLight, 2);
            }
            SelfDestroy();
        }
    }
}