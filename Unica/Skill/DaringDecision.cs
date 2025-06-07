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
    /// Daring Decision
    /// Discard all skills.
    /// </summary>
    public class DaringDecision : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.1f)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Skill> list = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill
            && skill.Master != BattleSystem.instance.AllyTeam.LucyAlly).ToList();

            if (list.Count <= 0) return;

            int discardedSkillsCount = list.Count;

            foreach (Skill skill in list)
            {
                skill.Delete(false);
            }

            this.SkillBasePlus.Target_BaseDMG = (int)(this.BChar.GetStat.atk * 0.1f) * discardedSkillsCount;

            ApplyBuffAfterDiscard();
            HealAfterDiscard(discardedSkillsCount);

            for (int i = 0; i < list.Count / 4; i++)
            {
                BattleSystem.instance.AllyTeam.Draw();
            }

            if (list.Count == 2 || list.Count == 3)
            {
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_DrawNextTurn, this.BChar, false, 0, false, -1, false);
                return;
            }
            else if (list.Count >= 4)
            {
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_DrawNextTurn, this.BChar, false, 0, false, -1, false);
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_DrawNextTurn, this.BChar, false, 0, false, -1, false);
            }
        }
        public void ApplyBuffAfterDiscard()
        {
            if (this.MySkill.Master.Info.KeyData == ModItemKeys.Character_Unica)
            {
                this.BChar.BuffAdd(ModItemKeys.Buff_BottomDeal, this.BChar, false, 0, false, -1, false);
                this.BChar.BuffAdd(ModItemKeys.Buff_StackingtheDeck, this.BChar, false, 0, false, -1, false);
            }
        }
        public void HealAfterDiscard(int discardedSkillsCount)
        {
            if (discardedSkillsCount > 0)
            {
                this.BChar.Heal(BattleSystem.instance.DummyChar, discardedSkillsCount * 2f, false, true, null);
            }
        }        
    }
}