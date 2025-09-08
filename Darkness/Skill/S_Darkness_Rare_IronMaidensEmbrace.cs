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
    /// Iron Maiden's Embrace
    /// When used create a party barrier equal &a <color=#FF7C34>(Max HP * 1.5)</color>.
    /// </summary>
    public class S_Darkness_Rare_IronMaidensEmbrace : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.maxhp * 0.5f)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayDarknessSound(SkillD, BChar);

            if (BChar.BarrierHP >= 15)
            {
                BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.maxhp * 0.5f);

                if (BChar.BarrierHP >= 30)
                {
                    BattleSystem.DelayInput(ExtraCast());
                }
            }
        }

        public IEnumerator ExtraCast()
        {
            yield return null;

            string skillKey = ModItemKeys.Skill_S_Darkness_Rare_IronMaidensEmbrace_0;

            Skill skill = Skill.TempSkill(skillKey, BChar, BChar.MyTeam);
            skill.FreeUse = true;

            if (BChar != null && !BChar.Dummy && !BChar.IsDead)
            {
                BChar.MyTeam.partybarrier.BarrierHP += (int)(BChar.GetStat.maxhp * 0.5f);
                BChar.BuffAdd(ModItemKeys.Buff_B_Darkness_DelightfulDefense, BChar, false, 0, false, -1, false);
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, BChar, false, false, true, null));
            }
        }
    }
}