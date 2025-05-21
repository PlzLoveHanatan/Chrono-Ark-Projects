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
namespace Raphi
{
    /// <summary>
    /// Overwhelming Prayer
    /// Cost is reduced by 1 whenever a skill is exchanged or discarded.
    /// When played from hand, draw 2 skills.
    /// Sheathe : Draw this skill again.
    /// </summary>
    public class OverwhelmingPrayer : SkillExtedned_IlyaP, IP_Discard
    {
        public override void Init()
        {
            base.Init();
            UseNum = 0;
        }
        public override void HandInit()
        {
            base.HandInit();
            UseNum = 0;
        }
        public override void UsedDeckInit()
        {
            base.UsedDeckInit();
            UseNum = 0;
        }
        public override void FixedUpdate()
        {
            APChange = -UseNum;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            UseNum = 0;

            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            BattleSystem.instance.AllyTeam.Draw(2);
        }
        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            if (skill != Last && skill != MySkill)
            {
                BattleSystem.DelayInput(Discard());
                Last = skill;
            }
        }
        public IEnumerator Discard()
        {
            UseNum++;
            yield return null;
            yield break;
        }
        public override void IlyaWaste()

        {
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

        public int UseNum;
        private Skill Last;
    }
}