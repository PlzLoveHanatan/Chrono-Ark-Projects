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
    public class S_Ex_Rinne_0 : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 2;
        }

        public override IEnumerator DrawAction()
        {
            Utils.AllyTeam.Draw();
            Utils.GlitchEffect(MySkill, 1);
            return base.DrawAction();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.DelayInputAfter(CreateNewSkill(MySkill));
        }

        public IEnumerator CreateNewSkill(Skill skill)
        {
            yield return null;

            var oldSkill = Utils.AllyTeam.Skills_Deck.Concat(Utils.AllyTeam.Skills_UsedDeck).FirstOrDefault(x => x != null && x.MySkill.KeyID == skill.MySkill.KeyID && x.ExtendedFind_DataName(ModItemKeys.SkillExtended_S_Ex_Rinne_0) != null);
            if (oldSkill != null)
            {
                if (Utils.AllyTeam.Skills_Deck.Contains(oldSkill))
                {
                    Utils.AllyTeam.Skills_Deck.Remove(oldSkill);
                }

                if (Utils.AllyTeam.Skills_UsedDeck.Contains(oldSkill))
                {
                    Utils.AllyTeam.Skills_UsedDeck.Remove(oldSkill);
                }
            }

            //Skill newSkill = Utils.CreateSkill(BChar, skill.MySkill.KeyID, false, false, 0, skill.AP - 1, true, false);
            //MySkill?.ExtendedAdd_Battle(this);

            if (MySkill?.ExtendedFind_DataName(ModItemKeys.SkillExtended_S_Ex_Rinne_Swift_Mana) is S_Ex_Rinne_Swift_Mana ex)
            {
                ex.AP--;
            }
            else
            {
                MySkill?.ExtendedAdd_Battle(ModItemKeys.SkillExtended_S_Ex_Rinne_Swift_Mana);
            }

            int index = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, BChar.MyTeam.Skills_Deck.Count + 1);
            BChar.MyTeam.Skills_Deck.Insert(index, MySkill);
        }
    }
}