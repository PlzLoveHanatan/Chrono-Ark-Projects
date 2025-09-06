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
    public class S_Ex_E_BloomingPetal : Skill_Extended
    {
        public string BloomingPetal => ModLocalization.Blooming_Petal ?? "";

        public override void Init()
        {
            MySkill.Disposable = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int randomIndex = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, BChar.MyTeam.Skills_Deck.Count + 1);
            BChar.MyTeam.Skills_Deck.Insert(randomIndex, this.MySkill);
        }

        public override string DescExtended(string desc)
        {
            return BloomingPetal + "\n" + base.DescExtended(desc);
        }
    }
}