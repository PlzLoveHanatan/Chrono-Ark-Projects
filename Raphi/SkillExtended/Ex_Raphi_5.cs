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
    public class Ex_Raphi_5 : SkillExtedned_IlyaP
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 1
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_0
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_9_Rare
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_8_Rare
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_2
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Public_21
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Mement_6
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Mement_R1;
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
    }
}