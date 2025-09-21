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
    public class B_SuperHero_GloryofJustice : Buff, IP_Hit, IP_Dodge, IP_Awake
    {
        public override string DescExtended()
        {
            int attack = (int)(BChar.GetStat.atk * 0.5f);
            return base.DescExtended().Replace("&a", attack.ToString());
        }

        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if (Dmg >= 1)
            {
                HandleJusticeAction(SP.UseStatus);
            }
        }

        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == BChar)
            {
                HandleJusticeAction(SP.UseStatus);
            }
        }

        private void HandleJusticeAction(BattleChar target)
        {
            if (target.Info.Ally && !Utils.SuperVillainMod(BChar)) return;

            var skillKey = ModItemKeys.Skill_S_SuperHero_JusticeGlory;

            if (BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false) is B_SuperHero_HeroComplex complex && complex.StackNum >= 20 && !Utils.SuperHeroMod(BChar))
            {
                skillKey = ModItemKeys.Skill_S_SuperHero_JusticeGlory_0;
            }

            Skill skill = Skill.TempSkill(skillKey, BChar, BChar.MyTeam);
            skill.FreeUse = true;
            skill.PlusHit = true;
            BattleSystem.DelayInput(Wait());
            BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, target, false, false, true, null));
            EffectView.TextOutSimple(BChar, BuffData.Name);
        }

        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.2f);
            yield break;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff.BuffData.Key == ModItemKeys.Buff_B_SuperHero_GloryofJustice && BuffTaker != Utils.SuperHero)
            {
                BuffTaker.BuffRemove(ModItemKeys.Buff_B_SuperHero_GloryofJustice);
            }
        }

        public void Awake()
        {
            if (BChar.Info.Passive is P_SuperHero superHero)
            {
                superHero.GloryOFJustice = true;
            }
        }
    }
}