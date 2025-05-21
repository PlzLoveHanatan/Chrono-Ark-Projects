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
	/// Sheathe : Restore 1 Mana and draw 1 random user's skill from the discard pile, apply 'Discarded after 1 turn'.
	/// </summary>
    public class AngelicArmour : SkillExtedned_IlyaP
    {
        public override void IlyaWaste()
        {
            BattleSystem.instance.AllyTeam.AP += 1;
            BattleSystem.DelayInput(this.Waste());
        }

        public IEnumerator Waste()
        {
            List<Skill> list = new List<Skill>();
            foreach (Skill skill in this.BChar.MyTeam.Skills_UsedDeck)
            {
                if (skill.Master.Info.KeyData == ModItemKeys.Character_Raphi)
                {
                    list.Add(skill);
                }
            }

            if (list.Count > 0)
            {
                Skill selectedSkill = list[RandomManager.RandomInt(this.BChar.GetRandomClass().Main, 0, list.Count)];
                BattleSystem.instance.AllyTeam.Skills_UsedDeck.Remove(selectedSkill);
                BattleSystem.instance.AllyTeam.Skills_Deck.Insert(0, selectedSkill);
                BattleSystem.instance.AllyTeam.Draw(new BattleTeam.DrawInput(this.Drawinput));
            }

            yield return null;
        }
        public void Drawinput(Skill skill)
        {
            skill.AutoDelete = 1;
        }
    }
}