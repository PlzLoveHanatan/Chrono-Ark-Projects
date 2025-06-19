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
namespace Darkness
{
    /// <summary>
    /// Shield of Faith
    /// While this skill is counting, reduce all incoming damage by 30%, and gain &a barrier when you cast your own skill.
    /// </summary>
    public class S_Darkness_ShieldofFaith : Skill_Extended
    {
        private int Barrierhp;

        public override void Init()
        {
            base.Init();
            OnePassive = true;
            SkillBaseFinal.Target_BaseDMG = 0;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_10_Ex).Particle_Path;
        }

        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", SkillBasePlus.Target_BaseDMG.ToString());
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Barrierhp = 0;
            foreach (BattleAlly battleAlly in BChar.BattleInfo.AllyList)
            {
                Barrierhp += battleAlly.BarrierHP;
            }
            if (Barrierhp >= 15)
            {
                SkillBasePlus.Target_BaseDMG = (int)(Barrierhp * 0.5f);
                base.SkillParticleOn();
                return;
            }
            else if (Barrierhp >= 1)
            {
                SkillBasePlus.Target_BaseDMG = (int)(Barrierhp * 0.5f);
                return;
            }
            
            SkillBasePlus.Target_BaseDMG = 0;
            base.SkillParticleOff();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            FixedUpdate();

            var buff = ModItemKeys.Buff_B_Darkness_BustyTaunt;

            if (BChar.BarrierHP >= 15)
                MySkill.MySkill.NODOD = true;

            if (BChar.BarrierHP >= 25)
            {
                foreach (var target in Targets)
                {
                    target.BuffAdd(buff, BChar, false, 999, false, -1, false);
                }
            }

            Utils.TryPlayDarknessSound(SkillD, BChar);

            //BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.maxhp * 0.5f);

            //foreach (var e in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            //{
            //    e.BuffAdd(ModItemKeys.Buff_B_Darkness_BustyTaunt, BChar, false, 999, false, -1, false);
            //}
        }
    }
}