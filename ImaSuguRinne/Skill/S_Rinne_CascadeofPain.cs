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
        public override void Init()
        {
            OnePassive = true;
        }

        public override IEnumerator DrawAction()
        {
            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_CascadeofPain_0)
            {
                Utils.CastSkill(BChar, MySkill);
            }
            return base.DrawAction();
        }

        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.atk * 0.2f)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int debuffs = EnemyDebuffs(Targets[0]);
            int additionalDamage = (int)(debuffs * (BChar.GetStat.atk * 0.2f));

            SkillBasePlus.Target_BaseDMG = additionalDamage;

            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_CascadeofPain_0)
            {
                Utils.AllyTeam.Draw();
            }
            else
            {
                Skill skill = Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_CascadeofPain_0, true, true, 0, 0, true, false);
                Utils.InsertSkillInDeck(BChar, skill, 3);
                //Utils.CopiesSelection(BChar, skill);
            }
        }

        public override void Special_PointerEnter(BattleChar Char)
        {
            int debuffs = EnemyDebuffs(Char);
            int additionalDamage = (int)(debuffs * (BChar.GetStat.atk * 0.2f));

            SkillBasePlusPreview.Target_BaseDMG = additionalDamage;
            base.Special_PointerEnter(Char);
        }

        public int EnemyDebuffs(BattleChar target)
        {
            int debuffs = target.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).Count();
            return debuffs;
        }
    }
}