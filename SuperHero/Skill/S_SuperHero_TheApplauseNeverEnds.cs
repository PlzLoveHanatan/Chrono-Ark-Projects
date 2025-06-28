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
using Steamworks;
using static UnityEngine.ParticleSystem;
namespace SuperHero
{
    /// <summary>
    /// The Applause Never Ends
    /// </summary>
    public class S_SuperHero_TheApplauseNeverEnds : Skill_Extended, IP_SkillUse_User_After
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.maxhp * 0.5f)).ToString());
        }
        public override bool Terms()
        {
            if (BChar.Info.KeyData == ModItemKeys.Character_SuperHero)
                return true;

            return false;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int barrierValue = (int)(BChar.GetStat.maxhp * 0.5f);
            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_EgoShield, BChar, false, 0, false, -1, false).BarrierHP += barrierValue;

            foreach (var target in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                target.BuffAdd(ModItemKeys.Buff_B_SuperHero_HerosSpotlight, BChar, false, 999, false, -1, false);
        }

        public void SkillUseAfter(Skill SkillD)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_HerosSpotlight;
            var enemy = BattleSystem.instance.EnemyTeam.AliveChars_Vanish;

            foreach (var target in enemy)
            {
                if (target?.BuffReturn(buff, false) == null)
                {
                    target.BuffAdd(buff, BChar, false, 999, false, -1, false);
                }
            }
        }
    }
}