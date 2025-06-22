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
using NLog.Targets;
namespace Darkness
{
    /// <summary>
    /// Clumsy Slash
    /// </summary>
    public class S_Darkness_ClumsySlash : Skill_Extended
    {
        private bool DarknessAttackMisses;
        public override void Init()
        {
            base.Init();
            OnePassive = true;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override void FixedUpdate()
        {
            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
            }
            if (BChar.BarrierHP >= 15)
            {
                MySkill.MySkill.NODOD = true;
                base.SkillParticleOn();

                return;
            }

            base.SkillParticleOff();
        }
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            DarknessAttackMisses = true;
        }

        private IEnumerator Miss()
        {
            if (DarknessAttackMisses) yield break;
            Utils.PlayDarknessBattleDialogue2(MySkill, BChar);

            yield return null;
        }


        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            DarknessAttackMisses = false;

            if (Utils.DarknessVoiceDialogue)
            {
                BattleSystem.DelayInput(Miss());
            }

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Darkness_SideSlash, BChar, BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);

            if (BChar.BarrierHP >= 15 && Utils.DarknessVoiceDialogue)
            {
                Utils.PlayDarknessBattleDialogue(MySkill, BChar);
            }
            else if (!DarknessAttackMisses)
            {
                Utils.TryPlayDarknessSound(SkillD, BChar);
            }
        }
    }
}