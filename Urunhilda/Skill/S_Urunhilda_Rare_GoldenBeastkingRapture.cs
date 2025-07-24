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
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
namespace Urunhilda
{
    /// <summary>
    /// Golden Beastking Rapture
    /// </summary>
    public class S_Urunhilda_Rare_GoldenBeastkingRapture : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var target = Targets[0];
            BChar.Heal(BattleSystem.instance.DummyChar, 3, false, true, null);

            Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Urunhilda_DummyHeal, BChar, BChar.MyTeam);
            healingParticle.PlusHit = true;
            healingParticle.FreeUse = true;
            BChar.ParticleOut(healingParticle, BChar);
            BattleSystem.DelayInput(Monetaryluck(target));
        }

        public IEnumerator Monetaryluck(BattleChar Target)
        {
            if (!Target.IsDead) yield break;
            InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 500));
            Utils.IncreaseArkPassiveNum(1);

            var team = BattleSystem.instance.AllyTeam;
            var skill = team.Skills_Deck.Concat(team.Skills_UsedDeck)
                .FirstOrDefault(x => x != null && x.MySkill.KeyID == ModItemKeys.Skill_S_Urunhilda_Rare_GoldenBeastkingRapture);

            if (skill != null)
            {
                if (team.Skills_Deck.Contains(skill))
                {
                    team.Skills_Deck.Remove(skill);
                }
                else if (team.Skills_UsedDeck.Contains(skill))
                {
                    team.Skills_UsedDeck.Remove(skill);
                }
            }
            yield return null;
        }
    }
}