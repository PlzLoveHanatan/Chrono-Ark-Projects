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
        public bool SuperHeroGlory;
        public override string DescExtended()
        {
            int attack = (int)(BChar.GetStat.atk * 0.4f);
            var complex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var heroComplex = BChar.BuffReturn(complex, false) as B_SuperHero_HeroComplex;
            int index = 0;
            if (BattleSystem.instance != null && heroComplex != null)
            {
                if (SuperHeroGlory)
                {
                    return base.DescExtended().Replace("&a", attack.ToString()).Replace("&b", 0.ToString());
                }
                else
                {
                    return base.DescExtended().Replace("&a", attack.ToString()).Replace("&b", (StackNum * 4).ToString());
                }
            }
            return base.DescExtended().Replace("&a", attack.ToString()).Replace("&b", index.ToString());
        }

        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            var superHero = ModItemKeys.Character_SuperHero;
            var complex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var heroComplex = BChar.BuffReturn(complex, false) as B_SuperHero_HeroComplex;

            if (Dmg >= 1 && SP.UseStatus.Info.KeyData != superHero)
            {
                var attacker = SP.UseStatus;
                var newTarget = SP.UseStatus;
                var justice = ModItemKeys.Skill_S_SuperHero_JusticeGlory;

                if (heroComplex != null && heroComplex.StackNum >= 25 && !SuperHeroGlory)
                {
                    justice = ModItemKeys.Skill_S_SuperHero_JusticeGlory_0;
                }

                Skill skill = Skill.TempSkill(justice, BChar, BChar.MyTeam);
                skill.FreeUse = true;
                skill.PlusHit = true;

                if (AllyTarget() && !SuperHeroGlory)
                {
                    newTarget = JusticeStrike();
                    if (StackNum >= 25)
                    {
                        Utils.ForceKill(newTarget);
                    }
                    
                    if (newTarget.Info.KeyData == ModItemKeys.Character_SuperHero)
                    {
                        return;
                    }
                    else if (newTarget == null)
                    {
                        newTarget = attacker;
                    }
                }

                BattleSystem.DelayInput(Wait());
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, newTarget, false, false, true, null));
                EffectView.TextOutSimple(BChar, BuffData.Name);
            }
        }

        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            var superHero = ModItemKeys.Character_SuperHero;
            var complex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var heroComplex = BChar.BuffReturn(complex, false) as B_SuperHero_HeroComplex;

            if (Char == BChar && SP.UseStatus.Info.KeyData != superHero)
            {
                var attacker = SP.UseStatus;
                var newTarget = SP.UseStatus;
                var justice = ModItemKeys.Skill_S_SuperHero_JusticeGlory;

                if (heroComplex != null && heroComplex.StackNum >= 25 && !SuperHeroGlory)
                {
                    justice = ModItemKeys.Skill_S_SuperHero_JusticeGlory_0;
                }

                Skill skill = Skill.TempSkill(justice, BChar, BChar.MyTeam);
                skill.FreeUse = true;
                skill.PlusHit = true;

                if (AllyTarget() && !SuperHeroGlory)
                {
                    newTarget = JusticeStrike();

                    if (StackNum >= 25)
                    {
                        Utils.ForceKill(newTarget);
                    }
                    if (newTarget.Info.KeyData == ModItemKeys.Character_SuperHero)
                    {
                        return;
                    }
                    else if (newTarget == null)
                    {
                        newTarget = attacker;
                    }
                }

                BattleSystem.DelayInput(Wait());
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, newTarget, false, false, true, null));
                EffectView.TextOutSimple(BChar, BuffData.Name);
            }
        }


        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.2f);
            yield break;
        }

        public bool AllyTarget()
        {
            var complex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var heroComplex = BChar.BuffReturn(complex, false) as B_SuperHero_HeroComplex;
            int index = 0;
            if (heroComplex != null)
            {
                index = heroComplex.StackNum * 4;
            }
            return RandomManager.RandomPer(BattleRandom.PassiveItem, 100, index);
        }

        public BattleChar JusticeStrike()
        {
            var superHero = ModItemKeys.Character_SuperHero;
            var newTargets = BattleSystem.instance.AllyTeam.AliveChars
                .Where(a => a != null && a.Info.KeyData != superHero)
                .ToList();

            if (newTargets.Count == 0)
            {
                return null;
            }

            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, newTargets.Count);
            return newTargets[index];
        }
    }
}