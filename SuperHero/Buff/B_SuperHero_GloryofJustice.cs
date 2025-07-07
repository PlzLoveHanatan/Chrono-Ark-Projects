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
namespace SuperHero
{
	/// <summary>
	/// Glory of Justice
	/// </summary>
    public class B_SuperHero_GloryofJustice : Buff, IP_DamageChange_Hit_sumoperation, IP_Dodge
    {
        private BattleChar BattleChar;
        private bool AllyTarget;

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(BChar.GetStat.atk * 0.5f)).ToString());
        }

        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0)
            {
                JusticeStrike();

                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_JusticeGlory, BChar, BChar.MyTeam);
                skill.FreeUse = true;
                skill.PlusHit = true;

                if (AllyTarget)
                {
                    SkillD.Master = BattleChar;
                }

                BattleSystem.DelayInput(Wait());
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, SkillD.Master, false, false, true, null));
                EffectView.TextOutSimple(BChar, BuffData.Name);
            }
        }

        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == BChar)
            {
                JusticeStrike();

                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_JusticeGlory, BChar, BChar.MyTeam);
                skill.FreeUse = true;
                skill.PlusHit = true;

                if (AllyTarget)
                {
                    SP.UseStatus = BattleChar;
                }

                BattleSystem.DelayInput(Wait());
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, SP.UseStatus, false, false, true, null));
                EffectView.TextOutSimple(BChar, BuffData.Name);
            }
        }


        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.2f);
            yield break;
        }

        public void JusticeStrike()
        {
            var superHero = ModItemKeys.Character_SuperHero;
            bool whoopsie = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 20);
            if (!whoopsie) return;

            var newTargets = BattleSystem.instance.AllyTeam.AliveChars
                .Where(a => a != null && a.Info.KeyData != superHero)
                .ToList();

            if (newTargets.Count == 0) return;

            AllyTarget = true;
            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, newTargets.Count);
            BattleChar = newTargets[index];
        }
    }
}