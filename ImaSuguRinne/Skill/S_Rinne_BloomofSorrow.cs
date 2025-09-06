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
	/// Bloom of Sorrow
	/// Create a random Rinne skill in hand. The created skill gains Exclude and Swiftness.
	/// </summary>
    public class S_Rinne_BloomofSorrow : Skill_Extended
    {
        public override IEnumerator DrawAction()
        {
            Utils.GlitchEffect(MySkill, 1);
            return base.DrawAction();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int randomIndex = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, Utils.RinneSkills.Count);
            string randomSkillKey = Utils.RinneSkills[randomIndex];

            Skill randomSkill = Skill.TempSkill(randomSkillKey, BChar, BChar.MyTeam);

            int ap = randomSkill.MySkill.UseAp - 2;

            Utils.CreateSkill(BChar, randomSkillKey, true, false, 0, ap, true, true);

            if (MySkill?.ExtendedFind_DataName(ModItemKeys.SkillExtended_S_Ex_Rinne_Swift_Mana) == null)
            {
                MySkill?.ExtendedAdd_Battle(ModItemKeys.SkillExtended_S_Ex_Rinne_Swift_Mana);
            }

            //Skill mySkill = Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_BloomofSorrow, false, false, 0, 1, true, false);
            int randomIndexMySkill = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, BChar.MyTeam.Skills_Deck.Count + 1);
            BChar.MyTeam.Skills_Deck.Insert(randomIndexMySkill, MySkill);
        }
    }
}