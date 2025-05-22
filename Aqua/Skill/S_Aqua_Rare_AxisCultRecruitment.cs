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
namespace Aqua
{
	/// <summary>
	/// Axis Cult Recruitment
	/// </summary>
    public class S_Aqua_Rare_AxisCultRecruitment : Skill_Extended, IP_DamageChange
    {
        private bool Recruitment = false;

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 30);

            if (alwaysLucky)
            {
                InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, 4));
                InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 400));
                Recruitment = true;
            }
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (Recruitment && !Target.IsDead)
            {
                Target.HPToZero();
            }
            return Damage;
        }
    }
}