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
using EmotionalSystem;
namespace XiaoLOR
{
	/// <summary>
	/// Emotional Turbulence
	/// Damage is increased by &a, based on Emotional Level.
	/// </summary>
    public class S_XiaoLORRare_EmotionalTurbulence : Skill_Extended, IP_DamageChange_sumoperation
    {
        public override string DescExtended(string desc)
        {
            if (BattleSystem.instance != null)
            {
                if (BChar.EmotionLevel() >= 1)
                {
                    return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.5f) * BChar.EmotionLevel()).ToString());
                }
                else if (BChar.EmotionLevel() >= 5)
                {
                    return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.5f) * 5).ToString());
                }
            }

            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.5f)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("Turbulence");

            Utils.ApplyBurn(Targets[0], this.BChar, 2);

            if (BChar.EmotionLevel() >= 3)
            {
                var target = Targets[0];

                if (target is BattleEnemy enemy && enemy.istaunt)
                {
                    foreach (Buff buff in target.Buffs)
                    {
                        if (buff.BuffData.TauntStat)
                        {
                            buff.SelfDestroy(false);
                        }
                    }

                    BattleSystem.DelayInput(this.Draw());
                }

                //if (target is BattleEnemy enemy && enemy.istaunt)
                //{
                //    target.BuffRemove(GDEItemKeys.Buff_B_EnemyTaunt, false);
                //    BattleSystem.DelayInput(this.Draw());
                //}
            }
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


        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (BChar.EmotionLevel() >= 1 && SkillD.MySkill.KeyID == ModItemKeys.Skill_S_XiaoLORRare_EmotionalTurbulence)
            {
                PlusDamage += (int)(this.BChar.GetStat.atk * 0.4f) * BChar.EmotionLevel();
            }

            else if (BChar.EmotionLevel() >= 5 && SkillD.MySkill.KeyID == ModItemKeys.Skill_S_XiaoLORRare_EmotionalTurbulence)
            {
                PlusDamage += (int)(this.BChar.GetStat.atk * 0.4f) * 5;
            }
        }
    }
}