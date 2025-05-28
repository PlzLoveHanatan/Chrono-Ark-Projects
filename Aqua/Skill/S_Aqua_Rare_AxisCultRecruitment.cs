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
using Spine;
namespace Aqua
{
    /// <summary>
    /// Axis Cult Recruitment
    /// </summary>
    public class S_Aqua_Rare_AxisCultRecruitment : Skill_Extended, IP_DamageChange
    {
        private bool Recruitment = false;

        public override void Init()
        {
            OnePassive = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.Name == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 30);

            if (alwaysLucky)
            {
                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 3);
                int index2 = RandomManager.RandomInt(BattleRandom.PassiveItem, 100, 300);

                InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, index));
                InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, index2));
                Recruitment = true;

                MySkill.isExcept = true;
            }
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {

            if (Recruitment && !Target.IsDead)
            {
                if (Target is BattleEnemy enemy && enemy.Boss ||
                    Target.Info.KeyData == GDEItemKeys.Enemy_TrialofStrength_Enemy ||
                    Target.Info.KeyData == GDEItemKeys.Enemy_TrialofBrave_Enemy1)
                {
                    Target.Damage(BChar, 60, false, true, false, 0, false, false, false);
                }
            }

            else if (Recruitment && !Target.IsDead)
            {
                Target.HPToZero();
            }
            return Damage;
        }
    }
}