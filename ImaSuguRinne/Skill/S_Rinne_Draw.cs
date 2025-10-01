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
namespace ImaSuguRinne
{
	/// <summary>
	/// Eternity Bloom
	/// When drawn, draw a skill.
	/// When played choose a skill from your deck to add to your hand. Copy this skill and shuffle it into your deck.
	/// </summary>
    public class S_Rinne_Draw : Skill_Extended
    {
        public override IEnumerator DrawAction()
        {
            Utils.AllyTeam.Draw();
            Utils.GlitchEffect(MySkill, 1);
            return base.DrawAction();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (BChar.MyTeam.Skills_Deck.Count == 0)
            {
                BattleSystem.DelayInputAfter(AddInDeck());
            }
            else
            {
                List<Skill> list = new List<Skill>();
                list.AddRange(BattleSystem.instance.AllyTeam.Skills_Deck);
                list = RandomManager.Shuffle<Skill>(RandomClass.CreateRandomClass(BChar.GetRandomClass().Main), list);
                BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(Del), ModLocalization.EverlastingYouth, false, true, true, false, true));
            }
        }

        public void Del(SkillButton Mybutton)
        {
            Mybutton.Myskill.Master.MyTeam.ForceDraw(Mybutton.Myskill, (BattleTeam.DrawInput)null);
            BattleSystem.DelayInputAfter(AddInDeck());
        }

        public IEnumerator AddInDeck()
        {
            yield return null;
            Skill skill = Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_Draw, false, false, 0, 0, true, false);
            Utils.InsertSkillInDeck(BChar, skill);
        }
    }
}