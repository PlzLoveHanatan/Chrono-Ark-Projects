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
        public override void FixedUpdate()
        {
            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

            MySkill.APChange = -skillsInHand.Count;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            Utils.TryPlayMiaSound(MySkill, BChar);

            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

            if (skillsInHand.Count <= 0) return;

            int discardNum = Math.Min(4, skillsInHand.Count);

            List<Skill> randomDiscard = RandomManager.Random(skillsInHand, MySkill.Master.GetRandomClass().SkillSelect, discardNum);

            PlusSkillPerFinal.Damage = discardNum * 15;

            foreach (Skill skill in randomDiscard)
            {
                skill.Delete(false);
            }

            for (int i = 0; i < 2; i++)
            {
                BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_Mia_DrawNextTurn, BChar, false, 0, false, -1, false);
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