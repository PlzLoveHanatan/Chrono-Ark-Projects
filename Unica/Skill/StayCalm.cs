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
namespace Unica
{
	/// <summary>
	/// Stay Calm
	/// Sheathe : Draw 1 skill.
	/// </summary>
    public class StayCalm : SkillExtedned_IlyaP
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
                if (skill.Master.Info.KeyData == ModItemKeys.Character_Unica)
                {
                    list.Add(skill);
                }
            }

            if (list.Count > 0)
            {
                Skill selectedSkill = list[RandomManager.RandomInt(this.BChar.GetRandomClass().Main, 0, list.Count)];
                BattleSystem.instance.AllyTeam.Skills_UsedDeck.Remove(selectedSkill);
                BattleSystem.instance.AllyTeam.Skills_Deck.Insert(0, selectedSkill);
                BattleSystem.instance.AllyTeam.Draw(selectedSkill, null);
            }

            yield return null;
            yield break;
        }
    }
}