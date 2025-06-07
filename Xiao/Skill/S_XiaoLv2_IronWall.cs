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
using EmotionalSystem;
namespace Xiao
{
    /// <summary>
    /// Iron Wall
    /// </summary>
    public class S_XiaoLv2_IronWall : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 4 && this.MySkill.BasicSkill)
            {
                this.MySkill.APChange = -1;
                base.SkillParticleOn();
                return;
            }
            base.SkillParticleOff();
        }

        public override string DescExtended(string desc)
        {
            int barrierValue = (int)(BChar.GetStat.def * 0.5f);
            int barrierGain = Math.Min(barrierValue, 30);

            return base.DescExtended(desc).Replace("&a", barrierGain.ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Guard", 100f, null, 0f, null, null, false, false);

            var target = Targets[0];
            target.BuffAdd(GDEItemKeys.Buff_B_EnemyTaunt, this.BChar, false, 0, false, -1, false);

            if (BChar.EmotionLevel() >= 3)
            {
                int barrierValue = (int)BChar.GetStat.def;
                int barrierGain = Math.Min(barrierValue, 20);

                BChar.BuffAdd(GDEItemKeys.Buff_B_Control_12_0_T, this.BChar, false, 0, false, -1, false).BarrierHP += barrierGain;

                //var debuffs = this.BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false);

                //if (debuffs.Any())
                //{
                //    this.BChar.BuffRemove(debuffs.Random(this.BChar.GetRandomClass().Main).BuffData.Key, false);
                //}
            }
        }
    }
}