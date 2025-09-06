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
    /// Fragment of Memory
    /// </summary>
    public class S_Rinne_FragmentofMemory : Skill_Extended
    {
        public override IEnumerator DrawAction()
        {
            Utils.AllyTeam.Draw();
            Utils.GlitchEffect(MySkill, 1);
            return base.DrawAction();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //Skill skill = Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_FragmentofMemory, false, false, 0, 0, true, false);
            if (MySkill.ExtendedFind_DataName(ModItemKeys.SkillExtended_S_Ex_Rinne_Swift_Mana) == null)
            {
                MySkill?.ExtendedAdd_Battle(ModItemKeys.SkillExtended_S_Ex_Rinne_Swift_Mana);
            }
            int randomIndex = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, BChar.MyTeam.Skills_Deck.Count + 1);
            BChar.MyTeam.Skills_Deck.Insert(randomIndex, this.MySkill);
        }
    }
}