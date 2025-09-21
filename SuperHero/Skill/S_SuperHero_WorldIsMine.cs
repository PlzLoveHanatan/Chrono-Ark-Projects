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
namespace SuperHero
{
    public class S_SuperHero_WorldIsMine : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            string text = Utils.SuperHeroMod(BChar) ? "" : ModLocalization.World_0;
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
                foreach (var ally in Utils.AllyTeam.AliveChars)
                {
                    if (ally == Utils.SuperHero) continue;

                    Utils.AddDebuff(ally, BChar, ModItemKeys.Buff_B_SuperHero_HerosSpotlight, 1);
                }
            }
        }
    }
}