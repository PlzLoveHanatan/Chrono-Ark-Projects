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
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;

            if (!SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "Darkness_NormalKnight")) return;
            {
                GDESkillData gdeskillData = new GDESkillData(MySkill.MySkill.KeyID);

                MySkill.Init(gdeskillData, BChar, BChar.MyTeam);

                MySkill.MySkill.PlusSkillView = ModItemKeys.Skill_S_Darkness_SideSlash_0;
            }
        }

        public override void FixedUpdate()
        {
            if (BChar.BarrierHP >= 15)
            {
                MySkill.MySkill.NODOD = true;
                SkillParticleOn();
                if (BChar.BarrierHP >= 25)
                {
                    MySkill.APChange = -1;
                }
            }
            else
            {
                SkillParticleOff();
            }
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
            string skillKey = ModItemKeys.Skill_S_Darkness_SideSlash;

            if (SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "Darkness_NormalKnight"))
            {
                skillKey = ModItemKeys.Skill_S_Darkness_SideSlash_0;
            }
            Skill skill = Skill.TempSkill(skillKey, BChar, BChar.MyTeam);

            BattleSystem.instance.AllyTeam.Add(skill, true);

            if (Utils.DarknessVoiceSkills)
            {
                if (BChar.BarrierHP >= 15)
                {
                    Utils.PlayDarknessBattleDialogue(MySkill, BChar);
                }
                else if (DarknessAttackMisses)

                {
                    Utils.TryPlayDarknessSound(SkillD, BChar);
                }
                else
                {
                    BattleSystem.DelayInput(Miss());
                }
            }
        }
    }
}