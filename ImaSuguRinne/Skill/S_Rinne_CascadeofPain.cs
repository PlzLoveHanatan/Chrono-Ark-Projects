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
	/// Cascade of Pain
	/// </summary>
    public class S_Rinne_CascadeofPain : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.2f)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Buff> buffs = Targets[0].GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false);
            int num = 0;
            foreach (Buff buff in buffs)
            {
                num++;
            }
            this.SkillBasePlus.Target_BaseDMG = (int)((float)num * (this.BChar.GetStat.atk * 0.2f));

            Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_CascadeofPain, true, true, 1, 1, true);
        }

        public override void Special_PointerEnter(BattleChar Char)
        {
            List<Buff> buffs = Char.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false);
            int num = 0;
            foreach (Buff buff in buffs)
            {
                num++;
            }
            this.SkillBasePlusPreview.Target_BaseDMG = (int)((float)num * (this.BChar.GetStat.atk * 0.2f));
            base.Special_PointerEnter(Char);
        }
    }
}