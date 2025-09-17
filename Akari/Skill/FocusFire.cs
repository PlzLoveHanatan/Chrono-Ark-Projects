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
    /// <summary>
    /// Focus Fire
    /// </summary>
    public class FocusFire : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.atk * 0.3f)).ToString());
        }

        public override void Init()
        {
            base.Init();
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (BChar.BattleInfo.EnemyList.Count == 1)
            {
                SkillBasePlus.Target_BaseDMG = (int)(BChar.GetStat.atk * 0.3f);
                SkillParticleOn();
            }
            else
            {
                SkillBasePlus.Target_BaseDMG = 0;
                SkillParticleOff();
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Gun_Normal", 100f, null, 0f, null, null, false, false);

            int discardedAmo = Utils.DiscardAndApplyAmmunition(BChar, 3, Targets[0]);

            if (discardedAmo > 0)
            {
                PlusSkillPerFinal.Damage = 20 * discardedAmo;
            }
        }
    }
}

