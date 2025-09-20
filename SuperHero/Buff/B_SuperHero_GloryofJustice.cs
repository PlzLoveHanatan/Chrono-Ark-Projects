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
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Experimental.UIElements.StyleEnums;
using Steamworks;
namespace SuperHero
{
    /// <summary>
    /// Glory of Justice
    /// </summary>
    public class B_SuperHero_GloryofJustice : Buff, IP_Hit, IP_Dodge
    {
        public override string DescExtended()
        {
            int attack = (int)(BChar.GetStat.atk * 0.4f);
            string text = ModLocalization.GloryOfJustice_0;
            var complex = BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false) as B_SuperHero_HeroComplex;

            if (BattleSystem.instance != null && (Utils.SuperHeroMod(BChar)))
            {
                text = ModLocalization.GloryOfJustice_1;
                return text.Replace("&a", attack.ToString());
            }
            return text.Replace("&a", attack.ToString()).Replace("&b", (complex.StackNum * 2).ToString());
        }

        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if (Dmg >= 1)
            {
                HandleJusticeAction(SP, SP.UseStatus);
            }
        }

        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == BChar)
            {
                HandleJusticeAction(SP, SP.UseStatus);
            }
        }

        private void HandleJusticeAction(SkillParticle SP, BattleChar defaultTarget)
        {
            if (SP.UseStatus.Info.KeyData == Utils.SuperHero.Info.KeyData) return;

            var complex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var heroComplex = BChar.BuffReturn(complex, false) as B_SuperHero_HeroComplex;

            var justiceSkillKey = ModItemKeys.Skill_S_SuperHero_JusticeGlory;

            if (heroComplex != null && heroComplex.StackNum >= 25 && !Utils.SuperHeroMod(BChar))
            {
                justiceSkillKey = ModItemKeys.Skill_S_SuperHero_JusticeGlory_0;
            }

            Skill skill = Skill.TempSkill(justiceSkillKey, BChar, BChar.MyTeam);
            skill.FreeUse = true;
            skill.PlusHit = true;

            var target = defaultTarget;

            if (!Utils.SuperHeroMod(BChar))
            {
                if (AllyTarget())
                {
                    var ally = JusticeStrike();
                    if (ally != null)
                    {
                        target = ally;

                        if (heroComplex != null && heroComplex.StackNum >= 25)
                        {
                            Utils.ForceKill(target);
                        }
                    }
                }
            }
            else
            {
                target = defaultTarget;
            }

            BattleSystem.DelayInput(Wait());
            BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, target, false, false, true, null));
            EffectView.TextOutSimple(BChar, BuffData.Name);
        }

        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.2f);
            yield break;
        }

        public bool AllyTarget()
        {
            int index = 0;

            if (BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false) is B_SuperHero_HeroComplex complex)
            {
                index = complex.StackNum * 2;
            }
            return RandomManager.RandomPer(BattleRandom.PassiveItem, 100, index);
        }

        public BattleChar JusticeStrike()
        {
            var newTargets = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null && a != Utils.SuperHero).ToList();

            if (newTargets.Count == 0)
            {
                return null;
            }

            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, newTargets.Count);
            return newTargets[index];
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff.BuffData.Key == ModItemKeys.Buff_B_SuperHero_GloryofJustice && BuffTaker != Utils.SuperHero)
            {
                BuffTaker.BuffRemove(ModItemKeys.Buff_B_SuperHero_GloryofJustice);
            }
        }
    }
}