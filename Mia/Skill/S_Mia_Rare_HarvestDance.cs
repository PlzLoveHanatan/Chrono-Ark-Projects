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
	/// Harvest Dance
	/// </summary>
    public class S_Mia_Rare_HarvestDance : SkillExtedned_IlyaP
    {
        public override void IlyaWaste()
        {
            BattleSystem.instance.AllyTeam.AP += 1;
            BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(BChar, MySkill, false, true, false));
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