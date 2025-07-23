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
namespace Urunhilda
{
    public class Ex_Urunhilda_Synergy_0 : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
        }
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsDamage;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var target = Targets[0];

            BattleSystem.DelayInput(Monetaryluck(target));
        }

        public IEnumerator Monetaryluck(BattleChar Target)
        {
            if (!Target.IsDead) yield break;
            InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 250));
            Utils.IncreaseArkPassiveNum(1);

            SelfDestroy();

            var team = BattleSystem.instance.AllyTeam;
            var skill = team.Skills_Deck.Concat(team.Skills_UsedDeck)
                .FirstOrDefault(x => x != null && x.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_Urunhilda_Synergy_0) != null);

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