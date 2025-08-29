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
namespace Xao
{
    /// <summary>
    /// Magical Girl Pussy Pleasure ♥♥♥♥
    /// </summary>
    public class S_Xao_MagicalGirlPussy_Love_3 : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (Xao_Combo.CurrentCombo >= 10)
            {
                MySkill.APChange = -1;
                SkillParticleOn();
            }
            else
            {
                SkillParticleOff();
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Xao_Combo.CurrentCombo >= 6)
            {
                foreach (var ally in Utils.AllyTeam.AliveChars)
                {
                    string affection = Utils.GetAffectionBuff(ally.Info);
                    Utils.AddBuff(ally, affection);

                    if (Xao_Combo.CurrentCombo >= 8)
                    {
                        string magicalDay = ModItemKeys.Buff_B_Xao_MagicalDay_0;
                        Utils.AddBuff(ally, magicalDay, 2);
                    }
                }
            }
            Utils.PlayXaoVoice(BChar, true);
        }
    }
}