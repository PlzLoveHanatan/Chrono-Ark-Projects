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
using Spine;
using NLog.Targets;
namespace Akari
{
    public class Ex_AmmunitionDiscard : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            MySkill.APChange = -1;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills.FindAll(s => Utils.Ammunition.Contains(s.MySkill.KeyID));
            if (ammunitionInHand.Count > 0)
            {
                SkillParticleOn();
            }
            else
            {
                SkillParticleOff();
            }
        }

        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 2 && MainSkill.IsDamage || MainSkill.IsHeal;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills.FindAll(s => Utils.Ammunition.Contains(s.MySkill.KeyID));

            if (ammunitionInHand.Count > 0)
            {
                MasterAudio.PlaySound("Gun_Normal", 100f, null, 0f, null, null, false, false);

                SkillD.ExtendedAdd(ModItemKeys.SkillExtended_Ex_JudgmentsPistol);

                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, ammunitionInHand.Count);
                Skill selectedAmmo = ammunitionInHand[index];
                selectedAmmo.Delete(false);
            }
        }
    }
}
    


