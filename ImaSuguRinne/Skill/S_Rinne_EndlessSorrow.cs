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
    /// <summary>
    /// Endless Sorrow
    /// This skill can be played repeatedly during this turn.
    /// </summary>
    public class S_Rinne_EndlessSorrow : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_WitchBoss_Ex_0).Particle_Path;
        }

        public override IEnumerator DrawAction()
        {
            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_EndlessSorrow_0)
            {
                Utils.CastSkill(BChar, MySkill);
            }
            return base.DrawAction();
        }

        public override void FixedUpdate()
        {
            if (BChar.BattleInfo.EnemyList.Count == 1)
            {
                SkillParticleOn();
            }
            else
            {
                SkillParticleOff();
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.BattleInfo.EnemyList.Count == 1)
            {
                Utils.AddDebuff(Targets[0], BChar, ModItemKeys.Buff_B_Rinne_SorrowResonance, 1);
            }

            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_EndlessSorrow_0)
            {
                Utils.AllyTeam.Draw();
            }
            else
            {
                Skill skill = Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_EndlessSorrow_0, true, true, 0, 0, true, false);
                Utils.InsertSkillInDeck(BChar, skill, 2);
            }
        }
    }
}