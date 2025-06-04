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
namespace Mia
{
	/// <summary>
	/// Meowstery Momentum
	/// </summary>
    public class S_Mia_MeowsteryMomentum : SkillExtedned_IlyaP
    {
        public override string DescExtended(string desc)
        {
            if (BattleSystem.instance != null)
            {
                List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

                if (skillsInHand != null)
                {
                    return base.DescExtended(desc)
                        .Replace("&a", ((int)(BChar.GetStat.atk * 0.2f) * skillsInHand.Count).ToString());
                }
            }

            return base.DescExtended(desc)
            .Replace("&a", ((int)(BChar.GetStat.atk * 0.2f)).ToString());
        }

        public override void FixedUpdate()
        {
            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

            MySkill.APChange = -skillsInHand.Count;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse) return;

            Utils.TryPlayMiaSound(MySkill, BChar);

            if (SkillD.BasicSkill) return;

            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

            if (skillsInHand.Count <= 0) return;

            SkillBasePlus.Target_BaseDMG = (int)(BChar.GetStat.atk * 0.2f) * skillsInHand.Count;

            foreach (Skill skill in skillsInHand)
            {
                skill.Delete(false);
            }

            for (int i = 0; i < skillsInHand.Count / 5; i++)
            {
                BattleSystem.instance.AllyTeam.Draw();
            }

            if (skillsInHand.Count == 2 || skillsInHand.Count == 3)
            {
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_Mia_DrawNextTurn, BChar, false, 0, false, -1, false);
                return;
            }
            else if (skillsInHand.Count >= 4)
            {
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_Mia_DrawNextTurn, BChar, false, 0, false, -1, false);
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_Mia_DrawNextTurn, BChar, false, 0, false, -1, false);
            }

            if (skillsInHand.Count >= 7)
            {
                if (BChar.Info.KeyData == ModItemKeys.Character_Mia)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Mia_SavageImpulse, BChar, false, 0, false, -1, false);
                }

                else
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Mia_InstinctSurge, BChar, false, 0, false, -1, false);
                }
            }
        }

        public override void IlyaWaste()
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            BattleSystem.DelayInput(Draw());
        }
        public IEnumerator Draw()
        {
            if (!MySkill.isExcept)
            {
                bool flag = false;
                using (List<Skill>.Enumerator enumerator = BattleSystem.instance.AllyTeam.Skills.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Current.CharinfoSkilldata == MySkill.CharinfoSkilldata)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDrawList(MySkill.CharinfoSkilldata, null, true));
                }
            }
            else
            {
                int num = -1;
                for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count; i++)
                {
                    if (BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].CharinfoSkilldata == MySkill.CharinfoSkilldata)
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
                        if (BattleSystem.instance.AllyTeam.Skills_Deck[j].CharinfoSkilldata == MySkill.CharinfoSkilldata)
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