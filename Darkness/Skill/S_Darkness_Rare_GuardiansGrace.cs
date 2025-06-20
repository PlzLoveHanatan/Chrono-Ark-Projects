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
using System.Web.UI.WebControls;
namespace Darkness
{
	/// <summary>
	/// Guardian's Grace
	/// Gain guaranteed Critical if  you have barrier remaining.
	/// </summary>
    public class S_Darkness_Rare_GuardiansGrace : Skill_Extended, IP_DamageChange
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.4f)).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;
        }

        public override void FixedUpdate()
        {
            OnePassive = true;
            if (BChar.BarrierHP >= 15)
            {
                base.SkillParticleOn();
                if (BChar.BarrierHP >= 25)
                {
                    MySkill.APChange = -1;
                }
                return;
            }

            base.SkillParticleOff();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayDarknessSound(SkillD, BChar);

            List<Buff> buffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.BUFF, false, false);
            int num = 0;
            foreach (Buff buff in buffs)
            {
                num += buff.StackNum;
            }
            this.SkillBasePlus.Target_BaseDMG = (int)(num * (this.BChar.GetStat.atk * 0.4f));
        }

        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (BChar.BarrierHP >= 15)
            {
                Cri = true;
            }
            return Damage;
        }
    }
}