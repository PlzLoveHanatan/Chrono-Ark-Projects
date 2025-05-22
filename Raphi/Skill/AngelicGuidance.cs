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
	/// Angelic Guidance
	/// Sheathe : View the draw pile and select one party member's class skill to add to your hand. Selected skill's cost is reduced by 1.
	/// </summary>
    public class AngelicGuidance : SkillExtedned_IlyaP
    {
        public override void IlyaWaste()
        {
            BattleSystem.DelayInput(Waste());
        }
        public IEnumerator Waste()
        {
            new List<Skill>();
            List<Skill> list = new List<Skill>();
            list.AddRange(BattleSystem.instance.AllyTeam.Skills_Deck);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Master.IsLucyNoC || list[i].MySkill.Rare)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            yield return BattleSystem.instance.StartCoroutine(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(Del), ModLocalization.DrawPile));
            yield return null;
            yield break;
        }

        public void Del(SkillButton Myskill)
        {
            BattleSystem.instance.AllyTeam.Draw(Myskill.Myskill, new BattleTeam.DrawInput(Drawinput));
        }
        public void Drawinput(Skill skill)
        {
            skill.ExtendedAdd(new Skill_Extended
            {
                APChange = -1,
            });
        }
    }
}