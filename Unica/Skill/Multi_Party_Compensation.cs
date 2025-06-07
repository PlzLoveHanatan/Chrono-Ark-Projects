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
namespace Unica
{
    /// <summary>
    /// Multi-Party Compensation
    /// Sheathe : Cast this skill on Random target, Restore 2 mana.
    /// </summary>
    public class Multi_Party_Compensation : SkillExtedned_IlyaP
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.5f)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Insurance_Plan", 100f, null, 0f, null, null, false, false);
            if (SkillD.FreeUse) return;

            base.SkillUseSingle(SkillD, Targets);

            var discardList = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill
            && skill.Master != BattleSystem.instance.AllyTeam.LucyAlly).ToList();

            if (discardList.Count == 0) return;

            var skillToDiscard = discardList.FirstOrDefault(skill => skill.ExtendedFind_DataName(ModItemKeys.SkillExtended_ExSheathe) != null)
                ?? discardList.Where(skill => skill.Master.Info.KeyData == ModItemKeys.Character_Unica).OrderBy(skill => skill.AP).FirstOrDefault()
                ?? discardList.OrderBy(skill => skill.AP).FirstOrDefault();

            skillToDiscard?.Delete(false);

            if (this.BChar.Info.KeyData == ModItemKeys.Character_Unica)
            {
                new P_Unica().ApplyEffects(this.BChar, 1);
            }

            foreach (BattleChar battleChar in Targets)
            {
                List<Buff> buffs = battleChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false);
                if (buffs.Count >= 1)
                {
                    buffs.Random(this.BChar.GetRandomClass().Main).SelfDestroy(false);
                }
            }

            foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
            {
                battleChar.BuffAdd(ModItemKeys.Buff_InsurancePlan, this.BChar, false, 0, false, -1, false);
            }

            this.Off();
            if (Targets.Count == 1)
            {
                this.On();
            }
            base.SkillUseSingle(SkillD, Targets);
        }
        public override void FixedUpdate()
        {
            if (this.BChar.BattleInfo.EnemyList.Count == 1)
            {
                base.SkillParticleOn();
                this.On();
                return;
            }
            base.SkillParticleOff();
            this.Off();
        }
        public void On()
        {
            this.SkillBasePlus.Target_BaseDMG = (int)(this.BChar.GetStat.atk * 0.5f);
        }
        public void Off()
        {
            this.SkillBasePlus.Target_BaseDMG = 0;
        }
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }
        public override void IlyaWaste()
        {
            BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(this.BChar, this.MySkill, false, true, false));
            foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
            {
                battleChar.BuffAdd(ModItemKeys.Buff_InsuranceCoverage, this.BChar, false, 0, false, -1, false);
            }
            BattleSystem.instance.AllyTeam.AP += 1;
            BattleSystem.instance.AllyTeam.Draw();
        }
    }
}