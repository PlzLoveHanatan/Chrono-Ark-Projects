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
using NLog.Targets;
using Spine;
using System.Security.Cryptography;
namespace SuperHero
{
    public class E_SuperHero_JusticeSword : EquipBase, IP_SkillUse_User, IP_PlayerTurn, IP_BattleStart_Ones
    {
        public bool FirstMarkCheck = true;


        public override string DescInit()
        {
            string text = ModLocalization.JusticeSword_0;

            if (BChar != null && BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false) is B_SuperHero_HeroComplex complex)
            {
                if (complex.StackNum >= 20 && !Utils.SuperHeroMod(BChar) || Utils.SuperVillainMod(BChar))
                {
                    text = ModLocalization.JusticeSword_1;
                }
            }
            return base.DescInit() + "\n" + text;
        }

        public void BattleStart(BattleSystem Ins)
        {
            FirstMarkCheck = true;
        }

        public override void FixedUpdate()
        {
            var justiceDamage = PlayData.TSavedata.GetCustomValue<JusticeSword>();
            if (justiceDamage == null)
            {
                justiceDamage = new JusticeSword();
                PlayData.TSavedata.AddCustomValue(justiceDamage);
                justiceDamage.JusticeDamage = 0;
            }
            PlusStat.atk = justiceDamage.JusticeDamage;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (Utils.HeroAttacksWithMark.Contains(SkillD.MySkill.KeyID) && SkillD.Master == Utils.SuperHero)
            {
                foreach (var target in Targets)
                {
                    if (target.Info.Ally) continue;

                    if (target.BuffReturn(ModItemKeys.Buff_B_SuperHero_MarkofJustice, false) is B_SuperHero_MarkofJustice mark)
                    {
                        Utils.AddDebuff(target, BChar, ModItemKeys.Buff_B_SuperHero_MarkofJustice, 1);
                        mark.MarkStacks++;
                        mark.BuffStat();
                    }
                }
            }
        }

        public void Turn()
        {
            var heroComplex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            string skillKey = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1;

            if (Utils.SuperHero && Utils.SuperHero.BuffReturn(heroComplex, false) is B_SuperHero_HeroComplex complex && !Utils.SuperHeroMod(BChar) /*&& (BattleSystem.instance.TurnNum < 3 || complex.StackNum < 25)*/)
            {
                if (complex.StackNum >= 20)
                {
                    skillKey = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0;
                }
            }

            Utils.AddBuff(BChar, BattleSystem.instance.DummyChar, heroComplex);
            Utils.CreateSkill(BChar, skillKey, true, true, 1, 0, true);

            var markofJustice = ModItemKeys.Buff_B_SuperHero_MarkofJustice;

            foreach (var enemy in Utils.EnemyTeam.AliveChars_Vanish)
            {
                if (enemy != null)
                {
                    Utils.AddDebuff(enemy, BChar, markofJustice);

                    if (enemy.BuffReturn(markofJustice, false) is B_SuperHero_MarkofJustice mark)
                    {
                        if (mark?.BuffData.MaxStack != 5 && FirstMarkCheck)
                        {
                            mark.BuffData.MaxStack = 5;
                            FirstMarkCheck = false;
                        }
                    }
                }
            }

            if (Utils.SuperVillainMod(BChar))
            {
                foreach (var ally in Utils.AllyTeam.AliveChars)
                {
                    Utils.AddDebuff(ally, BChar, markofJustice);
                }
            }
        }
    }
}