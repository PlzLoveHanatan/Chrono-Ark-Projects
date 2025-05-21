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

namespace Akari
{
    public class ClassandRespect : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", (PlayData.TSavedata.ClassandRespectScale * 2).ToString()).Replace("&b", "2");
        }
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            BattleSystem.instance.AllyTeam.AP += 2;

            if (hit.IsDead) 
            {
                PlayData.TSavedata.ClassandRespectScale++;
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            base.SkillUseSingle(SkillD, Targets);
            MasterAudio.PlaySound("Melee_Normal1", 100f, null, 0f, null, null, false, false);            
            this.SkillBasePlus.Target_BaseDMG = PlayData.TSavedata.ClassandRespectScale * 2;
        }    
    }
}
