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
using static System.Windows.Forms.LinkLabel;
namespace SuperHero
{
    /// <summary>
    /// Unwanted Success Story
    /// </summary>
    public class S_SuperHero_UnwantedSuccessStory : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            string text = Utils.SuperHeroMod(BChar) ? "" : ModLocalization.Unwanted_0;
            return base.DescExtended(desc).Replace("Description", text);
        }

        public override bool Terms()
        {
            return BChar.Info.KeyData == ModItemKeys.Character_SuperHero;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (!Utils.SuperHeroMod(BChar))
            {
                var allies = Utils.AllyTeam.AliveChars.Where(x => x != Utils.SuperHero).ToList();
                int index = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, allies.Count);
                var randomAlly = allies[index];

                Utils.AddDebuff(randomAlly, BChar, ModItemKeys.Buff_B_SuperHero_HeroPresence);
            }
        }
    }
}