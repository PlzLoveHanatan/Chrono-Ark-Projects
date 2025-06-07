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
    /// Impugnatio Ultima
    /// Discard all skills in hand and increase this skill's damage by &a for each skill discarded.
    /// </summary>
    public class ImpugnatioUltima : SkillExtedned_IlyaP
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.4f)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Skill> list = BattleSystem.instance.AllyTeam.Skills
                .Where(skill => skill != this.MySkill && skill.MySkill.KeyID != GDEItemKeys.Skill_S_LBossFirst_Block)
                .ToList();

            if (list.Count <= 0) return;

            int discardedSkillsCount = list.Count;

            foreach (Skill skill in list)
            {
                skill.Delete(false);
            }

            this.SkillBasePlus.Target_BaseDMG = (int)(this.BChar.GetStat.atk * 0.4f) * discardedSkillsCount;

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
        public override void IlyaWaste()
        {
            BattleSystem.DelayInput(this.Draw());
        }
        public IEnumerator Draw()
        {
            if (!this.MySkill.isExcept)
            {
                bool flag = false;
                using (List<Skill>.Enumerator enumerator = BattleSystem.instance.AllyTeam.Skills.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Current.CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDrawList(this.MySkill.CharinfoSkilldata, null, true));
                }
            }
            else
            {
                int num = -1;
                for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count; i++)
                {
                    if (BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
                    {
                        num = i;
                        break;
                    }
                }
                if (num != -1)
                {
                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.RemoveAt(num);
                }
                else
                {
                    for (int j = 0; j < BattleSystem.instance.AllyTeam.Skills_Deck.Count; j++)
                    {
                        if (BattleSystem.instance.AllyTeam.Skills_Deck[j].CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
                        {
                            BattleSystem.instance.AllyTeam.Skills_Deck.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            yield return null;
            yield break;
        }
    }
}