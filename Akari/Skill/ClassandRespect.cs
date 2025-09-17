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
        public override void Init()
        {
            SkillBasePlus.Target_BaseDMG = GetNum();
        }

        public override string DescExtended(string desc)
        {
            int damage = GetNum();
            return base.DescExtended(desc).Replace("&a", damage.ToString()).Replace("&b", "2");
        }

        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            BattleSystem.instance.AllyTeam.AP += 2;

            if (hit.IsDead)
            {
                AddOnKill();
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Melee_Normal1", 100f, null, 0f, null, null, false, false);

            SkillBasePlus.Target_BaseDMG = GetNum();
        }

        public static int GetNum()
        {
            var scale = PlayData.TSavedata.GetCustomValue<ClassandRespectScale>();

            if (scale == null)
            {
                scale = new ClassandRespectScale();
                PlayData.TSavedata.AddCustomValue(scale);
                scale.currentDamage = 0;
            }

            return scale.currentDamage;
        }

        public static void AddOnKill()
        {
            var scale = PlayData.TSavedata.GetCustomValue<ClassandRespectScale>();

            if (scale == null)
            {
                scale = new ClassandRespectScale();
                PlayData.TSavedata.AddCustomValue(scale);
                scale.currentDamage = 0;
            }

            scale.currentDamage += 2;
        }
    }
}
